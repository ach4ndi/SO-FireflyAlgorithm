using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Andi;
using TugasSOFirefly.Library;
using WeifenLuo.WinFormsUI.Docking;

namespace TugasSOFirefly.FormV
{
    public partial class Frm_Settings : DockContent
    {
        Random angkarand = new Random();

        public Frm_Settings()
        {
            InitializeComponent();

            nmr_digit.Value = GlobalConfiguration.Digit;
            nmr_jeda.Value = GlobalConfiguration.JedaThread;
        }

        private void nmr_digit_ValueChanged(object sender, EventArgs e)
        {
            GlobalConfiguration.Digit = (int) nmr_digit.Value;

            double angka = angkarand.NextDouble();

            kryptonLabel2.Text = angka.Rounding(GlobalConfiguration.Digit) + " (" + angka+")";
        }

        private void nmr_jeda_ValueChanged(object sender, EventArgs e)
        {
            GlobalConfiguration.JedaThread = (int)nmr_jeda.Value;

            kryptonLabel4.Text = (double)GlobalConfiguration.JedaThread/1000 +" detik";
        }
    }
}
