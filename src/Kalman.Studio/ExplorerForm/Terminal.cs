﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalman.Studio
{
    public partial class Terminal : DockExplorer
    {
        delegate void AppendTextCallback(string text);

        public Terminal()
        {
            InitializeComponent();
        }

        public void AppendText(string text)
        {
            if (this.rtbCommand.InvokeRequired)
            {
                AppendTextCallback d = new AppendTextCallback(AppendText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (string.IsNullOrEmpty(text)) return;
                rtbCommand.AppendText(text);
            }
        }

        /// <summary>
        /// 向输出窗口追加一行文本
        /// </summary>
        /// <param name="s"></param>
        public void AppendLine(string text)
        {
            AppendText(text + Environment.NewLine);
        }

        /// <summary>
        /// 清除所有输出文本
        /// </summary>
        public void ClearText()
        {
            rtbCommand.Clear();
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            rtbCommand.Copy();
        }

        private void menuItemClear_Click(object sender, EventArgs e)
        {
            rtbCommand.Clear();
        }

        private void menuItemSelectAll_Click(object sender, EventArgs e)
        {
            rtbCommand.SelectAll();
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            rtbCommand.Exit += new Command.RichConsoleBox.ExitEventHandler(rtbCommand_Exit);
        }

        private void rtbCommand_Exit(object sender, System.EventArgs e)
        {
            Config.MainForm.ShowTerminal(true);
        }
    }
}
