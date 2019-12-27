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
using System.Data.OleDb;
using System.IO;

namespace Innovate_Diary
{
    public partial class Form1 : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        Settings st = new Settings();
        string connstring;

        public Form1()
        {
            InitializeComponent();
            //panel1.BackColor = ColorTranslator.FromHtml("#004da3");
            panel1.Width = 196;
            string dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+ "\\iNN DIARY";
            if(!Directory.Exists(dbpath))
            {
                Directory.CreateDirectory(dbpath);
                
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", dbpath);
            connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\iNN_DIARY.mdb";
            connection = new OleDbConnection(connstring);
            gunaCirclePictureBox1.ContextMenuStrip = contextMenuStrip1;
            try
            {
                if (st.ImageFile != string.Empty)
                {
                    gunaCirclePictureBox1.Image = Image.FromFile(st.ImageFile);
                }
                else
                {
                    gunaCirclePictureBox1.Image = Image.FromFile("dp.png");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ToolTip t = new ToolTip();
            t.ToolTipTitle = Application.ProductName;
            t.SetToolTip(gunaCirclePictureBox1, "Right Click on image for options");
            t.IsBalloon = true;
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
           
            
            
           if(!string.IsNullOrWhiteSpace(st.FullName))
           {
               label2.Text = "[ " + st.FullName + " ]";
           }
            timer1.Start();
            
        }
  
        void Showing(object b)
        {
            int i = panel3.Controls.Count;
            if(i>0)
            {
                panel3.Controls.RemoveAt(0);
            }
            Form fs = b as Form;
            fs.TopLevel = false;
            fs.Dock = DockStyle.Fill;
            panel3.Controls.Add(fs);
            fs.Show();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongDateString() + " Time:" + DateTime.Now.ToString("hh:mm:ss");
            

        }

        private void changePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter="Image Files|*.png;*.jpg;*.jpeg*";
            open.InitialDirectory=Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            open.Title="Changing Picture";
            if(open.ShowDialog()==DialogResult.OK)
            {
                gunaCirclePictureBox1.Image = Image.FromFile(open.FileName);
                st.ImageFile = open.FileName;
                st.Save();
            }
            
        }

        private void useDefaultPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gunaCirclePictureBox1.Image = Image.FromFile("dp.png");
            st.ImageFile = "dp.png";
            st.Save();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {

           
            label2.Text = "[ " + st.FullName + " ]";

            foreach (Control c in panel3.Controls)
            {
                panel3.Controls.Remove(c);
            }
            panel3.Controls.Add(gunaPanel1);

        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
           
            Add_New_Note nw = new Add_New_Note();
            Showing(nw);
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            My_Notes my = new My_Notes();
            Showing(my);
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Trash_Notes tn = new Trash_Notes();
            Showing(tn);
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (st.EmailSettingsC == false)
            {
                MessageBox.Show("Email account has not been configured\nGo to settings and comfigure your email account", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            st.From = "new";
            st.Save();
            Send_Email sending = new Send_Email();
            Showing(sending);

        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            
            App_Setting stt = new App_Setting();
            Showing(stt);
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            About_Us abt = new About_Us();
            Showing(abt);
        }

        //private void gunaImageButton1_Click(object sender, EventArgs e)
        //{
        //    //if(panel1.Width==196)
        //    //{
        //    //    panel1.Width = 44;
                
        //    //    panel5.Location = new Point(310, 284);
        //    //    label2.Location = new Point(227, 344);
        //    //    gunaPictureBox1.Location = new Point(223, 16);
        //    //    gunaImageButton1.Location = new Point(3, 5);
        //    //    //panel1.Visible = false;
        //    //    //gunaTransition1.ShowSync(panel1);

        //    //}
        //    //else if(panel1.Width==44)
        //    //{
        //    //    panel1.Width = 196;
                
        //    //    panel5.Location = new Point(234, 284);
        //    //    label2.Location = new Point(151, 344);
        //    //    gunaPictureBox1.Location = new Point(147, 16);
        //    //    gunaImageButton1.Location = new Point(0, 6);
        //    //    //panel1.Visible = false;
        //    //    //gunaTransition1.ShowSync(panel1);
        //    //}
            
        //}
       
    }
}
