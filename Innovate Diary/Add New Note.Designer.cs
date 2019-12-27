namespace Innovate_Diary
{
    partial class Add_New_Note
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_New_Note));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.button1 = new System.Windows.Forms.Button();
            this.Ntitle = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.button2 = new System.Windows.Forms.Button();
            this.Ntag = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.Nid = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 45);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(215, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Add New Note";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(185, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add New Note";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape3});
            this.shapeContainer1.Size = new System.Drawing.Size(944, 556);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape3
            // 
            this.rectangleShape3.BorderColor = System.Drawing.Color.Silver;
            this.rectangleShape3.Location = new System.Drawing.Point(9, 57);
            this.rectangleShape3.Name = "rectangleShape3";
            this.rectangleShape3.Size = new System.Drawing.Size(917, 285);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("ITC Avant Garde Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(9, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 33);
            this.button1.TabIndex = 5;
            this.button1.Text = "Save Note";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Ntitle
            // 
            this.Ntitle.BorderColorFocused = System.Drawing.Color.Silver;
            this.Ntitle.BorderColorIdle = System.Drawing.Color.Silver;
            this.Ntitle.BorderColorMouseHover = System.Drawing.Color.Silver;
            this.Ntitle.BorderThickness = 1;
            this.Ntitle.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ntitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ntitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ntitle.isPassword = false;
            this.Ntitle.Location = new System.Drawing.Point(9, 410);
            this.Ntitle.Margin = new System.Windows.Forms.Padding(4);
            this.Ntitle.Name = "Ntitle";
            this.Ntitle.Size = new System.Drawing.Size(918, 35);
            this.Ntitle.TabIndex = 6;
            this.Ntitle.Tag = "Enter Note Title Here";
            this.Ntitle.Text = "Enter Note Title Here";
            this.Ntitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Teal;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("ITC Avant Garde Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(298, 505);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(629, 33);
            this.button2.TabIndex = 8;
            this.button2.Text = "Clear All";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Ntag
            // 
            this.Ntag.BorderColorFocused = System.Drawing.Color.Silver;
            this.Ntag.BorderColorIdle = System.Drawing.Color.Silver;
            this.Ntag.BorderColorMouseHover = System.Drawing.Color.Silver;
            this.Ntag.BorderThickness = 1;
            this.Ntag.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ntag.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ntag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Ntag.isPassword = false;
            this.Ntag.Location = new System.Drawing.Point(9, 457);
            this.Ntag.Margin = new System.Windows.Forms.Padding(4);
            this.Ntag.Name = "Ntag";
            this.Ntag.Size = new System.Drawing.Size(918, 35);
            this.Ntag.TabIndex = 7;
            this.Ntag.Tag = "Enter Note Tag Name Here [ Optional ]";
            this.Ntag.Text = "Enter Note Tag Name Here [ Optional ]";
            this.Ntag.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // Nid
            // 
            this.Nid.BorderColorFocused = System.Drawing.Color.Silver;
            this.Nid.BorderColorIdle = System.Drawing.Color.Silver;
            this.Nid.BorderColorMouseHover = System.Drawing.Color.Silver;
            this.Nid.BorderThickness = 1;
            this.Nid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Nid.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Nid.isPassword = false;
            this.Nid.Location = new System.Drawing.Point(9, 363);
            this.Nid.Margin = new System.Windows.Forms.Padding(4);
            this.Nid.Name = "Nid";
            this.Nid.Size = new System.Drawing.Size(918, 35);
            this.Nid.TabIndex = 9;
            this.Nid.Tag = "Give your note an ID";
            this.Nid.Text = "Give your note an ID";
            this.Nid.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.richTextBox1.Location = new System.Drawing.Point(21, 68);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(830, 261);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "enter note here";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Add_New_Note
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(944, 556);
            this.ControlBox = false;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Nid);
            this.Controls.Add(this.Ntitle);
            this.Controls.Add(this.Ntag);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Add_New_Note";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Add_New_Note_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape3;
        private System.Windows.Forms.Button button1;
        private Bunifu.Framework.UI.BunifuMetroTextbox Ntitle;
        private System.Windows.Forms.Button button2;
        private Bunifu.Framework.UI.BunifuMetroTextbox Ntag;
        private Bunifu.Framework.UI.BunifuMetroTextbox Nid;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
    }
}