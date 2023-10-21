namespace KeyAuth
{
    public partial class Warn : global::System.Windows.Forms.Form
    {
        protected override void Dispose(bool disposing)
        {
            bool flag = disposing && this.components != null;
            if (flag)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Warn));
            this.label1 = new System.Windows.Forms.Label();
            this.Datedederniéreédition = new System.Windows.Forms.DateTimePicker();
            this.Datedefin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.RgstrBtn = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneRoundedButton4 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneRoundedTextBox1 = new Siticone.UI.WinForms.SiticoneRoundedTextBox();
            this.siticoneRoundedTextBox2 = new Siticone.UI.WinForms.SiticoneRoundedTextBox();
            this.siticoneRoundedButton1 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.key = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PseudoJoueur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RaisonWarn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PseudoStaff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datedederniereedition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date_de_fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Statue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siticoneRoundedButton3 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.siticoneRoundedTextBox3 = new Siticone.UI.WinForms.SiticoneRoundedTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.siticoneRoundedButton5 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneRoundedButton6 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.siticoneControlBox1 = new Siticone.UI.WinForms.SiticoneControlBox();
            this.siticoneShadowForm = new Siticone.UI.WinForms.SiticoneShadowForm(this.components);
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.siticoneRoundedButton7 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneRoundedButton9 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.siticoneRoundedTextBox4 = new Siticone.UI.WinForms.SiticoneRoundedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.siticoneRoundedButton10 = new Siticone.UI.WinForms.SiticoneRoundedButton();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnTools = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // Datedederniéreédition
            // 
            this.Datedederniéreédition.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            resources.ApplyResources(this.Datedederniéreédition, "Datedederniéreédition");
            this.Datedederniéreédition.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Datedederniéreédition.Name = "Datedederniéreédition";
            this.Datedederniéreédition.Value = new System.DateTime(2023, 9, 23, 0, 0, 0, 0);
            this.Datedederniéreédition.ValueChanged += new System.EventHandler(this.Datedederniéreédition_ValueChanged);
            // 
            // Datedefin
            // 
            this.Datedefin.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.Datedefin.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            resources.ApplyResources(this.Datedefin, "Datedefin");
            this.Datedefin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Datedefin.Name = "Datedefin";
            this.Datedefin.Value = new System.DateTime(2023, 9, 24, 0, 0, 0, 0);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label10.Name = "label10";
            // 
            // RgstrBtn
            // 
            this.RgstrBtn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.RgstrBtn.BorderThickness = 1;
            this.RgstrBtn.CheckedState.Parent = this.RgstrBtn;
            this.RgstrBtn.CustomImages.Parent = this.RgstrBtn;
            this.RgstrBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.RgstrBtn, "RgstrBtn");
            this.RgstrBtn.ForeColor = System.Drawing.Color.White;
            this.RgstrBtn.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.RgstrBtn.HoveredState.Parent = this.RgstrBtn;
            this.RgstrBtn.Name = "RgstrBtn";
            this.RgstrBtn.ShadowDecoration.Parent = this.RgstrBtn;
            this.RgstrBtn.Click += new System.EventHandler(this.RgstrBtn_Click);
            // 
            // siticoneRoundedButton4
            // 
            this.siticoneRoundedButton4.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton4.BorderThickness = 1;
            this.siticoneRoundedButton4.CheckedState.Parent = this.siticoneRoundedButton4;
            this.siticoneRoundedButton4.CustomImages.Parent = this.siticoneRoundedButton4;
            this.siticoneRoundedButton4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton4, "siticoneRoundedButton4");
            this.siticoneRoundedButton4.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton4.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton4.HoveredState.Parent = this.siticoneRoundedButton4;
            this.siticoneRoundedButton4.Name = "siticoneRoundedButton4";
            this.siticoneRoundedButton4.ShadowDecoration.Parent = this.siticoneRoundedButton4;
            this.siticoneRoundedButton4.Click += new System.EventHandler(this.SiticoneRoundedButton4_Click);
            // 
            // siticoneRoundedTextBox1
            // 
            this.siticoneRoundedTextBox1.AllowDrop = true;
            this.siticoneRoundedTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.siticoneRoundedTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.siticoneRoundedTextBox1.DefaultText = "";
            this.siticoneRoundedTextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.siticoneRoundedTextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.siticoneRoundedTextBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox1.DisabledState.Parent = this.siticoneRoundedTextBox1;
            this.siticoneRoundedTextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneRoundedTextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox1.FocusedState.Parent = this.siticoneRoundedTextBox1;
            this.siticoneRoundedTextBox1.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox1.HoveredState.Parent = this.siticoneRoundedTextBox1;
            resources.ApplyResources(this.siticoneRoundedTextBox1, "siticoneRoundedTextBox1");
            this.siticoneRoundedTextBox1.Name = "siticoneRoundedTextBox1";
            this.siticoneRoundedTextBox1.PasswordChar = '\0';
            this.siticoneRoundedTextBox1.PlaceholderText = "";
            this.siticoneRoundedTextBox1.SelectedText = "";
            this.siticoneRoundedTextBox1.ShadowDecoration.Parent = this.siticoneRoundedTextBox1;
            this.siticoneRoundedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // siticoneRoundedTextBox2
            // 
            this.siticoneRoundedTextBox2.AllowDrop = true;
            this.siticoneRoundedTextBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.siticoneRoundedTextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.siticoneRoundedTextBox2.DefaultText = "";
            this.siticoneRoundedTextBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.siticoneRoundedTextBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.siticoneRoundedTextBox2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox2.DisabledState.Parent = this.siticoneRoundedTextBox2;
            this.siticoneRoundedTextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneRoundedTextBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox2.FocusedState.Parent = this.siticoneRoundedTextBox2;
            this.siticoneRoundedTextBox2.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox2.HoveredState.Parent = this.siticoneRoundedTextBox2;
            resources.ApplyResources(this.siticoneRoundedTextBox2, "siticoneRoundedTextBox2");
            this.siticoneRoundedTextBox2.Name = "siticoneRoundedTextBox2";
            this.siticoneRoundedTextBox2.PasswordChar = '\0';
            this.siticoneRoundedTextBox2.PlaceholderText = "";
            this.siticoneRoundedTextBox2.SelectedText = "";
            this.siticoneRoundedTextBox2.ShadowDecoration.Parent = this.siticoneRoundedTextBox2;
            this.siticoneRoundedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // siticoneRoundedButton1
            // 
            this.siticoneRoundedButton1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton1.BorderThickness = 1;
            this.siticoneRoundedButton1.CheckedState.Parent = this.siticoneRoundedButton1;
            this.siticoneRoundedButton1.CustomImages.Parent = this.siticoneRoundedButton1;
            this.siticoneRoundedButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton1, "siticoneRoundedButton1");
            this.siticoneRoundedButton1.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton1.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton1.HoveredState.Parent = this.siticoneRoundedButton1;
            this.siticoneRoundedButton1.Name = "siticoneRoundedButton1";
            this.siticoneRoundedButton1.ShadowDecoration.Parent = this.siticoneRoundedButton1;
            this.siticoneRoundedButton1.Click += new System.EventHandler(this.SiticoneRoundedButton1_Click_1);
            // 
            // key
            // 
            resources.ApplyResources(this.key, "key");
            this.key.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.key.Name = "key";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label11.Name = "label11";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.dataGridView1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.PseudoJoueur,
            this.RaisonWarn,
            this.PseudoStaff,
            this.Datedederniereedition,
            this.Date_de_fin,
            this.Statue});
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Id
            // 
            resources.ApplyResources(this.Id, "Id");
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // PseudoJoueur
            // 
            resources.ApplyResources(this.PseudoJoueur, "PseudoJoueur");
            this.PseudoJoueur.Name = "PseudoJoueur";
            this.PseudoJoueur.ReadOnly = true;
            // 
            // RaisonWarn
            // 
            resources.ApplyResources(this.RaisonWarn, "RaisonWarn");
            this.RaisonWarn.Name = "RaisonWarn";
            this.RaisonWarn.ReadOnly = true;
            // 
            // PseudoStaff
            // 
            resources.ApplyResources(this.PseudoStaff, "PseudoStaff");
            this.PseudoStaff.Name = "PseudoStaff";
            this.PseudoStaff.ReadOnly = true;
            // 
            // Datedederniereedition
            // 
            resources.ApplyResources(this.Datedederniereedition, "Datedederniereedition");
            this.Datedederniereedition.Name = "Datedederniereedition";
            this.Datedederniereedition.ReadOnly = true;
            // 
            // Date_de_fin
            // 
            resources.ApplyResources(this.Date_de_fin, "Date_de_fin");
            this.Date_de_fin.Name = "Date_de_fin";
            this.Date_de_fin.ReadOnly = true;
            // 
            // Statue
            // 
            resources.ApplyResources(this.Statue, "Statue");
            this.Statue.Name = "Statue";
            this.Statue.ReadOnly = true;
            // 
            // siticoneRoundedButton3
            // 
            this.siticoneRoundedButton3.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton3.BorderThickness = 1;
            this.siticoneRoundedButton3.CheckedState.Parent = this.siticoneRoundedButton3;
            this.siticoneRoundedButton3.CustomImages.Parent = this.siticoneRoundedButton3;
            this.siticoneRoundedButton3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton3, "siticoneRoundedButton3");
            this.siticoneRoundedButton3.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton3.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton3.HoveredState.Parent = this.siticoneRoundedButton3;
            this.siticoneRoundedButton3.Name = "siticoneRoundedButton3";
            this.siticoneRoundedButton3.ShadowDecoration.Parent = this.siticoneRoundedButton3;
            this.siticoneRoundedButton3.Click += new System.EventHandler(this.SiticoneRoundedButton3_Click_1);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label13.Name = "label13";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // siticoneRoundedTextBox3
            // 
            this.siticoneRoundedTextBox3.AllowDrop = true;
            this.siticoneRoundedTextBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.siticoneRoundedTextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.siticoneRoundedTextBox3.DefaultText = "";
            this.siticoneRoundedTextBox3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.siticoneRoundedTextBox3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.siticoneRoundedTextBox3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox3.DisabledState.Parent = this.siticoneRoundedTextBox3;
            this.siticoneRoundedTextBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneRoundedTextBox3.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox3.FocusedState.Parent = this.siticoneRoundedTextBox3;
            this.siticoneRoundedTextBox3.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox3.HoveredState.Parent = this.siticoneRoundedTextBox3;
            resources.ApplyResources(this.siticoneRoundedTextBox3, "siticoneRoundedTextBox3");
            this.siticoneRoundedTextBox3.Name = "siticoneRoundedTextBox3";
            this.siticoneRoundedTextBox3.PasswordChar = '\0';
            this.siticoneRoundedTextBox3.PlaceholderText = "";
            this.siticoneRoundedTextBox3.SelectedText = "";
            this.siticoneRoundedTextBox3.ShadowDecoration.Parent = this.siticoneRoundedTextBox3;
            this.siticoneRoundedTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label18.Name = "label18";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label19.Name = "label19";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label21.Name = "label21";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label24.Name = "label24";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Name = "label23";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label22.Name = "label22";
            // 
            // siticoneRoundedButton5
            // 
            this.siticoneRoundedButton5.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton5.BorderThickness = 1;
            this.siticoneRoundedButton5.CheckedState.Parent = this.siticoneRoundedButton5;
            this.siticoneRoundedButton5.CustomImages.Parent = this.siticoneRoundedButton5;
            this.siticoneRoundedButton5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton5, "siticoneRoundedButton5");
            this.siticoneRoundedButton5.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton5.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton5.HoveredState.Parent = this.siticoneRoundedButton5;
            this.siticoneRoundedButton5.Name = "siticoneRoundedButton5";
            this.siticoneRoundedButton5.ShadowDecoration.Parent = this.siticoneRoundedButton5;
            this.siticoneRoundedButton5.Click += new System.EventHandler(this.SiticoneRoundedButton5_Click);
            // 
            // siticoneRoundedButton6
            // 
            this.siticoneRoundedButton6.BorderColor = System.Drawing.Color.Transparent;
            this.siticoneRoundedButton6.BorderThickness = 1;
            this.siticoneRoundedButton6.CheckedState.Parent = this.siticoneRoundedButton6;
            this.siticoneRoundedButton6.CustomImages.Parent = this.siticoneRoundedButton6;
            this.siticoneRoundedButton6.FillColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.siticoneRoundedButton6, "siticoneRoundedButton6");
            this.siticoneRoundedButton6.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton6.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton6.HoveredState.Parent = this.siticoneRoundedButton6;
            this.siticoneRoundedButton6.Name = "siticoneRoundedButton6";
            this.siticoneRoundedButton6.ShadowDecoration.Parent = this.siticoneRoundedButton6;
            this.siticoneRoundedButton6.Click += new System.EventHandler(this.SiticoneRoundedButton6_Click);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label15.Name = "label15";
            // 
            // siticoneControlBox1
            // 
            resources.ApplyResources(this.siticoneControlBox1, "siticoneControlBox1");
            this.siticoneControlBox1.BorderRadius = 10;
            this.siticoneControlBox1.ControlBoxType = Siticone.UI.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.siticoneControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneControlBox1.HoveredState.Parent = this.siticoneControlBox1;
            this.siticoneControlBox1.IconColor = System.Drawing.Color.White;
            this.siticoneControlBox1.Name = "siticoneControlBox1";
            this.siticoneControlBox1.ShadowDecoration.Parent = this.siticoneControlBox1;
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.listBox2, "listBox2");
            this.listBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox2.ForeColor = System.Drawing.Color.White;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Name = "listBox2";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label16.Name = "label16";
            // 
            // siticoneRoundedButton7
            // 
            this.siticoneRoundedButton7.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton7.BorderThickness = 1;
            this.siticoneRoundedButton7.CheckedState.Parent = this.siticoneRoundedButton7;
            this.siticoneRoundedButton7.CustomImages.Parent = this.siticoneRoundedButton7;
            this.siticoneRoundedButton7.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton7, "siticoneRoundedButton7");
            this.siticoneRoundedButton7.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton7.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton7.HoveredState.Parent = this.siticoneRoundedButton7;
            this.siticoneRoundedButton7.Name = "siticoneRoundedButton7";
            this.siticoneRoundedButton7.ShadowDecoration.Parent = this.siticoneRoundedButton7;
            this.siticoneRoundedButton7.Click += new System.EventHandler(this.siticoneRoundedButton7_Click);
            // 
            // siticoneRoundedButton9
            // 
            this.siticoneRoundedButton9.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton9.BorderThickness = 1;
            this.siticoneRoundedButton9.CheckedState.Parent = this.siticoneRoundedButton9;
            this.siticoneRoundedButton9.CustomImages.Parent = this.siticoneRoundedButton9;
            this.siticoneRoundedButton9.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton9, "siticoneRoundedButton9");
            this.siticoneRoundedButton9.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton9.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton9.HoveredState.Parent = this.siticoneRoundedButton9;
            this.siticoneRoundedButton9.Name = "siticoneRoundedButton9";
            this.siticoneRoundedButton9.ShadowDecoration.Parent = this.siticoneRoundedButton9;
            this.siticoneRoundedButton9.Click += new System.EventHandler(this.siticoneRoundedButton9_Click);
            // 
            // siticoneRoundedTextBox4
            // 
            this.siticoneRoundedTextBox4.AllowDrop = true;
            this.siticoneRoundedTextBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.siticoneRoundedTextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.siticoneRoundedTextBox4.DefaultText = "";
            this.siticoneRoundedTextBox4.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.siticoneRoundedTextBox4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.siticoneRoundedTextBox4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox4.DisabledState.Parent = this.siticoneRoundedTextBox4;
            this.siticoneRoundedTextBox4.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneRoundedTextBox4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneRoundedTextBox4.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox4.FocusedState.Parent = this.siticoneRoundedTextBox4;
            this.siticoneRoundedTextBox4.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneRoundedTextBox4.HoveredState.Parent = this.siticoneRoundedTextBox4;
            resources.ApplyResources(this.siticoneRoundedTextBox4, "siticoneRoundedTextBox4");
            this.siticoneRoundedTextBox4.Name = "siticoneRoundedTextBox4";
            this.siticoneRoundedTextBox4.PasswordChar = '\0';
            this.siticoneRoundedTextBox4.PlaceholderText = "";
            this.siticoneRoundedTextBox4.SelectedText = "";
            this.siticoneRoundedTextBox4.ShadowDecoration.Parent = this.siticoneRoundedTextBox4;
            this.siticoneRoundedTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label29.Name = "label29";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label17.Name = "label17";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label20.Name = "label20";
            // 
            // siticoneRoundedButton10
            // 
            this.siticoneRoundedButton10.BorderColor = System.Drawing.Color.DodgerBlue;
            this.siticoneRoundedButton10.BorderThickness = 1;
            this.siticoneRoundedButton10.CheckedState.Parent = this.siticoneRoundedButton10;
            this.siticoneRoundedButton10.CustomImages.Parent = this.siticoneRoundedButton10;
            this.siticoneRoundedButton10.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.siticoneRoundedButton10, "siticoneRoundedButton10");
            this.siticoneRoundedButton10.ForeColor = System.Drawing.Color.White;
            this.siticoneRoundedButton10.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.siticoneRoundedButton10.HoveredState.Parent = this.siticoneRoundedButton10;
            this.siticoneRoundedButton10.Name = "siticoneRoundedButton10";
            this.siticoneRoundedButton10.ShadowDecoration.Parent = this.siticoneRoundedButton10;
            this.siticoneRoundedButton10.Click += new System.EventHandler(this.siticoneRoundedButton10_Click);
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label25.Name = "label25";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label26.Name = "label26";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.panel6.Controls.Add(this.button5);
            this.panel6.Controls.Add(this.button4);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.button2);
            this.panel6.Controls.Add(this.btnTools);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.button5.ForeColor = System.Drawing.Color.Silver;
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.button4.ForeColor = System.Drawing.Color.Silver;
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.panel7.Controls.Add(this.btnHelp);
            this.panel7.Controls.Add(this.btnExit);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.btnHelp.ForeColor = System.Drawing.Color.Silver;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.btnExit.ForeColor = System.Drawing.Color.Silver;
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.button1.ForeColor = System.Drawing.Color.Silver;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.button2.ForeColor = System.Drawing.Color.Silver;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnTools
            // 
            this.btnTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(16)))), ((int)(((byte)(22)))));
            resources.ApplyResources(this.btnTools, "btnTools");
            this.btnTools.FlatAppearance.BorderSize = 0;
            this.btnTools.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.btnTools.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.btnTools.ForeColor = System.Drawing.Color.Silver;
            this.btnTools.Name = "btnTools";
            this.btnTools.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(127)))), ((int)(((byte)(243)))));
            resources.ApplyResources(this.button3, "button3");
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.button3.ForeColor = System.Drawing.Color.Silver;
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // Warn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.siticoneRoundedButton10);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.siticoneRoundedButton9);
            this.Controls.Add(this.siticoneRoundedTextBox4);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.siticoneRoundedButton7);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.siticoneControlBox1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.siticoneRoundedButton6);
            this.Controls.Add(this.siticoneRoundedButton5);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.siticoneRoundedTextBox3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.siticoneRoundedButton3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.key);
            this.Controls.Add(this.siticoneRoundedButton1);
            this.Controls.Add(this.siticoneRoundedTextBox2);
            this.Controls.Add(this.siticoneRoundedTextBox1);
            this.Controls.Add(this.siticoneRoundedButton4);
            this.Controls.Add(this.RgstrBtn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Datedefin);
            this.Controls.Add(this.Datedederniéreédition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Warn";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Token: 0x04000001 RID: 1
        private global::System.ComponentModel.IContainer components = null;

        // Token: 0x0400000A RID: 10
        private global::System.Windows.Forms.Label label1;
        protected System.Windows.Forms.DateTimePicker Datedederniéreédition;
        protected System.Windows.Forms.DateTimePicker Datedefin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Siticone.UI.WinForms.SiticoneRoundedButton RgstrBtn;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton4;
        private Siticone.UI.WinForms.SiticoneRoundedTextBox siticoneRoundedTextBox1;
        private Siticone.UI.WinForms.SiticoneRoundedTextBox siticoneRoundedTextBox2;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton1;
        private System.Windows.Forms.Label key;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Siticone.UI.WinForms.SiticoneRoundedTextBox siticoneRoundedTextBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton5;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private Siticone.UI.WinForms.SiticoneControlBox siticoneControlBox1;
        private Siticone.UI.WinForms.SiticoneShadowForm siticoneShadowForm;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label16;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton7;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton9;
        private Siticone.UI.WinForms.SiticoneRoundedTextBox siticoneRoundedTextBox4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private Siticone.UI.WinForms.SiticoneRoundedButton siticoneRoundedButton10;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnTools;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PseudoJoueur;
        private System.Windows.Forms.DataGridViewTextBoxColumn RaisonWarn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PseudoStaff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Datedederniereedition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date_de_fin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Statue;
    }
}
