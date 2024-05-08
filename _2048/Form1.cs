using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace _2048
{
  public partial class Form1 : Form
  {
        private List<List<int>> matrix = InitMatrix();
        private int difficulty = 1;
        private static System.Timers.Timer aTimer = new System.Timers.Timer();
        private static bool lose;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            AddRandomNums();
            update_UI();
        }
        
        private static List<List<int>> InitMatrix()
        {
            List<List<int>> matrix = new List<List<int>>(4);
            for (int i = 0; i < 4; i++)
            {
                
                matrix.Add(new List<int>{0,0,0,0}); 
            }
            return matrix; 
        }
        
        private Dictionary<int, Label> InitDict()
        {
            Dictionary<int, Label> dict = new Dictionary<int, Label>();
            
            dict.Add(0, label1);
            dict.Add(1, label2);
            dict.Add(2, label3);
            dict.Add(3, label4);
            dict.Add(4, label5);
            dict.Add(5, label6);
            dict.Add(6, label7);
            dict.Add(7, label8);
            dict.Add(8, label9);
            dict.Add(9, label10);
            dict.Add(10, label11);
            dict.Add(11, label12);
            dict.Add(12, label13);
            dict.Add(13, label14);
            dict.Add(14, label15);
            dict.Add(15, label16);
            
            return dict; 
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoseLabel.Text = "";
            lose = LoseCheck(lose);
            //if (lose) {return;}
            aTimer.Stop();
            aTimer.Elapsed += OnTimedEvent;
            if (difficulty == 1)
            {
                aTimer.Interval = 5000;
            } else if (difficulty == 2)
            {
                aTimer.Interval = 2000;
            }
            aTimer.Start();
            lose = false;
            bool moved;
            switch (e.KeyChar)
            {
                case 'w':
                    DebugLabel.Text = "hahaha";
                    moved = ShiftVertical(true);
                    if(moved)
                    {
                        AddRandomNums();
                    }
                    update_UI();
                    LoseCheck(lose);
                    break;
                case 'a':
                    moved = ShiftHorizontal(false);
                    if(moved)
                    {
                        AddRandomNums();
                    }
                    update_UI();
                    lose = LoseCheck(lose);
                    break;
                case 's':
                    moved = ShiftVertical(false);
                    if(moved)
                    {
                        AddRandomNums();
                    }
                    update_UI();
                    LoseCheck(lose);
                    break;
                case 'd':
                    moved = ShiftHorizontal(true);
                    if(moved)
                    {
                        AddRandomNums();
                    }
                    update_UI();
                    LoseCheck(lose);
                    break;
            }
            
        }

        private void AddRandomNums()
        {
            Dictionary<int, Label> dict = InitDict();
            Random rnd = new Random();
            int x = rnd.Next(4);
            int y = rnd.Next(4);
            
            try
            {
                while (matrix[x][y] != 0)
                {
                    x = rnd.Next(4);
                    y = rnd.Next(4);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                DebugLabel.Text += $"x = {x}\ny = {y}";
                throw;
            }

            matrix[x][y] = 2;
            Label value;
            dict[(x*4+y)].Text = "2";
        }

        private bool LoseCheck(bool lose)
        {
            if (lose)
            { reset(true);return true;}
            int count = 4;
            for (int i = 0; i < 4; i++)
            {
                if (matrix[i].Contains(0) == false)
                {
                    count--;
                }
            }

            if (count == 0)
            {
                int x;
                int y;
                for (int i = 0; i < 16; i++)
                {
                    x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                    y = i % 4;
                    if (y == 3){continue;}
                    
                    if (matrix[x][y] == matrix[x][y + 1])
                    {
                        return false;
                    }
                }
                for (int i = 0; i < 12; i++)
                {
                    x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                    y = i % 4;
                    
                    if (matrix[x][y] == matrix[x+1][y])
                    {
                        return false;
                    }
                }

                reset(true);
                return true;
            }
            
            return false;
        }

        private void DebugLabel_Click(object sender, EventArgs e)
        {
            DebugLabel.Text = "";
        }
        
        private bool ShiftVertical(bool up)
        {
            bool moved = false;
            int x;
            int y;
            int val;
            if (up)
            {
                for (int i = 4; i < 16; i++)
                {
                    x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                    y = i % 4;
                    val = matrix[x][y];
                    if (val == 0)
                    {
                        continue;
                    }
                    
                    while (matrix[x - 1][y] == 0)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x - 1][y] = val;
                        x--;
                        if (x == 0) {break;}
                    }
                    
                    if (x == 0) {continue;}
                    if (matrix[x - 1][y] == val)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x - 1][y] += val;
                    }
                }
            }
            else
            {
                for (int i = 11; i > -1; i--)
                {
                    x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                    y = i % 4;
                    val = matrix[x][y];
                    if (val == 0)
                    {
                        continue;
                    }
                    
                    while (matrix[x + 1][y] == 0)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x + 1][y] = val;
                        x++;
                        if (x == 3)
                        {
                            break;
                        }
                    }
                    
                    if (x == 3) {continue;}
                    if (matrix[x + 1][y] == val)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x + 1][y] += val;
                    }
                }
            }

            return moved;
        }

        private bool ShiftHorizontal(bool right)
        {
            bool moved = false;
            int x;
            int y;
            int val;
            if (right)
            {
                for (int i = 15; i > -1; i--)
                {
                    x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                    y = i % 4;
                    val = matrix[x][y];
                    if (val == 0||y == 3)
                    {
                        continue;
                    }
                    
                    while (matrix[x][y+1] == 0)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x][y+1] = val;
                        y++;
                        if (y == 3) {break;}
                    }
                    
                    if (y == 3) {continue;}
                    if (matrix[x][y+1] == val)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x][y+1] += val;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                    y = i % 4;
                    val = matrix[x][y];
                    if (val == 0||y == 0)
                    {
                        continue;
                    }
                    
                    while (matrix[x][y-1] == 0)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x][y-1] = val;
                        y--;
                        if (y == 0)
                        {
                            break;
                        }
                    }
                    
                    if (y == 0) {continue;}
                    if (matrix[x][y-1] == val)
                    {
                        moved = true;
                        matrix[x][y] = 0;
                        matrix[x][y-1] += val;
                    }
                }
            }

            return moved;
        }
        
        private void update_UI()
        {
            Dictionary<int, Label> dict = InitDict();
            int x;
            int y;
            int score = 0;
            for (int i = 0; i < 16; i ++ )
            {
                x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                y = i % 4;
                
                score += matrix[x][y];
                dict[i].Text = matrix[x][y].ToString();
                switch (matrix[x][y])
                {
                    case 0: dict[i].BackColor = Color.DarkGray;
                        break;
                    case 2: dict[i].BackColor = Color.Khaki;
                        break;
                    case 4: dict[i].BackColor = Color.Aquamarine;
                        break;
                    case 8: dict[i].BackColor = Color.Aqua;
                        break;
                    case 16: dict[i].BackColor = Color.Teal;
                        break;
                    case 32: dict[i].BackColor = Color.Coral;
                        break;
                    case 64: dict[i].BackColor = Color.Chocolate;
                        break;
                    case 128: dict[i].BackColor = Color.OrangeRed;
                        break;
                    case 256: dict[i].BackColor = Color.Firebrick;
                        break;
                    case 512: dict[i].BackColor = Color.DarkOrchid;
                        break;
                    case 1024: dict[i].BackColor = Color.Purple;
                        break;
                    case 2048: dict[i].BackColor = Color.Indigo;
                        break;
                }
            }

            DebugLabel.Text = $"Score: {score.ToString()}";
        }
        
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            lose = true;
        }

        private void easybutton_Click(object sender, EventArgs e)
        {
            difficulty = 0;
        }

        private void mediumbutton_Click(object sender, EventArgs e)
        {
            difficulty = 1;
        }

        private void hardbutton_Click(object sender, EventArgs e)
        {
            difficulty = 2;
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            reset(false);
        }

        private void reset(bool message)
        {
            if (message)
            {
                LoseLabel.Text = "You lost";
            }
            matrix = InitMatrix();
            AddRandomNums();
            update_UI();
        }
        
  }
}