using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using ManagedBass;
using static System.Net.Mime.MediaTypeNames;

namespace Rhythm
{
    public class GameSurface : Control
    {
        // No idea what to call this in a proper way \\
        private int LastHitDelta = 0;

        private readonly string Map;
        private Keybinds Keybinds;

        // Audio \\
        public WaveOutEvent OutputDevice;
        public AudioFileReader AudioFile;

        public int Stream;

        // Game Tick \\
        private Stopwatch Stopwatch = new Stopwatch();
        private int StopwatchOffset = 0; // work on adding offset for songs that start way earlier than the map chart itself

        // Gameplay \\
        private int Combo;

        private const int ColumnAmount = 4;
        private const int LaneWidth = 80;
        private const int LaneSpacing = 60;

        private float ScrollSpeed = 3f;

        private System.Drawing.Image GreenCircleImage;
        private System.Drawing.Image PurpleCircleImage;
        private System.Drawing.Image ReceptorImage;
        private System.Drawing.Image ReceptorActiveImage;

        private int MarvelousHits;
        private int PerfectHits;
        private int GreatHits;
        private int GoodHits;
        private int BadHits;
        private int Misses;

        private bool[] ColumnPressed = new bool[4];

        private float AccuracyCalculation;

        // Data \\
        private List<Note> Notes = new List<Note>();
        private List<HitAnimation> HitAnimations = new List<HitAnimation>();

        public bool MapEnded = false;

        // Keybinds \\
        private Keys Column0Key => Keybinds.Column0Key;
        private Keys Column1Key => Keybinds.Column1Key;
        private Keys Column2Key => Keybinds.Column2Key;
        private Keys Column3Key => Keybinds.Column3Key;

        // Judgements \\
        private Dictionary<string, float> JudgementTimes = new Dictionary<string, float>
        {
            {"Marvelous", 32},
            {"Perfect", 75},
            {"Great", 115},
            {"Good", 150},
            {"Bad", 185},
            {"Miss", 210},
        };

        private String JudgementText;

