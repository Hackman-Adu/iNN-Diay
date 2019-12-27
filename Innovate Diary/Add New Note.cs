using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using System.Data.OleDb;
using System.IO;
using Innovate_Diary.Properties;

namespace Innovate_Diary
{
    public partial class Add_New_Note : Form
    {
        string color;
        Settings st = new Settings();
        string tag;
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        OleDbDataAdapter adapt;
        string connstring;
        public Add_New_Note()
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
            timer1.Start();
            foreach(Control c in this.Controls)
            {
                if(c is BunifuMetroTextbox)
                {
                    BunifuMetroTextbox t = c as BunifuMetroTextbox;
                    t.Enter += t_Enter;
                    t.Leave += t_Leave;
                }
            }
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = Application.ProductName.ToString();
            tip.IsBalloon = true;
            tip.SetToolTip(pictureBox2, "Click for font options");
            richTextBox1.AcceptsTab = true;
        }

        void t_Leave(object sender, EventArgs e)
        {
            BunifuMetroTextbox t = sender as BunifuMetroTextbox;
            if(string.IsNullOrWhiteSpace(t.Text))
            {
                t.Text = t.Tag.ToString();
            }
        }

        void t_Enter(object sender, EventArgs e)
        {
            BunifuMetroTextbox t = sender as BunifuMetroTextbox;
           if(t.Text==t.Tag.ToString())
           {
               t.Text = "";
           }
        }

        private void Add_New_Note_Load(object sender, EventArgs e)
        {
            //richTextBox1.Font = st.Ufont;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Resetting();
        }
        void Resetting()
        {
            foreach (Control c in this.Controls)
            {
                if (c is BunifuMetroTextbox)
                {
                    BunifuMetroTextbox t = c as BunifuMetroTextbox;
                    t.Text = t.Tag.ToString();
                }
            }
           richTextBox1.Text= "enter note here";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(Nid.Text==Nid.Tag.ToString()||Ntitle.Text==Ntitle.Tag.ToString())
           {
               MessageBox.Show("Title or ID cannot be empty", "Saving Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return;
           }
            if(string.IsNullOrWhiteSpace(richTextBox1.Text)||richTextBox1.Text=="enter note here")
            {
                MessageBox.Show("Cannot save empty note", "Saving Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                insertingNotes();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }
        void insertingNotes()
        {
            
            if(Ntag.Text==Ntag.Tag.ToString()||string.IsNullOrWhiteSpace(Ntag.Text))
            {
                tag = "No Tag";
                
            }
            else
            {
            tag= Ntag.Text;
             
            }
            connection.Open();
            string checkingDuplicates = "SELECT * FROM Trash_Notes WHERE Note_ID='" + Nid.Text + "'";
            command = new OleDbCommand(checkingDuplicates, connection);
            reader = command.ExecuteReader();
            if(!reader.Read())
            {        
                connection.Close();
                string datecreated = DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToString("hh:mm:ss");
                string dateupdated = "Not Updated";
                connection.Open();
                string insert = "INSERT INTO My_Notes(Note_ID,Note_Title,Note_Content,Note_Tag,DateCreated,DateUpdated)VALUES(@id,@title,@content,@tag,@Dc,@Du)";
                command = new OleDbCommand(insert, connection);
                command.Parameters.AddWithValue("@id", Nid.Text.ToUpper());
                command.Parameters.AddWithValue("@title", Ntitle.Text);
                command.Parameters.AddWithValue("@content", richTextBox1.Text);
                command.Parameters.AddWithValue("@tag", tag);
                command.Parameters.AddWithValue("@Dc", datecreated);
                command.Parameters.AddWithValue("@Du", dateupdated);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Note of ID " + Nid.Text.ToUpper() + "has been successfully saved", "Saving New Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Resetting();
            }
            else
            {
                MessageBox.Show("Note of ID " + Nid.Text.ToUpper() + " already exists in the trash\nUse different ID", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            char[] sep = { ' ', '\n','.',',',';',':','?' };
            int i = richTextBox1.Text.Split(sep, StringSplitOptions.RemoveEmptyEntries).Length;
            label2.Text = "Words Count: " + i.ToString();
            
            
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FontDialog fn = new FontDialog();
            fn.ShowEffects = true;
           
           if(fn.ShowDialog()==DialogResult.OK)
           {
              
               richTextBox1.Font = fn.Font;
               
           }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        
    }
}
