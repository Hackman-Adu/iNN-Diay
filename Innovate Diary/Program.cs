using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Innovate_Diary.Properties;
using System.Diagnostics;

namespace Innovate_Diary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings st = new Settings();
            Process[] pros = Process.GetProcessesByName(Application.ProductName);
            if(pros.Length>1)
            {

            }
            else
            {
                if(st.Rpassword=="Yes")
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new EnterPassword());
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            }
        }
    }
}
