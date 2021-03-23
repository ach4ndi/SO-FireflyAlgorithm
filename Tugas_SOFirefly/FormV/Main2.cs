using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Andi;
using ComponentFactory.Krypton.Toolkit;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using TugasSOFirefly.Library;
using WeifenLuo.WinFormsUI.Docking;
using Image = System.Drawing.Image;
using LineStyle = OxyPlot.LineStyle;

namespace TugasSOFirefly.FormV
{
    [System.ComponentModel.DataObject()]
    public partial class Main2 : DockContent
    {
        #region Form Parent dan Event Thread Flags
        private MainForm thismain;
        public static Thread threadsatu;

        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        #endregion

        #region Variabel Persiapan
        private double[,] popmemberawal;
        private int dbsave = 1;
        private double[] X;
        private double[] X0;
        private double[] Y;
        private double[] Y0;
        private double[] I;
        private double[] I0;
        Random rnd = new Random();
        public static laporanakhir akhir = new laporanakhir();
        datapopulasiiterasi laporan = new datapopulasiiterasi();
        laporanpop laporanpopupasi = new laporanpop();
        #endregion

        public Main2(MainForm main)
        {
            InitializeComponent();
            
            #region Lainnya
            generatecomboboxmember();
            thismain = main;

            cmb_stop.SelectedIndex = 0;
            inp_fungsi.SelectedIndex = 0;
            createGridPopulationMember();
            #endregion

            #region set variable default

            num_xmin.Value = FunctionVariabel.Xmin;
            num_xmax.Value = FunctionVariabel.Xmax;
            num_ymax.Value = FunctionVariabel.Ymax;
            num_ymin.Value = FunctionVariabel.Ymin;
            #endregion

            #region events
            inp_alpha.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            inp_beta.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            inp_delta.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            inp_gamma.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            kryptonTextBox1.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            //kryptonTextBox2.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            //kryptonTextBox3.KeyPress += GlobalConfiguration.setKeyPressEventOnlyNumber;
            inp_beta.Leave += Leaves;
            inp_delta.Leave += Leaves;
            inp_gamma.Leave += Leaves;

            kryptonTextBox1.TextChanged += kryptonTextBox1_TextChanged;
            kryptonTextBox2.TextChanged += kryptonTextBox1_TextChanged;
            #endregion
        }

