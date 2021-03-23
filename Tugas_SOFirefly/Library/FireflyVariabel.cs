using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andi;

namespace TugasSOFirefly.Library
{
    public class FireflyVariabel
    {
        #region Variable parameter
        private static double _alpha = 0.5;
        private static double _beta = 1;
        private static double _delta = 0.8;
        private static double _gamma = 0.3;
        private static double[,] data;
        private static double _value = 0;

        public static double Alpha
        {
            set { _alpha = value; }
            get { return _alpha; }
        }

        public static double Beta
        {
            set { _beta = value; }
            get { return _beta; }
        }

        public static double Delta
        {
            set { _delta = value; }
            get { return _delta; }
        }

        public static double Gamma
        {
            set { _gamma = value; }
            get { return _gamma; }
        }

        public static double[,] PopulationMember
        {
            set { data = value; }
            get { return data; }
        }

        public static double ValueStop
        {
            set { _value = value; }
            get { return _value; }
        }
        #endregion

        #region GetterSetter
        public int getBanyakPopulasi()
        {
            return (int)data.GetLongLength(0);
        }

        public int getBanyakPopulasiDimensi()
        {
            return (int)data.GetLongLength(1);
        }
        #endregion

        #region Fungsi Main Firefly
        private static List<Andi.IndexNumber> Ranking(double[] data)
        {
            List<Andi.IndexNumber> datasementara = new List<Andi.IndexNumber>();

            for (int i = 0; i < data.Length; i++)
            {
                Andi.IndexNumber var = new Andi.IndexNumber();

                var.Index = i;
                var.Value = data[i];
                datasementara.Add(var);
            }

            return datasementara.OrderBy(x => x.Value).ToList();
        }

        private static double[] Sorting(double[] data, List<Andi.IndexNumber> dataranking)
        {
            int panjang = data.Length;
            int[] datarank = new int[panjang];
            double[] hasilsorting = new double[panjang];

            for (int i = 0; i < panjang; i++)
            {
                datarank[i] = dataranking[i].Index;
            }

            for (int i = 0; i < panjang; i++)
            {
                hasilsorting[i] = dataranking[datarank[i]].Value;
            }

            return hasilsorting;
        }

        public static double[] SortingValueBy(double[] data, params int[] index)
        {
            double[] hasilsorting = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                hasilsorting[i] = data[index[i]];
            }

            return hasilsorting;
        }

        public static int[] RankingIndexList(double[] data)
        {
            List<Andi.IndexNumber> DataSeb = Ranking(data);
            int[] listarray = new int[DataSeb.Count];

            for (int i = 0; i < DataSeb.Count; i++)
            {
                listarray[i] = DataSeb[i].Index;
            }

            return listarray; // Index Ranking
        }

        public static double[] RankingValueList(double[] data)
        {
            List<Andi.IndexNumber> DataSeb = Ranking(data);
            double[] listarray = new double[DataSeb.Count];

            for (int i = 0; i < DataSeb.Count; i++)
            {
                listarray[i] = DataSeb[i].Value;
            }

            return listarray; // Nilai Z / Intensitas cahaya
        }

        private static double[] Sorting(double[] data, int[] ranklist)
        {
            double[] datasemen = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                datasemen[i] = data[ranklist[i]];
            }

            return datasemen; // Mengurutkan data X dan Y berdasarkan Index Ranking
        }

        public static double[] FindRange(double[] data,params int[] range)
        {
            #region matlab script
            /*
                function [xn,yn]=findrange(xn,yn,range)
                for i=1:length(yn),
                   if xn(i)<=range(1), xn(i)=range(1); end
                   if xn(i)>=range(2), xn(i)=range(2); end
                   if yn(i)<=range(3), yn(i)=range(3); end
                   if yn(i)>=range(4), yn(i)=range(4); end
                end 
             */
            #endregion

            int countrange = data.Length;
            double[] datarange = data;

            for (int i = 0; i < countrange; i++)
            {
                if (data[i] <= (double)range[0])
                {
                    datarange[i] = range[0];
                }

                else if (data[i] >= (double)range[1])
                {
                    datarange[i] = range[1];
                }
            }

            return datarange;
        }

        public static double FindRange(params double[] data)
        {
            if (data[0] <= (double)data[1])
            {
                return data[1];
            }

            if (data[0] >= (double)data[2])
            {
                return data[2];
            }

            return data[0];
        }

        public static double[] PopulateMember(int size, int[] range)
        {
            double[] datanagka = new double[size];

            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                datanagka[i] = rnd.NextDouble(range[0], range[1]).Rounding(GlobalConfiguration.Digit);
            }

            return datanagka;
        }

        public static double Jarak(double xn, double xo, double yn, double yo)
        {
            // sqrt((xn(i)-xo(j))^2+(yn(i)-yo(j))^2)

            double a1 = (xn - xo);
            a1 = Math.Pow(a1, 2);
            double a2 = yn - yo;
            a2 = Math.Pow(a2, 2);
            return Math.Sqrt(a1 + a2);
        }

        public static double Beta1(double betanol, double r,double gamma)
        {
            //beta0*exp(-gamma*r.^2);
            return (betanol * Math.Pow(Math.E,(-(gamma) * Math.Pow(r, 2))));
        }

        public static double Movement(double xn, double xo, double beta, double alpha, Random rnd, params int[] range)
        {
            //xn(i).*(1-beta)+xo(j).*beta+alpha.*(rand-0.5)

            return ((xn * (1 - beta) + xo * beta + alpha * (rnd.NextDouble(0, 1) - 0.5)));
        }
        #endregion
    }
}
