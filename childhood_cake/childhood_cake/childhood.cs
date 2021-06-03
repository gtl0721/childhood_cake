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
    public partial class childhood : Form
    {
        public int GBoxN = 0;
        public List<string> material_list = new List<string>();
        public string BasicPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        public childhood()
        {
            InitializeComponent();
            InitializeVariable();
            TimerStrat();
        }
        private void InitializeVariable()
        {
            Load += new System.EventHandler(this.Main_load);
            Closing += new CancelEventHandler(Main_close);
        }
        private void Main_close(object sender, CancelEventArgs e)
        {
            Timer1.Stop();
            Application.ExitThread();
            this.Close();
            Environment.Exit(Environment.ExitCode);
        }
        private void Main_load(object sender, System.EventArgs e)
        {
            this.Width = 840;
            this.Height = 570;
            groupBox1.Width = 800;
            groupBox1.Height = 100;
            Timer1.Start();
        }
        private void TimerStrat() 
        {
            var Timer1 = new System.Timers.Timer(100);
            Timer1.Elapsed += Timer1_Tick;
        }
        public delegate void Timer1_Tick_t(object sender, EventArgs e);
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (textBox4.InvokeRequired) { Timer1_Tick_t d = new Timer1_Tick_t(Timer1_Tick); Invoke(d, sender, e); }
            else { textBox4.Text = DateTime.Now.ToString(); }
        }
        private void CreateGBox(string text)//Create GroupBox
        {
            GroupBox Gbox = new GroupBox();
            Gbox.Name = "GBox" + GBoxN;
            Gbox.Text = text + "-" + (GBoxN + 1);
            Gbox.Width = 200;
            Gbox.Height = 170;
            if (GBoxN < 4)
            {
                int row = GBoxN / 4;
                Gbox.Left = GBoxN * 200 + 10;
                Gbox.Top = row * 200 + 120;
            }
            else if (GBoxN < 8)
            {
                int list = GBoxN - 4;
                int row = GBoxN / 4;
                Gbox.Left = list * 200 + 10;
                Gbox.Top = row * 200 + 120;
                this.Width = 840;
                this.Height = 570;
            }
            else if (GBoxN < 12)
            {
                int list = GBoxN - 8;
                int row = GBoxN / 4;
                Gbox.Left = list * 200 + 10;
                Gbox.Top = row * 200 + 120;
                this.Width = 840;
                this.Height = 770;
            }
            else
            {
                MessageBox.Show("太多材料啦!!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Controls.Add(Gbox);
            CreateControls(Gbox);

            GBoxN++;
        }
        private void CreateControls(GroupBox gBox)
        {
            for (int i = 0; i < 5; i++)
            {
                #region Label
                Label LB = new Label();
                LB.Name = "LB" + GBoxN + i;
                switch (i)
                {
                    case 0:
                        LB.Text = "材料名稱:";
                        break;
                    case 1:
                        LB.Text = "材料單價:";
                        break;
                    case 2:
                        LB.Text = "材料數量:";
                        break;
                    case 3:
                        LB.Text = "材料總價:";
                        break;
                    case 4:
                        LB.Text = "其它說明:";
                        break;
                }

                LB.Width = 70;
                LB.Height = 12;
                LB.Left = 20;
                LB.Top = i * 30 + 20;
                gBox.Controls.Add(LB);
                #endregion

                #region TextBox
                TextBox Txt = new TextBox();
                Txt.Name = "T" + GBoxN + i;
                Txt.Width = 100;
                Txt.Height = 12;
                Txt.Left = 90;
                Txt.Top = i * 30 + 16;
                if (i == 0)
                {
                    Txt.Text = "材料" + (GBoxN + 1);
                }
                if (i == 4)
                {
                    Txt.BackColor = Color.LightBlue;
                }
                if (i == 1 || i == 2 || i == 3)
                {
                    Txt.BackColor = Color.Gold;
                    Txt.Text = "0";
                }
                gBox.Controls.Add(Txt);
                #endregion
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateGBox("材料");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long sum = 0;
            bool check_flg = true;
            int TxtB1_int = 0;
            int TxtB2_int = 0;
            string[] B = new string[5];
            if (GBoxN == 0) { MessageBox.Show("老闆，請先加材料喔~", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else 
            {
                for (int i = 0; i < GBoxN; i++) 
                {
                    GroupBox GB = (GroupBox)Controls["GBox" + i];
                    TextBox TxtB0 = (TextBox)GB.Controls["T" + i + "0"]; //名稱
                    TextBox TxtB1 = (TextBox)GB.Controls["T" + i + "1"]; //單價
                    TextBox TxtB2 = (TextBox)GB.Controls["T" + i + "2"]; //數量
                    TextBox TxtB3 = (TextBox)GB.Controls["T" + i + "3"]; //總價
                    TextBox TxtB4 = (TextBox)GB.Controls["T" + i + "4"]; //說明
                    TxtB1_int = Convert.ToInt32(TxtB1.Text);                  
                    TxtB2_int = Convert.ToInt32(TxtB2.Text);
                    
                    if (TxtB1_int != 0 && TxtB2_int != 0)
                    {
                        TxtB3.Text = Convert.ToString(TxtB1_int * TxtB2_int);
                        sum += Convert.ToInt64(TxtB3.Text);
                        B[0] = "材料 = " + TxtB0.Text;
                        B[1] = "單價 = " + TxtB1.Text;
                        B[2] = "數量 = " + TxtB2.Text;
                        B[3] = "總價 = " + TxtB3.Text;
                        B[4] = "說明 = " + TxtB4.Text;
                        for (int j = 0; j < 5; j++) 
                        {
                            material_list.Add(B[j]);
                        }
                    }
                    else { MessageBox.Show("老闆，第" + (i + 1) + "筆材料有內容為0喔!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); check_flg = false; }
                }
                if (check_flg == true)
                {
                    textBox1.Text = Convert.ToString(sum);
                    textBox3.Text = Convert.ToString(sum + Convert.ToInt16(textBox2.Text));
                    button3.Enabled = true;
                }
                else { return; }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sale_calculation sale = new sale_calculation();
            sale.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try 
            {
                string datapath = BasicPath + "\\" + DateTime.Now.ToShortDateString();
                DirectoryInfo new_file = Directory.CreateDirectory(datapath);
                using (StreamWriter sw = new StreamWriter(datapath + "\\" + "成本紀錄.txt", false, Encoding.Unicode))
                {
                    for (int i = 0; i < material_list.Count; i++)
                    {
                        string count = material_list[i];
                        sw.WriteLine(count);
                        if ((i + 1) % 5 == 0 && i != 0) { sw.WriteLine(""); }
                    }
                    sw.WriteLine("成本 = " + textBox1.Text);
                    sw.WriteLine("租金 = " + textBox2.Text);
                    sw.WriteLine("總成本 = " + textBox3.Text);
                }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, "Communication", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            MessageBox.Show(this, "資料存在桌面囉!", "Communication", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GroupBox Gbox = new GroupBox();
            Gbox.Controls.Remove(Gbox);
            Gbox.Dispose();
        }
    }
}
