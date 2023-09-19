using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Plane
{
    class Game
    {
        private const int speed = 5;
        private MainForm mainForm;
        private Background background;
        private Player player;
        private System.Threading.Timer timer;
        private List<Enemy> enemyList = new List<Enemy>();
        private bool isStart = false;
        private bool canOpen = true;
        private int Score = 0;

        public Game(MainForm mF)
        {
            this.mainForm = mF;
            this.background = new Background(speed);
            this.player = new Player(speed);
        }
        
        public void Draw(Graphics g)
        {
            background.Draw(g);
            player.Draw(g);
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Draw(g);
            }
        }

        public void KeyDown(String key)
        {
            player.KeyDown(key);
            background.KeyDown(key);
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].KeyDown(key);
            }
        }
        public void DetectCollision()
        {
            if (!isStart)
            {
                if (player.Y <= 230&&player.Y>=150&&player.X>=130&&player.X<=230)
                {

                    ShowAboutDialog();
                }
                else if (player.Y <= 230 && player.Y >= 150 && player.X <= 100)
                {
                    StartGame();
                }
                else
                {
                    canOpen = true;
                }
            }
            else
            {
                for (int i = 0; i < player.bulletList.Count; i++)
                {
                    if (player.bulletList[i].Y <= -40)
                    {
                        player.bulletList.Remove(player.bulletList[i]);
                    }
                }
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].Y >= 850)
                    {
                        enemyList.Remove(enemyList[i]);
                        continue;
                    }
                    if (enemyList[i].Y <= player.Y + (enemyList[i].isBoss?72:48) && enemyList[i].Y >= player.Y - 50 && enemyList[i].X <= player.X + 50 && enemyList[i].X >= player.X - (enemyList[i].isBoss ? 72 : 48))
                    {
                        changeScore(enemyList[i].isBoss ? -6 : -3);
                        enemyList.Remove(enemyList[i]);
                        continue;
                    }
                    for (int j = 0; j < player.bulletList.Count; j++)
                    {
                        if (enemyList[i].Y <= player.bulletList[j].Y + (enemyList[i].isBoss ? 72 : 48) && enemyList[i].Y >= player.bulletList[j].Y - 41 && enemyList[i].X <= player.bulletList[j].X + 8 && enemyList[i].X >= player.bulletList[j].X - (enemyList[i].isBoss ? 72 : 48))
                        {
                            enemyList[i].Health--;
                            if (enemyList[i].Health == 0)
                            {
                                changeScore(enemyList[i].isBoss ? 4 : 1);
                                enemyList.Remove(enemyList[i]);
                            }
                            player.bulletList.Remove(player.bulletList[j]);
                            break;
                        }
                    }
                    
                }
            }
        }
        public void StartGame()
        {
            isStart = true;
            mainForm.HideMenu();
            player.Fire();
            generateEnemy();
        }
        private void changeScore(int score)
        {
            this.Score += score;
            if (this.Score < 0)
            {
                mainForm.ShowMenu();
                player.StopFire();
                stopGenerateEnemy();
                Score = 0;
                isStart = false;
                canOpen = false;
            }
            mainForm.updateScore(this.Score);
        }
        private void generateEnemy()
        {
            Random rd = new Random();
            enemyList.Add(new Enemy(rd.Next() % 209,-48, speed/2, false,rd.Next()%2==0,rd));
            timer = new System.Threading.Timer(randomEnemy, rd, 100, 400);
        }
        private void stopGenerateEnemy()
        {
            timer.Dispose();
        }
        private void randomEnemy(object state)
        {
            Random rd = state as Random;
            int val = rd.Next();
            if (val%100 < 20)
            {
                if (val%5 == 0)
                {
                    enemyList.Add(new Enemy(val%209,-72,speed/3,true,val%2==0, rd));
                }
                else
                {
                    enemyList.Add(new Enemy(val%232,-48,speed/2,false,val%2==0, rd));
                }
            }

        }
        public void ShowAboutDialog()
        {
            if (!canOpen) return;
            if (CheckFormIsOpen("关于作者") == false)
            {
                AboutDialog aboutDialog = new AboutDialog();
                aboutDialog.Show();
                canOpen = false;
            }
        }
        private bool CheckFormIsOpen(string Forms)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Text == Forms)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }
    }
}
