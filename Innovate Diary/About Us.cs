using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Innovate_Diary
{
    public partial class About_Us : Form
    {
        public About_Us()
        {
            InitializeComponent();
            //Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            try
            {
                gunaLabel1.Text = "System Name: " + Environment.MachineName;
            }
            catch(Exception)
            {

            }

        }

       

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("You are about to visit our homepage\nClick Yes to continue", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.Yes)
            {
                Process.Start("http://www.itworldinnovate.com");
            }
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("control", "system");
        }

       
        

        private void gunaLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            string file = "ReadMe.txt";
            try
            {
                Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        
    }
}
