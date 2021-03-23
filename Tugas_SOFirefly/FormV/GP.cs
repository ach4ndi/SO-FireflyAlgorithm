using System;

using System.Text;
using System.Windows.Forms;
using Andi;
using TugasSOFirefly.Library;
using WeifenLuo.WinFormsUI.Docking;

namespace TugasSOFirefly.FormV
{
    public partial class GP : DockContent
    {
        private double[,] popmemberawal;
        public GP()
        {
            InitializeComponent();
        }

        void createGridPopulationMember()
        {
            Random rnd = new Random();
            double[,] rndNum = new double[(int)pop_member.Value, 2];

            for (int i = 0; i < (int)pop_member.Value; i++)
            {
                rndNum[i, 0] = rnd.NextDouble(FunctionVariabel.Xmin, FunctionVariabel.Xmax).Rounding(GlobalConfiguration.Digit);
                rndNum[i, 1] = rnd.NextDouble(FunctionVariabel.Ymin, FunctionVariabel.Ymax).Rounding(GlobalConfiguration.Digit);
            }

            popmemberawal = rndNum;
            MakeGrid.Build(grid_popmember, (int)pop_member.Value, 2, new[] { "X", "Y" });
            MakeGrid.Fill(grid_popmember, rndNum);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            createGridPopulationMember();

            double[] hasil = new double[popmemberawal.GetLongLength(0)];

            for (int i = 0; i < popmemberawal.GetLongLength(0); i++)
            {
                hasil[i] = FunctionVariabel.goldsteinprice(popmemberawal[i, 0], popmemberawal[i, 1]);
            }

            MakeGrid.Build(grid1, (int)popmemberawal.GetLongLength(0), 1, MakeGrid.HeaderColumnsHeader.none);
            MakeGrid.Fill(grid1, hasil);
        }
    }
}
