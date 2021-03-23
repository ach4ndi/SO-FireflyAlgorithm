using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugasSOFirefly.Library
{
    public class datapopulasiiterasi
    {
        public int Index { get; set; }

        public int Iterasi { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double I { get; set; }
    }

    [System.ComponentModel.DataObject()]
    public partial class laporanpopulasiskhir : List<datapopulasiiterasi>
    {
        
    }

    public static partial class laporanXYIpIterasiExtension
    {
        public static int[] getIndex(this List<datapopulasiiterasi> data)
        {
            int[] u = new int[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].Index;
            }

            return u;
        }

        public static double[] getX(this List<datapopulasiiterasi> data)
        {
            double[] u = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].X;
            }

            return u;
        }

        public static double[] getY(this List<datapopulasiiterasi> data)
        {
            double[] u = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].Y;
            }

            return u;
        }

        public static double[] getI(this List<datapopulasiiterasi> data)
        {
            double[] u = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].I;
            }

            return u;
        }
    }
}
