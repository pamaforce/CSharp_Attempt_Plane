using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Plane
{
    class Bullet
    {
        private static Image bulletImg = Properties.Resources.bullet;
        private int Speed;
        public int X;
        public int Y;

        public Bullet(int x,int y,int speed)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
        }
        public void Draw(Graphics g)
        {
            this.Y -= this.Speed;
            g.DrawImage(bulletImg, X, Y);
        }


    }
}
