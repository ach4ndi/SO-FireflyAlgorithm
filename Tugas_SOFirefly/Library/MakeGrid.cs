using System;
using System.Drawing;
using Andi;
using SourceGrid;
using ContentAlignment = DevAge.Drawing.ContentAlignment;

namespace TugasSOFirefly.Library
{
    public class MakeGrid
    {
        #region enums
        public enum HeaderColumnsHeader
        {
            both, columns, rows, none
        }

        public enum SorterColumns
        {
            sort, nosort
        }

        public enum ResizeColumns
        {
            autocell, noauto
        }
        #endregion

        #region private function
        private static void Reset(Grid a, int m, int n, HeaderColumnsHeader headerstate)
        {
            a.Redim(0, 0);
            switch (headerstate)
            {
                case HeaderColumnsHeader.both:
                    a.Redim(m + 1, n + 1);
                    a.FixedRows = 1;
                    a.FixedColumns = 1;
                    break;
                case HeaderColumnsHeader.columns:
                    a.Redim(m, n + 1);
                    a.FixedRows = 0;
                    a.FixedColumns = 1;
                    break;
                case HeaderColumnsHeader.rows:
                    a.FixedRows = 1;
                    a.FixedColumns = 0;
                    a.Redim(m + 1, n);
                    break;
                case HeaderColumnsHeader.none:
                    a.FixedRows = 0;
                    a.FixedColumns = 0;
                    a.Redim(m, n);
                    break;
            }
        }

        private static void Build(Grid a, int m, int n, Color warna, HeaderColumnsHeader headerstate = HeaderColumnsHeader.both, ResizeColumns autoresize = ResizeColumns.autocell, string pointzero = "#", string[] headername = null, int positionx = 0, int positiony = 0, SorterColumns sortable = SorterColumns.sort)
        {
            try
            {
                MakeGrid.Reset(a, m, n, headerstate);

                SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
                SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
                view.BackColor = warna;

                if (headerstate == HeaderColumnsHeader.both || headerstate == HeaderColumnsHeader.rows)
                {
                    for (int r = a.FixedRows; r < a.RowsCount; r++)
                    {
                        a[r, 0] = new SourceGrid.Cells.RowHeader(r);
                    }
                }

                if (headerstate == HeaderColumnsHeader.both || headerstate == HeaderColumnsHeader.columns)
                {
                    try
                    {
                        for (int c = a.FixedColumns; c < a.ColumnsCount; c++)
                        {
                            SourceGrid.Cells.ColumnHeader header = new
                                SourceGrid.Cells.ColumnHeader(headername[c - 1]);
                            header.AutomaticSortEnabled = true;
                            header.View.TextAlignment = ContentAlignment.MiddleCenter;
                            a[0, c] = header;

                        }
                    }
                    catch
                    {

                    }
                }

                SourceGrid.Cells.ColumnHeader header1 = new SourceGrid.Cells.ColumnHeader(pointzero);

                if (sortable == SorterColumns.sort)
                {
                    header1.SortComparer = new SourceGrid.MultiColumnsComparer(1, 2, 3, 4);
                }

                a[0, 0] = header1;

                for (int r = a.FixedRows; r < a.RowsCount; r++)
                {
                    for (int c = a.FixedColumns; c < a.ColumnsCount; c++)
                    {
                        a[r, c] = new SourceGrid.Cells.Cell("");
                        a[r, c].Editor = editor;
                        a[r, c].View = view;
                    }
                }

                a.Update();
                a.Selection.Focus(new SourceGrid.Position(positionx, positiony), true);

                if (autoresize == ResizeColumns.autocell)
                {
                    a.AutoSizeCells();
                }
            }
            catch
            {
                
            }
            
        }

        private static void Fill(Grid a, object[,] data, Color warna, bool multiline = true)
        {
            SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
            SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
            view.BackColor = warna;
            editor.Control.Multiline = multiline;

            for (int r = a.FixedRows; r < a.RowsCount; r++)
            {
                for (int c = a.FixedRows; c < a.ColumnsCount; c++)
                {
                    a[r, c] = new SourceGrid.Cells.Cell(data[r - 1, c - 1]);
                    a[r, c].Editor = editor;
                    a[r, c].View = view;
                }
            }

            a.Update();
            a.Selection.Focus(new SourceGrid.Position(0, 0), true);
            a.AutoSizeCells();
        }