        void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(kryptonTextBox1.Text);
                double b = double.Parse(kryptonTextBox2.Text);
                kryptonTextBox3.Text = "" + FunctionVariabel.NumberFormat(getfunctionvalue(inp_fungsi.SelectedIndex, a, b));
            }
            catch
            {
                kryptonTextBox3.Text = "Ada penuliasn yang salah";
            }
            
        }

        #region event listener
        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            Cetak();
            //Reporting ne = new Reporting();
            //ne.Report(new Report1(akhir));
            //ne.ShowDialog(this);
        }
        
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            kryptonButton2.Enabled = false;
            kryptonButton4.Enabled = false;
            kryptonButton5.Enabled = true;
            kryptonButton7.Enabled = true;

            _pauseEvent.Reset();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            kryptonButton2.Enabled = true;
            kryptonButton4.Enabled = false;
            kryptonButton5.Enabled = false;
            kryptonButton7.Enabled = false;
            thismain.statusbar1.Text = "[dihentikan]";
            _shutdownEvent.Set();

            // Make sure to resume any paused threads
            _pauseEvent.Set();

            // Wait for the thread to exit
            threadsatu.Join();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            kryptonButton2.Enabled = false;
            kryptonButton4.Enabled = true;
            kryptonButton5.Enabled = true;
            kryptonButton7.Enabled = false;

            _pauseEvent.Set();
        }

        private void inp_fungsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inp_fungsi.SelectedIndex == 0)
            {
                num_xmin.Enabled = false;
                num_ymin.Enabled = false;
            }
            else
            {
                num_xmin.Enabled = true;
                num_ymin.Enabled = true;
            }
        }

        private void rnd_inp1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            inp_alpha.SelectedIndex = rnd.Next(0, inp_alpha.Items.Count - 1);
            //inp_beta.SelectedIndex = rnd.Next(0, inp_beta.Items.Count - 1);
            inp_delta.SelectedIndex = rnd.Next(0, inp_delta.Items.Count - 1);
            inp_gamma.SelectedIndex = rnd.Next(0, inp_gamma.Items.Count - 1);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            createGridPopulationMember();
        }

        private void num_xmin_ValueChanged(object sender, EventArgs e)
        {
            FunctionVariabel.Xmin = (int)num_xmin.Value;
        }

        private void num_ymin_ValueChanged(object sender, EventArgs e)
        {
            FunctionVariabel.Ymin = (int)num_ymin.Value;
        }

        private void num_xmax_ValueChanged(object sender, EventArgs e)
        {
            FunctionVariabel.Xmax = (int)num_xmax.Value;
            if (inp_fungsi.SelectedIndex == 0)
            {
                FunctionVariabel.Xmin = -(int)num_xmax.Value;
                num_xmin.Value = -(int) num_xmax.Value;
            }
        }

        private void num_ymax_ValueChanged(object sender, EventArgs e)
        {
            FunctionVariabel.Ymax = (int)num_ymax.Value;
            if (inp_fungsi.SelectedIndex == 0)
            {
                FunctionVariabel.Ymin = -(int)num_ymax.Value;
                num_ymin.Value = -(int)num_ymax.Value;
            }
        }

        private void inp_alpha_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireflyVariabel.Alpha = double.Parse(inp_alpha.Text);
        }

        private void inp_beta_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireflyVariabel.Beta = double.Parse(inp_beta.Text);
        }

        private void inp_gamma_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireflyVariabel.Gamma = double.Parse(inp_gamma.Text);
        }

        private void inp_delta_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireflyVariabel.Delta = double.Parse(inp_delta.Text);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copytoclipboard();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            OpenFile.getOpen(grid_popmember);

            pop_member.Value = grid_popmember.RowsCount - 1;
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (pop_member.Value < 30)
            {
                Random rnd = new Random();
                object[] rndNum = new object[2];

                for (int i = 0; i < 2; i++)
                {
                    rndNum[i] = rnd.NextDouble(FunctionVariabel.Xmin, FunctionVariabel.Xmax).Rounding(3);
                }

                pop_member.Value += 1;
                MakeGrid.AddRows(grid_popmember, rndNum);
            }
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonCheckBox1.Checked)
            {
                kryptonCheckBox4.Checked = false;
                kryptonCheckBox4.Enabled = false;
            }
            else
            {
                kryptonCheckBox4.Enabled = true;
            }
        }
        #endregion

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            #region inputan awal
            database.sql_con.Open();
            //object[] datadbiterasi = new object[7];

            int batastoleransiminimal = (int) kryptonNumericUpDown1.Value;
            int settoleransi = 0;
            int batasinitial = 0;

            switch (inp_fungsi.SelectedIndex)
            {
                case 0:
                    batasinitial = 3;
                    break;
                case 1:
                case 2:
                case 3:
                    batasinitial = 0;
                    break;
                default:
                    batasinitial = 0;
                    break;
            }

            kryptonButton2.Enabled = false;
            kryptonButton4.Enabled = true;
            kryptonButton5.Enabled = true;
            kryptonButton7.Enabled = false;

            FireflyVariabel.Alpha = double.Parse(inp_alpha.Text);
            FireflyVariabel.Beta = double.Parse(inp_beta.Text); //beta0
            FireflyVariabel.Delta = double.Parse(inp_delta.Text);
            FireflyVariabel.Gamma = double.Parse(inp_gamma.Text);

            if (dbsave == 1)
            {
                object[] datadb = new object[8];
                datadb[0] = DateTime.Now.ToString();
                datadb[2] = cmb_stop.Text;
                datadb[4] = FireflyVariabel.Alpha.ToString();
                datadb[5] = FireflyVariabel.Beta.ToString();
                datadb[6] = FireflyVariabel.Gamma.ToString();
                datadb[7] = FireflyVariabel.Delta.ToString();

                datadb[1] = "";
                datadb[3] = 0;
                database.InsertKeterangan(datadb);
            }
            popmemberawal = MakeGrid.Return(grid_popmember);

            int value = (int)val_stop.Value;
            int banyakpopulasi = (int)pop_member.Value;

            thismain.progress.Maximum = value;
            X = new double[banyakpopulasi];
            Y = new double[banyakpopulasi];

            int tipeberhenti = cmb_stop.SelectedIndex;
            int tipefungsi = inp_fungsi.SelectedIndex;
            bool statusjeda = kryptonCheckBox2.Checked;
            bool statusupdatepopulasidata = kryptonCheckBox1.Checked;
            bool statusplotpopulasi = kryptonCheckBox3.Checked;
            int[] rank = new int[banyakpopulasi];
            #endregion

            #region Kebutuhan dalam Thread
            int i = 0, j = 0;

            _shutdownEvent = new ManualResetEvent(false);
            _pauseEvent = new ManualResetEvent(true);

            DateTime dt = DateTime.Now;
            //TimeSpan ts = DateTime.Now - dt;
            #endregion

            #region Menghapus variable laporan dan membuat grid
            akhir.Clear();
            laporanpopupasi.Clear();
            MakeGrid.Build(grid1, banyakpopulasi, 4, MakeGrid.HeaderColumnsHeader.none);
            //MakeGrid.Build(grid2, 1, 4, MakeGrid.HeaderColumnsHeader.none);
            #endregion

            movedpopinput(banyakpopulasi); // memindahkan variabel
            
            threadsatu = new Thread(
            new ThreadStart(() =>
            {
                while (j<4)
                {
                    #region 1. Input Fungsi Fitness
                    double[] zn = new double[banyakpopulasi];

                    for (int k = 0; k < banyakpopulasi; k++)
                    {
                        zn[k] = FunctionVariabel.NumberFormat(getfunctionvalue(tipefungsi, X[k], Y[k]));
                    }

                    rank = FireflyVariabel.RankingIndexList(zn);
                    I = zn;
                    I = FireflyVariabel.RankingValueList(zn);
                    #endregion

                    #region 2. ranking X Y dan duplikasi variabel
                    X = FireflyVariabel.SortingValueBy(X, rank);
                    Y = FireflyVariabel.SortingValueBy(Y, rank);

                    I0 = I; // duplikasi
                    X0 = X; // duplikasi
                    Y0 = Y; // duplikasi
                    #endregion

                    double r = 0, beta =0;

                    #region 3. Movement
                    for (int u = 0; u < banyakpopulasi; u++)
                    {
                        for (int v = 0; v < banyakpopulasi; v++)
                        {
                            if (I[u] > I0[v])
                            {
                                r = FireflyVariabel.Jarak(X[u], X0[v], Y[u], Y0[v]);
                                beta = FireflyVariabel.Beta1(FireflyVariabel.Beta, r, FireflyVariabel.Gamma);

                                /*xn(i)=xn(i).*(1-beta)+xo(j).*beta+alpha.*(rand-0.5);
                                yn(i)=yn(i).*(1-beta)+yo(j).*beta+alpha.*(rand-0.5);*/

                                X[u] = FireflyVariabel.Movement(X[u], X0[v], beta, FireflyVariabel.Alpha, rnd,
                                    FunctionVariabel.Xmin, FunctionVariabel.Xmax);
                                //MessageBox.Show(r+" / "+beta.ToString());
                                Y[u] = FireflyVariabel.Movement(Y[u], Y0[v], beta, FireflyVariabel.Alpha, rnd,
                                    FunctionVariabel.Ymin, FunctionVariabel.Ymax);
                            }
                        }
                    }

                    kryptonLabel12.BeginInvoke(new Action(() =>
                    {
                        kryptonLabel12.Text = "Min -> " + FunctionVariabel.NumberFormat(I0[0]) + " [" + FunctionVariabel.NumberFormat(X0[0]) + "," + FunctionVariabel.NumberFormat(Y0[0]) + "]";
                    }));

                    if (FunctionVariabel.NumberFormat(I0.Min()) <= batasinitial)
                    {
                        settoleransi += 1;
                    }
                    else
                    {
                        settoleransi = 0;
                    }

                    kryptonLabel13.BeginInvoke(new Action(() =>
                    {
                        kryptonLabel13.Text = settoleransi +"";
                    }));

                    X = FireflyVariabel.FindRange(X, FunctionVariabel.Xmin, FunctionVariabel.Xmax);
                    Y = FireflyVariabel.FindRange(Y, FunctionVariabel.Ymin, FunctionVariabel.Ymax);
                    #endregion

                    #region 4. Update Alpha
                    FireflyVariabel.Alpha = FireflyVariabel.Alpha * FireflyVariabel.Delta;
                    #endregion

                    #region A. Update Plot dan Grid


                    dataakhir data = new dataakhir();
                    data.Index = (i + 1);
                    data.minimum = I.Min();
                    data.maximum = I.Max();
                    data.average = I.Average();
                    akhir.Add(data);

                    grid1.BeginInvoke(new Action(() =>
                    {
                        if (statusupdatepopulasidata)
                        {
                            MakeGrid.Fill(grid1, rank, 0);
                            MakeGrid.Fill(grid1, X0, 1);
                            MakeGrid.Fill(grid1, Y0, 2);
                            MakeGrid.Fill(grid1, I0, 3);
                        }
                    }));

                    plot1.BeginInvoke(new Action(() =>
                    {
                        if (statusplotpopulasi)
                        {
                            setplot(plot1, X0, Y0);
                        }
                        else
                        {
                            plot1.Model = null;
                        }
                        
                    }));
                    #endregion

                    #region B. pause state
                    _pauseEvent.WaitOne(Timeout.Infinite);
                    #endregion

                    #region C. update status bar
                    thismain.statusStrip1.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (tipeberhenti == 0)
                            {
                                thismain.progress.Value = (i + 1);
                                thismain.statusbar1.Text = "Iterasi ke " + (i + 1) + " dengan jeda " + GlobalConfiguration.JedaThread + " milidetik";
                            }
                            else if (tipeberhenti == 1)
                            {
                                thismain.progress.Value = ((int)(value - (DateTime.Now - dt).TotalSeconds));
                                thismain.statusbar1.Text = "Kurang " + ((int)(value - (DateTime.Now - dt).TotalSeconds)) + " detik lagi | Iterasi Ke : " + i + " dengan jeda " + GlobalConfiguration.JedaThread + " mili detik";
                            }
                            else
                            {
                                thismain.progress.Value = ((int)(value - (DateTime.Now - dt).TotalMilliseconds));
                                thismain.statusbar1.Text = "Kurang " + ((int)(value - (DateTime.Now - dt).TotalMilliseconds)) + " milidetik lagi | Iterasi Ke : " + i + " dengan jeda " + GlobalConfiguration.JedaThread + " mili detik";
                            }
                        }
                        catch
                        {
                            
                        }
                        
                    }));
                    #endregion

                    #region D. kondisi berhenti
                    if (_shutdownEvent.WaitOne(0))
                        break;

                    if (settoleransi >= batastoleransiminimal)
                    {
                        break;
                    }

                    if(statusjeda){
                        Thread.Sleep(GlobalConfiguration.JedaThread);
                    }

                    if (tipeberhenti == 0)
                    {
                        if (i >= value - 1)
                        {
                            break;
                        }
                    }
                    else if (tipeberhenti == 1)
                    {
                        if ((DateTime.Now - dt).TotalSeconds >= value)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if ((DateTime.Now - dt).TotalMilliseconds >= value)
                        {
                            break;
                        }
                    }
                    #endregion

                    i++;
                }

                if (dbsave == 1)
                {
                    database.UpdateKeterangan(DateTime.Now.ToString(), database.GetLastID(), "date_end");
                    database.UpdateKeterangan(i, database.GetLastID(), "iterasi_end");

                    for (int k = 0; k < akhir.Count; k++)
                    {
                        database.InsertAkhir(database.GetLastID(), k, akhir[k].minimum, X0[X0.Length - 1], Y0[Y0.Length - 1], akhir[k].maximum, akhir[k].average);
                    }
                }

                #region pesan penutup
                grid1.BeginInvoke(new Action(() =>
                {
                    if (kryptonCheckBox4.Checked)
                    {
                        MakeGrid.Fill(grid1, rank, 0);
                        MakeGrid.Fill(grid1, X, 1);
                        MakeGrid.Fill(grid1, Y, 2);
                        MakeGrid.Fill(grid1, I, 3);
                    }
                }));

                kryptonComboBox1.BeginInvoke(new Action(() =>
                {
                    kryptonComboBox1.SelectedIndex = 0;
                }));
                kryptonComboBox1.BeginInvoke(new Action(() =>
                {
                kryptonComboBox2.SelectedIndex = 0;
                }));
                thismain.statusStrip1.BeginInvoke(new Action(() =>
                {
                    if (tipeberhenti == 0)
                    {
                        thismain.statusbar1.Text = "[selesai] berhenti di iterasi " + (i + 1) + " dengan waktu " + (DateTime.Now - dt).Duration().ToString().Substring(0,8);
                    }
                }));

                kryptonButton2.BeginInvoke(new Action(() =>
                {
                    kryptonButton2.Enabled = true;
                }));

                kryptonButton4.BeginInvoke(new Action(() =>
                {
                    kryptonButton4.Enabled = false;
                }));

                kryptonButton5.BeginInvoke(new Action(() =>
                {
                    kryptonButton5.Enabled = false;
                }));

                kryptonButton7.BeginInvoke(new Action(() =>
                {
                    kryptonButton7.Enabled = false;
                }));

                navpagetab3.BeginInvoke(new Action(() =>
                {
                    navpagetab3.Enabled = true;
                }));
                database.sql_con.Close();
                #endregion
            }));

            threadsatu.Start();
        }

        #region Fungsi Pembantu
        private double getfunctionvalue(int tipefungsi, double X, double Y)
        {
            switch (tipefungsi)
            {
                case 0:
                    return FunctionVariabel.goldsteinprice(X, Y);
                case 1:
                    return Andi.Fungsi.Bohachevsky.getValue(X, Y, Enums.BohaType.Boha1);
                case 2:
                    return Andi.Fungsi.Bohachevsky.getValue(X, Y, Enums.BohaType.Boha2);
                case 3:
                    return Andi.Fungsi.Bohachevsky.getValue(X, Y, Enums.BohaType.Boha3);
                case 4:
                    return FunctionVariabel.fourpeaks(X, Y);
                default:
                    return 0;
            }
        }

        private void copytoclipboard()
        {
            string clipdata = "";

            for (int i = 0; i < popmemberawal.GetLongLength(0); i++)
            {
                for (int j = 0; j < popmemberawal.GetLongLength(1); j++)
                {
                    clipdata += popmemberawal[i, j];

                    if (j < popmemberawal.GetLongLength(1) - 1)
                    {
                        clipdata += ",";
                    }
                }
                if (i < popmemberawal.GetLongLength(0) - 1)
                {
                    clipdata += Environment.NewLine;
                }
            }

            Clipboard.SetText(clipdata);
        }

        private void generatecomboboxmember()
        {
            for (int i = 1; i <= 1000; i++)
            {
                inp_alpha.Items.Add((double)i / 1000);
                inp_gamma.Items.Add((double)i / 1000);

                if (i >= 500)
                {
                    inp_beta.Items.Add((double)i / 1000);
                }

                if (i >= 700)
                {
                    inp_delta.Items.Add((double)i / 1000);
                }
            }
        }

        private void createGridPopulationMember()
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

        private void setplot(Plot a, double[] x, double[] y, string namaseries = "")
        {
            a.AutoSize = true;
            a.BackColor = Color.White;
            
            var model = new PlotModel(namaseries) { };

            var s1 = new ScatterSeries();
            var s2 = new ScatterSeries();
            s1 = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 1.7,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 2.4
            };

            s2 = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 0.1,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 0.1
            };

            for (int i = 0; i < x.Length; i++)
            {
                s1.Points.Add(new ScatterPoint(x[i], y[i]));
            }

            s2.Points.Add(new ScatterPoint(-2, -2));
            s2.Points.Add(new ScatterPoint(2, 2));

            model.IsLegendVisible = false;
            model.Series.Add(s1);
            model.Series.Add(s2);

            var linearAxis2 = new LinearAxis();
            linearAxis2.MajorGridlineStyle = LineStyle.Solid;
            linearAxis2.MinorGridlineStyle = LineStyle.Dot;
            linearAxis2.Position = AxisPosition.Bottom;
            linearAxis2.PositionAtZeroCrossing = true;
            linearAxis2.TickStyle = OxyPlot.Axes.TickStyle.Crossing;
            model.Axes.Add(linearAxis2);

            var linearAxis1 = new LinearAxis();
            linearAxis1.MajorGridlineStyle = LineStyle.Solid;
            linearAxis1.MinorGridlineStyle = LineStyle.Dot;
            linearAxis1.PositionAtZeroCrossing = true;
            linearAxis1.TickStyle = OxyPlot.Axes.TickStyle.Crossing;
            model.Axes.Add(linearAxis1);

            a.Model = model;
        }

        //laporanpopupasi

        private void setplot(Plot a, string namaseries = "")
        {
            List<dataakhir> tempdata = null;

            switch (kryptonComboBox1.SelectedIndex)
            {
                case 0:
                    tempdata = akhir.OrderBy(x => x.Index).ToList();
                    break;
                case 1:
                    tempdata = akhir.OrderBy(x => x.minimum).ToList();
                    break;
                case 2:
                    tempdata = akhir.OrderBy(x => x.maximum).ToList();
                    break;
                case 3:
                    tempdata = akhir.OrderBy(x => x.average).ToList();
                    break;
                case 4:
                    tempdata = akhir.OrderByDescending(x => x.minimum).ToList();
                    break;
                case 5:
                    tempdata = akhir.OrderByDescending(x => x.maximum).ToList();
                    break;
                case 6:
                    tempdata = akhir.OrderByDescending(x => x.average).ToList();
                    break;
            }

            //a.AutoSize = true;
            a.BackColor = Color.White;

            var model = new PlotModel(namaseries) { };

            var s1 = new LineSeries();
            var s2 = new LineSeries();
            var s3 = new LineSeries();

            s1 = new LineSeries("Minimum")
            {
                LineStyle =  LineStyle.Solid,
                Color = OxyColors.Black,
                MarkerType = MarkerType.Cross,
                MarkerSize = 1.2,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 1
            };

            s2 = new LineSeries("Maximum")
            {
                LineStyle = LineStyle.Solid,
                Color = OxyColors.DeepPink,
                MarkerType = MarkerType.Cross,
                MarkerSize = 1.2,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 1
            };

            s3 = new LineSeries("Average")
            {
                LineStyle = LineStyle.Solid,
                Color = OxyColors.DeepSkyBlue,
                MarkerType = MarkerType.Cross,
                MarkerSize = 1.2,
                MarkerStroke = OxyColors.Black,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 1
            };

            model.IsLegendVisible = true;

            if (kryptonComboBox2.SelectedIndex == 0 || kryptonComboBox2.SelectedIndex == 1)
            {
                for (int i = 0; i < tempdata.Count(); i++)
                {
                    s1.Points.Add(new DataPoint(i, tempdata[i].minimum));
                }
                model.Series.Add(s1);
            }
            if (kryptonComboBox2.SelectedIndex == 0 || kryptonComboBox2.SelectedIndex == 2)
            {
                for (int i = 0; i < tempdata.Count(); i++)
                {
                    s2.Points.Add(new DataPoint(i, tempdata[i].maximum));
                }
                model.Series.Add(s2);
            }
            
            if (kryptonComboBox2.SelectedIndex == 0 || kryptonComboBox2.SelectedIndex == 3)
            {
                for (int i = 0; i < tempdata.Count(); i++)
                {
                    s3.Points.Add(new DataPoint(i, tempdata[i].average));
                }
                model.Series.Add(s3);
            }
            
            a.Model = model;
        }

        private void Cetak()
        {
            ReportViewer dummyDoc = new ReportViewer();

            List<dataakhir> tempdata = null;

            switch (kryptonComboBox1.SelectedIndex)
            {
                case 0:
                    tempdata = akhir.OrderBy(x => x.Index).ToList();
                    break;
                case 1:
                    tempdata = akhir.OrderBy(x => x.minimum).ToList();
                    break;
                case 2:
                    tempdata = akhir.OrderBy(x => x.maximum).ToList();
                    break;
                case 3:
                    tempdata = akhir.OrderBy(x => x.average).ToList();
                    break;
                case 4:
                    tempdata = akhir.OrderByDescending(x => x.minimum).ToList();
                    break;
                case 5:
                    tempdata = akhir.OrderByDescending(x => x.maximum).ToList();
                    break;
                case 6:
                    tempdata = akhir.OrderByDescending(x => x.average).ToList();
                    break;
            }

            Report1 datalaporan = new Report1(tempdata, laporanpopupasi);

            datalaporan.addAlpha(inp_alpha.Text+" ("+FireflyVariabel.Alpha.Rounding(GlobalConfiguration.Digit) + ")");
            datalaporan.addBeta(FireflyVariabel.Beta + "");
            datalaporan.addDelta(FireflyVariabel.Delta + "");
            datalaporan.addFungsi(inp_fungsi.Text + " [" + FunctionVariabel.Xmin + " " + FunctionVariabel.Xmax + ";" + FunctionVariabel.Ymin + " " + FunctionVariabel.Ymax+"]");
            datalaporan.addGamma(FireflyVariabel.Gamma + "");
            datalaporan.addJumlahPopulasi(pop_member.Value + "");
            using (var stream = new MemoryStream())
            {
                var pngExporter = new PngExporter();
                pngExporter.Export(plot2.Model, stream);
                datalaporan.addPicture(Image.FromStream(stream));
            }
            
            dummyDoc.Report(datalaporan);
            if (thismain.dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = thismain;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(thismain.dockPanel1);
        }

        private void movedpopinput(int banyakpopulasi)
        {
            for (int k = 0; k < banyakpopulasi; k++)
            {
                X[k] = popmemberawal[k, 0];
                Y[k] = popmemberawal[k, 1];

                datapopulasiawal datao = new datapopulasiawal();

                datao.X = X[k];
                datao.Y = Y[k];
                datao.Index = (k + 1);

                laporanpopupasi.Add(datao);
            }
        }
        #endregion

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setplot(plot2);

            MakeGrid.Build(grid3, akhir.Count,4,"#","iterasi","Min","Max","Average");

            List<dataakhir> tempdata = null;

            switch (kryptonComboBox1.SelectedIndex)
            {
                case 0:
                    tempdata = akhir.OrderBy(x => x.Index).ToList();
                    break;
                case 1:
                    tempdata = akhir.OrderBy(x => x.minimum).ToList();
                    break;
                case 2:
                    tempdata = akhir.OrderBy(x => x.maximum).ToList();
                    break;
                case 3:
                    tempdata = akhir.OrderBy(x => x.average).ToList();
                    break;
                case 4:
                    tempdata = akhir.OrderByDescending(x => x.minimum).ToList();
                    break;
                case 5:
                    tempdata = akhir.OrderByDescending(x => x.maximum).ToList();
                    break;
                case 6:
                    tempdata = akhir.OrderByDescending(x => x.average).ToList();
                    break;
            }

            double[,] data = new double[akhir.Count,4];
            for (int i = 0; i < akhir.Count; i++)
            {
                data[i, 0] = tempdata[i].Index;
                data[i, 1] = tempdata[i].minimum;
                data[i, 2] = tempdata[i].maximum;
                data[i, 3] = tempdata[i].average;
            }

            MakeGrid.Fill(grid3,data);
            //MakeGrid.Fill(akhir.ToArray());
        }

        private void Leaves(object sender, EventArgs e)
        {
            if (double.Parse(((KryptonComboBox) sender).Text) > 1)
            {
                ((KryptonComboBox) sender).Text = "1";
                thismain.statusbar1.Text = ((KryptonComboBox) sender).Name + " lebih dari 1";
            }
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setplot(plot2);

            MakeGrid.Build(grid3, akhir.Count, 4, "#", "iterasi", "Min", "Max", "Average");

            List<dataakhir> tempdata = null;

            switch (kryptonComboBox1.SelectedIndex)
            {
                case 0:
                    tempdata = akhir.OrderBy(x => x.Index).ToList();
                    break;
                case 1:
                    tempdata = akhir.OrderBy(x => x.minimum).ToList();
                    break;
                case 2:
                    tempdata = akhir.OrderBy(x => x.maximum).ToList();
                    break;
                case 3:
                    tempdata = akhir.OrderBy(x => x.average).ToList();
                    break;
                case 4:
                    tempdata = akhir.OrderByDescending(x => x.minimum).ToList();
                    break;
                case 5:
                    tempdata = akhir.OrderByDescending(x => x.maximum).ToList();
                    break;
                case 6:
                    tempdata = akhir.OrderByDescending(x => x.average).ToList();
                    break;
            }

            double[,] data = new double[akhir.Count, 4];
            for (int i = 0; i < akhir.Count; i++)
            {
                data[i, 0] = tempdata[i].Index;
                data[i, 1] = tempdata[i].minimum;
                data[i, 2] = tempdata[i].maximum;
                data[i, 3] = tempdata[i].average;
            }

            MakeGrid.Fill(grid3, data);
        }

        private void kryptonCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (kryptonCheckBox5.Checked)
            {
                dbsave = 1;
            }
            else
            {
                dbsave = 0;
            }
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            X = new double[(int)pop_member.Value];
            Y = new double[(int)pop_member.Value];

            for (int k = 0; k < (int) pop_member.Value; k++)
            {
                X[k] = popmemberawal[k, 0];
                Y[k] = popmemberawal[k, 1];
            }

            setplot(plot1, X, Y);
        }
    }
}
