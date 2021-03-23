using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.Reporting;
using WeifenLuo.WinFormsUI.Docking;

namespace TugasSOFirefly.FormV
{
    public partial class ReportViewer : DockContent
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        public void Report(Report n)
        {
            this.reportViewer1.ReportSource = n;
            this.reportViewer1.RefreshReport();
        }
    }
}