        private static void Fill2(Grid a, object[,] data, Color warna, bool multiline = true)
        {
            SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
            SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox(typeof(double));

            string nol = "";

            for (int i = 0; i < GlobalConfiguration.Digit; i++)
            {
                nol += "#";
            }

            view.BackColor = warna;
            editor.Control.Multiline = multiline;

            for (int r = a.FixedRows; r < a.RowsCount; r++)
            {
                for (int c = a.FixedRows; c < a.ColumnsCount; c++)
                {
                    a[r, c] = new SourceGrid.Cells.Cell(((double) data[r - 1, c - 1]).ToString("0."+nol));
                    a[r, c].Editor = editor;
                    a[r, c].View = view;
                }
            }

            a.Update();
            a.Selection.Focus(new SourceGrid.Position(0, 0), true);
            a.AutoSizeCells();
        }

        private static void Fill2(Grid a, object[] data, Color warna, int kolom)
        {
            SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
            view.BackColor = Color.Snow;

            //Editor (IDataModel) shared between all the cells
            SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));

            editor.Control.Multiline = true;

            string nol = "";

            for (int i = 0; i < GlobalConfiguration.Digit; i++)
            {
                nol += "#";
            }

            for (int r = a.FixedRows; r < a.RowsCount; r++)
            {
                a[r, kolom] = new SourceGrid.Cells.Cell(((double)data[r]).ToString("0." + nol));
                a[r, kolom].Editor = editor;
                a[r, kolom].View = view;
            }

