﻿using System;
using Microsoft.VisualBasic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using PS3Lib;
using PS3Lib.NET;
using System.Reflection;
using System.IO.Compression;
using System.Globalization;
using System.IO;
using Dragon_Age_Inquisition;

namespace Dragon_Age_Inquisition
{
    public partial class InputBox : Form
    {
        //Argument and return
        public Form1.IBArg[] Arg;
        public int ret = 0;
        public static Keys keyVal;

        //Form1's width, height, left, and top
        public int fmWidth = 0;
        public int fmHeight = 0;
        public int fmLeft = 0;
        public int fmTop = 0;

        //Form1's Colors
        public Color fmBackColor = Form1.ncBackColor;
        public Color fmForeColor = Form1.ncForeColor;

        //Text box array
        TextBox[] textBoxArg;

        private void AllButtons_MouseEnter(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var name = currentButton.Name;
            currentButton.ForeColor = fmBackColor;
            currentButton.BackColor = fmForeColor;
        }
        private void AllButtons_MouseLeave(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var name = currentButton.Name;
            currentButton.ForeColor = fmForeColor;
            currentButton.BackColor = fmBackColor;
        }


        public InputBox()
        {
            InitializeComponent();
        }
        private void InputBox_Load(object sender, EventArgs e)
        {
            if (Arg == null || Arg.Length <= 0)
            {
                ret = 2;
                Dispose();
                Close();
                return;
            }

            BackColor = fmBackColor;
            ForeColor = fmForeColor;
            foreach (Control ctrl in Controls)
            {
                ctrl.BackColor = fmBackColor;
                ctrl.ForeColor = fmForeColor;
            }

            foreach (var button in this.Controls.OfType<Button>())
            {
                button.MouseEnter += AllButtons_MouseEnter;
                button.MouseLeave += AllButtons_MouseLeave;
            }

            textBoxArg = new TextBox[Arg.Length];
            Label[] labelArg = new Label[Arg.Length];

            Height = (Arg.Length * 40) + 60;
            ibOkay.Top = this.Height - ibOkay.Height - 10;
            ibCancel.Top = this.Height - ibCancel.Height - 10;

            int locX = (fmWidth / 2) - (this.Width / 2) + fmLeft;
            int locY = (fmHeight / 2) - (this.Height / 2) + fmTop;
            this.Location = new Point(locX, locY);

            for (int i = 0; i < textBoxArg.Length; i++)
            {
                //Textbox
                var txt = new TextBox();
                textBoxArg[i] = txt;
                txt.Name = "txtBox" + i.ToString();
                txt.Text = Arg[i].defStr;
                txt.Width = this.Width - 20;
                txt.Location = new Point(10, 20 + (i * 40));
                txt.Visible = true;
                txt.BackColor = fmBackColor;
                txt.ForeColor = fmForeColor;
                tBoxIndex = Controls.Count;
                Controls.Add(txt);

                //Label
                var lbl = new Label();
                labelArg[i] = lbl;
                lbl.Name = "argLabel" + i.ToString();
                lbl.Text = Arg[i].label;
                if (lbl.Text.EndsWith(" KeyBind:"))
                    txt.KeyDown += new KeyEventHandler(KeyHandler_KeyDown);
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Width = this.Width - 20;
                lbl.Location = new Point(10, (i * 40));
                lbl.Visible = true;
                lbl.BackColor = fmBackColor;
                lbl.ForeColor = fmForeColor;
                Controls.Add(lbl);
            }
        }

        int tBoxIndex = 0;
        private void KeyHandler_KeyDown(object sender, KeyEventArgs e)
        {
            keyVal = e.KeyData;
            ((TextBox)Controls[tBoxIndex]).Text = e.KeyData.ToString().Replace(", ", " + ");
            e.SuppressKeyPress = true;
        }
        private string ParseKeyData(Keys Key)
        {
            string ret = Key.ToString().Replace(", ", " + ")
                            .Replace("D0", "0")
                            .Replace("D1", "1")
                            .Replace("D2", "2")
                            .Replace("D3", "3")
                            .Replace("D4", "4")
                            .Replace("D5", "5")
                            .Replace("D6", "6")
                            .Replace("D7", "7")
                            .Replace("D8", "8")
                            .Replace("D9", "9");
            return ret;
        }
        private void ibOkay_Click(object sender, EventArgs e)
        {
            ret = 1;
            for (int x = 0; x < Arg.Length; x++)
            {
                Arg[x].retStr = textBoxArg[x].Text;
            }
            Dispose();
            Close();
        }
        private void ibCancel_Click(object sender, EventArgs e)
        {
            ret = 2;
            Dispose();
            Close();
        }
    }
}
