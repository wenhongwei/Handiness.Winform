using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Handiness.Winform.Control
{
    public class DigitalDisplay : Panel
    {
        private Color _circleColor = Color.WhiteSmoke;
        private string _contentText;
        public int test;
        /********************************/
        public DigitalDisplay() : base()
        {

        }

        [Description("背景圆的颜色")]
        public Color CircleColor
        {
            get
            {
                return _circleColor;
            }

            set
            {
                _circleColor = value;
            }
        }
        [Description("文字内容")]
        public string ContentText
        {
            get
            {
                return _contentText;
            }
            set
            {
                if (String.IsNullOrEmpty(_contentText))
                {
                    this._contentText = value;
                }
                else
                {
                    this.ShowNumber(value);
                }
            }
        }
        private Object _syncObj = new Object();
        public void ShowNumber(String number)
        {
            this._contentText = number;
            PaintEventArgs e = new PaintEventArgs(this.CreateGraphics(), this.ClientRectangle);
            this.OnPaint(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;


            Rectangle shadow = new Rectangle(0, 1, this.ClientRectangle.Width - 8, this.ClientRectangle.Height - 8);
            Color shadowColor = Color.FromArgb(175, 175, 175);
            Pen shadowPen = new Pen(shadowColor);
            g.DrawEllipse(shadowPen, shadow);
            Brush fillBrush = new SolidBrush(shadowColor);
            g.FillEllipse(fillBrush, shadow);

            Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width - 10, this.ClientRectangle.Height - 10);
            Pen p = new Pen(this._circleColor);
            g.DrawEllipse(p, rect);
            Brush b = new SolidBrush(this._circleColor);
            g.FillEllipse(b, rect);

            Brush textColor = new SolidBrush(this.ForeColor);
            SizeF size = g.MeasureString(_contentText, this.Font);
            int circleWidth = this.ClientRectangle.Width - 5;
            int circleHeight = this.ClientRectangle.Height - 5;
            Point location = new Point(Convert.ToInt32(circleWidth / 2 - size.Width / 2), Convert.ToInt32(circleHeight / 2 - size.Height / 2));
            g.DrawString(_contentText, this.Font, textColor, location);
            base.OnPaint(e);
        }
    }
}
