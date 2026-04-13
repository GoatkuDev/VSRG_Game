
namespace Rhythm
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            KeyDisplay = new Label();
            GameTick = new System.Windows.Forms.Timer(components);
            BeatmapSelectionBox = new ListBox();
            BeatmapSearchBar = new TextBox();
            BeatmapSelectionPanel = new Panel();
            GameplayPanel = new Panel();
            CurrentMap = new Label();
            NowPlaying = new Label();
            SongBackgroundBox = new PictureBox();
            MainPanel = new Panel();
            VSRGLogo = new PictureBox();
            PlayButton = new Button();
            SettingsButton = new Button();
            KeybindsPanel = new FlowLayoutPanel();
            Keybind0 = new Button();
            Keybind1 = new Button();
            Keybind2 = new Button();
            Keybind3 = new Button();
            Leave = new Button();
            ScorePanel = new TableLayoutPanel();
            MissBox = new Label();
            BadBox = new Label();
            GoodBox = new Label();
            GreatBox = new Label();
            MarvelousBox = new Label();
            PerfectBox = new Label();
            ScoreMapName = new Label();
            ScoreMapBackground = new PictureBox();
            ScoreRankingPanel = new Panel();
            LetterRankingBox = new Label();
            AccuracyBox = new Label();
            BeatmapPreviewBackground = new PictureBox();
            BeatmapSelectionPanel.SuspendLayout();
            GameplayPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SongBackgroundBox).BeginInit();
            MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VSRGLogo).BeginInit();
            KeybindsPanel.SuspendLayout();
            ScorePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ScoreMapBackground).BeginInit();
            ScoreRankingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BeatmapPreviewBackground).BeginInit();
            SuspendLayout();
            // 
            // KeyDisplay
            // 
            KeyDisplay.BackColor = SystemColors.Desktop;
            KeyDisplay.Font = new Font("Impact", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KeyDisplay.ForeColor = SystemColors.ControlLightLight;
            KeyDisplay.Location = new Point(1, 116);
            KeyDisplay.Margin = new Padding(2, 0, 2, 0);
            KeyDisplay.Name = "KeyDisplay";
            KeyDisplay.RightToLeft = RightToLeft.No;
            KeyDisplay.Size = new Size(100, 100);
            KeyDisplay.TabIndex = 0;
            KeyDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GameTick
            // 
            GameTick.Enabled = true;
            GameTick.Interval = 1;
            // 
            // BeatmapSelectionBox
            // 
            BeatmapSelectionBox.BackColor = Color.Black;
            BeatmapSelectionBox.Dock = DockStyle.Fill;
            BeatmapSelectionBox.Font = new Font("Impact", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BeatmapSelectionBox.ForeColor = Color.White;
            BeatmapSelectionBox.FormattingEnabled = true;
            BeatmapSelectionBox.Location = new Point(0, 37);
            BeatmapSelectionBox.Name = "BeatmapSelectionBox";
            BeatmapSelectionBox.RightToLeft = RightToLeft.Yes;
            BeatmapSelectionBox.Size = new Size(868, 702);
            BeatmapSelectionBox.TabIndex = 1;
            // 
            // BeatmapSearchBar
            // 
            BeatmapSearchBar.BackColor = Color.Black;
            BeatmapSearchBar.Dock = DockStyle.Top;
            BeatmapSearchBar.Font = new Font("Impact", 18F);
            BeatmapSearchBar.ForeColor = Color.White;
            BeatmapSearchBar.Location = new Point(0, 0);
            BeatmapSearchBar.Name = "BeatmapSearchBar";
            BeatmapSearchBar.Size = new Size(868, 37);
            BeatmapSearchBar.TabIndex = 2;
            // 
            // BeatmapSelectionPanel
            // 
            BeatmapSelectionPanel.Controls.Add(BeatmapSelectionBox);
            BeatmapSelectionPanel.Controls.Add(BeatmapSearchBar);
            BeatmapSelectionPanel.Dock = DockStyle.Right;
            BeatmapSelectionPanel.Location = new Point(613, 0);
            BeatmapSelectionPanel.Name = "BeatmapSelectionPanel";
            BeatmapSelectionPanel.Size = new Size(868, 739);
            BeatmapSelectionPanel.TabIndex = 3;
            BeatmapSelectionPanel.Visible = false;
            // 
            // GameplayPanel
            // 
            GameplayPanel.Anchor = AnchorStyles.None;
            GameplayPanel.Controls.Add(CurrentMap);
            GameplayPanel.Controls.Add(NowPlaying);
            GameplayPanel.Controls.Add(SongBackgroundBox);
            GameplayPanel.Location = new Point(1, 164);
            GameplayPanel.Name = "GameplayPanel";
            GameplayPanel.Size = new Size(402, 356);
            GameplayPanel.TabIndex = 5;
            GameplayPanel.Visible = false;
            // 
            // CurrentMap
            // 
            CurrentMap.Dock = DockStyle.Bottom;
            CurrentMap.ForeColor = Color.FromArgb(192, 255, 255);
            CurrentMap.Location = new Point(0, 52);
            CurrentMap.Name = "CurrentMap";
            CurrentMap.Size = new Size(402, 109);
            CurrentMap.TabIndex = 9;
            CurrentMap.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NowPlaying
            // 
            NowPlaying.Dock = DockStyle.Top;
            NowPlaying.ForeColor = Color.FromArgb(192, 255, 255);
            NowPlaying.Location = new Point(0, 0);
            NowPlaying.Name = "NowPlaying";
            NowPlaying.Size = new Size(402, 44);
            NowPlaying.TabIndex = 8;
            NowPlaying.Text = "Now playing:";
            NowPlaying.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SongBackgroundBox
            // 
            SongBackgroundBox.Dock = DockStyle.Bottom;
            SongBackgroundBox.Location = new Point(0, 161);
            SongBackgroundBox.Name = "SongBackgroundBox";
            SongBackgroundBox.Size = new Size(402, 195);
            SongBackgroundBox.SizeMode = PictureBoxSizeMode.Zoom;
            SongBackgroundBox.TabIndex = 7;
            SongBackgroundBox.TabStop = false;
            // 
            // MainPanel
            // 
            MainPanel.Anchor = AnchorStyles.None;
            MainPanel.Controls.Add(VSRGLogo);
            MainPanel.Controls.Add(PlayButton);
            MainPanel.Controls.Add(SettingsButton);
            MainPanel.Location = new Point(559, 242);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(377, 343);
            MainPanel.TabIndex = 4;
            // 
            // VSRGLogo
            // 
            VSRGLogo.Dock = DockStyle.Bottom;
            VSRGLogo.Image = Properties.Resources.VSRGLogo;
            VSRGLogo.Location = new Point(0, 239);
            VSRGLogo.Name = "VSRGLogo";
            VSRGLogo.Size = new Size(377, 104);
            VSRGLogo.SizeMode = PictureBoxSizeMode.Zoom;
            VSRGLogo.TabIndex = 2;
            VSRGLogo.TabStop = false;
            // 
            // PlayButton
            // 
            PlayButton.Dock = DockStyle.Top;
            PlayButton.Font = new Font("Impact", 30F);
            PlayButton.ForeColor = Color.White;
            PlayButton.Location = new Point(0, 110);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(377, 110);
            PlayButton.TabIndex = 1;
            PlayButton.Text = "Play";
            PlayButton.UseVisualStyleBackColor = false;
            PlayButton.MouseClick += PlayButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.Dock = DockStyle.Top;
            SettingsButton.Font = new Font("Impact", 30F);
            SettingsButton.ForeColor = Color.White;
            SettingsButton.Location = new Point(0, 0);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(377, 110);
            SettingsButton.TabIndex = 0;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = false;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // KeybindsPanel
            // 
            KeybindsPanel.Controls.Add(Keybind0);
            KeybindsPanel.Controls.Add(Keybind1);
            KeybindsPanel.Controls.Add(Keybind2);
            KeybindsPanel.Controls.Add(Keybind3);
            KeybindsPanel.Location = new Point(20, 576);
            KeybindsPanel.Name = "KeybindsPanel";
            KeybindsPanel.Size = new Size(514, 120);
            KeybindsPanel.TabIndex = 6;
            KeybindsPanel.Visible = false;
            KeybindsPanel.WrapContents = false;
            // 
            // Keybind0
            // 
            Keybind0.Font = new Font("Impact", 30F);
            Keybind0.ForeColor = Color.White;
            Keybind0.Location = new Point(3, 3);
            Keybind0.Name = "Keybind0";
            Keybind0.Size = new Size(122, 110);
            Keybind0.TabIndex = 1;
            Keybind0.Text = "R";
            Keybind0.UseVisualStyleBackColor = false;
            Keybind0.KeyDown += Keybind0_KeyDown;
            // 
            // Keybind1
            // 
            Keybind1.Font = new Font("Impact", 30F);
            Keybind1.ForeColor = Color.White;
            Keybind1.Location = new Point(131, 3);
            Keybind1.Name = "Keybind1";
            Keybind1.Size = new Size(122, 110);
            Keybind1.TabIndex = 2;
            Keybind1.Text = "T";
            Keybind1.UseVisualStyleBackColor = false;
            Keybind1.KeyDown += Keybind1_KeyDown;
            // 
            // Keybind2
            // 
            Keybind2.Font = new Font("Impact", 30F);
            Keybind2.ForeColor = Color.White;
            Keybind2.Location = new Point(259, 3);
            Keybind2.Name = "Keybind2";
            Keybind2.Size = new Size(122, 110);
            Keybind2.TabIndex = 3;
            Keybind2.Text = "N";
            Keybind2.UseVisualStyleBackColor = false;
            Keybind2.KeyDown += Keybind2_KeyDown;
            // 
            // Keybind3
            // 
            Keybind3.Font = new Font("Impact", 30F);
            Keybind3.ForeColor = Color.White;
            Keybind3.Location = new Point(387, 3);
            Keybind3.Name = "Keybind3";
            Keybind3.Size = new Size(122, 110);
            Keybind3.TabIndex = 4;
            Keybind3.Text = "M";
            Keybind3.UseVisualStyleBackColor = false;
            Keybind3.KeyDown += Keybind3_KeyDown;
            // 
            // Leave
            // 
            Leave.BackColor = Color.FromArgb(255, 128, 128);
            Leave.Font = new Font("Microsoft YaHei UI", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Leave.ForeColor = Color.White;
            Leave.Location = new Point(1, 0);
            Leave.Name = "Leave";
            Leave.Size = new Size(110, 113);
            Leave.TabIndex = 5;
            Leave.Text = "<-";
            Leave.UseVisualStyleBackColor = false;
            Leave.Visible = false;
            Leave.Click += Leave_Click;
            // 
            // ScorePanel
            // 
            ScorePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            ScorePanel.ColumnCount = 2;
            ScorePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ScorePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ScorePanel.Controls.Add(MissBox, 1, 3);
            ScorePanel.Controls.Add(BadBox, 0, 3);
            ScorePanel.Controls.Add(GoodBox, 1, 2);
            ScorePanel.Controls.Add(GreatBox, 0, 2);
            ScorePanel.Controls.Add(MarvelousBox, 1, 1);
            ScorePanel.Controls.Add(PerfectBox, 0, 1);
            ScorePanel.Controls.Add(ScoreMapName, 0, 0);
            ScorePanel.Controls.Add(ScoreMapBackground, 0, 4);
            ScorePanel.Location = new Point(432, 12);
            ScorePanel.Name = "ScorePanel";
            ScorePanel.RowCount = 5;
            ScorePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.0256386F));
            ScorePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.02564F));
            ScorePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.02564F));
            ScorePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.02564F));
            ScorePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 35.8974342F));
            ScorePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ScorePanel.Size = new Size(146, 119);
            ScorePanel.TabIndex = 7;
            ScorePanel.Visible = false;
            // 
            // MissBox
            // 
            MissBox.Dock = DockStyle.Fill;
            MissBox.Font = new Font("Impact", 28F);
            MissBox.ForeColor = Color.FromArgb(115, 0, 0);
            MissBox.Location = new Point(76, 58);
            MissBox.Name = "MissBox";
            MissBox.Size = new Size(66, 18);
            MissBox.TabIndex = 23;
            MissBox.Text = "Miss : 0";
            MissBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BadBox
            // 
            BadBox.Dock = DockStyle.Fill;
            BadBox.Font = new Font("Impact", 28F);
            BadBox.ForeColor = Color.FromArgb(64, 110, 231);
            BadBox.Location = new Point(4, 58);
            BadBox.Name = "BadBox";
            BadBox.Size = new Size(65, 18);
            BadBox.TabIndex = 22;
            BadBox.Text = "Bad : 0";
            BadBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GoodBox
            // 
            GoodBox.Dock = DockStyle.Fill;
            GoodBox.Font = new Font("Impact", 28F);
            GoodBox.ForeColor = Color.FromArgb(183, 130, 232);
            GoodBox.Location = new Point(76, 39);
            GoodBox.Name = "GoodBox";
            GoodBox.Size = new Size(66, 18);
            GoodBox.TabIndex = 21;
            GoodBox.Text = "Good : 0";
            GoodBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GreatBox
            // 
            GreatBox.Dock = DockStyle.Fill;
            GreatBox.Font = new Font("Impact", 28F);
            GreatBox.ForeColor = Color.FromArgb(147, 248, 132);
            GreatBox.Location = new Point(4, 39);
            GreatBox.Name = "GreatBox";
            GreatBox.Size = new Size(65, 18);
            GreatBox.TabIndex = 20;
            GreatBox.Text = "Great : 0";
            GreatBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MarvelousBox
            // 
            MarvelousBox.Dock = DockStyle.Fill;
            MarvelousBox.Font = new Font("Impact", 28F);
            MarvelousBox.ForeColor = Color.FromArgb(252, 252, 252);
            MarvelousBox.Location = new Point(76, 20);
            MarvelousBox.Name = "MarvelousBox";
            MarvelousBox.Size = new Size(66, 18);
            MarvelousBox.TabIndex = 19;
            MarvelousBox.Text = "Marvelous : 1252";
            MarvelousBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PerfectBox
            // 
            PerfectBox.Dock = DockStyle.Fill;
            PerfectBox.Font = new Font("Impact", 28F);
            PerfectBox.ForeColor = Color.FromArgb(252, 244, 113);
            PerfectBox.Location = new Point(4, 20);
            PerfectBox.Name = "PerfectBox";
            PerfectBox.Size = new Size(65, 18);
            PerfectBox.TabIndex = 18;
            PerfectBox.Text = "Perfect : 0";
            PerfectBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ScoreMapName
            // 
            ScorePanel.SetColumnSpan(ScoreMapName, 2);
            ScoreMapName.Dock = DockStyle.Fill;
            ScoreMapName.Font = new Font("Impact", 25F);
            ScoreMapName.ForeColor = Color.FromArgb(192, 255, 255);
            ScoreMapName.Location = new Point(4, 1);
            ScoreMapName.Name = "ScoreMapName";
            ScoreMapName.Size = new Size(138, 18);
            ScoreMapName.TabIndex = 17;
            ScoreMapName.Text = "Now playing:";
            ScoreMapName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ScoreMapBackground
            // 
            ScorePanel.SetColumnSpan(ScoreMapBackground, 2);
            ScoreMapBackground.Dock = DockStyle.Fill;
            ScoreMapBackground.Location = new Point(4, 80);
            ScoreMapBackground.Name = "ScoreMapBackground";
            ScoreMapBackground.Size = new Size(138, 35);
            ScoreMapBackground.SizeMode = PictureBoxSizeMode.Zoom;
            ScoreMapBackground.TabIndex = 15;
            ScoreMapBackground.TabStop = false;
            // 
            // ScoreRankingPanel
            // 
            ScoreRankingPanel.Anchor = AnchorStyles.None;
            ScoreRankingPanel.BorderStyle = BorderStyle.FixedSingle;
            ScoreRankingPanel.Controls.Add(LetterRankingBox);
            ScoreRankingPanel.Controls.Add(AccuracyBox);
            ScoreRankingPanel.Location = new Point(360, 116);
            ScoreRankingPanel.Name = "ScoreRankingPanel";
            ScoreRankingPanel.Size = new Size(337, 173);
            ScoreRankingPanel.TabIndex = 5;
            ScoreRankingPanel.Visible = false;
            // 
            // LetterRankingBox
            // 
            LetterRankingBox.Dock = DockStyle.Fill;
            LetterRankingBox.Font = new Font("Impact", 65F);
            LetterRankingBox.ForeColor = Color.FromArgb(252, 252, 252);
            LetterRankingBox.Location = new Point(0, 62);
            LetterRankingBox.Name = "LetterRankingBox";
            LetterRankingBox.Size = new Size(335, 109);
            LetterRankingBox.TabIndex = 21;
            LetterRankingBox.Text = "SS+";
            LetterRankingBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AccuracyBox
            // 
            AccuracyBox.Dock = DockStyle.Top;
            AccuracyBox.Font = new Font("Impact", 28F);
            AccuracyBox.ForeColor = Color.FromArgb(252, 252, 252);
            AccuracyBox.ImageAlign = ContentAlignment.BottomCenter;
            AccuracyBox.Location = new Point(0, 0);
            AccuracyBox.Name = "AccuracyBox";
            AccuracyBox.Size = new Size(335, 62);
            AccuracyBox.TabIndex = 20;
            AccuracyBox.Text = "Accuracy : 100.00%";
            AccuracyBox.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BeatmapPreviewBackground
            // 
            BeatmapPreviewBackground.Location = new Point(127, 13);
            BeatmapPreviewBackground.Name = "BeatmapPreviewBackground";
            BeatmapPreviewBackground.Size = new Size(214, 92);
            BeatmapPreviewBackground.SizeMode = PictureBoxSizeMode.Zoom;
            BeatmapPreviewBackground.TabIndex = 8;
            BeatmapPreviewBackground.TabStop = false;
            BeatmapPreviewBackground.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1481, 739);
            Controls.Add(BeatmapPreviewBackground);
            Controls.Add(ScoreRankingPanel);
            Controls.Add(ScorePanel);
            Controls.Add(GameplayPanel);
            Controls.Add(KeybindsPanel);
            Controls.Add(MainPanel);
            Controls.Add(BeatmapSelectionPanel);
            Controls.Add(KeyDisplay);
            Controls.Add(Leave);
            Font = new Font("Impact", 22F);
            KeyPreview = true;
            Margin = new Padding(2);
            Name = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            BeatmapSelectionPanel.ResumeLayout(false);
            BeatmapSelectionPanel.PerformLayout();
            GameplayPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SongBackgroundBox).EndInit();
            MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)VSRGLogo).EndInit();
            KeybindsPanel.ResumeLayout(false);
            ScorePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ScoreMapBackground).EndInit();
            ScoreRankingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BeatmapPreviewBackground).EndInit();
            ResumeLayout(false);
        }

        private Label KeyDisplay;
        private System.Windows.Forms.Timer GameTick;
        private ListBox BeatmapSelectionBox;
        private TextBox BeatmapSearchBar;
        private Panel BeatmapSelectionPanel;
        private Panel MainPanel;
        private Button SettingsButton;
        private Button PlayButton;
        private PictureBox VSRGLogo;
        private FlowLayoutPanel KeybindsPanel;
        private Button Keybind0;
        private Button Keybind1;
        private Button Keybind2;
        private Button Keybind3;
        private Button Leave;
        private PictureBox SongBackgroundBox;
        private Panel GameplayPanel;
        private TableLayoutPanel ScorePanel;
        private PictureBox ScoreMapBackground;
        private Label NowPlaying;
        private Label CurrentMap;
        private Label MissBox;
        private Label BadBox;
        private Label GoodBox;
        private Label GreatBox;
        private Label MarvelousBox;
        private Label PerfectBox;
        private Label ScoreMapName;
        private Panel ScoreRankingPanel;
        private Label LetterRankingBox;
        private Label AccuracyBox;
        private PictureBox BeatmapPreviewBackground;
    }
}
