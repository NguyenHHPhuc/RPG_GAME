using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace RPG.Controls
{
    // Hoạt ảnh trận đấu: lao tới, tên bay, đổi màu khi trúng đòn, ngã, khói, chữ bay.
    public partial class BattleControl
    {
        // Vị trí khói so với chân nhân vật sau khi ngã
        private const int DeathSmokeOffsetX = 18;
        private const int DeathSmokeOffsetY = -11;

        // Mốc thời gian (tick 20ms): 1-8 lao tới, 8 trúng đòn, 9-16 quay về.
        // Nếu mục tiêu chết: 17-22 dừng, 23-30 ngã, 31-55 khói.
        private const int HitFrame = 8;
        private const int TintEndFrame = 12;
        private const int ReturnEndFrame = 16;
        private const int FallStartFrame = 23;
        private const int FallEndFrame = 30;
        private const int DeathEndFrame = 55;
        private const float FallAngleStep = 90f / (FallEndFrame - FallStartFrame + 1);

        private readonly System.Windows.Forms.Timer animationTimer;
        private PictureBox? movingCharacter;
        private PictureBox? damagedCharacter;
        private int animationStep;
        private int animationStartX;
        private int animationStartY;
        private int animationDirection;
        private int strikeDistance;
        private int damageToShow;
        private bool targetWasDefeated;
        private bool isArrowAttack;
        private Action? animationFinished;

        private Image? arrowImage;
        private Image? tintedImage;
        private Image? damagedOriginalImage;
        private Point arrowStart;
        private Point arrowEnd;
        private Point arrowPosition;

        private Image? fallenImage;
        private Size fallenSize;
        private float fallAngle;
        private Point fallenPivot;
        private Point smokeOrigin;
        private readonly List<Point> deathParticles = new List<Point>();
        private readonly Random particleRandom = new Random();

        private readonly List<FloatingText> floatingTexts = new List<FloatingText>();
        private readonly System.Windows.Forms.Timer floatingTextTimer;

        private static readonly Color DamageColor = ColorTranslator.FromHtml("#FF4500");
        private static readonly Color BleedColor = ColorTranslator.FromHtml("#8B0000");
        private static readonly Color HealColor = ColorTranslator.FromHtml("#90EE90");
        private static readonly Color ManaColor = ColorTranslator.FromHtml("#00BFFF");
        private static readonly Color GoldColor = ColorTranslator.FromHtml("#FFD700");
        private static readonly Color BlockColor = ColorTranslator.FromHtml("#4682B4");
        private static readonly Color SmokeColor = ColorTranslator.FromHtml("#F5F5F5");
        private static readonly Color ShadowColor = ColorTranslator.FromHtml("#000000");

        // Tạo một lần, dùng lại mỗi lần vẽ
        private readonly Font floatingFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        private readonly SolidBrush smokeBrush = new SolidBrush(Color.FromArgb(180, SmokeColor));

        // finished chạy khi hoạt ảnh kết thúc
        private void AnimateStrike(bool playerStrikes, int damage, Action finished)
        {
            PictureBox attacker = playerStrikes ? picBattlePlayer : picBattleEnemy;
            PictureBox target = playerStrikes ? picBattleEnemy : picBattlePlayer;
            movingCharacter = attacker;
            damagedCharacter = target;
            animationStartX = attacker.Left;
            animationStartY = attacker.Top;
            animationDirection = playerStrikes ? 1 : -1;
            isArrowAttack = !playerStrikes && enemyShootsArrows;

            // Lao tới sát mục tiêu, chừa lại 8px
            strikeDistance = playerStrikes
                ? target.Left - attacker.Right - 8
                : attacker.Left - target.Right - 8;
            strikeDistance = Math.Max(0, strikeDistance);

            animationStep = 0;
            damageToShow = damage;
            animationFinished = finished;
            targetWasDefeated = playerStrikes ? enemy.IsDead : player.IsDead;

            damagedOriginalImage = target.Image;
            tintedImage?.Dispose();
            tintedImage = damagedOriginalImage == null ? null : MakeDamageTint(damagedOriginalImage);

            arrowStart = CenterOf(attacker);
            arrowEnd = CenterOf(target);
            arrowPosition = arrowStart;

            fallenImage = null;
            fallAngle = 0;
            deathParticles.Clear();

            SetActionButtonsEnabled(false);
            animationTimer.Start();
        }

        private static Point CenterOf(Control control)
        {
            return new Point(control.Left + control.Width / 2, control.Top + control.Height / 2);
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            if (movingCharacter == null || damagedCharacter == null)
                return;

            animationStep++;

            if (animationStep <= ReturnEndFrame)
                UpdateAttackMotion(movingCharacter);
            else if (targetWasDefeated && animationStep <= FallEndFrame)
                UpdateFall(damagedCharacter);
            else if (targetWasDefeated)
                UpdateSmoke();

            if (animationStep == HitFrame)
                OnStrikeHit(damagedCharacter);
            else if (animationStep == TintEndFrame)
                damagedCharacter.Image = damagedOriginalImage;

            pnlEncounterScene.Invalidate();

            if (animationStep >= (targetWasDefeated ? DeathEndFrame : ReturnEndFrame))
                EndStrikeAnimation(movingCharacter, damagedCharacter);
        }

        private void UpdateAttackMotion(PictureBox attacker)
        {
            if (isArrowAttack)
            {
                if (animationStep <= HitFrame)
                    arrowPosition = new Point(
                        arrowStart.X + (arrowEnd.X - arrowStart.X) * animationStep / HitFrame,
                        arrowStart.Y + (arrowEnd.Y - arrowStart.Y) * animationStep / HitFrame);
                return;
            }

            // progress: 0 -> 8 khi lao tới, 8 -> 0 khi quay về
            int progress = animationStep <= HitFrame ? animationStep : ReturnEndFrame - animationStep;
            attacker.Left = animationStartX + animationDirection * strikeDistance * progress / HitFrame;
        }

        private void OnStrikeHit(PictureBox target)
        {
            // Sát thương 0 = bị Khiên Chắn chặn
            if (damageToShow <= 0)
            {
                ShowFloatingText("Chặn!", target, BlockColor);
                UpdateBattleUI();
                return;
            }

            if (tintedImage != null)
                target.Image = tintedImage;
            ShowFloatingText($"-{damageToShow}", target, DamageColor);
            UpdateBattleUI();
        }

        private void UpdateFall(PictureBox target)
        {
            if (animationStep < FallStartFrame)
                return;

            if (animationStep == FallStartFrame && damagedOriginalImage != null)
            {
                // Ẩn PictureBox, từ đây ảnh được vẽ xoay trong Paint
                target.Visible = false;
                fallenImage = damagedOriginalImage;

                // PictureBox dùng Zoom nên ảnh thật nhỏ hơn khung; vẽ đúng cỡ để không bị kéo dài
                fallenSize = GetZoomedSize(fallenImage, target.Size);
                int feetY = target.Top + (target.Height + fallenSize.Height) / 2;
                fallenPivot = new Point(target.Left + target.Width / 2, feetY);
                smokeOrigin = new Point(fallenPivot.X + DeathSmokeOffsetX, fallenPivot.Y + DeathSmokeOffsetY);
            }

            fallAngle = (animationStep - FallStartFrame + 1) * FallAngleStep;
        }

        private static Size GetZoomedSize(Image image, Size box)
        {
            float scale = Math.Min((float)box.Width / image.Width, (float)box.Height / image.Height);
            return new Size((int)(image.Width * scale), (int)(image.Height * scale));
        }

        private void UpdateSmoke()
        {
            if (animationStep % 2 == 0)
                deathParticles.Add(new Point(smokeOrigin.X + particleRandom.Next(-12, 13),
                    smokeOrigin.Y - particleRandom.Next(0, 12)));

            for (int i = 0; i < deathParticles.Count; i++)
            {
                Point particle = deathParticles[i];
                particle.Y -= 2;
                particle.X += particleRandom.Next(-1, 2);
                deathParticles[i] = particle;
            }
        }

        private void EndStrikeAnimation(PictureBox attacker, PictureBox target)
        {
            animationTimer.Stop();
            attacker.Location = new Point(animationStartX, animationStartY);
            if (!targetWasDefeated)
                target.Image = damagedOriginalImage;

            movingCharacter = null;
            damagedCharacter = null;
            tintedImage?.Dispose();
            tintedImage = null;
            deathParticles.Clear();
            pnlEncounterScene.Invalidate();

            Action? finished = animationFinished;
            animationFinished = null;
            finished?.Invoke();
        }

        // Phủ tông đỏ lên sprite khi trúng đòn
        private static Image MakeDamageTint(Image source)
        {
            Bitmap tinted = new Bitmap(source.Width, source.Height);
            using Graphics graphics = Graphics.FromImage(tinted);
            ColorMatrix matrix = new ColorMatrix(new float[][]
            {
                new float[] { 0.65f, 0, 0, 0, 0 },
                new float[] { 0, 0.12f, 0, 0, 0 },
                new float[] { 0, 0, 0.12f, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0.35f, 0, 0, 0, 1 }
            });
            using ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix);
            graphics.DrawImage(source, new Rectangle(0, 0, tinted.Width, tinted.Height),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
            return tinted;
        }

        private void ShowFloatingText(string message, PictureBox character, Color color)
        {
            floatingTexts.Add(new FloatingText
            {
                Message = message,
                Position = new PointF(character.Left + character.Width / 2, character.Top - 18),
                TextColor = color,
                TicksLeft = 40
            });
            floatingTextTimer.Start();
        }

        private void FloatingTextTimer_Tick(object? sender, EventArgs e)
        {
            // Duyệt ngược để xóa phần tử không bị lệch chỉ số
            for (int i = floatingTexts.Count - 1; i >= 0; i--)
            {
                FloatingText floatingText = floatingTexts[i];
                floatingText.Position = new PointF(floatingText.Position.X, floatingText.Position.Y - 0.35f);
                floatingText.TicksLeft--;
                if (floatingText.TicksLeft <= 0)
                    floatingTexts.RemoveAt(i);
            }

            pnlEncounterScene.Invalidate();
            if (floatingTexts.Count == 0)
                floatingTextTimer.Stop();
        }

        private void pnlEncounterScene_Paint(object? sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            if (isArrowAttack && animationStep <= HitFrame && arrowImage != null)
            {
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(arrowPosition.X, arrowPosition.Y);
                if (arrowEnd.X < arrowStart.X)
                    graphics.RotateTransform(180);
                graphics.DrawImage(arrowImage, -16, -6, 32, 12);
                graphics.Restore(state);
            }

            // Xoay quanh chân để tạo cảm giác ngã
            if (fallenImage != null)
            {
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(fallenPivot.X, fallenPivot.Y);
                graphics.RotateTransform(fallAngle);
                graphics.DrawImage(fallenImage, -fallenSize.Width / 2, -fallenSize.Height,
                    fallenSize.Width, fallenSize.Height);
                graphics.Restore(state);
            }

            foreach (Point particle in deathParticles)
                graphics.FillEllipse(smokeBrush, particle.X, particle.Y, 7, 7);

            foreach (FloatingText floatingText in floatingTexts)
            {
                int alpha = Math.Min(255, floatingText.TicksLeft * 9);
                SizeF textSize = graphics.MeasureString(floatingText.Message, floatingFont);
                float x = floatingText.Position.X - textSize.Width / 2;
                float y = floatingText.Position.Y;
                using SolidBrush shadow = new SolidBrush(Color.FromArgb(alpha, ShadowColor));
                using SolidBrush textBrush = new SolidBrush(Color.FromArgb(alpha, floatingText.TextColor));
                graphics.DrawString(floatingText.Message, floatingFont, shadow, x + 1, y + 1);
                graphics.DrawString(floatingText.Message, floatingFont, textBrush, x, y);
            }
        }

        private sealed class FloatingText
        {
            public string Message { get; set; } = "";
            public PointF Position { get; set; }
            public Color TextColor { get; set; }
            public int TicksLeft { get; set; }
        }
    }
}
