using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_calculate
{
    public partial class Form3 : Form
    {
        bool Point = true;                              //用于检验是否能输入小数点
        int LocofPoint = 0;                             //用于记录小数点位置
        double weight = 0.0;                                 //单位转换的权值
        double result = 0.0;                                //用于记录单位转换的结果

        public Form3()
        {
            InitializeComponent();
            this.tabCtrl1.SelectedIndex = 0;
            this.btnSign.Enabled = false;
            this.cb1.SelectedIndex = 0;
            this.cb2.SelectedIndex = 0;
            this.cb4.SelectedIndex = 0;
            this.cb6.SelectedIndex = 0;
        }

        private void btn1_Click(object sender, EventArgs e)//数字键1
        {
            if (lbl1.Text == "0")
                lbl1.Text = "1";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-1";
            else
                lbl1.Text += '1';
        }

        private void btn2_Click(object sender, EventArgs e)//数字键2
        {
            if (lbl1.Text == "0")
                lbl1.Text = "2";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-2";
            else
                lbl1.Text += '2';
        }

        private void btn3_Click(object sender, EventArgs e)//数字键3
        {
            if (lbl1.Text == "0")
                lbl1.Text = "3";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-3";
            else
                lbl1.Text += '3';
        }

        private void btn4_Click(object sender, EventArgs e)//数字键4
        {
            if (lbl1.Text == "0")
                lbl1.Text = "4";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-4";
            else
                lbl1.Text += '4';
        }

        private void btn5_Click(object sender, EventArgs e)//数字键5
        {
            if (lbl1.Text == "0")
                lbl1.Text = "5";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-5";
            else
                lbl1.Text += '5';
        }

        private void btn6_Click(object sender, EventArgs e)//数字键6
        {
            if (lbl1.Text == "0")
                lbl1.Text = "6";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-6";
            else
                lbl1.Text += '6';
        }

        private void btn7_Click(object sender, EventArgs e)//数字键7
        {
            if (lbl1.Text == "0")
                lbl1.Text = "7";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-7";
            else
                lbl1.Text += '7';
        }

        private void btn8_Click(object sender, EventArgs e)//数字键8
        {
            if (lbl1.Text == "0")
                lbl1.Text = "8";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-8";
            else
                lbl1.Text += '8';
        }

        private void btn9_Click(object sender, EventArgs e)//数字键9
        {
            if (lbl1.Text == "0")
                lbl1.Text = "9";
            else if (lbl1.Text == "-0")
                lbl1.Text = "-9";
            else
                lbl1.Text += '9';
        }

        private void btn0_Click(object sender, EventArgs e)//数字键0
        {
            if (lbl1.Text != "0" && lbl1.Text != "-0")
                lbl1.Text += '0';
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            lbl1.Text = "0";
            Point = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int x = lbl1.Text.Length;
            if (x > 0)
            {
                if (x == 1)
                    lbl1.Text = "0";
                else
                {
                    lbl1.Text = lbl1.Text.Substring(0, x - 1).Trim();
                    if (x-1 == LocofPoint)
                        Point = true;
                }
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (String.Equals(lbl1.Text.Substring(0,1),"-"))
                lbl1.Text = lbl1.Text.Substring(1, lbl1.Text.Length-1);
            else
                lbl1.Text = '-' + lbl1.Text;
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (Point)
            {
                lbl1.Text += '.';
                LocofPoint = lbl1.Text.Length - 1;
                Point = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UnitConvert1();
        }

        private void lbl1_TextChanged(object sender, EventArgs e)
        {
            this.UnitConvert1();
            this.UnitConvert2();
            switch (this.tabCtrl1.SelectedIndex)
            {
                case 0: lbl2.Text = System.Convert.ToString(result); break;
                case 1: lbl4.Text = System.Convert.ToString(result); break;
                case 2: lbl6.Text = System.Convert.ToString(result); break;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                this.UnitConvert1();
                this.UnitConvert2();
                lbl2.Text = System.Convert.ToString(result);
            
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UnitConvert1();
            this.UnitConvert2();
            lbl4.Text = System.Convert.ToString(result);

        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UnitConvert1();
            this.UnitConvert2();
            lbl6.Text = System.Convert.ToString(result);

        }
        private void UnitConvert1()                         //将输入转换为权值
        {
            if (this.tabCtrl1.SelectedIndex == 0)
            {
                string value = this.cb1.SelectedItem.ToString();
                string s = lbl1.Text.Substring(0, lbl1.Text.Length);
                double i = System.Convert.ToDouble(s);
                switch (value)
                {
                    case "微秒": weight = i / 1000000; break;
                    case "毫秒": weight = i / 1000; break;
                    case "秒": weight = i; break;
                    case "分钟": weight = 60 * i; break;
                    case "小时": weight = 3600 * i; break;
                    case "天": weight = 86400 * i; break;
                    case "周": weight = 604800 * i; break;
                    case "年": weight = 31557600 * i; break;
                }
            }
            else if (this.tabCtrl1.SelectedIndex == 1)
            {
                string value = this.cb1.SelectedItem.ToString();
                string s = lbl1.Text.Substring(0, lbl1.Text.Length);
                double i = System.Convert.ToDouble(s);
                switch (value)
                {
                    case "克拉": weight = i / 5000; break;
                    case "毫克": weight = i / 1000000; break;
                    case "克": weight = i / 1000; break;
                    case "千克": weight = i; break;
                    case "公吨": weight = 1000 * i; break;
                    case "磅": weight = 2.204623 * i; break;
                    case "英石": weight = i / 0.157473; break;
                }
            }
            else if (this.tabCtrl1.SelectedIndex == 2)
            {
                string value = this.cb1.SelectedItem.ToString();
                string s = lbl1.Text.Substring(0, lbl1.Text.Length);
                double i = System.Convert.ToDouble(s);
                switch (value)
                {
                    case "度": weight = i; break;
                    case "弧度": weight = i * 57.3; break;
                    case "梯度": weight = i * 63.66; break;
                }
            }
        }
        private void UnitConvert2()                        //将权值换算为输出值
        {
            double i = 0.0;
            if (this.tabCtrl1.SelectedIndex == 0)
            {
                string value = this.cb2.SelectedItem.ToString();
                switch (value)
                {
                    case "微秒": i = weight * 1000000; break;
                    case "毫秒": i = weight * 1000; break;
                    case "秒": i = weight; break;
                    case "分钟": i = weight / 60; break;
                    case "小时": i = weight / 3600; break;
                    case "天": i = weight / 86400; break;
                    case "周": i = weight / 604800; break;
                    case "年": i = weight / 31557600; break;
                }
            }
            else if (this.tabCtrl1.SelectedIndex == 1)
            {
                string value = this.cb4.SelectedItem.ToString();
                switch (value)
                {
                    case "克拉": i = weight * 5000; break;
                    case "毫克": i = weight * 1000000; break;
                    case "克": i = weight * 1000; break;
                    case "千克": i = weight; break;
                    case "公吨": i = weight / 1000; break;
                    case "磅": i = weight / 2.204623; break;
                    case "英石": i = weight * 0.157473; break;
                }
            }
            else if (this.tabCtrl1.SelectedIndex == 2)
            {
                string value = this.cb6.SelectedItem.ToString();
                switch (value)
                {
                    case "度": i = weight; break;
                    case "弧度": i = weight / 57.3; break;
                    case "梯度": i = weight / 63.66; break;
                }
            }
            result = i;
        }
        private void tabCtrl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = this.tabCtrl1.SelectedIndex;
            switch (value)
            {
                case 0:
                    this.btnSign.Enabled = false;
                    this.cb1.Items.Clear();
                    this.cb1.Items.Add("微秒");
                    this.cb1.Items.Add("毫秒");
                    this.cb1.Items.Add("秒");
                    this.cb1.Items.Add("分钟");
                    this.cb1.Items.Add("小时");
                    this.cb1.Items.Add("天");
                    this.cb1.Items.Add("周");
                    this.cb1.Items.Add("年");
                    this.cb1.SelectedIndex = 0;
                    break;
                case 1:
                    this.btnSign.Enabled = false;
                    this.cb1.Items.Clear();
                    this.cb1.Items.Add("克拉");
                    this.cb1.Items.Add("毫克");
                    this.cb1.Items.Add("克");
                    this.cb1.Items.Add("千克");
                    this.cb1.Items.Add("公吨");
                    this.cb1.Items.Add("磅");
                    this.cb1.Items.Add("英石");
                    this.cb1.SelectedIndex = 0;
                    break;
                case 2:
                    this.btnSign.Enabled = true;
                    this.cb1.Items.Clear();
                    this.cb1.Items.Add("度");
                    this.cb1.Items.Add("弧度");
                    this.cb1.Items.Add("梯度");
                    this.cb1.SelectedIndex = 0;
                    break;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 标准型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm1 = new Form1();
            fm1.ShowDialog();
            this.Close();
        }

        private void 数制转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm2 = new Form2();
            fm2.ShowDialog();
            this.Close();
        }

        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Designed by 502ers.");
        }





        

    }

}
