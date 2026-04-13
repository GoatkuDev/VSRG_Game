using System;
using Microsoft.VisualBasic.Devices;
using System.Data;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Windows.Forms;
using NAudio.Wave;
using ManagedBass;
using System.Threading.Tasks;

namespace Rhythm
{
    public partial class Form1 : Form
    {
        private List<Beatmap> Beatmaps = new List<Beatmap>();
        private List<Beatmap> FilteredBeatmaps = new List<Beatmap>();

        private int DisplayLimit = 25;

        private string AudioFileName = null;
        private string BackgroundFileName = null;

        private int Stream;

        bool WaitingForKey = false;
        Keybinds Keybinds = new Keybinds();

        private GameSurface VSRG;
        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
            KeyPreview = true;

            // Song Selection \\
            BeatmapSelectionBox.SelectedIndexChanged += BeatmapSelectionBox_SelectedIndexChanged;
            BeatmapSelectionBox.KeyDown += BeatmapSelectionBox_KeyDown;

            BeatmapSearchBar.KeyDown += BeatmapSearchBar_KeyDown;
        }

        // Game Tick + Map End \\
        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (VSRG == null) return;

            VSRG.UpdateGame();
            if (VSRG.MapEnded == true)
            {
                GameTick.Stop();

                VSRG.ScoreDisplay(MarvelousBox, PerfectBox, GreatBox, GoodBox, BadBox, MissBox, AccuracyBox, LetterRankingBox);
                EndMap();

                GameplayPanel.Visible = false;
                ScorePanel.Visible = true;
                ScoreRankingPanel.Visible = true;
                Leave.Visible = true;
            }
        }
        private void EndMap()
        {
            VSRG.CleanupSong();
            Controls.Remove(VSRG);
            VSRG.Dispose();
            VSRG = null;
        }

        // Beatmap Selection \\
        private void BeatmapSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Beatmap Selected = (Beatmap)BeatmapSelectionBox.SelectedItem;

            if (VSRG == null)
            {
                string FilePath = Selected.FilePath;
                string MapFolder = Path.GetDirectoryName(FilePath);

                bool InEvents = false;
                foreach (var Line in File.ReadAllLines(Selected.FilePath))
                {
                    // Audio File \\
                    if (Line.StartsWith("AudioFilename:"))
                    {
                        AudioFileName = Line.Split(':')[1].Trim();

                        string AudioFilePath = Path.Combine(MapFolder, AudioFileName);

                        if (File.Exists(AudioFilePath))
                        {
                            LoadSong(AudioFilePath);
                            PlaySong();
                            continue;
                        }
                    }

                    // Background Image File \\
                    if (Line.StartsWith("[Events]"))
                    {
                        InEvents = true;
                        continue;
                    }

                    if (InEvents)
                    {
                        if (Line.StartsWith("[")) break;
                        if (Line.StartsWith("//")) continue;

                        if (Line.ToLower().Contains(".png") || Line.ToLower().Contains(".jpg"))
                        {
                            string[] Parts = Line.Split(',');

                            if (Parts.Length >= 3)
                            {
                                BackgroundFileName = Parts[2].Trim('"');

                                string BackgroundFilePath = Path.Combine(MapFolder, BackgroundFileName);

                                if (File.Exists(BackgroundFilePath))
                                {
                                    BeatmapPreviewBackground.Image = Image.FromFile(BackgroundFilePath);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private async void BeatmapSelectionBox_KeyDown(object sender, KeyEventArgs Key)
        {
            if (Key.KeyCode == Keys.Enter)
            {
                if (BeatmapSelectionBox.SelectedItem != null)
                {
                    this.ActiveControl = null; /* uhhh noticed that turning off visibility for a control doesnt make the control be out of focus
                    which lead to whoever playing this can just press enter again to start another play which broke the game */

                    BeatmapPreviewBackground.Visible = false;

                    Beatmap Selected = (Beatmap)BeatmapSelectionBox.SelectedItem;

                    string FilePath = Selected.FilePath;
                    string MapFolder = Path.GetDirectoryName(FilePath);

                    CurrentMap.Text = Selected.ToString();

                    VSRG = new GameSurface(FilePath, Keybinds);
                    VSRG.Dock = DockStyle.Fill;
                    Controls.Add(VSRG);

                    StopSong();

                    bool InEvents = false;
                    foreach (var Line in File.ReadAllLines(FilePath))
                    {
                        // Audio File \\
                        if (Line.StartsWith("AudioFilename:"))
                        {
                            AudioFileName = Line.Split(':')[1].Trim();
                            continue;
                        }

                        // Background Image File \\
                        if (Line.StartsWith("[Events]"))
                        {
                            InEvents = true;
                            continue;
                        }

                        if (InEvents)
                        {
                            if (Line.StartsWith("[")) break;
                            if (Line.StartsWith("//")) continue;

                            if (Line.ToLower().Contains(".png") || Line.ToLower().Contains(".jpg"))
                            {
                                string[] Parts = Line.Split(',');

                                if (Parts.Length >= 3)
                                {
                                    BackgroundFileName = Parts[2].Trim('"');
                                    break;
                                }
                            }
                        }
                    }

                    string AudioFilePath = Path.Combine(MapFolder, AudioFileName);

                    if (File.Exists(AudioFilePath))
                    {
                        VSRG.LoadSong(AudioFilePath);
                    }
                    
                    string BackgroundFilePath = Path.Combine(MapFolder, BackgroundFileName);

                    if (File.Exists(BackgroundFilePath))
                    {
                        SongBackgroundBox.Image = Image.FromFile(BackgroundFilePath);
                    }

                    BeatmapSelectionPanel.Visible = false;
                    Leave.Visible = false;
                    GameplayPanel.Visible = true;

                    // Score Panel \\
                    ScoreMapName.Text = CurrentMap.Text;
                    ScoreMapBackground.Image = SongBackgroundBox.Image;

                    VSRG.StartMap();
                    VSRG.PlaySong();
                    GameTick.Tick += GameTick_Tick;
                    GameTick.Start();
                }
            }
        }
        private void BeatmapSearchBar_KeyDown(object sender, KeyEventArgs Key)
        {
            if (Key.KeyCode == Keys.Enter)
            {
                UpdateList();
            }
        }

        // Beatmap Utilities \\
        private string GetOsuSongsFolder()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "osu!", "Songs");

            if (Directory.Exists(path)) return path;

            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        Beatmap ParseBeatmap(string Path)
        {
            var Lines = File.ReadAllLines(Path);

            Beatmap Map = new Beatmap();
            Map.FilePath = Path;

            foreach (var Line in Lines)
            {
                if (Line.StartsWith("Title:"))
                    Map.Title = Line.Substring(6).Trim();

                if (Line.StartsWith("Artist:"))
                    Map.Artist = Line.Substring(7).Trim();

                if (Line.StartsWith("Version:"))
                    Map.Version = Line.Substring(8).Trim();
            }
            return Map;
        }
        private void LoadBeatmaps()
        {
            string SongsPath = GetOsuSongsFolder();
            var SongsFolders = Directory.GetDirectories(SongsPath);

            Beatmaps.Clear();

            foreach (var Folder in SongsFolders)
            {
                var Files = Directory.GetFiles(Folder, "*.osu");

                foreach (var File in Files.Take(DisplayLimit))
                {
                    Beatmaps.Add(ParseBeatmap(File));
                }
            }
        }
        private void UpdateList()
        {
            string SearchText = BeatmapSearchBar.Text.ToLower().Trim();

            var FilteredBeatmaps = Beatmaps.Where(Map => Map.Title.ToLower().Contains(SearchText) || Map.Artist.ToLower().Contains(SearchText) ||
            Map.Version.ToLower().Contains(SearchText)).Take(DisplayLimit).ToList();

            BeatmapSelectionBox.BeginUpdate();
            BeatmapSelectionBox.Items.Clear();

            foreach (var Map in FilteredBeatmaps)
            {
                BeatmapSelectionBox.Items.Add(Map);
            }

            BeatmapSelectionBox.EndUpdate();
        }

        // Input Signaling \\
        private void Form1_KeyDown(object sender, KeyEventArgs Key)
        {
            KeyDisplay.Text = Key.KeyCode.ToString();

            if (Key.KeyCode == Keys.Escape || Key.KeyCode == Keys.Delete)
            {
                this.Close();
            }

            VSRG?.OnKeyDown(Key.KeyCode);
        }

        // Play Button \\
        private void PlayButton_Click(object sender, EventArgs e)
        {
            MainPanel.Visible = false;

            BeatmapSelectionPanel.Visible = true;

            Leave.Visible = true;

            BeatmapPreviewBackground.Visible = true;
        }

        // Song Preview \\
        public void LoadSong(string Path)
        {
            CleanupSong();

            Stream = Bass.CreateStream(Path);
            Bass.ChannelSetAttribute(Stream, ChannelAttribute.Volume, 0.25f);
        }
        public void PlaySong()
        {
            if (Stream != 0)
            {
                Bass.ChannelPlay(Stream);
            }
        }
        public void StopSong()
        {
            Bass.ChannelStop(Stream);
        }
        public void CleanupSong()
        {
            if (Stream != 0)
            {
                Bass.ChannelStop(Stream);
                Bass.StreamFree(Stream);
                Stream = 0;
            }
        }

        // Settings \\
        private void SettingsButton_Click(Object sender, EventArgs e)
        {
            Leave.Visible = true;

            Keybind0.Text = Keybinds.Column0Key.ToString();
            Keybind1.Text = Keybinds.Column1Key.ToString();
            Keybind2.Text = Keybinds.Column2Key.ToString();
            Keybind3.Text = Keybinds.Column3Key.ToString();

            MainPanel.Visible = false;

            KeybindsPanel.Visible = true;

            WaitingForKey = true;
        }
        private void Leave_Click(object sender, EventArgs e)
        {
            Leave.Visible = false;

            if (KeybindsPanel.Visible == false  && ScorePanel.Visible == false)
            {
                BeatmapPreviewBackground.Visible = false;
                BeatmapSelectionPanel.Visible = false;
                MainPanel.Visible = true;
                // CleanupSong(); think about if I should clean the song after going back to main menu, need to get feedback for it
            }

            if (KeybindsPanel.Visible)
            {
                KeybindsPanel.Visible = false;
                MainPanel.Visible = true;

                WaitingForKey = false;
            }

            if (ScorePanel.Visible)
            {
                ScorePanel.Visible = false;
                ScoreRankingPanel.Visible = false;
                BeatmapSelectionPanel.Visible = true;
                BeatmapPreviewBackground.Visible = true;
                Leave.Visible = true;
            }
        }

        // Keybinds \\
        private void Keybind0_KeyDown(Object sender, KeyEventArgs KeyPressed)
        {
            if (!WaitingForKey) return;

            Keybind0.Text = KeyPressed.KeyCode.ToString();
            Keybinds.Column0Key = KeyPressed.KeyCode;
        }
        private void Keybind1_KeyDown(Object sender, KeyEventArgs KeyPressed)
        {
            if (!WaitingForKey) return;

            Keybind1.Text = KeyPressed.KeyCode.ToString();
            Keybinds.Column1Key = KeyPressed.KeyCode;
        }
        private void Keybind2_KeyDown(Object sender, KeyEventArgs KeyPressed)
        {
            if (!WaitingForKey) return;

            Keybind2.Text = KeyPressed.KeyCode.ToString();
            Keybinds.Column2Key = KeyPressed.KeyCode;
        }
        private void Keybind3_KeyDown(Object sender, KeyEventArgs KeyPressed)
        {
            if (!WaitingForKey) return;

            Keybind3.Text = KeyPressed.KeyCode.ToString();
            Keybinds.Column3Key = KeyPressed.KeyCode;
        }

        // Control Location Calculation \\
        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterControl(MainPanel);
            CenterControl(KeybindsPanel);

            ScorePanel.Size = new Size(1250, 650);
            CenterControl(ScorePanel);

            BottomRightControl(GameplayPanel);

            Leave.Location = new Point(0, 0);

            CenterControl(ScoreRankingPanel);
            ScoreRankingPanel.Top -= (int)(ScorePanel.Height / 2) + (ScoreRankingPanel.Height / 2);

            BeatmapPreviewBackground.Left = Leave.Width + 100;
            BeatmapPreviewBackground.Size = new Size(ClientSize.Width / 3, ClientSize.Height);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Bass.Init();

            LoadBeatmaps();
            UpdateList();

            CenterControl(MainPanel);
            CenterControl(KeybindsPanel);

            ScorePanel.Size = new Size(1250, 650);
            CenterControl(ScorePanel);

            BottomRightControl(GameplayPanel);

            Leave.Location = new Point(0, 0);

            CenterControl(ScoreRankingPanel);
            ScoreRankingPanel.Top -= (int)(ScorePanel.Height / 2) + (ScoreRankingPanel.Height / 2);

            BeatmapPreviewBackground.Left = Leave.Width + 100;
            BeatmapPreviewBackground.Size = new Size(ClientSize.Width / 3, ClientSize.Height);
        }
        private void CenterControl(Control Control)
        {
            Control.Left = (this.ClientSize.Width - Control.Width) / 2;
            Control.Top = (this.ClientSize.Height - Control.Height) / 2;
        }
        private void BottomRightControl(Control Control)
        {
            Control.Left = (this.ClientSize.Width - Control.Width);
            Control.Top = (this.ClientSize.Height - Control.Height);
        }
    }
}