using System;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace TugasSOFirefly.FormV
{
    public partial class Test_Threading : DockContent
    {
        public Test_Threading()
        {
            InitializeComponent();
        }

        private Thread thr1;
        private bool pausestate = false;
        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            _shutdownEvent = new ManualResetEvent(false);
            _pauseEvent = new ManualResetEvent(true);

            progressBar1.Maximum = 1000;

            thr1 = new Thread(
            new ThreadStart(() =>
            {
                int m = 0;

                while (m < 1000)
                {
                    _pauseEvent.WaitOne(Timeout.Infinite);

                    if (_shutdownEvent.WaitOne(0))
                        break;
                    else
                    {
                        Thread.Sleep(20);
                    }

                    progressBar1.BeginInvoke(
                        new Action(() =>
                        {
                            progressBar1.Value = m;
                        }
                    ));

                    m++;
                }

                /*
                for (int n = 0; n < 1000; n++)
                {
                    _pauseEvent.WaitOne(Timeout.Infinite);

                    if (_shutdownEvent.WaitOne(0))
                        break;
                    else
                    {
                        Thread.Sleep(20);
                    }

                    progressBar1.BeginInvoke(
                        new Action(() =>
                        {
                            progressBar1.Value = n;
                        }
                    ));
                }
                */
                MessageBox.Show("Thread completed!");
                progressBar1.BeginInvoke(
                    new Action(() =>
                    {
                        progressBar1.Value = 0;
                    }
                ));
            }
            ));
            thr1.Start();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            _pauseEvent.Reset();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            _pauseEvent.Set();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            _shutdownEvent.Set();

            // Make sure to resume any paused threads
            _pauseEvent.Set();

            // Wait for the thread to exit
            thr1.Join();
        }
    }
}
