using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Innovate_Diary.Properties;
using Bunifu.Framework.UI;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Innovate_Diary
{
    public partial class Send_Email : Form
    {
        Settings st = new Settings();
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        OleDbDataAdapter adapt;
        string connstring;
        string[] recipientsT;
        string[] recipientsBc;
        string[] recipientCt;
        MailSending snd;
        public Send_Email()
        {
            InitializeComponent();


            string dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\iNN DIARY";
            if (!Directory.Exists(dbpath))
            {
                Directory.CreateDirectory(dbpath);

            }
            AppDomain.CurrentDomain.SetData("DataDirectory", dbpath);
            connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\iNN_DIARY.mdb";
            connection = new OleDbConnection(connstring);
            gunaComboBox1.SelectedValueChanged += gunaComboBox1_SelectedValueChanged;
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = Application.ProductName.ToString();
            tip.IsBalloon = true;
            tip.SetToolTip(pictureBox2, "Click for font options");
            richTextBox1.AcceptsTab = true;
           
           
        }    
        void showingA()
        {
            connection.Open();
            string sl = "SELECT * FROM My_Address";
            command = new OleDbCommand(sl, connection);
            reader = command.ExecuteReader();
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                coll.Add(reader["All_Addresses"].ToString());
            }
            connection.Close();
            if (coll != null)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox t = c as TextBox;
                        t.AutoCompleteCustomSource = coll;
                    }
                }
            }
        }

        void gunaComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {

                MessageBox.Show("Sending email is still in progress\nNetwork may be slow\nYou will be notified when done\nDo not exit this window", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            connection.Open();
            string selecting = "SELECT Note_Content FROM My_Notes WHERE Note_Title='"+gunaComboBox1.Text+"'";
            command = new OleDbCommand(selecting, connection);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                richTextBox1.Text = reader["Note_Content"].ToString();
                sub.Text = gunaComboBox1.Text;
            }
            connection.Close();
            showingA();
          
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void showingNotes()
        {
            try
            {

                connection.Open();
                string selecting = "SELECT Note_Title FROM My_Notes";
                command = new OleDbCommand(selecting, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    gunaComboBox1.Items.Add(reader["Note_Title"].ToString());
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void Send_Email_Load(object sender, EventArgs e)
        {
            showingNotes();
            if(st.From=="old")
            {
                gunaComboBox1.Enabled = false;
                sub.Text = Note_Details.NoteT;
                richTextBox1.Text = Note_Details.content;
                Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
                btn1.Visible = true;
                label1.Visible = true;
                label2.ForeColor = Color.White;
                label2.Text = "Selected Note: " + sub.Text;
                gunaDragControl1.TargetControl = panel2;
                panel2.BackColor = Color.Teal;
                label1.ForeColor = Color.White;
                showingA();
            }
            
            timer1.Start();
            button4.Visible = false;
           
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {


                MessageBox.Show("Sending email is still in progress\n\nNetwork may be slow\n\nYou will be notified when done\n\nDO NOT EXIT THIS WINDOW", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (Control c in this.Controls)
            {
                if (c is BunifuMetroTextbox)
                {
                    BunifuMetroTextbox t = c as BunifuMetroTextbox;
                    t.Text = "";
                }
            }
            richTextBox1.Text = "";
           
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            CheckForIllegalCrossThreadCalls = false;

            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
               
            }
            else
            {
              
                MessageBox.Show("Sending email is still in progress\n\nNetwork may be slow\n\nYou will be notified when done\n\nDO NOT EXIT THIS WINDOW", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

            
            
        }
        void sendingEmail()
        {
            
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(st.EmailAddress);
            msg.Subject = sub.Text;
            msg.Body = richTextBox1.Text;
            msg.IsBodyHtml = true;
            recipientsT = ToT.Text.Split(';');
            recipientsBc = Bt.Text.Split(';');
            recipientCt = Ct.Text.Split(';');
            if (!string.IsNullOrWhiteSpace(ToT.Text))
            {
                foreach (string f in recipientsT)
                {
                    msg.To.Add(new MailAddress(f));

                }
            }
            if (!string.IsNullOrWhiteSpace(Bt.Text))
            {
                foreach (string f in recipientsBc)
                {
                    msg.Bcc.Add(new MailAddress(f));

                }
            }
            if (!string.IsNullOrWhiteSpace(Ct.Text))
            {
                foreach (string f in recipientCt)
                {
                    msg.CC.Add(new MailAddress(f));

                }
            }
            if (listBox1.Items.Count != 0)
            {
                foreach (string f in listBox1.Items)
                {
                    msg.Attachments.Add(new Attachment(f));
                }
            }
          
            SmtpClient sm = new SmtpClient();
            if (st.AccountType == "Gmail")
            {
                sm.Host = "smtp.gmail.com";
                sm.Port = 587;
            }
            else if (st.AccountType == "Yahoo")
            {
                sm.Host = "smtp.mail.yahoo.com";
                sm.Port = 587;

            }
            else if (st.AccountType == "Hotmail")
            {
                sm.Host = "smtp.live.com";
                sm.Port = 587;

            }
            else if (st.AccountType == "Outlook")
            {
                sm.Host = "smtp.live.com";
                sm.Port = 587;

            }
            else if (st.AccountType == "Office 365")
            {
                sm.Host = "smtp.live.com";
                sm.Port = 587;

            }

            sm.UseDefaultCredentials = false;
            sm.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nt = new NetworkCredential(st.EmailAddress, st.EmailPassword);
            sm.Credentials = nt;
            sm.EnableSsl = true;
            sm.Send(msg);         
            MessageBox.Show("Email successfully sent", "Sending Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AllAddresses();
        }
        void AllAddresses()
        {
            try
            {

                connection.Open();
                List<string> allA = new List<string>();
                allA.AddRange(recipientsT);
                allA.AddRange(recipientCt);
                allA.AddRange(recipientsBc);
                foreach (string f in allA)
                {
                    string inserting = "INSERT INTO My_Address (All_Addresses) VALUES ('" + f + "')";
                    command = new OleDbCommand(inserting, connection);
                    command.ExecuteNonQuery();
                    
                }
                connection.Close();
            }
            catch(Exception ex)
            {

            }

            
            
        }

       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
          
            try
            {
                sendingEmail();   
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "Check Internet Connection and Try Again", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {   if(backgroundWorker1.IsBusy==true)
            {

                MessageBox.Show("Sending email is still in progress\n\nNetwork may be slow\n\nYou will be notified when done\n\nDO NOT EXIT THIS WINDOW", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "File Attachment";
            open.Filter = "All Files|*.*";
            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            open.Multiselect = true;
            if(open.ShowDialog()==DialogResult.OK)
            {
                foreach(string f in open.FileNames)
                {
                    if(!listBox1.Items.Contains(f))
                    {
                        listBox1.Items.Add(f);
                    }
                }
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = listBox1.Items.Count;
            if(i==0||i==1)
            {
                label9.Text = "Attached File: " + i.ToString();
            }
            else
            {
                label9.Text = "Attached Files: " + i.ToString();
            }
            if(listBox1.Items.Count==0)
            {
                button4.Visible = false;
            }
            else
            {
                button4.Visible = true;
            }
        }

        private void gunaComboBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {


                MessageBox.Show("Sending email is still in progress\n\nNetwork may be slow\n\nYou will be notified when done\n\nDO NOT EXIT THIS WINDOW", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FontDialog fn = new FontDialog();
            fn.ShowEffects = true;

            if (fn.ShowDialog() == DialogResult.OK)
            {

                richTextBox1.Font = fn.Font;
            }
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {

                MessageBox.Show("Sending email is still in progress\n\nNetwork may be slow\n\nYou will be notified when done\n\nDO NOT EXIT THIS WINDOW", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            listBox1.Items.Clear();
            
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string a = "If you are getting authentication error\nyou need to enable [Allowing Less Secure Apps ] in your account\n---Sign in to your Account\n---Go to your [Account Security Settings]\n---Turn on [Allow Apps that use less secure sign in]";
            MessageBox.Show(a, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (backgroundWorker1.IsBusy == true)
            {


                MessageBox.Show("Sending email is still in progress\n\nNetwork may be slow\n\nYou will be notified when done\n\nDO NOT EXIT THIS WINDOW", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        

      

        
    }
}
