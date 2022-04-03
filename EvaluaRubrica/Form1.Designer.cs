namespace EvaluaRubrica
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnLogoutMenu = new System.Windows.Forms.Button();
            this.panelConfigSubmenu = new System.Windows.Forms.Panel();
            this.btnGeneralConfig = new System.Windows.Forms.Button();
            this.btnCourses = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnConfigMenu = new System.Windows.Forms.Button();
            this.panelData = new System.Windows.Forms.Panel();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnActividades = new System.Windows.Forms.Button();
            this.btnAlumnos = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.cbxSelectAsig = new System.Windows.Forms.ComboBox();
            this.panelChildFormContent = new System.Windows.Forms.Panel();
            this.panelSideMenu.SuspendLayout();
            this.panelConfigSubmenu.SuspendLayout();
            this.panelData.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.AutoScroll = true;
            this.panelSideMenu.BackColor = System.Drawing.Color.Black;
            this.panelSideMenu.Controls.Add(this.lblVersion);
            this.panelSideMenu.Controls.Add(this.btnLogoutMenu);
            this.panelSideMenu.Controls.Add(this.panelConfigSubmenu);
            this.panelSideMenu.Controls.Add(this.btnConfigMenu);
            this.panelSideMenu.Controls.Add(this.panelData);
            this.panelSideMenu.Controls.Add(this.btnData);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(250, 554);
            this.panelSideMenu.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblVersion.Location = new System.Drawing.Point(3, 537);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(96, 17);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Version: 1.0.5";
            // 
            // btnLogoutMenu
            // 
            this.btnLogoutMenu.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogoutMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogoutMenu.FlatAppearance.BorderSize = 0;
            this.btnLogoutMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogoutMenu.ForeColor = System.Drawing.Color.White;
            this.btnLogoutMenu.Image = global::EvaluaRubrica.Properties.Resources.exit_4;
            this.btnLogoutMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogoutMenu.Location = new System.Drawing.Point(0, 430);
            this.btnLogoutMenu.Name = "btnLogoutMenu";
            this.btnLogoutMenu.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLogoutMenu.Size = new System.Drawing.Size(250, 45);
            this.btnLogoutMenu.TabIndex = 2;
            this.btnLogoutMenu.Text = "Cerrar sesión";
            this.btnLogoutMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogoutMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogoutMenu.UseVisualStyleBackColor = false;
            this.btnLogoutMenu.Click += new System.EventHandler(this.btnLogoutMenu_Click);
            // 
            // panelConfigSubmenu
            // 
            this.panelConfigSubmenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelConfigSubmenu.Controls.Add(this.btnGeneralConfig);
            this.panelConfigSubmenu.Controls.Add(this.btnCourses);
            this.panelConfigSubmenu.Controls.Add(this.btnUsers);
            this.panelConfigSubmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConfigSubmenu.Location = new System.Drawing.Point(0, 310);
            this.panelConfigSubmenu.Name = "panelConfigSubmenu";
            this.panelConfigSubmenu.Size = new System.Drawing.Size(250, 120);
            this.panelConfigSubmenu.TabIndex = 1;
            // 
            // btnGeneralConfig
            // 
            this.btnGeneralConfig.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnGeneralConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGeneralConfig.FlatAppearance.BorderSize = 0;
            this.btnGeneralConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(110)))));
            this.btnGeneralConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneralConfig.Location = new System.Drawing.Point(0, 80);
            this.btnGeneralConfig.Name = "btnGeneralConfig";
            this.btnGeneralConfig.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnGeneralConfig.Size = new System.Drawing.Size(250, 40);
            this.btnGeneralConfig.TabIndex = 2;
            this.btnGeneralConfig.Text = "General";
            this.btnGeneralConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGeneralConfig.UseVisualStyleBackColor = false;
            this.btnGeneralConfig.Click += new System.EventHandler(this.btnGeneralConfig_Click);
            // 
            // btnCourses
            // 
            this.btnCourses.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnCourses.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCourses.FlatAppearance.BorderSize = 0;
            this.btnCourses.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(110)))));
            this.btnCourses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCourses.Location = new System.Drawing.Point(0, 40);
            this.btnCourses.Name = "btnCourses";
            this.btnCourses.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnCourses.Size = new System.Drawing.Size(250, 40);
            this.btnCourses.TabIndex = 1;
            this.btnCourses.Text = "Asignaturas";
            this.btnCourses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCourses.UseVisualStyleBackColor = false;
            this.btnCourses.Click += new System.EventHandler(this.btnCourses_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(110)))));
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Location = new System.Drawing.Point(0, 0);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnUsers.Size = new System.Drawing.Size(250, 40);
            this.btnUsers.TabIndex = 0;
            this.btnUsers.Text = "Usuarios";
            this.btnUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnConfigMenu
            // 
            this.btnConfigMenu.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnConfigMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConfigMenu.FlatAppearance.BorderSize = 0;
            this.btnConfigMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigMenu.ForeColor = System.Drawing.Color.White;
            this.btnConfigMenu.Image = global::EvaluaRubrica.Properties.Resources.config6;
            this.btnConfigMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigMenu.Location = new System.Drawing.Point(0, 265);
            this.btnConfigMenu.Name = "btnConfigMenu";
            this.btnConfigMenu.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnConfigMenu.Size = new System.Drawing.Size(250, 45);
            this.btnConfigMenu.TabIndex = 0;
            this.btnConfigMenu.Text = "Configuración";
            this.btnConfigMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfigMenu.UseVisualStyleBackColor = false;
            this.btnConfigMenu.Click += new System.EventHandler(this.btnConfigMenu_Click);
            // 
            // panelData
            // 
            this.panelData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelData.Controls.Add(this.btnReportes);
            this.panelData.Controls.Add(this.btnActividades);
            this.panelData.Controls.Add(this.btnAlumnos);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelData.Location = new System.Drawing.Point(0, 145);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(250, 120);
            this.panelData.TabIndex = 4;
            // 
            // btnReportes
            // 
            this.btnReportes.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnReportes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(110)))));
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Location = new System.Drawing.Point(0, 80);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnReportes.Size = new System.Drawing.Size(250, 40);
            this.btnReportes.TabIndex = 2;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnActividades
            // 
            this.btnActividades.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnActividades.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnActividades.FlatAppearance.BorderSize = 0;
            this.btnActividades.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(110)))));
            this.btnActividades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActividades.Location = new System.Drawing.Point(0, 40);
            this.btnActividades.Name = "btnActividades";
            this.btnActividades.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnActividades.Size = new System.Drawing.Size(250, 40);
            this.btnActividades.TabIndex = 1;
            this.btnActividades.Text = "Actividades";
            this.btnActividades.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActividades.UseVisualStyleBackColor = false;
            this.btnActividades.Click += new System.EventHandler(this.btnActividades_Click);
            // 
            // btnAlumnos
            // 
            this.btnAlumnos.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnAlumnos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAlumnos.FlatAppearance.BorderSize = 0;
            this.btnAlumnos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(110)))));
            this.btnAlumnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlumnos.Location = new System.Drawing.Point(0, 0);
            this.btnAlumnos.Name = "btnAlumnos";
            this.btnAlumnos.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnAlumnos.Size = new System.Drawing.Size(250, 40);
            this.btnAlumnos.TabIndex = 0;
            this.btnAlumnos.Text = "Alumnos";
            this.btnAlumnos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlumnos.UseVisualStyleBackColor = false;
            this.btnAlumnos.Click += new System.EventHandler(this.btnAlumnos_Click);
            // 
            // btnData
            // 
            this.btnData.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnData.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnData.FlatAppearance.BorderSize = 0;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.ForeColor = System.Drawing.Color.White;
            this.btnData.Image = global::EvaluaRubrica.Properties.Resources.data2;
            this.btnData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnData.Location = new System.Drawing.Point(0, 100);
            this.btnData.Name = "btnData";
            this.btnData.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnData.Size = new System.Drawing.Size(250, 45);
            this.btnData.TabIndex = 3;
            this.btnData.Text = "Datos";
            this.btnData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnData.UseVisualStyleBackColor = false;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.pictureBox1);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(250, 100);
            this.panelLogo.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::EvaluaRubrica.Properties.Resources.icon_white;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFilter.Controls.Add(this.label2);
            this.panelFilter.Controls.Add(this.lblUserName);
            this.panelFilter.Controls.Add(this.cbxSelectAsig);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(250, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(817, 77);
            this.panelFilter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ASIGNATURA";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(330, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(139, 31);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "USUARIO";
            // 
            // cbxSelectAsig
            // 
            this.cbxSelectAsig.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxSelectAsig.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSelectAsig.DisplayMember = "1";
            this.cbxSelectAsig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectAsig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSelectAsig.FormattingEnabled = true;
            this.cbxSelectAsig.Location = new System.Drawing.Point(257, 46);
            this.cbxSelectAsig.Name = "cbxSelectAsig";
            this.cbxSelectAsig.Size = new System.Drawing.Size(437, 24);
            this.cbxSelectAsig.TabIndex = 1;
            this.cbxSelectAsig.SelectedIndexChanged += new System.EventHandler(this.cbxSelectAsig_SelectedIndexChanged);
            // 
            // panelChildFormContent
            // 
            this.panelChildFormContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildFormContent.Location = new System.Drawing.Point(250, 77);
            this.panelChildFormContent.Name = "panelChildFormContent";
            this.panelChildFormContent.Size = new System.Drawing.Size(817, 477);
            this.panelChildFormContent.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.panelChildFormContent);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelSideMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1083, 593);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EvaluaRubrica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelSideMenu.ResumeLayout(false);
            this.panelSideMenu.PerformLayout();
            this.panelConfigSubmenu.ResumeLayout(false);
            this.panelData.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Panel panelConfigSubmenu;
        private System.Windows.Forms.Button btnGeneralConfig;
        private System.Windows.Forms.Button btnCourses;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnConfigMenu;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.ComboBox cbxSelectAsig;
        private System.Windows.Forms.Panel panelChildFormContent;
        private System.Windows.Forms.Button btnLogoutMenu;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnActividades;
        private System.Windows.Forms.Button btnAlumnos;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Label lblVersion;
    }
}

