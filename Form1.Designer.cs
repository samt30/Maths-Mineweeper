namespace MathsweeperWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FlagNumber = new Label();
            FlagSymbol = new Label();
            TimeCounter = new Label();
            Time = new Label();
            GameUI = new Panel();
            PauseButton = new Button();
            PauseCover = new Panel();
            AnotherRetry = new Button();
            AnotherQuit = new Button();
            PauseScreenText2 = new Label();
            PauseScreenText1 = new Label();
            TitleScreen = new Panel();
            PlayButton = new Button();
            SettingButton = new Button();
            Title = new Label();
            EndScreen = new Panel();
            button1 = new Button();
            PlayAgain = new Button();
            EndText = new Label();
            GameBoard = new Panel();
            SettingScreen = new Panel();
            CustomSettingText = new Label();
            Custom = new Button();
            HarderMathsMode = new Button();
            MathsMode = new Button();
            BinaryMode = new Button();
            ClassicMode = new Button();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            HardMode = new Button();
            NormalMode = new Button();
            EasyMode = new Button();
            CustomSetting = new Panel();
            MineWarnText = new Label();
            SizeWarnText = new Label();
            ConfirmCustom = new Button();
            CustomMine = new TextBox();
            CustomHeight = new TextBox();
            label4 = new Label();
            CustomWidth = new TextBox();
            label3 = new Label();
            GameUI.SuspendLayout();
            PauseCover.SuspendLayout();
            TitleScreen.SuspendLayout();
            EndScreen.SuspendLayout();
            SettingScreen.SuspendLayout();
            CustomSetting.SuspendLayout();
            SuspendLayout();
            // 
            // FlagNumber
            // 
            FlagNumber.Font = new Font("Microsoft JhengHei UI", 42F);
            FlagNumber.Location = new Point(89, 0);
            FlagNumber.Name = "FlagNumber";
            FlagNumber.Size = new Size(191, 80);
            FlagNumber.TabIndex = 0;
            FlagNumber.Text = "0";
            FlagNumber.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FlagSymbol
            // 
            FlagSymbol.Font = new Font("Microsoft JhengHei UI", 45F);
            FlagSymbol.ForeColor = Color.Red;
            FlagSymbol.Location = new Point(12, 0);
            FlagSymbol.Name = "FlagSymbol";
            FlagSymbol.Size = new Size(80, 80);
            FlagSymbol.TabIndex = 1;
            FlagSymbol.Text = "🚩";
            // 
            // TimeCounter
            // 
            TimeCounter.Font = new Font("Microsoft JhengHei UI", 50F);
            TimeCounter.Location = new Point(477, 0);
            TimeCounter.Name = "TimeCounter";
            TimeCounter.Size = new Size(330, 90);
            TimeCounter.TabIndex = 2;
            TimeCounter.Text = "0";
            // 
            // Time
            // 
            Time.Font = new Font("Microsoft JhengHei UI", 50F);
            Time.Location = new Point(264, 0);
            Time.Name = "Time";
            Time.Size = new Size(207, 90);
            Time.TabIndex = 3;
            Time.Text = "Time:";
            // 
            // GameUI
            // 
            GameUI.Anchor = AnchorStyles.Top;
            GameUI.Controls.Add(PauseButton);
            GameUI.Controls.Add(FlagSymbol);
            GameUI.Controls.Add(TimeCounter);
            GameUI.Controls.Add(Time);
            GameUI.Controls.Add(FlagNumber);
            GameUI.Location = new Point(0, 0);
            GameUI.Name = "GameUI";
            GameUI.Size = new Size(984, 90);
            GameUI.TabIndex = 4;
            // 
            // PauseButton
            // 
            PauseButton.Font = new Font("Microsoft JhengHei UI", 16F);
            PauseButton.Location = new Point(813, 28);
            PauseButton.Name = "PauseButton";
            PauseButton.Size = new Size(125, 50);
            PauseButton.TabIndex = 4;
            PauseButton.Text = "Pause";
            PauseButton.UseVisualStyleBackColor = true;
            PauseButton.Click += PauseButton_Click;
            // 
            // PauseCover
            // 
            PauseCover.Controls.Add(AnotherRetry);
            PauseCover.Controls.Add(AnotherQuit);
            PauseCover.Controls.Add(PauseScreenText2);
            PauseCover.Controls.Add(PauseScreenText1);
            PauseCover.Dock = DockStyle.Fill;
            PauseCover.Location = new Point(0, 0);
            PauseCover.Name = "PauseCover";
            PauseCover.Size = new Size(984, 761);
            PauseCover.TabIndex = 3;
            // 
            // AnotherRetry
            // 
            AnotherRetry.Anchor = AnchorStyles.Top;
            AnotherRetry.Font = new Font("Microsoft JhengHei UI", 12F);
            AnotherRetry.Location = new Point(400, 400);
            AnotherRetry.Name = "AnotherRetry";
            AnotherRetry.Size = new Size(200, 40);
            AnotherRetry.TabIndex = 5;
            AnotherRetry.Text = "Retry";
            AnotherRetry.UseVisualStyleBackColor = true;
            AnotherRetry.Click += AnotherRetry_Click;
            // 
            // AnotherQuit
            // 
            AnotherQuit.Anchor = AnchorStyles.Top;
            AnotherQuit.Font = new Font("Microsoft JhengHei UI", 12F);
            AnotherQuit.Location = new Point(400, 450);
            AnotherQuit.Name = "AnotherQuit";
            AnotherQuit.Size = new Size(200, 40);
            AnotherQuit.TabIndex = 4;
            AnotherQuit.Text = "Back to title screen";
            AnotherQuit.UseVisualStyleBackColor = true;
            AnotherQuit.Click += AnotherQuit_Click;
            // 
            // PauseScreenText2
            // 
            PauseScreenText2.Anchor = AnchorStyles.Top;
            PauseScreenText2.Font = new Font("Microsoft JhengHei UI", 16F);
            PauseScreenText2.Location = new Point(80, 320);
            PauseScreenText2.Name = "PauseScreenText2";
            PauseScreenText2.Size = new Size(800, 40);
            PauseScreenText2.TabIndex = 3;
            PauseScreenText2.Text = "\"take a rest, take a breath\"";
            PauseScreenText2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PauseScreenText1
            // 
            PauseScreenText1.Anchor = AnchorStyles.Top;
            PauseScreenText1.AutoSize = true;
            PauseScreenText1.Font = new Font("Microsoft JhengHei UI", 64F);
            PauseScreenText1.Location = new Point(150, 200);
            PauseScreenText1.Name = "PauseScreenText1";
            PauseScreenText1.Size = new Size(684, 109);
            PauseScreenText1.TabIndex = 3;
            PauseScreenText1.Text = "Game is Paused";
            // 
            // TitleScreen
            // 
            TitleScreen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TitleScreen.Controls.Add(PlayButton);
            TitleScreen.Controls.Add(SettingButton);
            TitleScreen.Controls.Add(Title);
            TitleScreen.Location = new Point(0, 0);
            TitleScreen.Name = "TitleScreen";
            TitleScreen.Size = new Size(984, 749);
            TitleScreen.TabIndex = 5;
            // 
            // PlayButton
            // 
            PlayButton.Anchor = AnchorStyles.Top;
            PlayButton.Location = new Point(400, 340);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(200, 50);
            PlayButton.TabIndex = 3;
            PlayButton.Text = "Play";
            PlayButton.UseVisualStyleBackColor = true;
            PlayButton.Click += PlayButton_Click;
            // 
            // SettingButton
            // 
            SettingButton.Anchor = AnchorStyles.Top;
            SettingButton.Location = new Point(400, 410);
            SettingButton.Name = "SettingButton";
            SettingButton.Size = new Size(200, 50);
            SettingButton.TabIndex = 2;
            SettingButton.Text = "Game Setting";
            SettingButton.UseVisualStyleBackColor = true;
            SettingButton.Click += SettingButton_Click;
            // 
            // Title
            // 
            Title.Anchor = AnchorStyles.Top;
            Title.Font = new Font("Microsoft JhengHei UI", 64F);
            Title.Location = new Point(190, 64);
            Title.Name = "Title";
            Title.Size = new Size(600, 150);
            Title.TabIndex = 1;
            Title.Text = "MineSweeper";
            Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // EndScreen
            // 
            EndScreen.Anchor = AnchorStyles.Top;
            EndScreen.Controls.Add(button1);
            EndScreen.Controls.Add(PlayAgain);
            EndScreen.Controls.Add(EndText);
            EndScreen.Location = new Point(275, 250);
            EndScreen.Name = "EndScreen";
            EndScreen.Size = new Size(450, 300);
            EndScreen.TabIndex = 3;
            EndScreen.Visible = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.Font = new Font("Microsoft JhengHei UI", 16F);
            button1.Location = new Point(150, 180);
            button1.Name = "button1";
            button1.Size = new Size(150, 40);
            button1.TabIndex = 2;
            button1.Text = "Quit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // PlayAgain
            // 
            PlayAgain.Anchor = AnchorStyles.Top;
            PlayAgain.Font = new Font("Microsoft JhengHei UI", 16F);
            PlayAgain.Location = new Point(150, 120);
            PlayAgain.Name = "PlayAgain";
            PlayAgain.Size = new Size(150, 40);
            PlayAgain.TabIndex = 1;
            PlayAgain.Text = "Play again";
            PlayAgain.UseVisualStyleBackColor = true;
            PlayAgain.Click += PlayAgain_Click;
            // 
            // EndText
            // 
            EndText.Anchor = AnchorStyles.Top;
            EndText.Font = new Font("Microsoft JhengHei UI", 30F);
            EndText.Location = new Point(75, 10);
            EndText.Name = "EndText";
            EndText.Size = new Size(300, 100);
            EndText.TabIndex = 0;
            EndText.Text = "WinOrLose";
            EndText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GameBoard
            // 
            GameBoard.Anchor = AnchorStyles.Top;
            GameBoard.AutoScroll = true;
            GameBoard.Location = new Point(0, 0);
            GameBoard.Name = "GameBoard";
            GameBoard.Size = new Size(10, 10);
            GameBoard.TabIndex = 3;
            // 
            // SettingScreen
            // 
            SettingScreen.Anchor = AnchorStyles.Top;
            SettingScreen.BackColor = SystemColors.ControlLight;
            SettingScreen.Controls.Add(CustomSettingText);
            SettingScreen.Controls.Add(Custom);
            SettingScreen.Controls.Add(HarderMathsMode);
            SettingScreen.Controls.Add(MathsMode);
            SettingScreen.Controls.Add(BinaryMode);
            SettingScreen.Controls.Add(ClassicMode);
            SettingScreen.Controls.Add(label2);
            SettingScreen.Controls.Add(label1);
            SettingScreen.Controls.Add(button2);
            SettingScreen.Controls.Add(HardMode);
            SettingScreen.Controls.Add(NormalMode);
            SettingScreen.Controls.Add(EasyMode);
            SettingScreen.Location = new Point(250, 200);
            SettingScreen.Name = "SettingScreen";
            SettingScreen.Size = new Size(500, 400);
            SettingScreen.TabIndex = 4;
            SettingScreen.Visible = false;
            // 
            // CustomSettingText
            // 
            CustomSettingText.Anchor = AnchorStyles.Top;
            CustomSettingText.AutoSize = true;
            CustomSettingText.Location = new Point(80, 295);
            CustomSettingText.Name = "CustomSettingText";
            CustomSettingText.Size = new Size(0, 15);
            CustomSettingText.TabIndex = 11;
            // 
            // Custom
            // 
            Custom.Anchor = AnchorStyles.Top;
            Custom.Font = new Font("Microsoft JhengHei UI", 12F);
            Custom.Location = new Point(340, 120);
            Custom.Name = "Custom";
            Custom.Size = new Size(90, 40);
            Custom.TabIndex = 10;
            Custom.Text = "Custom";
            Custom.UseVisualStyleBackColor = true;
            Custom.Click += Custom_Click;
            // 
            // HarderMathsMode
            // 
            HarderMathsMode.Anchor = AnchorStyles.Top;
            HarderMathsMode.Font = new Font("Microsoft JhengHei UI", 9F);
            HarderMathsMode.Location = new Point(340, 220);
            HarderMathsMode.Name = "HarderMathsMode";
            HarderMathsMode.Size = new Size(90, 40);
            HarderMathsMode.TabIndex = 9;
            HarderMathsMode.Text = "HarderMaths";
            HarderMathsMode.UseVisualStyleBackColor = true;
            HarderMathsMode.Click += HarderMathsMode_Click;
            // 
            // MathsMode
            // 
            MathsMode.Anchor = AnchorStyles.Top;
            MathsMode.Font = new Font("Microsoft JhengHei UI", 9F);
            MathsMode.Location = new Point(252, 220);
            MathsMode.Name = "MathsMode";
            MathsMode.Size = new Size(80, 40);
            MathsMode.TabIndex = 8;
            MathsMode.Text = "Maths";
            MathsMode.UseVisualStyleBackColor = true;
            MathsMode.Click += MathsMode_Click;
            // 
            // BinaryMode
            // 
            BinaryMode.Anchor = AnchorStyles.Top;
            BinaryMode.Font = new Font("Microsoft JhengHei UI", 9F);
            BinaryMode.Location = new Point(166, 220);
            BinaryMode.Name = "BinaryMode";
            BinaryMode.Size = new Size(80, 40);
            BinaryMode.TabIndex = 7;
            BinaryMode.Text = "Binary";
            BinaryMode.UseVisualStyleBackColor = true;
            BinaryMode.Click += BinaryMode_Click;
            // 
            // ClassicMode
            // 
            ClassicMode.Anchor = AnchorStyles.Top;
            ClassicMode.BackColor = Color.LightGray;
            ClassicMode.Font = new Font("Microsoft JhengHei UI", 9F);
            ClassicMode.Location = new Point(80, 220);
            ClassicMode.Name = "ClassicMode";
            ClassicMode.Size = new Size(80, 40);
            ClassicMode.TabIndex = 6;
            ClassicMode.Text = "Classic";
            ClassicMode.UseVisualStyleBackColor = false;
            ClassicMode.Click += ClassicMode_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 12F);
            label2.Location = new Point(80, 182);
            label2.Name = "label2";
            label2.Size = new Size(336, 20);
            label2.TabIndex = 5;
            label2.Text = "Display Mode (how numbers are presented)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F);
            label1.Location = new Point(80, 90);
            label1.Name = "label1";
            label1.Size = new Size(219, 20);
            label1.TabIndex = 4;
            label1.Text = "Game Difficulty (size of grid)";
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft JhengHei UI", 9F);
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(50, 36);
            button2.TabIndex = 3;
            button2.Text = "Back";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // HardMode
            // 
            HardMode.Anchor = AnchorStyles.Top;
            HardMode.Font = new Font("Microsoft JhengHei UI", 12F);
            HardMode.Location = new Point(252, 120);
            HardMode.Name = "HardMode";
            HardMode.Size = new Size(80, 40);
            HardMode.TabIndex = 2;
            HardMode.Text = "Expert";
            HardMode.UseVisualStyleBackColor = true;
            HardMode.Click += HardMode_Click;
            // 
            // NormalMode
            // 
            NormalMode.Anchor = AnchorStyles.Top;
            NormalMode.Font = new Font("Microsoft JhengHei UI", 7F);
            NormalMode.Location = new Point(166, 120);
            NormalMode.Name = "NormalMode";
            NormalMode.Size = new Size(80, 40);
            NormalMode.TabIndex = 1;
            NormalMode.Text = "Intermediate";
            NormalMode.UseVisualStyleBackColor = true;
            NormalMode.Click += NormalMode_Click;
            // 
            // EasyMode
            // 
            EasyMode.Anchor = AnchorStyles.Top;
            EasyMode.BackColor = Color.LightGray;
            EasyMode.Font = new Font("Microsoft JhengHei UI", 9F);
            EasyMode.Location = new Point(80, 120);
            EasyMode.Name = "EasyMode";
            EasyMode.Size = new Size(80, 40);
            EasyMode.TabIndex = 0;
            EasyMode.Text = "Beginner";
            EasyMode.UseVisualStyleBackColor = false;
            EasyMode.Click += EasyMode_Click;
            // 
            // CustomSetting
            // 
            CustomSetting.Anchor = AnchorStyles.Top;
            CustomSetting.BackColor = Color.LightGray;
            CustomSetting.Controls.Add(MineWarnText);
            CustomSetting.Controls.Add(SizeWarnText);
            CustomSetting.Controls.Add(ConfirmCustom);
            CustomSetting.Controls.Add(CustomMine);
            CustomSetting.Controls.Add(CustomHeight);
            CustomSetting.Controls.Add(label4);
            CustomSetting.Controls.Add(CustomWidth);
            CustomSetting.Controls.Add(label3);
            CustomSetting.Location = new Point(250, 200);
            CustomSetting.Name = "CustomSetting";
            CustomSetting.Size = new Size(500, 400);
            CustomSetting.TabIndex = 4;
            CustomSetting.Visible = false;
            // 
            // MineWarnText
            // 
            MineWarnText.AutoSize = true;
            MineWarnText.Font = new Font("Microsoft JhengHei UI", 9F);
            MineWarnText.ForeColor = Color.Red;
            MineWarnText.Location = new Point(80, 295);
            MineWarnText.Name = "MineWarnText";
            MineWarnText.Size = new Size(0, 15);
            MineWarnText.TabIndex = 7;
            // 
            // SizeWarnText
            // 
            SizeWarnText.AutoSize = true;
            SizeWarnText.BackColor = Color.LightGray;
            SizeWarnText.Font = new Font("Microsoft JhengHei UI", 9F);
            SizeWarnText.ForeColor = Color.Red;
            SizeWarnText.Location = new Point(80, 155);
            SizeWarnText.Name = "SizeWarnText";
            SizeWarnText.Size = new Size(0, 15);
            SizeWarnText.TabIndex = 6;
            // 
            // ConfirmCustom
            // 
            ConfirmCustom.Anchor = AnchorStyles.Top;
            ConfirmCustom.Font = new Font("Microsoft JhengHei UI", 12F);
            ConfirmCustom.Location = new Point(190, 340);
            ConfirmCustom.Name = "ConfirmCustom";
            ConfirmCustom.Size = new Size(120, 40);
            ConfirmCustom.TabIndex = 5;
            ConfirmCustom.Text = "Confirm";
            ConfirmCustom.UseVisualStyleBackColor = true;
            ConfirmCustom.Click += ConfirmCustom_Click;
            // 
            // CustomMine
            // 
            CustomMine.Location = new Point(80, 260);
            CustomMine.Name = "CustomMine";
            CustomMine.Size = new Size(100, 23);
            CustomMine.TabIndex = 4;
            // 
            // CustomHeight
            // 
            CustomHeight.Location = new Point(220, 120);
            CustomHeight.Name = "CustomHeight";
            CustomHeight.Size = new Size(100, 23);
            CustomHeight.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 16F);
            label4.Location = new Point(40, 200);
            label4.Name = "label4";
            label4.Size = new Size(234, 28);
            label4.TabIndex = 2;
            label4.Text = "Total number of mine";
            // 
            // CustomWidth
            // 
            CustomWidth.Location = new Point(80, 120);
            CustomWidth.Name = "CustomWidth";
            CustomWidth.Size = new Size(100, 23);
            CustomWidth.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 16F);
            label3.Location = new Point(40, 64);
            label3.Name = "label3";
            label3.Size = new Size(196, 28);
            label3.TabIndex = 0;
            label3.Text = "Width and Height";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 761);
            Controls.Add(SettingScreen);
            Controls.Add(CustomSetting);
            Controls.Add(TitleScreen);
            Controls.Add(GameUI);
            Controls.Add(EndScreen);
            Controls.Add(PauseCover);
            Controls.Add(GameBoard);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            GameUI.ResumeLayout(false);
            PauseCover.ResumeLayout(false);
            PauseCover.PerformLayout();
            TitleScreen.ResumeLayout(false);
            EndScreen.ResumeLayout(false);
            SettingScreen.ResumeLayout(false);
            SettingScreen.PerformLayout();
            CustomSetting.ResumeLayout(false);
            CustomSetting.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label FlagNumber;
        private Label FlagSymbol;
        private Label TimeCounter;
        private Label Time;
        private Panel GameUI;
        private Panel TitleScreen;
        private Label Title;
        private Button SettingButton;
        private Button PauseButton;
        private Panel PauseCover;
        private Label PauseScreenText1;
        private Label PauseScreenText2;
        private Panel GameBoard;
        private Panel EndScreen;
        private Button PlayButton;
        private Label EndText;
        private Button button1;
        private Button PlayAgain;
        private Panel SettingScreen;
        private Button EasyMode;
        private Label label1;
        private Button button2;
        private Button HardMode;
        private Button NormalMode;
        private Button HarderMathsMode;
        private Button MathsMode;
        private Button BinaryMode;
        private Button ClassicMode;
        private Label label2;
        private Button AnotherQuit;
        private Button AnotherRetry;
        private Button Custom;
        private Panel CustomSetting;
        private TextBox CustomHeight;
        private Label label4;
        private TextBox CustomWidth;
        private Label label3;
        private Button ConfirmCustom;
        private TextBox CustomMine;
        private Label MineWarnText;
        private Label SizeWarnText;
        private Label CustomSettingText;
    }
}
