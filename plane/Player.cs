using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Plane
{
    class Player
    {
        private static Image playerImg = Properties.Resources.plane;
        private int Speed;
        public int X = 115;
        public int Y = 475;
        private int RightWall = 225;
        private int BottomWall = 525;
        private Timer timer;
        public List<Bullet> bulletList = new List<Bullet>();

        public Player(int speed) {
            this.Speed = speed;
            this.RightWall = 230 - speed;
            this.BottomWall = 525 - speed;
        }
        public void Fire()
        {
            timer = new Timer(launchBullet, null, 100, 400);
        }
        public void StopFire()
        {
            timer.Dispose();
        }

        private void launchBullet(object state)
        {
            bulletList.Add(new Bullet(this.X + 21, this.Y, this.Speed * 2));

        }

        public void Draw(Graphics g)
        {
            g.DrawImage(playerImg, X, Y);
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Draw(g);
            }
        }

        public void KeyDown(String Key)
        {
            if (Key.Contains("A"))
            {
                if (X >= Speed) X -= Speed; else X = 0;
            }
            if (Key.Contains("D"))
            {
                if (X <= RightWall) X += Speed; else X = 230;
            }
            if (Key.Contains("W"))
            {
                if (Y >= Speed) Y -= Speed; else Y = 0;
            }
            if (Key.Contains("S"))
            {
                if (Y <= BottomWall) Y += Speed; else Y = 525;
            }
        }
    }

}
