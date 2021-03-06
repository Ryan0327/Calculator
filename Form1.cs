﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Data;

namespace Simple_calculate
{
    public partial class Form1 : Form
    {
        private double preAnswer;                        //前一个答案
        bool error=false;                                //判是否错误
        int errorinfoLen = 0;
        bool[] CanOperation = new bool[50];
        int[] pos = new int[50];
        bool ALT = false;                                //ALT
        bool flag = false;
        int mood = 0;                                //0是DEG,1是RAD,2是GRA
        int pos_x, pos_y;
        string t_s = string.Empty;
        char[] op = { '+', '-', '*', '/', '(', ')', '√', 'c', 's', 't','C','S','T','^','L','l','x','!', '=' };

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
        public Form1()
        { 
            InitializeComponent();
            this.Text = "Calculator";
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            if(error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '1';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '2';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '3';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();//给richTextBox焦点
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '4';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '5';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '6';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '7';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '8';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '9';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '0';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private int Cheat_OpEnable()             //判断是否可以输入加减乘除符号，返回0为不可输入，返回1为输入，返回-1为输入替换
        {
            int pos = DisPlay.SelectionStart;
            if (pos == 0) return 0;
            char ch = DisPlay.Text[pos - 1];
            if(ch=='+'||ch=='-'||ch=='*'||ch=='/')
            {
                return -1;
            }
            if (ch == '(' || ch == '\n' || ch == '√' || ch == 's'||ch=='n') return 0;            
            return 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            int branch = Cheat_OpEnable();
            if (branch==-1)                                           //当前一个字符为+-*/（时，输入替换
            {
                btnDel_Click(sender, e);
                DisPlay.SelectedText += '+';
                DisPlay.Focus();
                return;
            }
            if(branch==0)
            {
                DisPlay.Focus();
                return;
            }
            DisPlay.SelectedText += '+';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '-';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnMul_Click(object sender, EventArgs e)               //乘法运算
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            int branch = Cheat_OpEnable();
            if (branch == -1)                                           //当前一个字符为+-*/（时，输入替换
            {
                btnDel_Click(sender, e);
                DisPlay.SelectedText += '*';
                DisPlay.Focus();
                return;
            }
            if (branch == 0)
            {
                DisPlay.Focus();
                return;
            }
            DisPlay.SelectedText += '*';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnDiv_Click(object sender, EventArgs e)                 //除法运算
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            int branch = Cheat_OpEnable();
            if (branch == -1)                                           //当前一个字符为+-*/（时，输入替换
            {
                btnDel_Click(sender, e);
                DisPlay.SelectedText += '/';
                DisPlay.Focus();
                return;
            }
            if (branch == 0)
            {
                DisPlay.Focus();
                return;
            }
            DisPlay.SelectedText += '/';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnLBracket_Click(object sender, EventArgs e)             //左括号
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += '(';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnRBracket_Click(object sender, EventArgs e)              //右括号
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += ')';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            DisPlay.SelectedText += '.';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnleft_Click(object sender, EventArgs e)                          //单机左键事件
        {
            if (error)
            {
                ClearErrorinfo();
            }

            if (DisPlay.SelectionStart > 0&&DisPlay.SelectionStart<3)
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
            char ch=new char() ;
            if(DisPlay.Text!=string.Empty)
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
        private void btnup_Click(object sender, EventArgs e)                                  //向上键事件实现
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

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            DisPlay.SelectedText += preAnswer.ToString();
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnDel_Click(object sender, EventArgs e)
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

        private char Precede(char a, char ch)                //优先级判定
        {
            int pos_a=0;
            int pos_ch=0;
            for(int i=0;i<op.Length;i++)
            {
                if (op[i] == a)
                    pos_a = i;
                if (op[i] == ch)
                    pos_ch = i;
            }
            return priority[pos_a, pos_ch];
        }

        private double Operate(double a, char theta, double b)                  //运算函数
        {
            if (theta == '+') return a + b;
            if (theta == '-') return a - b;
            if (theta == '*') return a * b;
            if (theta == '/') return a / b;
            if (theta == '^') return Math.Pow(a, b);
            if (theta == 'x') return Math.Pow(b, 1/a);
            return 0;
        }

        private double Operate(double a, char theta)                             //运算函数的重载
        {
            if(theta=='!')
            {
                if(is_integer(a)&&a<171)
                {
                    return factorial(a);
                }
                Show_Errorinfo("Error!");
                flag = true;
            }
            if(theta=='L')
            {
                return Math.Log10(a);
            }
            if(theta=='l')
            {
                return Math.Log(a);
            }
            if (theta == '√')
            {
                return Math.Sqrt(a);
            }
            if (theta == 'c')
            {
                if(mood==0)
                {
                    a = a * Math.PI / 180;                                            //角度转换弧度
                    return Math.Round(Math.Cos(a), 14);
                }
                else return Math.Round(Math.Cos(a), 14);
            }
            if (theta == 's') 
            {
                if (mood == 0)
                {
                    a = a * Math.PI / 180;                                            //角度转换弧度
                    return Math.Round(Math.Sin(a), 14);
                }
                else return Math.Round(Math.Sin(a), 14);
            }
            if (theta == 't') 
            {
                if (mood == 0)
                {
                    a = a * Math.PI / 180;                                            //角度转换弧度
                    return Math.Round(Math.Tan(a), 14);
                }
                else return Math.Round(Math.Tan(a), 14);
            }
            if (theta == 'C')
            {
                if (mood == 0)
                {
                    return 180 / Math.PI * Math.Acos(a);       //弧度转换角度
                }
                else return Math.Acos(a);
            }
            if (theta == 'S')
            {
                if (mood == 0)
                {
                    return 180 / Math.PI * Math.Asin(a);       //弧度转换角度
                }
                else return Math.Asin(a);
            }
            if (theta == 'C')
            {
                if (mood == 0)
                {
                    return 180 / Math.PI * Math.Atan(a);       //弧度转换角度
                }
                else return Math.Atan(a);
            }
            return 0;
        }

        private double GetNumber(string Str,ref char ch ,ref int curr,ref bool is_num)      
            //四个参数，得到的字符串，当前对应字符，当前位置（引用），字符串长度
        {
            int Len = Str.Length;
            char[] num = new char[50];          //暂时存放double数
            int i = 0;
            ch = Str[curr++];           
            while (((ch <= '9' && ch >= '0') || ch == '.')&&i<Len)
            {
                is_num = true;
                if (i >=50)
                {
                    Show_Errorinfo("Number overflowed!");
                    return -1;
                }
                num[i++] = ch;
                ch = Str[curr++];
            }
            if(is_num)
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
            if(RowIndex<Row)
            {
                string s = DisPlay.Text.Substring(pos[RowIndex], pos[RowIndex + 1] - pos[RowIndex] - 1);
                if(s[0]=='=')
                {
                    s = s.Substring(1);
                }
                return s;
            
            }else
            {
                int x=DisPlay.Text.Length - pos[RowIndex] ;
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
                if(S[i]=='a')
                {
                    if ((i - 1 >= 0) && S[i - 1] == 'T'||(S[i+1]=='r'))
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
                    if ((i+3<S.Length-1)&&S[i + 1] == 'r' && S[i + 2] == 'c'&&(S[i+3]=='S'||S[i+3]=='C'||S[i+3]=='T')||S[i+1]=='n')
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
            for(int i=0;i<Len-1;i++)
            {
                if(Str[i]=='-'&&(i==0||(Str[i-1] > '9' || Str[i-1] < '0')))
                {
                    Str = Str.Insert(i, "0");
                    i++;
                    Len++;
                }
                if ((Str[i] <= '9' && Str[i] >= '0') && (Str[i + 1] == '(' || Str[i + 1] == '√' || Str[i + 1] == 'π' || Str[i + 1] == 's' || Str[i + 1] == 'c' || Str[i + 1] == 't'))    //数字后接左括号，根号，pi,cos,sin,tan默认乘法
                {
                    Str=Str.Insert(i+1, "*");
                    i++;
                    Len++;
                }
                if (Str[i] == ')' && (Str[i + 1] == '(' || Str[i + 1] == '√' || Str[i + 1] == 'π' || Str[i + 1] == 's' || Str[i + 1] == 'c' || Str[i + 1] == 't'))    //右括号后接左括号,根号,pi默认乘法
                {
                    Str = Str.Insert(i+1, "*");
                    i++;
                    Len++;
                }
                if (Str[i] == ')' && (Str[i + 1] <= '9' && Str[i + 1] >= '0'))     //括号后接数字默认乘法
                {
                    Str = Str.Insert(i+1, "*");
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
                double x= n * factorial(n - 1);
                if(x>double.MaxValue)
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
                    for(int j=i;j<s.Length;j++)
                    {
                        if (s[j] != '0')
                            return false;
                    }
                }
            }
            return true;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            if (!CanOperation[RowIndex]) 
            {
                DisPlay.Focus();
                return;
            }
            CanOperation[RowIndex] = false;
            bool Lastrow = false;
            string Str1 ;
            if ((Str1 = GetEnable_SubStr(ref Lastrow)) == "") return;        //Str1为得到的子串
            int Cheak = Cheak_Str(Str1);
            if (Cheak == 0)
            {
                DisPlay.Focus();
                return;
            }
            string Str = Str1+'=';                                       
            Str= StrDespose(Str);                                   //Str为处理过后可以进行运算的子串
            Stack<char> OPTR = new Stack<char>();                   //操作符栈
            Stack<double> OPND = new Stack<double>();               //操作数栈
            int Len = Str.Length;
            int curr = 0;
            char theta, x;
            double a = 0;                                           //操作数1，2
            double b = 0;
            double real_input = 0;                                  //压入多位数字
            bool is_num = false;                                    //判断读入是否为数字,true为数字
            OPTR.Push('=');
            char ch=new char();
            if ((real_input = GetNumber(Str, ref ch, ref curr, ref is_num)) == -1) return;
            while (ch != '=' || OPTR.Peek() != '=')
            {
                if (is_num)
                {
                    OPND.Push(real_input);
                    is_num = false;                 //一旦压入数字，则为下一次判断初始化
                    real_input = 0;
                }
                else
                {
                    while(ch==')')                                         //右括号的处理
                    {
                        bool have_leftbracket = false;
                        char[] StackTR = OPTR.ToArray();
                        for(int i=0;i<OPTR.Count;i++)
                        {
                            if (StackTR[i] == '(')
                            {
                                have_leftbracket = true;
                                break;
                            }
                        }
                        if (have_leftbracket) break;
                        if ((real_input = GetNumber(Str, ref ch, ref curr, ref is_num)) == -1) return;                                                
                    }
                    if (ch == '=' && OPTR.Peek() == '(')
                    {
                        while(OPTR.Peek()=='(')
                        {
                            OPTR.Pop();
                        }                       
                    }
                    switch (Precede(OPTR.Peek(), ch))
                    {
                        case '<':
                            OPTR.Push(ch);
                            if ((real_input = GetNumber(Str, ref ch, ref curr, ref is_num)) == -1) return;
                            break;
                        case '>':
                            theta = OPTR.Pop();
                            if (theta == '+' || theta == '-' || theta == '*' || theta == '/' || theta == '^' || theta == 'x')//普通运算符运算
                            {
                                if (OPND.Count == 0)   //出栈前检测是否为空
                                {
                                    Show_Errorinfo("Error: \"" + theta + "\"not enough operand");
                                    return;
                                }
                                b = OPND.Pop();
                                if (OPND.Count == 0)
                                {
                                    Show_Errorinfo("Error: \"" + theta + "\"not enough operand");
                                    return;
                                }
                                a = OPND.Pop();
                                OPND.Push(Operate(a, theta, b));
                                
                            }
                            else                        
                            {
                                if (OPND.Count == 0)
                                {
                                    Show_Errorinfo("Error: not enough operand");
                                    return;
                                }
                                a = OPND.Pop();
                                OPND.Push(Operate(a, theta));
                                if (flag)
                                {
                                    flag = false;
                                    return;
                                }
                            }                 
                            break;
                        case '=':
                            x = OPTR.Pop();
                            if ((real_input = GetNumber(Str, ref ch, ref curr, ref is_num)) == -1) return;
                            break;
                    }
                }              
            }
            //输出
            if(t_s==string.Empty)
            {
                if (OPND.Count == 0)
                {
                    if (Str1 == "π")
                    {
                        DisPlay.Text = DisPlay.Text + "\n=" + Math.PI.ToString() + '\n';
                        t_s = DisPlay.Text;
                        preAnswer = Math.PI;
                    }
                    else
                    {
                        DisPlay.Text = DisPlay.Text + "\n=" + Str.Remove(Str.Length - 1) + '\n';
                        t_s = DisPlay.Text;
                        preAnswer = double.Parse(Str.Remove(Str.Length - 1));
                    }
                }
                else
                {
                    if (Lastrow == true)
                    {
                        DisPlay.Text = DisPlay.Text + "\n=" + OPND.Peek() + '\n';                //最后一行的增加
                        t_s = DisPlay.Text;
                    }
                    else
                    {
                        DisPlay.Text = DisPlay.Text + Str1 + "\n=" + OPND.Peek() + '\n';         //非最后一行的增加
                        t_s = DisPlay.Text;
                    }
                    preAnswer = OPND.Peek();
                }
                setMouse();
            }
            else
            {
                if (OPND.Count == 0)
                {
                    if (Str1 == "π")
                    {
                        DisPlay.Text = t_s + Str1+"\n=" + Math.PI.ToString() + '\n';
                        t_s = DisPlay.Text;
                        preAnswer = Math.PI;
                    }
                    else
                    {
                        DisPlay.Text = t_s + Str1+"\n=" + Str.Remove(Str.Length - 1) + '\n';
                        t_s = DisPlay.Text;
                        preAnswer = double.Parse(Str.Remove(Str.Length - 1));
                    }
                }
                else
                {
                    if (Lastrow == true)
                    {
                        DisPlay.Text = t_s + Str1+"\n=" + OPND.Peek() + '\n';                //最后一行的增加
                        t_s = DisPlay.Text;
                        t_s = DisPlay.Text;
                    }
                    else
                    {
                        DisPlay.Text = t_s + Str1 + "\n=" + OPND.Peek() + '\n';         //非最后一行的增加
                        t_s = DisPlay.Text;
                    }
                    preAnswer = OPND.Peek();
                }
                setMouse();
            }
        }

        public void setMouse()
        {
            DisPlay.Focus();   //给richTextBox焦点
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

        private void btnroot_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "3x√(";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }       
            DisPlay.SelectedText += "√(";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnpi_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "!";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }    
            DisPlay.SelectedText += 'π';
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnsin_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "arcSin(";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }
            DisPlay.SelectedText += "Sin(";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btncos_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "arcCos(";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }   
            DisPlay.SelectedText += "Cos(";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btntan_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "arcTan(";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }   
            DisPlay.SelectedText += "Tan(";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if(ALT)
            {
                DisPlay.SelectedText += "^3";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }          
            DisPlay.SelectedText += "^2";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnPower_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "x√(";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }            
            DisPlay.SelectedText += "^";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
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

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "10^";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }
            DisPlay.SelectedText += "log";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }

