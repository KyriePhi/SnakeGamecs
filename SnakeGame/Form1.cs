using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
          /* Designing and Programming :     */
         /*  Mehmet Berat ŞENEL             */
        /*   This project under cc license */
        public Form1()
        {
            InitializeComponent();
        }
        private static bool movexp=false, moveyp=false, movexn=false, moveyn = false;
        private static int Length;
        private static int Score;
        private static Panel[] panels= new Panel[100];
        private static int Foodx,Foody;
        private static Panel Food = new Panel();
        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            for (int i = 0; i < Length; i++)
            {
                panel1.Controls.Add(panels[i]);
            }
            NextLevel();
            FoodCreator();
            for (int i = Length; i > 1; i--)
            {
                Panel panell = panels[i - 1];
                Panel panelll = panels[i - 2];
                panell.Location = new System.Drawing.Point(panelll.Location.X, panelll.Location.Y);
            }
            if (moveyn)
            {
                Panel panel = panels[0];
                panel.Location = new System.Drawing.Point(panel.Location.X, panel.Location.Y + 15);
                panels[0] = panel;
            }
            else if (moveyp)
            {
                Panel panel = panels[0];
                panel.Location = new System.Drawing.Point(panel.Location.X, panel.Location.Y - 15);
                panels[0] = panel;
            }
            else if (movexp)
            {
                Panel panel = panels[0];
                panel.Location = new System.Drawing.Point(panel.Location.X + 15, panel.Location.Y);
                panels[0] = panel;
            }
            else if (movexn)
            {
                Panel panel = panels[0];
                panel.Location = new System.Drawing.Point(panel.Location.X - 15, panel.Location.Y);
                panels[0] = panel;
            }
        }
        private void SnakeBody(int name,int locationx,int locationy)
        {
            Panel panel = new Panel();
            panel.BackgroundImage = global::SnakeGame.Properties.Resources.body;
            panel.Location = new System.Drawing.Point(locationx, locationy);
            panel.Name = name.ToString();
            panel.Size = new System.Drawing.Size(10, 10);
            panel1.Controls.Add(panel);
            panels[name] = panel;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            panel1.Controls.Clear();
            Random rnd = new Random();
            int locx = rnd.Next(10, 410);
            int locy = rnd.Next(10, 410);
            Foodx = rnd.Next(10, 410);
            Foody = rnd.Next(10, 410);
            SnakeBody(0,locx,locy);
            Length = 1;
            FoodCreator();
            moveyp = false;
            moveyn = false;
            movexp = false;
            movexn = false;
            timer1.Start();
            Score = 0;
            label2.Text = Score.ToString();
            label3.Text = Length.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
            {
                moveyp = true;
                moveyn = false;
                movexp = false;
                movexn = false;
            }
            else if (e.KeyCode == Keys.D)
            {
                movexp = true;
                moveyn = false;
                moveyp = false;
                movexn = false;
            }
            else if (e.KeyCode == Keys.S)
            {
                moveyn = true;
                moveyp = false;
                movexp = false;
                movexn = false;
            }
            else if (e.KeyCode == Keys.A)
            {
                movexn = true;
                moveyn = false;
                movexp = false;
                moveyp = false;
            }
        }
        private void FoodCreator()
        {
            Food.BackColor = Color.IndianRed;
            Food.Location = new System.Drawing.Point(Foodx, Foody);
            Food.Name = "Food";
            Food.Size = new System.Drawing.Size(5, 5);
            panel1.Controls.Add(Food);
        }
        private void NextLevel()
        {
            Panel panel = panels[0];
            if (( panel.Location.X >= Foodx-9 ) & ( panel.Location.X <= Foodx+8))
            {
                if (( panel.Location.Y >= Foody-8) & ( panel.Location.Y <= Foody + 9))
                {
                    Random rnd = new Random();
                    Foodx = rnd.Next(10, 410);
                    Foody = rnd.Next(10, 410);
                    if (movexp)
                    {
                        SnakeBody(Length, panel.Location.X-(15*Length), panel.Location.Y);
                    }
                    else if (movexn)
                    {
                        SnakeBody(Length, panel.Location.X + (15 * Length), panel.Location.Y);
                    }
                    else if (moveyp)
                    {
                        SnakeBody(Length, panel.Location.X, panel.Location.Y - (15 * Length));
                    }
                    else if (moveyn)
                    {
                        SnakeBody(Length, panel.Location.X, panel.Location.Y + (15 * Length));
                    }
                    Length += 1;
                    Score += Length * 10;
                    label2.Text = Score.ToString();
                    label3.Text = Length.ToString();
                }
            }
        }
    }
}
