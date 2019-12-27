using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Innovate_Diary.Properties;
using Microsoft.VisualBasic;

namespace Innovate_Diary
{
    public partial class EnterPassword : Form
    {
        Settings st = new Settings();
        public EnterPassword()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void EnterPassword_Load(object sender, EventArgs e)
        {
            if(st.FullName!=string.Empty)
            {
                label4.Text = "[ " + st.FullName + " ]";

            }
            ToT.isPassword = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(ToT.Text==st.SystemP)
            {
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong password", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ToT.Focus();
            }
        }

        private void ToT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                button2_Click(ToT, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = Interaction.InputBox("Enter your ID", Application.ProductName);
            if(email!=st.Semail)
            {
                MessageBox.Show("Wrong ID", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string newpassword1 = Interaction.InputBox("Enter your new password", Application.ProductName);
            string newpassword2 = Interaction.InputBox("Confirm password", Application.ProductName);
            if(newpassword1.ToUpper()!=newpassword2.ToUpper())
            {
                MessageBox.Show("Password do not match", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                
            }
            else
            {
                st.SystemP = newpassword2;
                st.Save();
                MessageBox.Show("Password Successfully Updated", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
    }
}
