using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsApplication2
{
    /// <summary>
    /// Summary description for InfiniteProgress.
    /// </summary>
    public class InfiniteProgress : System.Windows.Forms.Control
    {
        public InfiniteProgress()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Color1 = Color.White;
            Color2 = Color.Blue;
            Position = 0;
            Step = 5;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            LinearGradientBrush b = new LinearGradientBrush(
                this.Bounds, Color1, Color2, 0, false);
            b.WrapMode = WrapMode.TileFlipX;
            b.TranslateTransform(Position, 0, MatrixOrder.Append);
            pe.Graphics.FillRectangle(b, 0, 0, this.Width, this.Height);
            b.Dispose();

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        public void OnTick(object sender, EventArgs args)
        {
            Position += Step;
            if (Position > this.Width)
                Position = -this.Width;
            this.Invalidate();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                if (_timer == null)
                {
                    _timer = new System.Windows.Forms.Timer();
                    _timer.Interval = 20;
                    _timer.Tick += new EventHandler(OnTick);
                }

                _timer.Start();
            }
            else
            {
                if (_timer != null)
                {
                    _timer.Stop();
                }
            }

            base.OnVisibleChanged(e);
        }

        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public float Position { get; set; }
        public float Step { get; set; }

        private Timer _timer;

    }
}
