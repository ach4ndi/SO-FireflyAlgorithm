using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using TugasSOFirefly.FormV;
using TugasSOFirefly.Library;
using WeifenLuo.WinFormsUI.Docking;

namespace TugasSOFirefly
{
    public partial class MainForm : KryptonForm
    {
        public MainForm()
        {
            InitializeComponent();
            exExit.Click += GlobalConfiguration.setClickEventCloseGlobal;
            OpenMain();
        }

        void OpenMain()
        {
            Main2 dummyDoc = new Main2(this);

            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(this.dockPanel1);
        }

        private void exAbout_Click(object sender, EventArgs e)
        {
            Frm_About dummyDoc = new Frm_About();

            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(this.dockPanel1);
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Settings dummyDoc = new Frm_Settings();

            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(this.dockPanel1);
        }

        private void office2010BlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kMan_1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
        }

        private void office2010BlackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kMan_1.GlobalPaletteMode = PaletteModeManager.Office2010Black;
        }

        private void office2010SilverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kMan_1.GlobalPaletteMode = PaletteModeManager.Office2010Silver;
        }

        private void office2007BlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kMan_1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMain();
        }

        private void threadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_Threading dummyDoc = new Test_Threading();

            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(this.dockPanel1);
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GP dummyDoc = new GP();

            if (this.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(this.dockPanel1);
        }

        private void formclose(object sender, FormClosedEventArgs e)
        {
            // Wait for the thread to exit

            Application.ExitThread();
        }
    }
}