        // Replicated First \\
        public GameSurface(string MapParameter, Keybinds Keybinds)
        {
            this.Map = MapParameter;
            this.Keybinds = Keybinds;

            GreenCircleImage = System.Drawing.Image.FromFile(@"GreenCircle.png");
            PurpleCircleImage = System.Drawing.Image.FromFile(@"PurpleCircle.png");
            ReceptorImage = System.Drawing.Image.FromFile(@"Receptor.png");
            ReceptorActiveImage = System.Drawing.Image.FromFile(@"ReceptorActive.png");

            LoadBeatmap(MapParameter);

            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        // Input Detection \\
        public void OnKeyDown(Keys Key)
        {
            int Column = KeyToColumn(Key);
            if (Column == -1) return;

            ColumnPressed[Column] = true;
            CheckHit(Column);
        }

        // Keys Corresponding To Column \\
        private int KeyToColumn(Keys Key)
        {
            if (Key == Column0Key) return 0;
            if (Key == Column1Key) return 1;
            if (Key == Column2Key) return 2;
            if (Key == Column3Key) return 3;
            return -1;
        }

        // Judgement Calculation \\
        private void CheckHit(int Column)
        {
            int CurrentTime = (int)Stopwatch.ElapsedMilliseconds;

            var NotesInColumn = Notes.Where(Note => Note.Column == Column).ToList();
            if (NotesInColumn.Count == 0) return;

            var Note = NotesInColumn.OrderBy(Note => Math.Abs(Note.HitPoint - CurrentTime)).First();

            int SignedDelta = CurrentTime - Note.HitPoint;
            int Delta = Math.Abs(SignedDelta);

            LastHitDelta = SignedDelta;

            if (Delta <= JudgementTimes["Marvelous"])
            {
                HitResult("MARVELOUS", Note);
                Combo += 1;
                JudgementText = "MARVELOUS";
                MarvelousHits += 1;
            }
            else if (Delta <= JudgementTimes["Perfect"])
            {
                HitResult("PERFECT", Note);
                Combo += 1;
                JudgementText = "PERFECT";
                PerfectHits += 1;
            }
            else if (Delta <= JudgementTimes["Great"])
            {
                HitResult("GREAT", Note);
                Combo += 1;
                JudgementText = "GREAT";
                GreatHits += 1;
            }
            else if (Delta <= JudgementTimes["Good"])
            {
                HitResult("GOOD", Note);
                Combo += 1;
                JudgementText = "GOOD";
                GoodHits += 1;
            }
            else if (Delta <= JudgementTimes["Bad"])
            {
                HitResult("BAD", Note);
                Combo += 1;
                JudgementText = "BAD";
                BadHits += 1;
            }
            else if (Delta <= JudgementTimes["Miss"])
            {
                HitResult("MISS", Note);
                Combo = 0;
                JudgementText = "MISS";
                Misses += 1;
            }
        }

        // Judgement Properties \\
        private void HitResult(string Result, Note Note)
        {
            Notes.Remove(Note);

            Color JudgementColor;

            switch (Result)
            {
                case "MARVELOUS":
                    JudgementColor = Color.FromArgb(252, 252, 252);
                    break;
                case "PERFECT":
                    JudgementColor = Color.FromArgb(252, 244, 113);
                    break;
                case "GREAT":
                    JudgementColor = Color.FromArgb(147, 248, 132);
                    break;
                case "GOOD":
                    JudgementColor = Color.FromArgb(183, 130, 232);
                    break;
                case "BAD":
                    JudgementColor = Color.FromArgb(64, 110, 231);
                    break;
                default:
                    JudgementColor = Color.FromArgb(115, 0, 0);
                    break;
            }

            HitAnimations.Add(new HitAnimation{Color = JudgementColor, StartTime = (int)Stopwatch.ElapsedMilliseconds, Alpha = 1f});

            Invalidate();
        }

        // Load & Start Maps \\
        public void StartMap()
        {
            Stopwatch.Restart();
        }
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
        private int GetColumnFromBeatmapX(int BeatmapX)
        {
            return BeatmapX * ColumnAmount / 512;
        }
        private float GetXForColumn(int Column)
        {
            float PlayfieldWidth = ColumnAmount * (LaneWidth + LaneSpacing);
            float CenterOffset = (ClientSize.Width - PlayfieldWidth) / 2;

            return CenterOffset + Column * (LaneWidth + LaneSpacing) + (LaneWidth - GreenCircleImage.Width) / 2;
        }
        private void AddNoteFromBeatmap(int BeatmapX, int BeatmapTime)
        {
            int Column = GetColumnFromBeatmapX(BeatmapX);
            float x = GetXForColumn(Column);
            float y = -GreenCircleImage.Height;
            int HitPoint = BeatmapTime;

            if (Column == 0 || Column == 3)
            {
                Notes.Add(new Note(x, y, Column, HitPoint, PurpleCircleImage));
            }
            else
            {
                Notes.Add(new Note(x, y, Column, HitPoint, GreenCircleImage));
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Stopwatch.ElapsedMilliseconds != 0)
            {
                int SongTime = (int)Stopwatch.ElapsedMilliseconds;

                // Delta Debug Draw \\
                DrawHitDelta(e.Graphics);

                // Note To StopWatch Debug \\
                var ClosestNote = Notes.OrderBy(Note => Math.Abs(Note.HitPoint - SongTime)).FirstOrDefault();

                if (ClosestNote != null)
                {
                    e.Graphics.DrawString(
                        $"ClosestNoteTime: {ClosestNote.HitPoint} ms",
                        new System.Drawing.Font("Consolas", 14),
                        Brushes.White,
                        new PointF(10, 80)
                    );
                }

                // Note & Receptor Draw \\
                DrawReceptors(e.Graphics);

                foreach (Note Note in Notes)
                {
                    float x = GetXForColumn(Note.Column);
                    if (Note.y > -50)
                    {
                        e.Graphics.DrawImage(Note.Image, x, Note.y);
                    }
                }

                // Combo Drawing \\
                string ComboText = $"{Combo}";
                System.Drawing.Font ComboFont = new System.Drawing.Font("Bahnschrift", 32f, FontStyle.Bold);

                SizeF ComboSize = e.Graphics.MeasureString(ComboText, new System.Drawing.Font("Varela Round", 34f));
                float CenterX = ClientSize.Width / 2f;
                float CenterY = 80f;

                float ComboX = CenterX - ComboSize.Width / 2f - 25;
                float ComboY = CenterY - ComboSize.Height / 2f + 250;

                e.Graphics.DrawString(ComboText, ComboFont, Brushes.White, ComboX, ComboY);

                // Accuracy Drawing \\
                if (!(MarvelousHits == 0 && PerfectHits == 0 && GreatHits == 0 && GoodHits == 0 && BadHits == 0 && Misses == 0))
                {
                    AccuracyCalculation = (float)(300 * (MarvelousHits + PerfectHits) + (200 * GreatHits) + (100 * GoodHits) + (50 * BadHits))
                    /
                    (300 * (MarvelousHits + PerfectHits + GreatHits + GoodHits + BadHits + Misses));

                    SizeF AccuracySize = e.Graphics.MeasureString($"{AccuracyCalculation}%", new System.Drawing.Font("Varela Round", 32f));

                    float AccuracyX = CenterX - AccuracySize.Width / 2f + 600;
                    float AccuracyY = CenterY - AccuracySize.Height / 2f + 50;

                    e.Graphics.DrawString((AccuracyCalculation * 100f).ToString("0.00") + "%", ComboFont, Brushes.Wheat, AccuracyX, AccuracyY);
                }

                // Song Time (ms) Drawing \\
                e.Graphics.DrawString(
                    $"SongTime: {SongTime} ms",
                    new System.Drawing.Font("Consolas", 14),
                    Brushes.White,
                    new PointF(10, 60)
                );

                // Hit Animations Depending On Judgement \\
                foreach (var Properties in HitAnimations)
                {
                    float x = ClientSize.Width / 2 - 45;
                    float y = ComboY + 50;

                    using (SolidBrush Brush = new SolidBrush(Color.FromArgb((int)(Properties.Alpha * 255), Properties.Color)))
                    {
                        e.Graphics.FillEllipse(Brush, x, y, 30, 20);
                    }
                }
            }
        }

        // Receptor Size & Position Calculation \\
        private void DrawReceptors(Graphics g)
        {
            int HitLineY = ClientSize.Height - 150;

            for (int Column = 0; Column < 4; Column++)
            {
                float x = GetXForColumn(Column);

                float OffsetX = x - ReceptorImage.Width / 2f + GreenCircleImage.Width / 2f;
                float OffsetY = HitLineY - ReceptorImage.Height / 2f + 50;

                g.DrawImage(ReceptorActiveImage, OffsetX, OffsetY, GreenCircleImage.Width, ReceptorImage.Height - 135);
            }
        }

        // Judgement Display (ms) Calculations \\
        private void DrawHitDelta(Graphics Graphics)
        {
            Graphics.FillRectangle(Brushes.Black, 10, 10, 200, 40);

            Graphics.DrawRectangle(Pens.White, 10, 10, 200, 40);

            string text = $"Hit Delta: {LastHitDelta} ms";
            Graphics.DrawString(text, new System.Drawing.Font("Consolas", 14, FontStyle.Bold), Brushes.White, new PointF(15, 20));
        }

        // Frame Refreshes \\
        public void UpdateGame()
        {
            if (Notes.Count == 0)
            {
                MapEnded = true;
                return;
            }
            else
            {
                if (Stopwatch.ElapsedMilliseconds != 0)
                {
                    int SongTime = (int)Stopwatch.ElapsedMilliseconds;
                    int HitLineY = ClientSize.Height - 150;
                    float ApproachTime = 1500f / ScrollSpeed;
                    float TravelDistance = 1200f;

                    foreach (var Note in Notes)
                    {
                        float TimeLeft = Note.HitPoint - SongTime;

                        if (TimeLeft > ApproachTime)
                        {
                            Note.y = -1000;
                            continue;
                        }

                        float Time = 1f - (TimeLeft / ApproachTime);
                        if (Time < 0) Time = 0;
                        if (Time > 1) Time = 1;

                        Note.y = HitLineY - (1f - Time) * TravelDistance;
                    }

                    int CurrentTime = (int)Stopwatch.ElapsedMilliseconds;

                    for (int i = HitAnimations.Count - 1; i >= 0; i--)
                    {
                        var h = HitAnimations[i];

                        float TimeAlive = CurrentTime - h.StartTime;
                        h.Alpha = 1f - (TimeAlive / 150f);

                        if (h.Alpha <= 0) HitAnimations.RemoveAt(i);
                    }

                    for (int i = Notes.Count - 1; i >= 0; i--)
                    {
                        var Note = Notes[i];

                        if (CurrentTime - Note.HitPoint > 150)
                        {
                            Notes.RemoveAt(i);
                            HitResult("MISS", Notes[i]);
                            Combo = 0;
                            Misses += 1;
                            break; // think about if better or not gonna test some other time
                        }
                    }
                    Invalidate();
                }
            }
        }

        // Reading The .Osu Files For HitPoints \\
        public void LoadBeatmap(string Path)
        {
            var Lines = File.ReadAllLines(Path);

            bool InHitObjects = false;

            foreach (var Line in Lines)
            {
                if (Line.StartsWith("[HitObjects]"))
                {
                    InHitObjects = true;
                    continue;
                }

                if (!InHitObjects)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(Line))
                {
                    continue;
                }

                var S = Line.Split(",");

                int BeatmapX = int.Parse(S[0]); 
                int Time = int.Parse(S[2]);

                AddNoteFromBeatmap(BeatmapX, Time);
            }
        }