        private void btnLn_Click(object sender, EventArgs e)
        {
            if (error)
            {
                ClearErrorinfo();
            }
            int RowIndex = DisPlay.GetLineFromCharIndex(DisPlay.SelectionStart);
            CanOperation[RowIndex] = true;
            if (ALT)
            {
                DisPlay.SelectedText += "e^";
                DisPlay.Select(DisPlay.SelectionStart, 0);
                DisPlay.Focus();
                ResetALT();
                return;
            }
            DisPlay.SelectedText += "ln(";
            DisPlay.Select(DisPlay.SelectionStart, 0);
            DisPlay.Focus();
        }


        private void btnALT_Click(object sender, EventArgs e)
        {
            DisPlay.Focus();
            if(ALT)
            {
                ALT = false;
                labALT.ForeColor = SystemColors.Control;
            }
            else
            {
                ALT = true;
                labALT.ForeColor = Color.SteelBlue;
            }         
        }

        private void ResetALT()
        {
            ALT = false;
            labALT.ForeColor = SystemColors.Control;
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMood_Click(object sender, EventArgs e)
        {
            mood = (mood + 1) % 2;
            if(mood==0)
            {
                labMood.Text = "DEG";
                labMood.ForeColor = Color.SteelBlue;
            }
            if (mood == 1)
            {
                labMood.Text = "RAD";
                labMood.ForeColor = Color.SteelBlue;
            }
        }

        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Designed by 502ers.");
        }

        private void 数制转换_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm2 = new Form2();
            fm2.ShowDialog();
            this.Close();
        }

        private void 单位转换_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fm3 = new Form3();
            fm3.ShowDialog();
            this.Close();
        }
   
    }
}
