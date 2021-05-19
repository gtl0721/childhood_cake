using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace childhood_cake
{
    public partial class sale_calculation : Form
    {
        int orangle = 0;
        int custard = 0;
        int cheese = 0;
        int brownsugar = 0;
        public sale_calculation()
        {
            InitializeComponent();
            InitializeVariable();
        }
        private void InitializeVariable()
        {
            Load += new System.EventHandler(this.Main_load);
            Closing += new CancelEventHandler(Main_close);
        }
        public delegate void AddStatusText_t(string text, Color col);
        public void AddStatusText(string text, Color col)
        {
            if (MsgBox.InvokeRequired)
            {
                AddStatusText_t d = new AddStatusText_t(AddStatusText);
                Invoke(d, text, col);
            }
            else
            {
                MsgBox.ScrollToCaret();
                MsgBox.Select(MsgBox.TextLength, 1);
                MsgBox.SelectionColor = col;
                MsgBox.DeselectAll();
                MsgBox.AppendText(text + "\r\n");
            }
        }
        private void Main_close(object sender, CancelEventArgs e)
        {
            Application.ExitThread();
            this.Close();
            Environment.Exit(Environment.ExitCode);
        }
        private void Main_load(object sender, System.EventArgs e)
        {
            this.Width = 500;
            this.Height = 520;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            childhood main = new childhood();
            main.Visible = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            orangle += 1;
            textBox1.Text = Convert.ToString(orangle);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            orangle -= 1;
            textBox1.Text = Convert.ToString(orangle);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            custard += 1;
            textBox3.Text = Convert.ToString(custard);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            custard -= 1;
            textBox3.Text = Convert.ToString(custard);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cheese += 1;
            textBox2.Text = Convert.ToString(cheese);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cheese -= 1;
            textBox2.Text = Convert.ToString(cheese);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            brownsugar += 1;
            textBox4.Text = Convert.ToString(brownsugar);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            brownsugar -= 1;
            textBox4.Text = Convert.ToString(brownsugar);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int allnum = 0;
            int money;
            AddStatusText("現在是" + DateTime.Now.ToShortDateString() + DateTime.Now.ToString(" HH:mm ss tt"), Color.Blue);
            AddStatusText("我檢查一下你今天的業績......", Color.Blue);
            Thread.Sleep(200);
            allnum = orangle + custard + cheese + brownsugar;
            if (allnum < 10) { AddStatusText("今天生意不太好喔!!!", Color.Green); }
            else if (allnum < 20) { AddStatusText("可能才差不多打平喔!!!", Color.Green); }
            else { AddStatusText("WOW!!!今天很厲害喔!!!", Color.Green); }
            money = orangle * 40 + (custard + cheese + brownsugar) * 50;
            AddStatusText("今天賺了......", Color.Blue);
            AddStatusText("總共" + Convert.ToString(money) + "元", Color.Green);
        }
    }
}
