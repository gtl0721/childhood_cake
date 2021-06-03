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
using System.IO;

namespace childhood_cake
{
    public partial class sale_calculation : Form
    {
        childhood child = new childhood();
        public int orangle = 0;
        public int custard = 0;
        public int cheese = 0;
        public int brownsugar = 0;
        public List<string> sale_list = new List<string>();

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
            if (orangle - 1 < 0) { orangle = 0; }
            else{orangle -= 1;}          
            textBox1.Text = Convert.ToString(orangle);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            custard += 1;
            textBox3.Text = Convert.ToString(custard);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (custard - 1 < 0) { custard = 0; }
            else { custard -= 1;  }        
            textBox3.Text = Convert.ToString(custard);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cheese += 1;
            textBox2.Text = Convert.ToString(cheese);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (cheese - 1 < 0) { cheese = 0; }
            else { cheese -= 1; }
            textBox2.Text = Convert.ToString(cheese);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            brownsugar += 1;
            textBox4.Text = Convert.ToString(brownsugar);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (brownsugar - 1 < 0) { brownsugar = 0; }
            else { brownsugar -= 1; }
            textBox4.Text = Convert.ToString(brownsugar);
        }

        private void button10_Click(object sender, EventArgs e)
        {         
            int money = 0;
            int allnum = 0;
            string[] salenum = new string[5];
            AddStatusText("現在是" + DateTime.Now.ToShortDateString() + DateTime.Now.ToString(" HH:mm ss"), Color.Blue);
            AddStatusText("我檢查一下你今天的業績......", Color.Blue);
            Thread.Sleep(200);
            allnum = orangle + custard + cheese + brownsugar;
            if (allnum < 10) { AddStatusText("今天生意不太好喔!!!", Color.Green); }
            else if (allnum < 20) { AddStatusText("可能才差不多打平喔!!!", Color.Green); }
            else { AddStatusText("WOW!!!今天很厲害喔!!!", Color.Green); }
            money = orangle * 40 + (custard + cheese + brownsugar) * 50;
            AddStatusText("今天賺了......", Color.Blue);
            AddStatusText("總共" + Convert.ToString(money) + "元", Color.Green);
            salenum[0] = "原味" + "     " + orangle + "   " + "40" + "   " + 40 * orangle;
            salenum[1] = "卡士達" + "   " + custard + "   " + "50" + "   " + 50 * orangle;
            salenum[2] = "起司" + "     " + cheese + "   " + "50" + "   " + 50 * orangle;
            salenum[3] = "黑糖麻吉" + " " + brownsugar + "   " + "50" + "   " + 50 * orangle;
            salenum[4] = "總營業額" + " " + money;
            for (int i = 0; i < 5; i++)
            {
                sale_list.Add(salenum[i]);
            }
            button11.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string datapath = child.BasicPath + "\\" + DateTime.Now.ToShortDateString();
                DirectoryInfo new_file = Directory.CreateDirectory(datapath);
                using (StreamWriter sw = new StreamWriter(datapath + "\\" + "銷貨紀錄.txt", false, Encoding.Unicode))
                {
                    sw.WriteLine("口味  數量  單價  總價");
                    for (int i = 0; i < sale_list.Count; i++)
                    {
                        string count = sale_list[i];
                        sw.WriteLine(count);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Communication", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            MessageBox.Show(this, "資料存在桌面囉!", "Communication", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