        // Score Display \\
        public (string Rank, Color Color) GetRank(float Accuracy)
        {
            if (Accuracy == 100) return ("SS+", Color.FromArgb(252, 252, 252));
            if (Accuracy >= 95) return ("S", Color.FromArgb(252, 244, 113));
            if (Accuracy >= 90) return ("A", Color.FromArgb(147, 248, 132));
            if (Accuracy >= 80) return ("B", Color.FromArgb(64, 110, 231));
            if (Accuracy >= 70) return ("C", Color.FromArgb(183, 130, 232));
            return ("D", Color.FromArgb(115, 0, 0));
        }
        public void ScoreDisplay(Control MarvelousBox, Control PerfectBox, Control GreatBox, Control GoodBox, Control BadBox, Control MissBox, Control AccuracyBox, Control LetterRankingBox)
        {
            // Judgements \\
            MarvelousBox.Text = $"Marvelous : {MarvelousHits}";
            PerfectBox.Text = $"Perfect : {PerfectHits}";
            GreatBox.Text = $"Great : {GreatHits}";
            GoodBox.Text = $"Good : {GoodHits}";
            BadBox.Text = $"Bad : {BadHits}";
            MissBox.Text = $"Miss : {Misses}";

            // Rankings \\
            var MapRanking = GetRank((AccuracyCalculation*100f));

            LetterRankingBox.Text = MapRanking.Rank;
            LetterRankingBox.ForeColor = MapRanking.Color;

            AccuracyBox.Text = $"Accuracy : {AccuracyCalculation*100f:0.00}%";
            AccuracyBox.ForeColor = MapRanking.Color;
        }
    }
}