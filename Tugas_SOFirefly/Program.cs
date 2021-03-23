using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TugasSOFirefly
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] //[STAhread]
        static void Main()
        {
            Andi.Extension.Utils.MemoryManager.AttachApp();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