            a.Update();
            a.Selection.Focus(new SourceGrid.Position(0, 0), true);
            a.AutoSizeCells();
        }

        private static void Fill(Grid a, object[] data, Color warna, int kolom)
        {
            SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
            view.BackColor = Color.Snow;

            //Editor (IDataModel) shared between all the cells
            SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));

            editor.Control.Multiline = true;

            for (int r = a.FixedRows; r < a.RowsCount; r++)
            {
                a[r, kolom] = new SourceGrid.Cells.Cell(data[r]);
                a[r, kolom].Editor = editor;
                a[r, kolom].View = view;
            }

            a.Update();
            a.Selection.Focus(new SourceGrid.Position(0, 0), true);
            a.AutoSizeCells();
        }

        #endregion

        #region Draw New Grid
        public static void Build(Grid a, int m, int n)
        {
            Build(a, m, n, Color.Snow);
        }

        public static void Build(Grid a, int m, int n,HeaderColumnsHeader header)
        {
            Build(a, m, n, Color.Snow, header);
        }

        public static void Build(Grid a, int m, int n, params string[] headername)
        {
            Build(a, m, n, Color.Snow, HeaderColumnsHeader.both, ResizeColumns.autocell, "#", headername);
        }

        public static void Build(Grid a, int m, int n, int px, int py, params string[] headername)
        {
            Build(a, m, n, Color.Snow, HeaderColumnsHeader.both, ResizeColumns.autocell, "#", headername,px,py);
        }

        public static void Build(Grid a, int m, int n, ResizeColumns resizecolumn,params string[] headername)
        {
            Build(a, m, n, Color.Snow, HeaderColumnsHeader.both, resizecolumn, "#", headername);
        }

        public static void Build(Grid a, int m, int n, string pointname, params string[] headername)
        {
            Build(a, m, n, Color.Snow, HeaderColumnsHeader.both, ResizeColumns.autocell, pointname, headername);
        }
        #endregion

        #region Fill Random Number
        public static void FillRandom(Grid a, int rounddigit = 4,int minrnd = -1, int maxrnd = 1)
        {
            SourceGrid.Cells.Views.Cell view = new SourceGrid.Cells.Views.Cell();
            view.BackColor = Color.Snow;
            SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));

            Random rnd = new Random();

            for (int r = a.FixedRows; r < a.RowsCount; r++)
            {
                for (int c = a.FixedColumns; c < a.ColumnsCount; c++)
                {
                    a[r, c] = new SourceGrid.Cells.Cell(rnd.NextDouble(minrnd, maxrnd).Rounding(rounddigit));
                    a[r, c].Editor = editor;
                    a[r, c].View = view;
                }
            }
            a.Update();
            a.Selection.Focus(new SourceGrid.Position(0, 0), true);
            a.AutoSizeCells();
        }
        #endregion

        #region Filling Grid
        public static void Fill(Grid a, double[,] data)
        {
            object[,] temp = new object[data.GetLongLength(0), data.GetLongLength(1)];

            for (int i = 0; i < data.GetLongLength(0); i++)
            {
                for (int j = 0; j < data.GetLongLength(1); j++)
                {
                    temp[i, j] = data[i, j];
                }
            }

            MakeGrid.Fill2(a,temp,Color.Snow);
        }

        public static void Fill(Grid a, string[,] data)
        {
            object[,] temp = new object[data.GetLongLength(0), data.GetLongLength(1)];

            for (int i = 0; i < data.GetLongLength(0); i++)
            {
                for (int j = 0; j < data.GetLongLength(1); j++)
                {
                    temp[i, j] = data[i, j];
                }
            }

            MakeGrid.Fill(a, temp, Color.Snow);
        }

        public static void Fill(Grid a, int[,] data)
        {
            object[,] temp = new object[data.GetLongLength(0), data.GetLongLength(1)];

            for (int i = 0; i < data.GetLongLength(0); i++)
            {
                for (int j = 0; j < data.GetLongLength(1); j++)
                {
                    temp[i, j] = data[i, j];
                }
            }

            MakeGrid.Fill(a, temp, Color.Snow);
        }

        public static void Fill(Grid a, double[] data, HeaderColumnsHeader header = HeaderColumnsHeader.both)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            switch (header)
            {
                case HeaderColumnsHeader.both:
                case HeaderColumnsHeader.rows:
                    MakeGrid.Fill2(a, temp, Color.Snow, 1);
                    break;
                case HeaderColumnsHeader.none:
                case HeaderColumnsHeader.columns:
                    MakeGrid.Fill2(a, temp, Color.Snow, 0);
                    break;
            }
        }

        public static void Fill(Grid a, double[] data)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            MakeGrid.Fill(a, temp, Color.Snow, a.FixedRows);
        }

        public static void Fill(Grid a, int[] data)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            MakeGrid.Fill(a, temp, Color.Snow, a.FixedRows);
        }

        public static void Fill(Grid a, string[] data)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            MakeGrid.Fill(a, temp, Color.Snow, a.FixedRows);
        }

        public static void Fill(Grid a, double[] data, int index)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            MakeGrid.Fill2(a, temp, Color.Snow, index);
        }

        public static void Fill(Grid a, int[] data, int index)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            MakeGrid.Fill(a, temp, Color.Snow, index);
        }

        public static void Fill(Grid a, string[] data, int index)
        {
            object[] temp = new object[data.Length];
            data.CopyTo(temp, 0);

            MakeGrid.Fill(a, temp, Color.Snow, index);
        }
        #endregion

        #region Returning Values
        public static double[,] Return(Grid a)
        {
            double[,] data = new double[a.RowsCount - a.FixedRows, a.ColumnsCount - a.FixedColumns];

            for (int r = a.FixedRows; r < a.RowsCount; r++)
            {
                for (int c = a.FixedColumns; c < a.ColumnsCount; c++)
                {
                    data[r - a.FixedRows, c - a.FixedColumns] = double.Parse(a[r, c].Value.ToString());
                }
            }

            return data;
        }
        #endregion

        #region Insert new rows
        public static void AddRows(Grid a, params object[] data)
        {
            int r = a.RowsCount;
            a.Rows.Insert(r);
            a[r, 0] = new SourceGrid.Cells.Cell(r, typeof(string));

            for (int i = a.FixedColumns; i < a.ColumnsCount; i++)
            {
                a[r, i] = new SourceGrid.Cells.Cell(data[i - a.FixedColumns], typeof(string));
            }
        }

        public static void AddRows(Grid a,int index, params object[] data)
        {
            int r = index;
            a.Rows.Insert(r);
            a[r, 0] = new SourceGrid.Cells.Cell(r, typeof(string));

            for (int i = a.FixedColumns; i < a.ColumnsCount; i++)
            {
                a[r, i] = new SourceGrid.Cells.Cell(data[i - a.FixedColumns], typeof(string));
            }
        }

        #endregion
    }
}
