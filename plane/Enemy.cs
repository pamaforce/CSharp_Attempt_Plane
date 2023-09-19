using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Plane
{
    class Enemy
    {
        private static Image enemyImg = Properties.Resources.enemy;
        private static Image enemyBossImg = Properties.Resources.enemyBoss;
        private int Speed;
        public int X;
        public int Y;
        public int Health;
        public bool isBoss;
        private int RightWall;
        private bool moveTrend;
        private Random random;

        public Enemy(int x, int y, int speed,bool iB,bool mT,Random rd)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.isBoss = iB;
            this.Health = iB ? 5 : 2;
            this.RightWall = 230 - (iB ? 22 : (-2));
            this.moveTrend = mT;
            this.random = rd;
        }
        public void Draw(Graphics g)
        {
            RandomMove();
            g.DrawImage(isBoss?enemyBossImg:enemyImg, X, Y);
        }
        private void RandomMove()
        {
            this.Y += this.Speed;
            int val = this.random.Next()%100;
            if (val < 35)
            {
                this.X -= this.Speed*(this.moveTrend?(-1):1);
                if (this.X <= 0)
                {
                    this.X = 0;
                    this.moveTrend = !this.moveTrend;
                }
                else if (this.X >= this.RightWall)
                {
                    this.X = this.RightWall;
                    this.moveTrend = !this.moveTrend;
                }
            }
            else if (val > 90)
            {
                this.moveTrend = !this.moveTrend;
            }
        }
        public void KeyDown(String Key)
        {
            if (Key.Contains("W"))
            {
                this.Y += (this.Speed/2);
            }
            if (Key.Contains("S"))
            {
                this.Y -= (this.Speed/2);
            }
        }
    }
}
