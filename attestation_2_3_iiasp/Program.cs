using System;
using System.Windows.Forms;
using attestation_2_3_iiasp.Garden_;

namespace attestation_2_3_iiasp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GardenForm());
        }
    }
}
