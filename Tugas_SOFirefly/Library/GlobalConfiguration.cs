using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Andi;
using ComponentFactory.Krypton.Toolkit;
using SourceGrid;

namespace TugasSOFirefly.Library
{
    public class GlobalConfiguration
    {
        #region variabel
        private static int _digit = 7;
        private static int _jedamilidetik = 100;
        #endregion

        #region Digit Function
        public static void resetValueDigit(int digit)
        {
            _digit = 4;
        }

        public static int Digit
        {
            set { _digit = value; }
            get { return _digit; }
        }
        #endregion

        #region Jeda Function
        public static void resetJedaValue(int jeda)
        {
            _jedamilidetik = 100;
        }

        public static int JedaThread
        {
            set { _jedamilidetik = value; }
            get { return _jedamilidetik; }
        }
        #endregion

        #region GlobalEventListener
        internal static void setClickEventCloseGlobal(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        internal static void setKeyPressEventOnlyNumber(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as KryptonComboBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        #endregion
    }

    

    public class OpenFile
    {
        public static double[,] getOpen(Grid a)
        {
            string TampunganData = null;
            OpenFileDialog bukaFileInputAnggota = new OpenFileDialog();

            bukaFileInputAnggota.Title = "Buka File";
            bukaFileInputAnggota.Filter = "CSV Text File|*.csv|All Files (*.*)|*.*";
            bukaFileInputAnggota.FilterIndex = 1;
            bukaFileInputAnggota.Multiselect = false;
            DialogResult result = bukaFileInputAnggota.ShowDialog();

            if (result == DialogResult.OK)
            {
                //string file = Path.GetFileName(bukaFileInputAnggota.FileName);
                //string path = Path.GetDirectoryName(file);

                System.IO.StreamReader sr = new System.IO.StreamReader(bukaFileInputAnggota.FileName);
                TampunganData = sr.ReadToEnd();

                string[] u = TampunganData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                int hitcount = (u[0].Split(',')).Count();
                double[,] temp = new double[u.Length, hitcount];

                for (int i = 0; i < u.Length; i++)
                {
                    string[] Temp1 = (u[i].Split(','));

                    for (int j = 0; j < hitcount; j++)
                    {
                        temp[i, j] = double.Parse(Temp1[j]);
                    }
                }
                MakeGrid.Build(a, temp.GetLength(0), 2, new[] { "X", "Y" });
                MakeGrid.Fill(a, temp);

                return MakeGrid.Return(a);
            }

            return null;
        }
    }
}
