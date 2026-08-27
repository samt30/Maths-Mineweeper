using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Numerics;

namespace MathsweeperWinForms
{
    public partial class Form1 : Form
    {
        // constants, unicode characters for mine and flag
        const string mine = "💣";
        const string flag = "🚩";

        // Colour
        Color unrevealedCellColour = Color.FromArgb(240, 240, 240);

        // constants
        const int minLengthOfCell = 90; //when game mode is maths
        const int minLengthOfCellAll = 30; //apply to all gamemode
        const float fontSizeRatio = 0.5f;  // cell size ratio to font height
        const int animationIntervalDuration = 50; // game tick between show and hide mine

        // maths expression tables
        string[,] multiplicationSets =
        {
            {"1x1", "2x0.5", "-1 x -1"},
            {"8x0.25", "4x0.5", "-2 x -1"},
            {"0.3*10", "6x0.5", "-1.5 x -2"},
            {"2x2", "8x0.5", "-2 x -2"},
            {"1.25x4", "10x0.5", "-2.5 x -2"},
            {"4x1.5", "12x0.5", "-2 x -3"},
            {"3.5x2", "14x0.5", "-7 x -1"},
            {"4x2", "16x0.5", "-8 x -1"},
        };

        // maths expression reading from file
        string[][] mathsExpressionSets = new string[][]
        {
            new string[] { "1" },
            new string[] { "2" },
            new string[] { "3" },
            new string[] { "4" },
            new string[] { "5" },
            new string[] { "6" },
            new string[] { "7" },
            new string[] { "8" }
        };
        float[][] expressionFontRatioCache = new float[8][];
        string mathsExpressionJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MathsExpression.json");

        // timer
        private StopwatchTimer timeCounter = new StopwatchTimer();

        private System.Windows.Forms.Timer gameTickTimer = new System.Windows.Forms.Timer();

        // Game settings
        private NumberDisplayingStyleList numberDisplayStyle = NumberDisplayingStyleList.Classic;


        int y_margin = 150;
        int x_margin = 10;

        // dpi factor, for another device show the text normally
        float dpiFactor = 1.0f;

        // runtime variables
        float defaultFontHeight;
        int MaxLengthOfCell;
        private int animationTick = 0;
        private int currentRows = 9;
        private int currentColumns = 9;
        private int currentNumOfMines = 10;
        private int numOfFlags;
        private int revealedCells;
        private bool isGameStart = false;
        private bool isGameEnd = false;
        private bool isGamePaused = false;
        private bool isWin = false;
        private bool isMineShowing = false;

        // efficiency warning
        private bool sizeIsWarned = false;

        // gameboard and check list
        private Cell[,] board = null!;
        private HashSet<(int, int)> checkedCells = new HashSet<(int, int)>();
        private HashSet<(int, int)> displayedCells = new HashSet<(int, int)>();
        (int, int)[] neighborCells = { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };  // position of 8 cells surrounding a cell

        // button cache
        private Button[,] buttons = null!;

        Random random = new Random();

        /// <summary>
        /// Constructor: initialize form components and set the initial game state.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            gameTickTimer.Interval = 10; // Update every 10 milliseconds (1tick = 10ms)
            gameTickTimer.Tick += DisplayTime;
            gameTickTimer.Tick += MineRevealingAnimation;
            gameTickTimer.Start();
        }

        /// <summary>
        /// Form load event.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            PauseCover.Visible = false;

            using (Graphics g = this.CreateGraphics())
            {
                dpiFactor = g.DpiX / 96f;
            }

