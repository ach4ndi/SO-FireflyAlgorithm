using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TugasSOFirefly.Library
{

    public class dataakhir
    {
        public int Index { get; set; }

        public double minimum { get; set; }

        public double average { get; set; }

        public double maximum { get; set; }
    }

    [System.ComponentModel.DataObject()]
    public class laporanakhir : List<dataakhir> 
    {


    }

    public static partial class laporanpopulasiExtension
    {
        public static int[] getIndex(this List<dataakhir> data)
        {
            int[] u = new int[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].Index;
            }

            return u;
        }

        public static double[] getMin(this List<dataakhir> data)
        {
            double[] u = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].minimum;
            }

            return u;
        }

        public static double[] getMax(this List<dataakhir> data)
        {
            double[] u = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].maximum;
            }

            return u;
        }

        public static double[] getAve(this List<dataakhir> data)
        {
            double[] u = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                u[i] = data[i].average;
            }

            return u;
        }
    }
}
