using System;
using Andi;

namespace TugasSOFirefly.Library
{
    public class FunctionVariabel
    {
        #region Variabel Function
        private static int _xmin = -2;
        private static int _xmax = 2;
        private static int _ymin = -2;
        private static int _ymax = 2;

        public static double goldsteinprice(double x, double y)
        {
            double a = 1 + Math.Pow((x + y + 1), 2)*(19 - 14*x + 3*Math.Pow(x, 2) - 14*y + 6*x*y + 3*Math.Pow(y, 2));
            double b = 30 + Math.Pow(2*x - 3*y, 2)*(18 - 32*x + 12*Math.Pow(x, 2) + 48*y - 36*x*y + 27*Math.Pow(y, 2));
            return (a*b);
        }

        public static double goldsteinprice(params double[] x)
        {
            double a = 1 + Math.Pow((x[0] + x[1] + 1), 2) * (19 - 14 * x[0] + 3 * Math.Pow(x[0], 2) - 14 * x[1] + 6 * x[0] * x[1] + 3 * Math.Pow(x[1], 2));
            double b = 30 + Math.Pow(2 * x[0] - 3 * x[1], 2) * (18 - 32 * x[0] + 12 * Math.Pow(x[0], 2) + 48 * x[1] - 36 * x[0] * x[1] + 27 * Math.Pow(x[1], 2));
            return (a * b);
        }

        public static double fourpeaks(double x, double y)
        {
            return Math.Exp(-Math.Pow(x - 4, 2)-Math.Pow(y - 4, 2)) + 
                Math.Exp(-Math.Pow(x + 4, 2)-Math.Pow(y + 4, 2)) +
                   2*(Math.Exp(-Math.Pow(x, 2)-Math.Pow(y, 2)) + 
                   Math.Exp(-Math.Pow(x, 2)-Math.Pow((y + 4), 2)));
        }

        public static double NumberFormat(double input)
        {
            input = input.Rounding(GlobalConfiguration.Digit+1);

            string nol = "";

            for (int i = 0; i < GlobalConfiguration.Digit; i++)
            {
                nol += "#";
            }

            string b = input.ToString("0." + nol);

            if (!b.Contains("."))
            {
                return double.Parse(b);
            }
            else
            {
                int a = b.IndexOf('.')+1;
                int c = b.Length-a-1;

                if (c > GlobalConfiguration.Digit)
                {
                    c = GlobalConfiguration.Digit;
                }
                return double.Parse(b.Substring(0, c+a));
            }
        }

        public static int Xmin
        {
            set { _xmin = value; }
            get { return _xmin; }
        }

        public static int Ymin
        {
            set { _ymin = value; }
            get { return _ymin; }
        }

        public static int Xmax
        {
            set { _xmax = value; }
            get { return _xmax; }
        }

        public static int Ymax
        {
            set { _ymax = value; }
            get { return _ymax; }
        }
        #endregion
    }
}
