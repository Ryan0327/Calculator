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
    /*struct inform
    {
        inform(int a)
        {
            leftbracket = a;
            rightbracket = a;
        }
        public int leftbracket;
        public int rightbracket;
    }*/
    public partial class Form2 : Form
    {
        private int preAnswer;                        //前一个答案
        bool error = false;                                //判是否错误
        int errorinfoLen = 0;
        int[] pos = new int[50];
        bool[] CanOperation = new bool[50];
        bool flag = false;
        int pos_x=0, pos_y=0;
        string t_s = string.Empty;
        char[] op = { '+', '-', '*', '/', '(', ')', '√', 'c', 's', 't', 'C', 'S', 'T', '^', 'L', 'l', 'x', '!', '=' };

        char[,] priority = 
     //    +   -   *   /   (   )  √   c   s   t   C   S   T   ^   L   l   x   !   =
        {{'>','>','<','<','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //+
         {'>','>','<','<','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //-
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //*
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    ///
         {'<','<','<','<','<','=','<','<','<','<','<','<','<','<','<','<','<','<','='},    //(
         {'>','>','>','>','>','0','>','>','>','>','>','>','>','>','>','>','>','>','>'},    //)
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //√
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //c
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //s
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //t
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //C
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //S
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //T
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //^
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //L
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //l
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //x
         {'>','>','>','>','<','>','<','<','<','<','<','<','<','<','<','<','<','<','>'},    //!
         {'<','<','<','<','<','<','<','<','<','<','<','<','<','<','<','<','<','<','='},    //=
        };
        public Form2()
        {
            InitializeComponent();
            this.Text = "Calculator";
        }  
        private double GetNumber(string Str, ref char ch, ref int curr, ref bool is_num)   //四个参数，得到的字符串，当前对应字符，当前位置（引用），字符串长度
        {
            int Len = Str.Length;
            char[] num = new char[50];          //暂时存放double数
            int i = 0;
            ch = Str[curr++];
            while (((ch <= '9' && ch >= '0') || ch == '.') && i < Len)
            {
                is_num = true;
                if (i >= 50)
                {
                    Show_Errorinfo("Number overflowed!");
                    return -1;
                }
                num[i++] = ch;
                ch = Str[curr++];
            }
            if (is_num)
            {
                string final = new string(num);
                return Convert.ToDouble(final);
            }
            return 0;
        }


        private string GetEnable_SubStr(ref bool Lastrow)                                  //选择当前执行行数代码
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            int Row = 0;
            pos[0] = 0;
            for (int i = 0; i < DisPlay.Text.Length; i++)
            {
                if (DisPlay.Text[i] == '\n')
                {
                    Row++;
                    pos[Row] = i + 1;
                }
            }
            if (RowIndex < Row)
            {
                string s = DisPlay.Text.Substring(pos[RowIndex], pos[RowIndex + 1] - pos[RowIndex] - 1);
                if (s[0] == '=')
                {
                    s = s.Substring(1);
                }
                return s;

            }
            else
            {
                int x = DisPlay.Text.Length - pos[RowIndex];
                Lastrow = true;
                string s = DisPlay.Text.Substring(pos[RowIndex], x);
                if (s[0] == '=')
                {
                    s = s.Substring(1);
                }
                return s;
            }
        }


        private int Cheak_Str(string S)                //检测得到的字符串是否合法
        {
            for (int i = 0; i < S.Length - 1; i++)
            {
                if (S[i] == '(' && S[i + 1] == ')')                   //括号中为空
                {
                    Show_Errorinfo("Error:bracket meaningless.");
                    return 0;
                }
                if ((S[i] == '+' || S[i] == '-' || S[i] == '*' || S[i] == '/') && (S[i + 1] == '+' || S[i + 1] == '*' || S[i + 1] == '/'))  //运算符后接运算符
                {
                    Show_Errorinfo("Error:too many Operator.");
                    return 0;
                }
                if (S[i] == 'T')
                {
                    if ((i + 2 < S.Length - 1) && S[i + 1] == 'a' && S[i + 2] == 'n')
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
                if (S[i] == 'C')
                {
                    if ((i + 2 < S.Length - 1) && S[i + 1] == 'o' && S[i + 2] == 's')
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
                if (S[i] == 'S')
                {
                    if ((i + 2 < S.Length - 1) && S[i + 1] == 'i' && S[i + 2] == 'n')
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
                if (S[i] == 'a')
                {
                    if ((i - 1 >= 0) && S[i - 1] == 'T' || (S[i + 1] == 'r'))
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
                if (S[i] == 'o')
                {
                    if ((i - 1 >= 0) && S[i - 1] == 'C')
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
                if (S[i] == 'i')
                {
                    if ((i - 1 >= 0) && S[i - 1] == 'S')
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
                if (S[i] == 'a')
                {
                    if ((i + 3 < S.Length - 1) && S[i + 1] == 'r' && S[i + 2] == 'c' && (S[i + 3] == 'S' || S[i + 3] == 'C' || S[i + 3] == 'T') || S[i + 1] == 'n')
                    { }
                    else
                    {
                        Show_Errorinfo("Calculation Error.");
                        return 0;
                    }
                }
            }
            return 1;
        }


        private string StrDespose(string Str)                                //对获得的字符串的处理
        {
            Str = Str.Replace("x√", "x");
            Str = Str.Replace("ln", "l");
            Str = Str.Replace("log", "L");
            Str = Str.Replace("arcCos", "C");
            Str = Str.Replace("arcSin", "S");
            Str = Str.Replace("arcTan", "T");
            Str = Str.Replace("Cos", "c");
            Str = Str.Replace("Sin", "s");
            Str = Str.Replace("Tan", "t");
            Str = Str.Replace("E", "*10^");

            int Len = Str.Length;
            for (int i = 0; i < Len - 1; i++)
            {
                if (Str[i] == '-' && (i == 0 || (Str[i - 1] > '9' || Str[i - 1] < '0')))
                {
                    Str = Str.Insert(i, "0");
                    i++;
                    Len++;
                }
                if ((Str[i] <= '9' && Str[i] >= '0') && (Str[i + 1] == '(' || Str[i + 1] == '√' || Str[i + 1] == 'π' || Str[i + 1] == 's' || Str[i + 1] == 'c' || Str[i + 1] == 't'))    //数字后接左括号，根号，pi,cos,sin,tan默认乘法
                {
                    Str = Str.Insert(i + 1, "*");
                    i++;
                    Len++;
                }
                if (Str[i] == ')' && (Str[i + 1] == '(' || Str[i + 1] == '√' || Str[i + 1] == 'π' || Str[i + 1] == 's' || Str[i + 1] == 'c' || Str[i + 1] == 't'))    //右括号后接左括号,根号,pi默认乘法
                {
                    Str = Str.Insert(i + 1, "*");
                    i++;
                    Len++;
                }
                if (Str[i] == ')' && (Str[i + 1] <= '9' && Str[i + 1] >= '0'))     //括号后接数字默认乘法
                {
                    Str = Str.Insert(i + 1, "*");
                    i++;
                    Len++;
                }
            }
            Str = Str.Replace("π", Math.PI.ToString());                           //pi的转换
            Str = Str.Replace("e", Math.E.ToString());
            return Str;
        }

        private double factorial(double n)
        {
            if (n == 1)
                return 1;
            if (n > 1)
            {
                double x = n * factorial(n - 1);
                if (x > double.MaxValue)
                {
                    Show_Errorinfo("Number overflowed!");
                    flag = true;
                    return 1;
                }
                return x;
            }
            return 1;
        }

        private bool is_integer(double a)
        {
            string s = a.ToString();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '.')
                {
                    for (int j = i; j < s.Length; j++)
                    {
                        if (s[j] != '0')
                            return false;
                    }
                }
            }
            return true;
        }

        public void setMouse()
        {
            DisPlay.Focus();//给richTextBox焦点
            DisPlay.Select(DisPlay.TextLength, 0);
        }
        private void Show_Errorinfo(string S)                      //显示错误信息
        {
            int pos = DisPlay.SelectionStart;
            DisPlay.Text = DisPlay.Text + '\n' + S;
            DisPlay.Select(pos, 0);
            DisPlay.Focus();
            error = true;
            errorinfoLen = S.Length;
        }
        private void ClearErrorinfo()                                    //任意键清除错误信息
        {
            int x = DisPlay.SelectionStart;
            errorinfoLen++;
            DisPlay.Text = DisPlay.Text.Substring(0, DisPlay.Text.Length - errorinfoLen);
            errorinfoLen = 0;
            error = false;
            DisPlay.Select(x, 0);                                      //重新选中
            DisPlay.Focus();
        }
        private void HEX_CheckedChanged(object sender, EventArgs e)                //进制 十六进制
        {
            this.btnExp.Enabled = false;
            this.btn9.Enabled = true;
            this.btn8.Enabled = true;
            this.btn7.Enabled = true;
            this.btn6.Enabled = true;
            this.btn5.Enabled = true;
            this.btn4.Enabled = true;
            this.btn3.Enabled = true;
            this.btn2.Enabled = true;
            this.A.Enabled = true;
            this.B.Enabled = true;
            this.C.Enabled = true;
            this.D.Enabled = true;
            this.E.Enabled = true;
            this.F.Enabled = true; 
            if (DisPlay.Text != "")
            {
                int d1;
                string d2;
                string s_txt = DisPlay.Text;
                string s1 = s_txt.Substring(0);
                if (this.OCT.Checked)//十六进制转换八进制
                {
                    int a = System.Convert.ToInt32(s1, 16);
                    d1 = a;
                    d2 = System.Convert.ToString(d1, 8);
                    DisPlay.Text = d2.ToString();
                }
                else if (this.DEC.Checked)//十六进制转换十进制
                {
                    int a = System.Convert.ToInt32(s1, 16);
                    d1 = a;
                    d2 = System.Convert.ToString(d1, 10);
                    DisPlay.Text = d2.ToString();
                }
                else if (this.BIN.Checked)//十六进制转换二进制
                {
                    int a = System.Convert.ToInt32(s1, 16);
                    d1 = a;
                    d2 = System.Convert.ToString(d1, 2);
                    DisPlay.Text = d2.ToString();
                }
                setMouse();
            }
        }
        private void DEC_CheckedChanged(object sender, EventArgs e)                   //进制 十进制
        {

            this.btnExp.Enabled = false;
            this.btn9.Enabled = true;
            this.btn8.Enabled = true;
            this.btn7.Enabled = true;
            this.btn6.Enabled = true;
            this.btn5.Enabled = true;
            this.btn4.Enabled = true;
            this.btn3.Enabled = true;
            this.btn2.Enabled = true;
            this.A.Enabled = false;
            this.B.Enabled = false;
            this.C.Enabled = false;
            this.D.Enabled = false;
            this.E.Enabled = false;
            this.F.Enabled = false;
            if (DisPlay.Text != "")
            {
                int d1;
                string d2;
                string s_txt = DisPlay.Text;
                string s1 = s_txt.Substring(0);
                if (this.OCT.Checked)//十进制转换八进制
                {
                    double a = Convert.ToDouble(s1);
                    d1 = (int)a;
                    d2 = System.Convert.ToString(d1, 8);
                    DisPlay.Text = d2.ToString();
                }
                else if (this.HEX.Checked)//十进制转换十六进制
                {
                    double a = Convert.ToDouble(s1);
                    d1 = (int)a;
                    d2 = System.Convert.ToString(d1, 16);
                    DisPlay.Text = d2.ToString().ToUpper();
                }
                else if (this.BIN.Checked)//十进制转换二进制
                {

                    double a = Convert.ToDouble(s1);
                    d1 = (int)a;
                    d2 = System.Convert.ToString(d1, 2);
                    DisPlay.Text = d2.ToString();
                }
                setMouse();
            }
        }

        private void OCT_CheckedChanged(object sender, EventArgs e)                    //进制 八进制
        {
            this.btnExp.Enabled = false;
            this.btn9.Enabled = false;
            this.btn8.Enabled = false;
            this.btn7.Enabled = true;
            this.btn6.Enabled = true;
            this.btn5.Enabled = true;
            this.btn4.Enabled = true;
            this.btn3.Enabled = true;
            this.btn2.Enabled = true;
            this.A.Enabled = false;
            this.B.Enabled = false;
            this.C.Enabled = false;
            this.D.Enabled = false;
            this.E.Enabled = false;
            this.F.Enabled = false;
            if (DisPlay.Text != "")
            {
                int d1;
                string d2;
                string s_txt = DisPlay.Text;
                string s1 = s_txt.Substring(0);
                if (this.HEX.Checked)//八进制转换十六进制
                {
                    int a = System.Convert.ToInt32(s1, 8);

                    d1 = a;
                    d2 = System.Convert.ToString(d1, 16);
                    DisPlay.Text = d2.ToString().ToUpper();
                }
                else if (this.DEC.Checked)//八进制转换十进制
                {
                    int a = System.Convert.ToInt32(s1, 8);

                    d1 = a;
                    d2 = System.Convert.ToString(d1, 10);
                    DisPlay.Text = d2.ToString();
                }
                else if (this.BIN.Checked)//八进制转换二进制
                {
                    int a = System.Convert.ToInt32(s1, 8);

                    d1 = a;
                    d2 = System.Convert.ToString(d1, 2);
                    DisPlay.Text = d2.ToString();
                }
                setMouse();
            }
        }

        private void BIN_CheckedChanged(object sender, EventArgs e)                    //进制 二进制 
        {
            this.btnExp.Enabled = false;
            this.btn9.Enabled = false;
            this.btn8.Enabled = false;
            this.btn7.Enabled = false;
            this.btn6.Enabled = false;
            this.btn5.Enabled = false;
            this.btn4.Enabled = false;
            this.btn3.Enabled = false;
            this.btn2.Enabled = false;
            this.btn1.Enabled = true;
            this.btn10.Enabled = true;
            this.A.Enabled = false;
            this.B.Enabled = false;
            this.C.Enabled = false;
            this.D.Enabled = false;
            this.E.Enabled = false;
            this.F.Enabled = false;
            if (DisPlay.Text != "")
            {
                int d1;
                string d2;
                string s_txt = DisPlay.Text;
                string s1 = s_txt.Substring(0);
                if (this.OCT.Checked)//二进制转换八进制
                {
                    int a = System.Convert.ToInt32(s1, 2);

                    d1 = a;
                    d2 = System.Convert.ToString(d1, 8);
                    DisPlay.Text = d2.ToString();
                }
                else if (this.DEC.Checked)//二进制转换十进制
                {
                    int a = System.Convert.ToInt32(s1, 2);

                    d1 = a;
                    d2 = System.Convert.ToString(d1, 10);
                    DisPlay.Text = d2.ToString();
                }
                else if (this.HEX.Checked)//二进制转换十六进制
                {
                    int a = System.Convert.ToInt32(s1, 2);

                    d1 = a;
                    d2 = System.Convert.ToString(d1, 16);
                    DisPlay.Text = d2.ToString().ToUpper();
                }
                setMouse();
            }
        }
        private void btnleft_Click(object sender, EventArgs e)                        //单机左键事件
        {
            if (error)
            {
                ClearErrorinfo();
            }

            if (DisPlay.SelectionStart > 0 && DisPlay.SelectionStart < 3)
            {
                DisPlay.SelectionStart--;
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
            }
            else
            {
                if (DisPlay.SelectionStart > 2)
                {
                    char ch = DisPlay.Text[DisPlay.SelectionStart - 1];
                    if (ch == 's' || ch == 'n') DisPlay.SelectionStart -= 3;                //有Cos,Sin,Tan时，左则三格
                    else DisPlay.SelectionStart--;
                }
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();                                                     //若为空的处置
            }
        }

        private void btnright_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            char ch = new char();
            if (DisPlay.Text != string.Empty)
            {
                ch = DisPlay.Text[DisPlay.SelectionStart];
            }
            if (ch == 'T' || ch == 'S' || ch == 'C')                                  //有Cos,Sin,Tan时，右则三格
            {
                DisPlay.SelectionStart += 3;
            }
            else
            {
                DisPlay.SelectionStart++;
            }
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }
        private void btnup_Click(object sender, EventArgs e)                          //向上键事件实现
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            if (RowIndex > 0)
            {
                int Row = 0;
                for (int i = 0; i < DisPlay.Text.Length; i++)
                {
                    if (DisPlay.Text[i] == '\n')
                    {
                        Row++;
                        pos[Row] = i + 1;
                    }
                }
                DisPlay.Select(pos[RowIndex - 1], 0);
            }
            DisPlay.Focus();
        }

        private void btndown_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int Row = 0;
            for (int i = 0; i < DisPlay.Text.Length; i++)
            {
                if (DisPlay.Text[i] == '\n')
                {
                    Row++;
                    pos[Row] = i + 1;
                }
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            if (RowIndex < Row)
            {
                DisPlay.Select(pos[RowIndex + 1], 0);
                DisPlay.Focus();
            }
        }
        private void btnPoint_Click(object sender, EventArgs e)                      //小数点
        {
            Button btn = (Button)sender;
            DisPlay.Text += btnPoint.Text;
            DisPlay.Focus();
        }
        private void btnAdd_Click(object sender, EventArgs e)                      //运算键 加+
        {
            Button btn = (Button)sender;
            DisPlay.SelectedText += " + ";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }
        private void btnSub_Click(object sender, EventArgs e)                      //运算键 减-
        {
            Button btn = (Button)sender;
            DisPlay.SelectedText += " - ";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }
        private void btnMul_Click(object sender, EventArgs e)                      //运算键 乘*
        {
            Button btn = (Button)sender;
            DisPlay.SelectedText += " * ";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }
        private void btnDiv_Click(object sender, EventArgs e)                      //运算键 除/
        {
            Button btn = (Button)sender;
            DisPlay.SelectedText += " / ";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }
        private void btnCE_Click(object sender, EventArgs e)
        {
            DisPlay.Text = string.Empty;                                   //符号运算重置
            DisPlay.Focus();
            for(int i=0;i<50;i++)
            {
                CanOperation[i] = false;
            }
            error = false;                                //判是否错误
            errorinfoLen = 0;
            pos = new int[50];
            t_s = string.Empty;
        }
       private void btnDel_Click(object sender, EventArgs e)                        //退格键 
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            int x = DisPlay.SelectionStart;
            if(x>0)
            {
                if (DisPlay.Text[x - 1] == 'n' || DisPlay.Text[x - 1] == 's')
                {
                    DisPlay.Text = DisPlay.Text.Remove(x - 3, 3);
                }
                else
                {
                    if (DisPlay.Text[x - 1] == 'o' || DisPlay.Text[x - 1] == 'a')
                    {
                        DisPlay.Text = DisPlay.Text.Remove(x - 2, 3);
                    }
                    else
                    {
                        if (DisPlay.Text[x - 1] == 'T' || DisPlay.Text[x - 1] == 'C' || DisPlay.Text[x - 1] == 'S')
                        {
                            DisPlay.Text = DisPlay.Text.Remove(x - 1, 3);
                        }
                        else
                        {
                            DisPlay.Text = DisPlay.Text.Remove(x - 1, 1);         //使用remove之后Selection会变更，用x记录变更前位置
                        }
                    }
                } 
                DisPlay.SelectionStart = x - 1;
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();                           
            }
            else DisPlay.Focus();
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void 查看帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Designed by 502ers.");
        }
        private void 标准型_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm1 = new Form1();
            fm1.ShowDialog();
            this.Close();
        }
        private void btnAnswer_Click(object sender, EventArgs e)
        {
            string s1="";
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (BIN.Checked) {
                s1 = System.Convert.ToString(preAnswer, 2);
            }
            if (OCT.Checked)
            {
                s1 = System.Convert.ToString(preAnswer,8);
            }
            if (HEX.Checked)
            {
                s1 = System.Convert.ToString(preAnswer, 16);
            }
            if (DEC.Checked)
            {
                s1 = System.Convert.ToString(preAnswer);
            }
            DisPlay.SelectedText += s1;
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            int d1;
            string d2;
            string s_txt = DisPlay.Text;
            int space = s_txt.IndexOf(' ');
            string s1 = s_txt.Substring(0, space).Trim(); //将第一个数字和空格合赋给字符串s1
            char opration = Convert.ToChar(s_txt.Substring((space + 1), 1)); //运算符
            string s2 = s_txt.Substring(space + 3);//将第二个数字赋给字符串s2
            if (this.OCT.Checked)//八进制
            {
                int i = System.Convert.ToInt32(s1, 8);
                int j = System.Convert.ToInt32(s2, 8);
                switch (opration)
                {
                    case '+':
                        d1 = i + j;
                        d2 = System.Convert.ToString(d1, 8);
                        break;
                    case '-':
                        d1 = i - j;
                        d2 = System.Convert.ToString(d1, 8);
                        break;
                    case '*':
                        d1 = i * j;
                        d2 = System.Convert.ToString(d1, 8);
                        break;
                    case '/':
                        if (j == 0)
                        {
                            throw new ApplicationException();
                        }
                        else
                        {
                            d1 = i / j;
                            d2 = System.Convert.ToString(d1, 8);
                        }
                        break;
                    default:
                        throw new ApplicationException();
                }
                DisPlay.Text = d2;
                DisPlay.Focus();
                preAnswer = System.Convert.ToInt32(d2,8);
            }
            if (this.HEX.Checked)//十六进制
            {
                int i = System.Convert.ToInt32(s1, 16);
                int j = System.Convert.ToInt32(s2, 16);
                switch (opration)
                {
                    case '+':
                        d1 = i + j;
                        d2 = System.Convert.ToString(d1, 16);
                        break;
                    case '-':
                        d1 = i - j;
                        d2 = System.Convert.ToString(d1, 16);
                        break;
                    case '*':
                        d1 = i * j;
                        d2 = System.Convert.ToString(d1, 16);
                        break;
                    case '/':
                        if (j == 0)
                        {
                            throw new ApplicationException();
                        }
                        else
                        {
                            d1 = i / j;
                            d2 = System.Convert.ToString(d1, 16);
                        }
                        break;
                    default:
                        throw new ApplicationException();
                }
                DisPlay.Text = d2.ToUpper();
                DisPlay.Focus();
                DisPlay.Select(DisPlay.TextLength, 0);
                preAnswer = System.Convert.ToInt32(d2,16);
            }
            if (this.BIN.Checked)//二进制
            {
                int i = System.Convert.ToInt32(s1, 2);
                int j = System.Convert.ToInt32(s2, 2);
                switch (opration)
                {
                    case '+':
                        d1 = i + j;
                        d2 = System.Convert.ToString(d1, 2);
                        break;
                    case '-':
                        d1 = i - j;
                        d2 = System.Convert.ToString(d1, 2);
                        break;
                    case '*':
                        d1 = i * j;
                        d2 = System.Convert.ToString(d1, 2);
                        break;
                    case '/':
                        if (j == 0)
                        {
                            throw new ApplicationException();
                        }
                        else
                        {
                            d1 = i / j;
                            d2 = System.Convert.ToString(d1, 2);
                        }
                        break;
                    default:
                        throw new ApplicationException();
                }
                DisPlay.Text = d2;
                DisPlay.Focus();
                DisPlay.Select(DisPlay.TextLength, 0);
                preAnswer = System.Convert.ToInt32(d2,2);
            }
            if (this.DEC.Checked)//十进制
            {
                double d;
                double a1 = Convert.ToDouble(s1);
                double a2 = Convert.ToDouble(s2);
                switch (opration)
                {
                    case '+':
                        d = a1 + a2;
                        break;
                    case '-':
                        d = a1 - a2;
                        break;
                    case '*':
                        d = a1 * a2;
                        break;
                    case '/':
                        if (a2 == 0)
                        {
                            throw new ApplicationException();
                        }
                        else
                        {
                            d = a1 / a2;
                        }
                        break;
                    default:
                        throw new ApplicationException();
                }
                DisPlay.Text = d.ToString();
                DisPlay.Focus();
                DisPlay.Select(DisPlay.TextLength, 0);
                preAnswer = (int)d;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '1';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '2';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '3';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '4';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '5';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '6';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '7';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '8';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '9';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += '0';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void A_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += 'A';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void B_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += 'B';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void C_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += 'C';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void D_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += 'D';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void E_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += 'E';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void F_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            DisPlay.SelectedText += 'F';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += "E";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 单位转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm3 = new Form3();
            fm3.ShowDialog();
            this.Close();
        }

        private void 退出ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}
