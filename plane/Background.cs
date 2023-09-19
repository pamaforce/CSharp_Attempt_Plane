using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Plane
{
    class Background
    {
        private int Y = -850;
        private int Speed;
        private static Image backgroundImg = Properties.Resources.background;

        public Background(int speed)
        {
            this.Speed = speed;
        }

        public void Draw(Graphics g)
        {
            this.Y += (this.Speed/2);
            if (this.Y >= 0)
            {
                this.Y = -850;
            }
            g.DrawImage(backgroundImg, 0, this.Y);
        }
        public void KeyDown(String Key)
        {
            if (Key.Contains("W"))
            {
                this.Y += this.Speed;
                if (this.Y >= 0)
                {
                    this.Y = -850;
                }
            }
            if (Key.Contains("S"))
            {
                this.Y -= this.Speed;
            }
        }
    }
}
