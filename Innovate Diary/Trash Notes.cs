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

namespace Innovate_Diary
{
    public partial class Trash_Notes : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        OleDbDataAdapter adapt;
        string connstring;
        int i;
        public Trash_Notes()
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
            gunaDataGridView1.ClearSelection();
            
        }
        void LoadingTrash(string q)
        {
            connection.Open();
            string all = q;
            command = new OleDbCommand(all, connection);
            adapt = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dt.Columns[0].ColumnName = "Note ID";
            dt.Columns[1].ColumnName = "Note Title";
            dt.Columns[2].ColumnName = "Note Tag";
            dt.Columns[3].ColumnName = "Note Content";
            dt.Columns[4].ColumnName = "Date Created";
            dt.Columns[5].ColumnName = "Date Updated";
            gunaDataGridView1.DataSource = dt;
            connection.Close();
            //foreach(DataGridViewRow r in gunaDataGridView1.Rows)
            //{
            //    foreach(DataGridViewCell c in r.Cells)
            //    {
            //        if(c.ColumnIndex==3)
            //        {
            //            if(c.FormattedValue!=string.Empty)
            //            {
            //                string newC = "Long Text";
            //                c.Value = newC.ToString();
            //            }
            //        }
            //    }
            //}
        }

        private void Trash_Notes_Load(object sender, EventArgs e)
        {
           
                gunaDataGridView1.ContextMenuStrip = contextMenuStrip1;
            
            try
            {
                timer1.Start();
                LoadingTrash("SELECT * FROM Trash_Notes");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i= gunaDataGridView1.Rows.Count;
            if(i==0)
            {
                label2.Text = "Empty Trash";
            }
            else
            {
                label2.Text = "My Trash [ " + i.ToString() + " ]";
            }
           
            
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

            if (bunifuMetroTextbox1.Text == bunifuMetroTextbox1.Tag.ToString())
            {

            }
            else
            {
                string qq = "SELECT * FROM Trash_Notes WHERE Note_ID LIKE '%" + bunifuMetroTextbox1.Text + "%'OR Note_Title LIKE'%" + bunifuMetroTextbox1.Text + "%' OR Note_Tag LIKE'%" + bunifuMetroTextbox1.Text + "%'OR Note_Content LIKE'%" + bunifuMetroTextbox1.Text + "%' ";
                try
                {
                    LoadingTrash(qq);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult r = MessageBox.Show("Do you want to permanently delete the selected note(s)?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    deleting();
                    i = gunaDataGridView1.Rows.Count;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void deleting()
        {
            connection.Open();
            for (int n = 0; n < gunaDataGridView1.SelectedRows.Count; n++)
            {

                string delete = "DELETE * FROM Trash_Notes WHERE Note_ID='" + gunaDataGridView1.SelectedRows[n].Cells[0].Value + "'";
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

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restoring();
            RestoreDelete();
        }
        void restoring()
        {
            connection.Open();
            for(int m=0;m<gunaDataGridView1.SelectedRows.Count;m++)
            {
                string restore = "INSERT INTO My_Notes(Note_ID,Note_Title,Note_Tag,Note_Content,DateCreated,DateUpdated)VALUES(@id,@nt,@ntt,@nC,@DC,@dm)";
                command = new OleDbCommand(restore, connection);
                command.Parameters.AddWithValue("@id", gunaDataGridView1.SelectedRows[m].Cells[0].Value);
                command.Parameters.AddWithValue("@nt", gunaDataGridView1.SelectedRows[m].Cells[1].Value);
                command.Parameters.AddWithValue("@ntt",gunaDataGridView1.SelectedRows[m].Cells[2].Value);
                command.Parameters.AddWithValue("@nC", gunaDataGridView1.SelectedRows[m].Cells[3].Value);
                command.Parameters.AddWithValue("@DC", gunaDataGridView1.SelectedRows[m].Cells[4].Value);
                command.Parameters.AddWithValue("@dm", gunaDataGridView1.SelectedRows[m].Cells[5].Value);
                command.ExecuteNonQuery();
                
                
                
            }
           
            connection.Close();
        }
        void RestoreDelete()
        {
            connection.Open();
            for (int m = 0; m < gunaDataGridView1.SelectedRows.Count; m++)
            {
                string restore = "DELETE * FROM Trash_Notes WHERE Note_ID='" + gunaDataGridView1.SelectedRows[m].Cells[0].Value + "'";
                command = new OleDbCommand(restore, connection);
                
                command.ExecuteNonQuery();



            }
            MessageBox.Show("Selected note(s) successfully restored", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            foreach (DataGridViewRow rr in gunaDataGridView1.SelectedRows)
            {
                gunaDataGridView1.Rows.Remove(rr);
            }
        }

        

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count != 0)
            {
                restoreToolStripMenuItem_Click(button5, e);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (gunaDataGridView1.SelectedRows.Count != 0)
            {
                deleteToolStripMenuItem_Click(button1, e);
            }
        }
    }
}
