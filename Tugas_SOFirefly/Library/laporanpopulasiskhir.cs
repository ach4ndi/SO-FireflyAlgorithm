using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugasSOFirefly.Library
{
    public class datapopulasiawal
    {
        public int Index { get; set; }

        public double Y { get; set; }

        public double X { get; set; }
    }

    [System.ComponentModel.DataObject()]
    public class laporanpop : List<datapopulasiawal>
    {


    }
}
