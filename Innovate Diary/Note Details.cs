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
using Guna.UI.Lib;
using Innovate_Diary.Properties;
using Word= Microsoft.Office.Interop.Word;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Text;



namespace Innovate_Diary
{
    public partial class Note_Details : Form
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataReader reader;
        OleDbDataAdapter adapt;
        public static string NoteT;
        public static string content;
        Settings st = new Settings();
        string connstring;
      
        public Note_Details()
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
            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = Application.ProductName.ToString();
            tip.IsBalloon = true;
            tip.SetToolTip(pictureBox2, "Click for font options");
            richTextBox1.AcceptsTab = true;
        }

        private void Note_Details_Load(object sender, EventArgs e)
        {
            label2.Text = My_Notes.NoT;
            populating();
            GraphicsHelper.ShadowForm(this);
        }
        void populating()
        {
            connection.Open();
            string target = My_Notes.NoteID;
            string selecting = "SELECT * FROM My_Notes WHERE Note_ID='" + target + "'";
            command = new OleDbCommand(selecting, connection);
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                Nid.Text = reader["Note_ID"].ToString();
                Nt.Text = reader["Note_Title"].ToString();
                ntt.Text = reader["Note_Tag"].ToString();
                dc.Text = reader["DateCreated"].ToString();
                dm.Text = reader["DateUpdated"].ToString();
                richTextBox1.Text = reader["Note_Content"].ToString();
               
            }
            connection.Close();
           
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              
                SaveAS();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SaveAS()
        { string fname;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text File|*.txt|Word Document|*.docx|PDF|*.pdf";
            save.FileName = Nt.Text;
            save.Title = "Save As";
            save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (save.ShowDialog() == DialogResult.OK)
            {
                if (save.FilterIndex == 1)
                {

                    StreamWriter rt = new StreamWriter(save.FileName);
                    if(st.IncludeTags==true)
                    {
                       fname = richTextBox1.Text + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Date Created: " + dc.Text + Environment.NewLine + "Date Modified: " + dm.Text + Environment.NewLine + "Tag Name: " + ntt.Text;
                   
                    }
                    else
                    {
                        fname = richTextBox1.Text + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Date Created: " + dc.Text + Environment.NewLine + "Date Modified: " + dm.Text;
                    }
                     rt.Write(fname);
                    rt.Close();
                }
                else if (save.FilterIndex == 2)
                { 
                    if(st.IncludeTags==true)
                    {
                        fname = richTextBox1.Text + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Date Created: " + dc.Text + Environment.NewLine + "Date Modified: " + dm.Text + Environment.NewLine + "Tag Name: " + ntt.Text;                 
                    }
                    else
                    {
                        fname = richTextBox1.Text + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Date Created: " + dc.Text + Environment.NewLine + "Date Modified: " + dm.Text;
                    }
                      Word.Application app = new Word.Application();
                    Word.Document doc = app.Documents.Add();
                    Word.Paragraph par = doc.Paragraphs.Add();
                    app.Visible = false;
                    par.Range.Text= fname;
                    par.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;                  
                    doc.SaveAs2(save.FileName);
                    doc.Close();
                    app.Quit();
                }
                else if (save.FilterIndex == 3)
                {
                    Document dd = new Document(iTextSharp.text.PageSize.A4,40,40,30,30);                   
                    FileStream sf = new FileStream(save.FileName, FileMode.Create);
                    PdfWriter pr = PdfWriter.GetInstance(dd, sf);
                    dd.Open();

                    string header = Nt.Text.ToUpper() + Environment.NewLine;
                    //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("dp.png");
                    //img.Alignment = Element.ALIGN_LEFT;
                    //iTextSharp.text.Rectangle rec=new iTextSharp.text.Rectangle(60,60);
                    //img.ScaleAbsolute(rec);
                    //dd.Add(img);
                    dd.AddTitle(Nt.Text);
                    dd.AddAuthor(st.FullName);
                    dd.AddSubject(ntt.Text);
                    iTextSharp.text.Font headerF = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 20, iTextSharp.text.Font.BOLD);
                    if(st.IncludeTags==true)
                    {
                        Paragraph headerP = new Paragraph(header, headerF);
                        headerP.Alignment = Element.ALIGN_CENTER;
                        dd.Add(headerP);
                    }
                    else
                    {
                        Paragraph headerP = new Paragraph(header, headerF);
                        headerP.Alignment = Element.ALIGN_CENTER;
                        headerP.SpacingAfter = 10;
                        dd.Add(headerP);
                    }
                    if(st.IncludeTags==true)
                    {
                        string subtitle = ntt.Text + Environment.NewLine;
                        iTextSharp.text.Font subF = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 15, iTextSharp.text.Font.ITALIC);
                        Paragraph sub = new Paragraph(subtitle, subF);
                        sub.Alignment = Element.ALIGN_CENTER;
                        sub.SpacingAfter = 10;
                        dd.Add(sub);
                    }
                    PdfPTable line = new PdfPTable(1);
                    line.WidthPercentage = 100;
                    line.HorizontalAlignment = Element.ALIGN_CENTER;
                    line.SpacingAfter = 10;
                    PdfPCell linecell = new PdfPCell();
                    linecell.FixedHeight = 0.4f;
                    linecell.Border = 0;
                    linecell.BackgroundColor = BaseColor.DARK_GRAY;
                    line.AddCell(linecell);
                    dd.Add(line);
                    string Content = richTextBox1.Text + Environment.NewLine;
                    iTextSharp.text.Font bodyF = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 12, iTextSharp.text.Font.NORMAL);
                    Paragraph bodyP = new Paragraph(Content,bodyF);
                    bodyP.Alignment = Element.ALIGN_JUSTIFIED;
                    bodyP.SpacingAfter = 5;
                    string subs = "Date Create: " + dc.Text + Environment.NewLine +"Date Modified: "+ dm.Text;
                    iTextSharp.text.Font subF1 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 11, iTextSharp.text.Font.ITALIC);
                    Paragraph subp = new Paragraph(subs, subF1);
                    subp.SpacingAfter = 10;
                    subp.Alignment = Element.ALIGN_LEFT;
                    dd.Add(bodyP);
                    PdfPTable line1 = new PdfPTable(1);
                    line1.WidthPercentage = 100;
                    line1.HorizontalAlignment = Element.ALIGN_CENTER;
                    line1.SpacingAfter = 10;
                    PdfPCell linecell1 = new PdfPCell();
                    linecell1.FixedHeight = 0.4f;
                    linecell1.Border = 0;
                    linecell1.BackgroundColor = BaseColor.DARK_GRAY;
                    line.AddCell(linecell);
                    dd.Add(line);
                    dd.Add(subp);
                    
                 
                   

                    
                    dd.Close();
                    
                   
              
                    
                  



                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updating();
           
        }
       void updating()
        {
            connection.Open();
            string date = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss");
            string update = "UPDATE My_Notes SET Note_ID=@id,Note_Title=@tt,Note_Tag=@tag,DateCreated=@dc,DateUpdated=@dateM,Note_Content=@con WHERE Note_ID=@id";
            command = new OleDbCommand(update, connection);
            command.Parameters.AddWithValue("@id", Nid.Text.ToUpper());
            command.Parameters.AddWithValue("@tt", Nt.Text);
            command.Parameters.AddWithValue("@tag",ntt.Text);
            command.Parameters.AddWithValue("@dc", dc.Text);
            command.Parameters.AddWithValue("@dateM", date);
            command.Parameters.AddWithValue("@con", richTextBox1.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Note of ID " + Nid.Text.ToUpper() + " successfully updated", "Updating Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dm.Text = DateTime.Now.ToLongDateString();


        }

       private void button4_Click(object sender, EventArgs e)
       {
           connection.Open();
           string deleting = "DELETE * FROM My_Notes WHERE Note_ID='" + Nid.Text + "'";
           command = new OleDbCommand(deleting, connection);

           DialogResult rr = MessageBox.Show("Do you want to permanently delete this note?", "Deleting", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
           if (rr == DialogResult.Yes)
           {
               command.ExecuteNonQuery();
               MessageBox.Show("Note successfully deleted", "Deleting Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
               connection.Close();
               this.Close();
               
           }
           else
           {
               connection.Close();
           }
           
           
       }

       private void button3_Click(object sender, EventArgs e)
       {
           if (st.EmailSettingsC == false)
           {
               MessageBox.Show("Email account has not been configured\nGo to settings and comfigure your email account", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
               return;
           }
           st.From = "old";
           st.Save();
           NoteT = Nt.Text;
           content = richTextBox1.Text;
           Send_Email sending = new Send_Email();          
           sending.ShowDialog();
           this.Close();
       }

       private void timer1_Tick(object sender, EventArgs e)
       {

           char[] sep = { ' ', '\n', '.', ',', ';', ':', '?' };
           int i = richTextBox1.Text.Split(sep, StringSplitOptions.RemoveEmptyEntries).Length;
           label9.Text = "Words Count: " + i.ToString();
           
       }

       private void pictureBox2_Click(object sender, EventArgs e)
       {

           FontDialog fn = new FontDialog();
           fn.ShowEffects = true;

           if (fn.ShowDialog() == DialogResult.OK)
           {

               richTextBox1.Font = fn.Font;
           }
       }

      
    }
}
