using System.Collections.Generic;
using System.Linq;

namespace TugasSOFirefly.Library
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class Report1 : Telerik.Reporting.Report
    {
        public Report1(laporanakhir a, laporanpop b)
        {
            InitializeComponent();
            laporanakhir.DataSource = a;
            laporanpopulasiawal.DataSource = b;
        }

        public Report1(List<dataakhir> a, laporanpop b)
        {
            InitializeComponent();
            laporanakhir.DataSource = a;
            laporanpopulasiawal.DataSource = b;
        }


        public void addPicture(Image img)
        {
            pictureBox1.Value = img;
            pictureBox1.Sizing = ImageSizeMode.ScaleProportional;
        }
        public void setHeaderText(string text)
        {
            textBox1.Value = text;
        }

        public void addHeaderText(string text)
        {
            textBox1.Value += text;
        }

        public void addJumlahPopulasi(string text)
        {
            textBox12.Value += text;
        }

        public void addAlpha(string text)
        {
            textBox13.Value += text;
        }

        public void addBeta(string text)
        {
            textBox15.Value += text;
        }

        public void addGamma(string text)
        {
            textBox14.Value += text;
        }

        public void addDelta(string text)
        {
            textBox16.Value += text;
        }

        public void addFungsi(string text)
        {
            textBox17.Value += text;
        }
    }
}