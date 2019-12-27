using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Innovate_Diary
{
    public partial class My_Notes : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        OleDbDataAdapter adapt;
        string connstring;
        public static string NoteID;
        public static string NoT;
        DataTable dtt;
        public My_Notes()
        {
            InitializeComponent();
            timer1.Start();
            gunaDataGridView1.CellFormatting += gunaDataGridView1_CellFormatting;
            string dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\iNN DIARY";
            if (!Directory.Exists(dbpath))
            {
                Directory.CreateDirectory(dbpath);

            }
            AppDomain.CurrentDomain.SetData("DataDirectory", dbpath);
            connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\iNN_DIARY.mdb";
            connection = new OleDbConnection(connstring);
            gunaDataGridView1.KeyDown += gunaDataGridView1_KeyDown;
            
           

        }

        void gunaDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }


        void gunaDataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control&&e.KeyCode==Keys.A)
            {
                e.Handled = true;
            }
            if(e.KeyCode==Keys.Delete)
            {
                e.Handled = true;

            }
        }
        void LodingNotes(string query)
        {
            try
            {

                connection.Open();
                string selecting = query;
                command = new OleDbCommand(selecting, connection);
                adapt = new OleDbDataAdapter(command);
                dtt = new DataTable();
                adapt.Fill(dtt);
                dtt.Columns[0].ColumnName = "Note ID";
                dtt.Columns[1].ColumnName = "Note Title";
                dtt.Columns[2].ColumnName = "Tag Name";
                dtt.Columns[3].ColumnName = "Note Content";
                gunaDataGridView1.DataSource = dtt;
                //foreach (DataGridViewRow r in gunaDataGridView1.Rows)
                //{
                //    foreach (DataGridViewCell cll in r.Cells)
                //    {
                //        if (cll.ColumnIndex == 3)
                //        {
                //            if (cll.Value != string.Empty)
                //            {
                //                string v = "Long Text";
                //                cll.Value = v.ToString();
                //            }
                //        }
                //    }
                //}
                connection.Close();
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void My_Notes_Load(object sender, EventArgs e)
        {
            gunaDataGridView1.ClearSelection();
            //foreach (DataGridViewColumn dd in gunaDataGridView1.Columns)
            //{
            //    dd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //foreach (DataGridViewColumn dr in gunaDataGridView1.Columns)
            //{
            //    dr.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            //foreach (DataGridViewRow rrs in gunaDataGridView1.Rows)
            //{
            //    rrs.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            //gunaDataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            //gunaDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
            //gunaDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Yellow;
            LodingNotes("SELECT Note_ID,Note_Title,Note_Tag,Note_Content FROM My_Notes");
            
            
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = gunaDataGridView1.SelectedRows.Count;
            label2.Text = "Selected Note(s): " + i.ToString();
            if(i==1)
            {
                button3.Text = "Delete Selected Note";
                button2.Enabled = true;
            }
            else
            {
                button3.Text = "Delete Selected Notes";
                button2.Enabled = false;
            }
            int count = gunaDataGridView1.Rows.Count;
            label3.Text = "Available Note(s): " + count.ToString();
            if(gunaDataGridView1.Rows.Count!=0)
            {
                button1.Visible = true;

            }
            else
            {
                button1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow r in gunaDataGridView1.Rows)
            {
                r.Selected = true;
            }
            
        }

        private void bunifuMetroTextbox1_Enter(object sender, EventArgs e)
        {
            if(bunifuMetroTextbox1.Text==bunifuMetroTextbox1.Tag.ToString())
            {
                bunifuMetroTextbox1.Text = "";
            }
        }

        private void bunifuMetroTextbox1_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(bunifuMetroTextbox1.Text))
            {
                bunifuMetroTextbox1.Text = bunifuMetroTextbox1.Tag.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult r;


            try
            {
                int i = gunaDataGridView1.SelectedRows.Count;
                if(i!=0)
                {

                    if (gunaDataGridView1.SelectedRows.Count > 1)
                    {
                        r = MessageBox.Show("Are you sure that you want to delete these notes?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        r = MessageBox.Show("Are you sure that you want to delete this note?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                    if (r == DialogResult.Yes)
                    {
                        deletingSelected();
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
            

        }
        void deletingSelected()
        {
            connection.Open();
            for(int i=0;i<gunaDataGridView1.SelectedRows.Count;i++)
            {
                string moving = @"INSERT INTO Trash_Notes(Note_ID,Note_Title,Note_Tag,Note_Content,
                                 DateCreated,DateUpdated) SELECT * FROM My_Notes WHERE Note_ID='"
                                + gunaDataGridView1.SelectedRows[i].Cells[0].Value + "'";
                command = new OleDbCommand(moving, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            connection.Open();
            for(int n=0;n<gunaDataGridView1.SelectedRows.Count;n++)
            {

                string delete = "DELETE * FROM My_Notes WHERE Note_ID='" + gunaDataGridView1.SelectedRows[n].Cells[0].Value + "'";
                command = new OleDbCommand(delete, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
            MessageBox.Show("Selected Note(s) successfully deleted", "Deleting Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (DataGridViewRow rr in gunaDataGridView1.SelectedRows)
            {
                gunaDataGridView1.Rows.Remove(rr);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            NoteID = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            NoT = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Note_Details more = new Note_Details();
            more.ShowDialog();
        }

       

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            if(bunifuMetroTextbox1.Text==bunifuMetroTextbox1.Tag.ToString())
            {

            }
            else
            {
                string qq = "SELECT Note_ID,Note_Title,Note_Tag,Note_Content FROM My_Notes WHERE Note_ID LIKE '%" + bunifuMetroTextbox1.Text + "%'OR Note_Title LIKE'%" + bunifuMetroTextbox1.Text + "%' OR Note_Tag LIKE'%" + bunifuMetroTextbox1.Text + "%'OR Note_Content LIKE'%" + bunifuMetroTextbox1.Text + "%' ";
                LodingNotes(qq);
            }
        }

        private void gunaDataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            button2_Click(gunaDataGridView1, e);
        }
        void ExportingAll()
        {
            LodingNotes("SELECT Note_ID,Note_Title,Note_Tag,Note_Content FROM My_Notes");
            string filename;
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\iNN Notes";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for(int i=0;i<gunaDataGridView1.Rows.Count;i++)
            {
                filename = path + "\\" + gunaDataGridView1.Rows[i].Cells[1].Value.ToString()+".txt";
                StreamWriter rtt = new StreamWriter(filename);
                string main = gunaDataGridView1.Rows[i].Cells[3].Value.ToString() + Environment.NewLine + Environment.NewLine + "Note Tag Name:" + gunaDataGridView1.Rows[i].Cells[2].Value.ToString()+Environment.NewLine+DateTime.Now.ToLongDateString();
                rtt.Write(main,true);
                rtt.Close();
            }
            MessageBox.Show("A backup of the notes has been made to:" + Environment.NewLine + path, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ExportingAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
