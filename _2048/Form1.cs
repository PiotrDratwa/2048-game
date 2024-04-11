using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
  public partial class Form1 : Form
  {
        private List<List<int>> matrix = InitMatrix();
        public Form1()
        {
            InitializeComponent();
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
            switch (e.KeyChar)
            {
                case 'w':
                    ShiftVertical(true);
                    AddRandomNums();
                    update_UI();
                    LoseCheck();
                    break;
                case 'a':
                    AddRandomNums();
                    LoseCheck();
                    break;
                case 's':
                    ShiftVertical(false);
                    AddRandomNums();
                    update_UI();
                    LoseCheck();
                    break;
                case 'd':
                    AddRandomNums();
                    LoseCheck();
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

        private bool LoseCheck()
        {
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
                return true;
            }

            return false;
        }

        private void DebugLabel_Click(object sender, EventArgs e)
        {
            DebugLabel.Text = "";
        }

        private void ShiftVertical(bool up)
        {
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
                        matrix[x][y] = 0;
                        matrix[x - 1][y] = val;
                        x--;
                        if (x == 0) {break;}
                    }
                    
                    if (x == 0) {break;}
                    if (matrix[x - 1][y] == val)
                    {
                        matrix[x][y] = 0;
                        matrix[x - 1][y] *= val;
                    }
                }
            }
            else
            {
                for (int i = 12; i > 0; i--)
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
                        matrix[x][y] = 0;
                        matrix[x + 1][y] = val;
                        x++;
                        if (x == 3)
                        {
                            break;
                        }
                    }

                    if (matrix[x + 1][y] == val)
                    {
                        matrix[x][y] = 0;
                        matrix[x + 1][y] *= val;
                    }
                }
            }
        }

        private void update_UI()
        {
            Dictionary<int, Label> dict = InitDict();
            int x;
            int y;
            for (int i = 0; i < 16; i ++ )
            {
                x = Convert.ToInt32(Math.Floor((double)i / 4.00));
                y = i % 4;
                dict[i].Text = matrix[x][y].ToString();
            }
        }
  }
}