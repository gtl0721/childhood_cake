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
    public partial class childhood : Form
    {
        public int GBoxN = 0;
        
        public childhood()
        {
            InitializeComponent();
            InitializeVariable();
        }
        private void InitializeVariable()
        {
            Load += new System.EventHandler(this.Main_load);
            Closing += new CancelEventHandler(Main_close);
        }
        private void Main_close(object sender, CancelEventArgs e)
        {
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
        }
        private void CreateGBox(string text)//Create GroupBox
        {
            #region GroupBox
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
            #endregion

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
            bool check_flg = false;
            if (GBoxN == 0) { MessageBox.Show("都還沒加材料，急啥??", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else 
            {
                for (int i = 0; i < GBoxN; i++) 
                {
                    GroupBox GB = (GroupBox)Controls["GBox" + i];
                    TextBox TxtB3 = (TextBox)GB.Controls["T" + i + "3"];
                    if (TxtB3.Text == "") { MessageBox.Show("你確定不用成本??", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); check_flg = true; break; }
                    sum += Convert.ToInt64(TxtB3.Text);
                }
                if (check_flg == false)
                {
                    textBox1.Text = Convert.ToString(sum);
                    textBox3.Text = Convert.ToString(sum + 7000);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sale_calculation sale = new sale_calculation();
            sale.Visible = true;
            this.Visible = false;
        }
    }
}
