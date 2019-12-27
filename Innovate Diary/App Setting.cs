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
using System.Globalization;
using Bunifu.Framework.UI;
using System.Net.Mail;

namespace Innovate_Diary
{
    public partial class App_Setting : Form
    {
        Settings st = new Settings();
        public App_Setting()
        {
            InitializeComponent();
           comboBox1.Items.Add("Gmail");
           comboBox1.Items.Add("Yahoo");
           comboBox1.Items.Add("Hotmail");
           comboBox1.Items.Add("Outlook");
           comboBox1.Items.Add("Office 365");
            
        }

        

        private void App_Setting_Load(object sender, EventArgs e)
        {
            EmaiA.Text = st.EmailAddress;
            EmaiP.Text = st.EmailPassword;
            SysP.Text = st.SystemP;
            Cp.Text = SysP.Text;
            fullname.Text = st.FullName;
            SQ.Text = st.Semail;
            
            if(st.Rpassword=="Yes")
            {
                gunaCheckBox2.Checked = true;
            }
            if(st.IncludeTags==true)
            {
                gunaCheckBox1.Checked = true;
            }
            comboBox1.Text = st.AccountType;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                MailAddress add = new MailAddress(EmaiA.Text);
                st.EmailAddress = add.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Email Address" + Environment.NewLine + ex.Message);
                return;
            }
            st.Semail = SQ.Text.ToString();
            
            st.FullName = fullname.Text.ToUpper();
            
            st.EmailPassword = EmaiP.Text;
            st.SystemP = SysP.Text.ToString();
            st.EmailSettingsC = true;
            st.AccountType = comboBox1.Text;
            
            
            
            if(gunaCheckBox2.Checked==true)
            {
                st.Rpassword = "Yes";
            }
            else
            {
                st.Rpassword = "No";
            }
            if(gunaCheckBox1.Checked==true)
            {
                st.IncludeTags = true;
            }
            else
            {
                st.IncludeTags = false;
            }

            foreach(Control c in groupBox1.Controls)
            {
                if(c is BunifuMetroTextbox)
                {
                    BunifuMetroTextbox t = c as BunifuMetroTextbox;
                    if(string.IsNullOrWhiteSpace(t.Text))
                    {
                        MessageBox.Show("All fields are required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            foreach (Control c in groupBox2.Controls)
            {
                if (c is BunifuMetroTextbox)
                {
                    BunifuMetroTextbox t = c as BunifuMetroTextbox;
                    if (string.IsNullOrWhiteSpace(t.Text))
                    {
                        MessageBox.Show("All fields are required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            if(string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Account type cannot be null", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
           
            if(SysP.Text.ToUpper().ToString()!=Cp.Text.ToUpper().ToString())
            {
                MessageBox.Show("Password do not match", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            st.Save();
            MessageBox.Show("Settings save successfully\nRESTART THE APPLICATION TO APPLY CHANGES", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

       
    }
}