            LoadMathsExpressionJson();
            SetExpressionFontCache();
        }

        private void LoadMathsExpressionJson()
        {
            if (System.IO.File.Exists(mathsExpressionJsonPath))
            {
                string jsonString = System.IO.File.ReadAllText(mathsExpressionJsonPath);
                string[][]? parsedData = System.Text.Json.JsonSerializer.Deserialize<string[][]>(jsonString);
                if (parsedData != null && parsedData.Length > 0)
                {
                    mathsExpressionSets = parsedData;

                    for (int i = 0; i < mathsExpressionSets.Length; i++)
                    {
                        expressionFontRatioCache[i] = new float[mathsExpressionSets[i].Length];
                    }
                }
            }
        }

        /// <summary>
        /// Handle cell mouse down events:
        /// - Left button: start the game or reveal a cell
        /// - Right button: toggle a flag (if the cell is not revealed)
        /// </summary>
        private void Button_MouseDown(object? sender, MouseEventArgs e)
        {
            if (isGamePaused) return;

            Button? clickedButton = sender as Button;
            if (clickedButton != null)
            {
                var coordinateOfClickedButton = GetCoordinate(int.Parse(clickedButton.Name));
                if (e.Button == MouseButtons.Left)
                {
                    if (!isGameStart)
                    {
                        StartGame(int.Parse(clickedButton.Name));
                        isGameStart = true;
                        revealedCells++;
                        timeCounter.Start();

                        PlaySound(0);
                    }
                    else if (isGameStart && !isGameEnd && !board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isFlagged
                        && !board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isReveal)
                    {
                        board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isReveal = true;
                        RevealingAdjCell(coordinateOfClickedButton);
                        DisplayBoard();

                        if (board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isMine)
                        {
                            isGameEnd = true;
                            timeCounter.Stop();
                            PauseButton.Text = "Continue";

                            PlaySound(2);
                        }
                        else
                        {
                            revealedCells++;

                            PlaySound(1);
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right && isGameStart && !isGameEnd)
                {
                    if (!board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isReveal)
                    {
                        if (board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isFlagged)
                        {
                            numOfFlags += 1;
                            PlaySound(4);
                        }
                        else
                        {
                            numOfFlags -= 1;
                            PlaySound(4);
                        }

                        board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isFlagged =
                        !board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isFlagged;

                        // display flag
                        clickedButton.Text = board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isFlagged ? flag : "";
                        clickedButton.ForeColor = board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].isFlagged ?
                            Color.Red : board[coordinateOfClickedButton.Item1, coordinateOfClickedButton.Item2].fontColour; // set flag color to red
                        clickedButton.Font = new Font(clickedButton.Font.FontFamily, defaultFontHeight);

                        FlagNumber.Text = $"{numOfFlags}";
                    }
                }

                // Check for win condition: if all non-mine cells are revealed
                if (revealedCells == currentRows * currentColumns - currentNumOfMines)
                {
                    isGameEnd = true;
                    timeCounter.Stop();
                    Debug.WriteLine("win");
                    isWin = true;
                    PauseButton.Text = "Continue";

                    PlaySound(3);
                }
            }
        }

        /// <summary>
        /// Initialize the game board: create buttons, calculate sizes, and initialize Cell objects.
        /// </summary>
        private void InitaliseGame()
        {
            TitleScreen.Visible = false;

            int UsableWidth = this.ClientSize.Width - x_margin * 2;
            int UsableHeight = this.ClientSize.Height - y_margin;

            GameBoard.Visible = true;
            GameBoard.Size = new Size(UsableWidth, UsableHeight);
            GameBoard.Location = new Point(x_margin, y_margin);

            int MaxWidthOfCell = UsableWidth / currentColumns;
            int MaxHeightOfCell = UsableHeight / currentRows;
            MaxLengthOfCell = MaxWidthOfCell > MaxHeightOfCell ? MaxHeightOfCell : MaxWidthOfCell;

            if (currentNumOfMines > currentRows * currentColumns - 9) currentNumOfMines = currentRows * currentColumns - 9;

            revealedCells = 0;

            timeCounter.Reset();

            isGameEnd = false;

            isWin = false;

            board = new Cell[currentRows, currentColumns];

            buttons = new Button[currentRows, currentColumns];

            checkedCells.Clear();
            displayedCells.Clear();

            numOfFlags = currentNumOfMines;
            FlagNumber.Text = $"{numOfFlags}";

            if (numberDisplayStyle == NumberDisplayingStyleList.Maths || numberDisplayStyle == NumberDisplayingStyleList.HarderMaths)
            {
                MaxLengthOfCell = MaxLengthOfCell < minLengthOfCell ? minLengthOfCell : MaxLengthOfCell;
            }
            else if (numberDisplayStyle == NumberDisplayingStyleList.Binary)
            {
                MaxLengthOfCell = MaxLengthOfCell < minLengthOfCellAll * 2 ? minLengthOfCellAll * 2 : MaxLengthOfCell;
            }
            else
            {
                MaxLengthOfCell = MaxLengthOfCell < minLengthOfCellAll ? minLengthOfCellAll : MaxLengthOfCell;
            }
            defaultFontHeight = (MaxLengthOfCell - 10) * fontSizeRatio / dpiFactor;


            // generating buttons
            int left_margin = (UsableWidth - currentColumns * MaxLengthOfCell) / 2;
            left_margin = left_margin < 0 ? 0 : left_margin;

            int count = 0;
            for (int r = 0; r < currentRows; r++)
            {
                for (int c = 0; c < currentColumns; c++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(MaxLengthOfCell, MaxLengthOfCell);
                    int x = c * MaxLengthOfCell + left_margin;
                    int y = r * MaxLengthOfCell;
                    btn.Location = new Point(x, y);
                    btn.Name = count.ToString();
                    btn.BackColor = unrevealedCellColour;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    count++;
                    btn.MouseDown += Button_MouseDown;
                    GameBoard.Controls.Add(btn);
                    board[r, c] = new Cell();
                    buttons[r, c] = btn;
                }
            }
        }

        /// <summary>
        /// Start the game after the first click: place mines, calculate adjacent mine counts, and reveal the starting cell.
        /// </summary>
        private void StartGame(int spawn)
        {
            // place mines on board after the first click
            GeneratingMine(spawn);
            // calculate the number of adjacent mines for each cell...
            Calculation();

            var coordinateOfSpawn = GetCoordinate(spawn);
            board[coordinateOfSpawn.Item1, coordinateOfSpawn.Item2].isReveal = true;

            // display
            RevealingAdjCell(coordinateOfSpawn);
            DisplayBoard();
        }

        /// <summary>
        /// Update the board display by calling DisplayCell for each cell.
        /// </summary>
        private void DisplayBoard()
        {
            for (int r = 0; r < currentRows; r++)
            {
                for (int c = 0; c < currentColumns; c++)
                {
                    DisplayCell(r, c);
                }
            }
        }
        /// <summary>
        /// Display the specified cell's content (text, font and color) and mark it as displayed.
        /// </summary>
        private void DisplayCell(int r, int c)
        {
            if (board[r, c].isReveal && !displayedCells.Contains((r, c)))
            {
                displayedCells.Add((r, c));

                buttons[r, c].Text = board[r, c].text;
                buttons[r, c].Font = new Font(buttons[r, c].Font.FontFamily, board[r, c].fontHeight);
                buttons[r, c].BackColor = Color.White;
                buttons[r, c].ForeColor = board[r, c].fontColour;

            }
        }

        private void DisplayTime(object? sender, EventArgs e)
        {
            if (isGamePaused) return;
            TimeCounter.Text = timeCounter.Elapsed.TotalSeconds.ToString("F3");
        }

        /// <summary>
        /// Generate non-overlapping mine positions using a partial Fisher–Yates shuffle and avoid the 3x3 spawn area.
        /// </summary>
        private void GeneratingMine(int spawn)
        {
            var coordinateOfSpawn = GetCoordinate(spawn);

            int totalCell = currentColumns * currentRows;
            // Fisher_Yates Shuffle and put excluded coordinates at the end
            int[] allNums = Enumerable.Range(0, totalCell).ToArray();
            for (int i = 0; i < currentNumOfMines; i++)
            {
                int nextIndex = random.Next(i, totalCell);
                int temp = allNums[i];
                allNums[i] = allNums[nextIndex];
                allNums[nextIndex] = temp;
            }

            int placedMine = 0;
            int j = 0;
            while (placedMine < currentNumOfMines)
            {
                var coordinateOfMine = GetCoordinate(allNums[j]);
                int r = coordinateOfMine.Item1;
                int c = coordinateOfMine.Item2;
                // 3*3 spawn area and inside board index range
                if (((r < coordinateOfSpawn.Item1 - 1 || r > coordinateOfSpawn.Item1 + 1) ||
                    (c < coordinateOfSpawn.Item2 - 1 || c > coordinateOfSpawn.Item2 + 1)) &&
                    (r >= 0 && r < currentRows) && (c >= 0 && c < currentColumns))
                {
                    board[r, c].isMine = true;
                    placedMine++;
                }
                j++;
            }
            Debug.WriteLine("MineGenerated");
        }

        /// <summary>
        /// Calculate the adjacent mine count for each cell and set the display text, font and color
        /// according to the selected number display style (Classic, Binary, Maths, HarderMaths).
        /// </summary>
        private void Calculation()
        {
            for (int r = 0; r < currentRows; r++)
            {
                for (int c = 0; c < currentColumns; c++)
                {
                    int adjMines = CalculateAdjacentMine(r, c);
                    board[r, c].adjacentMines = adjMines;

                    if (board[r, c].isMine)
                    {
                        board[r, c].text = mine;
                        board[r, c].fontHeight = defaultFontHeight /dpiFactor;
                    }
                    else if (adjMines == 0)
                    {
                        board[r, c].text = "";
                    }
                    else
                    {
                        switch (numberDisplayStyle)
                        {
                            case NumberDisplayingStyleList.Classic:
                                board[r, c].text = adjMines.ToString();
                                board[r, c].fontHeight = defaultFontHeight /dpiFactor;
                                board[r, c].fontColour = adjMines switch
                                {
                                    1 => Color.Blue,
                                    2 => Color.Green,
                                    3 => Color.Red,
                                    4 => Color.Purple,
                                    5 => Color.DarkRed,
                                    6 => Color.DarkGreen,
                                    7 => Color.Black,
                                    8 => Color.Gray,
                                    _ => Color.Black
                                };
                                break;
                            case NumberDisplayingStyleList.Binary:
                                string binaryString = Convert.ToString(adjMines, 2);
                                board[r, c].text = binaryString;
                                board[r, c].fontHeight = CalculateFontSizeMultiplier(this, binaryString) * MaxLengthOfCell /dpiFactor;
                                board[r, c].fontColour = Color.Black;
                                break;
                            case NumberDisplayingStyleList.Maths:
                                string expression = GetSimpleMathsExpression(adjMines);
                                board[r, c].text = expression;
                                board[r, c].fontHeight = CalculateFontSizeMultiplier(this, expression) * MaxLengthOfCell / dpiFactor;
                                board[r, c].fontColour = Color.Black;
                                break;
                            case NumberDisplayingStyleList.HarderMaths:
                                int expressionR = adjMines - 1;
                                int expressionC = random.Next(mathsExpressionSets[adjMines - 1].Length);
                                string harderExpression = mathsExpressionSets[expressionR][expressionC];
                                board[r, c].text = harderExpression;
                                board[r, c].fontHeight = expressionFontRatioCache[expressionR][expressionC] * MaxLengthOfCell / dpiFactor;
                                Debug.WriteLine(board[r, c].fontHeight);
                                board[r, c].fontColour = Color.Black;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// If a revealed cell has zero adjacent mines, use BFS (queue) to expand and reveal neighboring empty cells.
        /// </summary>
        private void RevealingAdjCell((int, int) clickedPosition)
        {
            Queue<(int, int)> cell_checklist = new Queue<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            if (board[clickedPosition.Item1, clickedPosition.Item2].adjacentMines == 0)
                cell_checklist.Enqueue((clickedPosition.Item1, clickedPosition.Item2));
            checkedCells.Add((clickedPosition.Item1, clickedPosition.Item2));

            while (cell_checklist.Count > 0)
            {
                var cur_pos = cell_checklist.Dequeue();

                board[cur_pos.Item1, cur_pos.Item2].isReveal = true;
                checkedCells.Add((cur_pos.Item1, cur_pos.Item2));

                if (visited.Contains(cur_pos)) { continue; }

                visited.Add((cur_pos.Item1, cur_pos.Item2));

                for (int i = 0; i < neighborCells.Length; i++)
                {
                    (int, int) neighborCell = (neighborCells[i].Item1 + cur_pos.Item1, neighborCells[i].Item2 + cur_pos.Item2);

                    if (checkedCells.Contains(neighborCell)) continue;

                    if (neighborCell.Item1 >= 0 && neighborCell.Item1 < currentRows &&
                        neighborCell.Item2 >= 0 && neighborCell.Item2 < currentColumns)
                    {
                        revealedCells += board[neighborCell.Item1, neighborCell.Item2].isReveal ? 0 : 1;
                        if (!board[neighborCell.Item1, neighborCell.Item2].isMine) // kinda useless
                            board[neighborCell.Item1, neighborCell.Item2].isReveal = true;
                        if (board[neighborCell.Item1, neighborCell.Item2].isFlagged)
                        {
                            board[neighborCell.Item1, neighborCell.Item2].isFlagged = false; // remove flag if any
                            numOfFlags++;
                            FlagNumber.Text = $"{numOfFlags}";
                        }

                        if (board[neighborCell.Item1, neighborCell.Item2].adjacentMines != 0)
                            checkedCells.Add(neighborCell);
                        else if (!visited.Contains(neighborCell))
                            cell_checklist.Enqueue(neighborCell);

                    }
                }

                Debug.WriteLine("reveal completed a loop");
            }
        }

        /// <summary>
        /// Count the number of mines in the 8 neighboring cells around the specified cell and return the count.
        /// </summary>
        int CalculateAdjacentMine(int r, int c)
        {
            int countOfAdjMine = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int adj_cell_r = r + i;
                    int adj_cell_c = c + j;
                    if (adj_cell_r >= 0 && adj_cell_r < currentRows &&
                        adj_cell_c >= 0 && adj_cell_c < currentColumns)
                    {
                        if (board[adj_cell_r, adj_cell_c].isMine)
                        {
                            countOfAdjMine++;
                        }
                    }
                }
            }
            return countOfAdjMine;
        }

        float CalculateFontSizeMultiplier(Control control, string text)
        {
            if (string.IsNullOrEmpty(text)) { return 0.1f; }

            using Font measureFont = new Font("Microsoft JhengHei UI", 24f, FontStyle.Regular);

            using Graphics g = control.CreateGraphics();

            // remove padding
            StringFormat format = StringFormat.GenericTypographic;

            // include space
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            SizeF sizeF = g.MeasureString(text, measureFont, PointF.Empty, format);

            if (sizeF.Width == 0) return 0.1f;

            // fixing the bug of font out of box, numbers are filled by guess, but important
            float multiplier = sizeF.Height / sizeF.Width;
            if (multiplier > 1) { multiplier = 1; }
            if (multiplier > 0.5f) { multiplier -= 0.15f; }

            return multiplier * 0.4f;
        }

        private void SetExpressionFontCache()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < mathsExpressionSets[i].Length; j++)
                {
                    expressionFontRatioCache[i][j] = CalculateFontSizeMultiplier(this, mathsExpressionSets[i][j]);
                }
            }
        }


        /// <summary>
        /// Convert a button index to a (row, column) coordinate. The index is assumed to be r*columns + c.
        /// </summary>
        (int, int) GetCoordinate(int num)
        {
            int r = num / currentColumns;
            int c = num % currentColumns;
            (int, int) coordinate = (r, c);
            return (coordinate);
        }

        /// <summary>
        /// Generate a simple math expression to represent a number (addition/subtraction, multiplication, or division),
        /// used for NumberDisplayingStyleList.Maths display mode.
        /// </summary>
        string GetSimpleMathsExpression(int num)
        {
            int choice = random.Next(1, 4);
            string expression = "";
            int a = 0;
            int b = 0;
            switch (choice)
            {
                case 1: // addition and subtraction
                    b = random.Next(-20, 4);
                    a = num - b;
                    expression = b >= 0 ? $"{a}+{b}" : $"{a}{b}";
                    break;
                case 2: //multiplication
                    a = random.Next(3);
                    expression = multiplicationSets[num - 1, a];
                    break;
                case 3: // division
                    b = random.Next(1, 16);
                    a = num * b;
                    expression = $"{a}/{b}";
                    break;
            }
            return expression.ToString();
        }


        private void MineRevealingAnimation(object? sender, EventArgs e)
        {
            if (!isGameEnd || !isGameStart) return;

            if (animationTick % animationIntervalDuration == 0)
            {
                for (int r = 0; r < currentRows; r++)
                {
                    for (int c = 0; c < currentColumns; c++)
                    {
                        if (board[r, c].isMine)
                        {
                            int nameOfButton = r * currentColumns + c;
                            Control[] targetButton = this.Controls.Find(nameOfButton.ToString(), true);
                            Button? btn = targetButton[0] as Button;
                            if (btn != null)
                            {
                                Color showingColour = isWin ? Color.LightGreen : Color.Pink;
                                btn.BackColor = isMineShowing ? showingColour : unrevealedCellColour;
                            }
                        }
                    }
                }
                isMineShowing = !isMineShowing;
            }

            animationTick++;
        }

        private void ClearPanelControls(Panel targetPanel)
        {
            if (targetPanel == null) return;

            //from back to forward
            for (int i = targetPanel.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = targetPanel.Controls[i];

                targetPanel.Controls.RemoveAt(i);

                // release memory
                ctrl.Dispose();
            }

            // clear whole controls
            targetPanel.Controls.Clear();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isGameEnd)
            {
                EndScreen.Visible = true;

                if (isWin)
                {
                    EndText.Text = "Welldone!";
                }
                else
                {
                    EndText.Text = "Gameover...";
                }

                return;
            }

            isGamePaused = !isGamePaused;
            switch (isGamePaused)
            {
                case true:
                    PauseCover.Visible = true;
                    PauseButton.Text = "Resume";
                    timeCounter.Stop();
                    break;
                case false:
                    PauseCover.Visible = false;
                    PauseButton.Text = "Pause";
                    if (isGameStart) timeCounter.Start();
                    break;
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            isGameStart = false;
            PauseButton.Text = "Pause";
            ClearPanelControls(GameBoard);
            InitaliseGame();
        }

        private void PlayAgain_Click(object sender, EventArgs e)
        {
            EndScreen.Visible = false;
            isGameStart = false;
            PauseButton.Text = "Pause";
            ClearPanelControls(GameBoard);
            InitaliseGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EndScreen.Visible = false;
            TitleScreen.Visible = true;
            isGameStart = false;
            ClearPanelControls(GameBoard);
        }

        private void HarderMathsMode_Click(object sender, EventArgs e)
        {
            numberDisplayStyle = NumberDisplayingStyleList.HarderMaths;
            ClassicMode.BackColor = Color.White;
            BinaryMode.BackColor = Color.White;
            MathsMode.BackColor = Color.White;

            HarderMathsMode.BackColor = Color.LightGray;
        }

        private void ClassicMode_Click(object sender, EventArgs e)
        {
            numberDisplayStyle = NumberDisplayingStyleList.Classic;
            BinaryMode.BackColor = Color.White;
            MathsMode.BackColor = Color.White;
            HarderMathsMode.BackColor = Color.White;

            ClassicMode.BackColor = Color.LightGray;
        }

        private void BinaryMode_Click(object sender, EventArgs e)
        {
            numberDisplayStyle = NumberDisplayingStyleList.Binary;
            ClassicMode.BackColor = Color.White;
            MathsMode.BackColor = Color.White;
            HarderMathsMode.BackColor = Color.White;

            BinaryMode.BackColor = Color.LightGray;
        }

        private void MathsMode_Click(object sender, EventArgs e)
        {
            numberDisplayStyle = NumberDisplayingStyleList.Maths;
            ClassicMode.BackColor = Color.White;
            BinaryMode.BackColor = Color.White;
            HarderMathsMode.BackColor = Color.White;

            MathsMode.BackColor = Color.LightGray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettingScreen.Visible = false;
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            SettingScreen.Visible = true;
        }

        private void EasyMode_Click(object sender, EventArgs e)
        {
            currentColumns = 9;
            currentRows = 9;
            currentNumOfMines = 10;

            EasyMode.BackColor = Color.LightGray;
            NormalMode.BackColor = Color.White;
            HardMode.BackColor = Color.White;
            Custom.BackColor = Color.White;

            CustomSettingText.Text = "";
        }

        private void NormalMode_Click(object sender, EventArgs e)
        {
            currentColumns = 16;
            currentRows = 16;
            currentNumOfMines = 40;

            EasyMode.BackColor = Color.White;
            NormalMode.BackColor = Color.LightGray;
            HardMode.BackColor = Color.White;
            Custom.BackColor = Color.White;

            CustomSettingText.Text = "";
        }

        private void HardMode_Click(object sender, EventArgs e)
        {
            currentColumns = 30;
            currentRows = 16;
            currentNumOfMines = 99;

            EasyMode.BackColor = Color.White;
            NormalMode.BackColor = Color.White;
            HardMode.BackColor = Color.LightGray;
            Custom.BackColor = Color.White;

            CustomSettingText.Text = "";
        }

        private void PlaySound(int Sound)
        {
            switch (Sound)
            {
                case 0: // start click
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        for (int freq = 600; freq <= 1500; freq += 150)
                        {
                            System.Console.Beep(freq, 15);
                        }
                    });
                    break;
                case 1: // safe click
                    System.Console.Beep(1200, 30);
                    break;
                case 2: // fail
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        for (int freq = 300; freq >= 100; freq -= 15)
                        {
                            System.Console.Beep(freq, 15);
                        }
                    });
                    break;
                case 3: // win
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        System.Console.Beep(523, 100);
                        System.Console.Beep(659, 100);
                        System.Console.Beep(784, 100);
                        System.Console.Beep(1046, 250);
                    });
                    break;
                case 4: // flag
                    System.Console.Beep(600, 50);
                    System.Console.Beep(900, 50);
                    break;
            }
        }

        private void AnotherQuit_Click(object sender, EventArgs e)
        {
            EndScreen.Visible = false;
            TitleScreen.Visible = true;
            isGameStart = false;
            isGamePaused = false;
            PauseCover.Visible = false;
            timeCounter.Stop();
            TimeCounter.Text = "0";
            isGameEnd = true;
            ClearPanelControls(GameBoard);
        }

        private void AnotherRetry_Click(object sender, EventArgs e)
        {
            QuickRetry();
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!isGameStart) return;

            if (e.KeyCode == Keys.R)
                QuickRetry();
        }

        private void QuickRetry()
        {
            isGamePaused = false;
            PauseCover.Visible = false;
            timeCounter.Stop();
            TimeCounter.Text = "0";
            ClearPanelControls(GameBoard);
            isGameStart = false;
            isGameEnd = false;
            InitaliseGame();
        }

        private void Custom_Click(object sender, EventArgs e)
        {
            CustomSetting.Visible = true;
            SettingScreen.Visible = false;

            Custom.BackColor = Color.LightGray;
            EasyMode.BackColor = Color.White;
            NormalMode.BackColor = Color.White;
            HardMode.BackColor = Color.White;
        }

        private void ConfirmCustom_Click(object sender, EventArgs e)
        {
            string customWidth = CustomWidth.Text;
            if (int.TryParse(customWidth, out int widthValue))
            {
                if (widthValue <= 0)
                {
                    SizeWarnText.Text = "Width can't be smaller than 1";
                    return;
                }
            }
            else
            {
                SizeWarnText.Text = "Value of width is invalid ";
                return;
            }

            string customHeight = CustomHeight.Text;
            if (int.TryParse(customHeight, out int heightValue))
            {
                if (heightValue <= 0)
                {
                    SizeWarnText.Text = "Height can't be smaller than 1";
                    return;
                }
            }
            else
            {
                SizeWarnText.Text = "Value of height is invalid ";
                return;
            }

            string customMine = CustomMine.Text;
            if (int.TryParse(customMine, out int mineValue))
            {
                if (mineValue <= 0)
                {
                    MineWarnText.Text = "Number of mine can't be smaller than 1";
                    return;
                }
            }
            else
            {
                MineWarnText.Text = "Value of mine is invalid ";
                return;
            }

            if ((heightValue * widthValue) > 2500)
            {
                SizeWarnText.Text = "The size of grid is too large, may be inefficient, are you sure?";
                if (!sizeIsWarned)
                {
                    sizeIsWarned = true;
                    return;
                }
            }
            if (mineValue > heightValue * widthValue - 9)
            {
                MineWarnText.Text = "Number of mine is larger than available space";
                return;
            }

            currentNumOfMines = mineValue;
            currentColumns = widthValue;
            currentRows = heightValue;

            SizeWarnText.Text = "";
            MineWarnText.Text = "";

            CustomSettingText.Text = $"Customised: w={widthValue}, h={heightValue}, {mineValue}/{widthValue*heightValue}";

            CustomSetting.Visible = false;
            SettingScreen.Visible = true;
        }
    }


    public class Cell
    {
        public bool isMine;
        public bool isReveal = false;
        public bool isFlagged = false;
        public int adjacentMines = 0;
        public float fontHeight = 10;
        public string text = "";
        public Color fontColour = Color.Black;
    }

    public class StopwatchTimer
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        // expose elapsed time
        public TimeSpan Elapsed => stopwatch.Elapsed;

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public void Reset()
        {
            stopwatch.Reset();
        }
    }

    enum NumberDisplayingStyleList
    {
        Classic,
        Binary,
        Maths,
        HarderMaths
    }
}
