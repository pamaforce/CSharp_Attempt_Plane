using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plane
{
    public partial class UserControl1 : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                //WS_CLIPCHILDREN 
                cp.Style &= ~0x02000000;
                return cp;
            }
        }

        public UserControl1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        internal MainForm parent;

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.Show();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            this.parent.StartGame();
        }
    }
}
