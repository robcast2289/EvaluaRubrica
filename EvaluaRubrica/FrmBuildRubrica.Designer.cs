namespace EvaluaRubrica
{
    partial class FrmBuildRubrica
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lblColumns = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelCopyTable = new System.Windows.Forms.Panel();
            this.btnCopyTable = new System.Windows.Forms.Button();
            this.cbxAsignatura = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxActividad = new System.Windows.Forms.ComboBox();
            this.btnImportDataCancel = new System.Windows.Forms.Button();
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCopyTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(151, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "+ Colum";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.newColumn);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(185, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "- Colum";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.delColumn);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(151, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(28, 22);
            this.button3.TabIndex = 2;
            this.button3.Text = "+ Fila";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.newRow);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(185, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 22);
            this.button4.TabIndex = 3;
            this.button4.Text = "- Fila";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.delRow);
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(9, 7);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(136, 13);
            this.lblColumns.TabIndex = 6;
            this.lblColumns.Text = "Insertar y eliminar columnas";
            this.lblColumns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(36, 30);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(109, 13);
            this.lblRows.TabIndex = 7;
            this.lblRows.Text = "Insertar y eliminar filas";
            this.lblRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReturn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCopyTable, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 303F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 56);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblRows);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.lblColumns);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(246, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 48);
            this.panel1.TabIndex = 0;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = global::EvaluaRubrica.Properties.Resources.save2;
            this.button6.Location = new System.Drawing.Point(125, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 48);
            this.button6.TabIndex = 5;
            this.button6.Text = "Guardar tabla";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Image = global::EvaluaRubrica.Properties.Resources.return3;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(4, 4);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(95, 48);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Regresar";
            this.btnReturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoScrollMinSize = new System.Drawing.Size(300, 300);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 394);
            this.panel2.TabIndex = 10;
            // 
            // panelCopyTable
            // 
            this.panelCopyTable.BackColor = System.Drawing.Color.FloralWhite;
            this.panelCopyTable.Controls.Add(this.btnImportDataCancel);
            this.panelCopyTable.Controls.Add(this.btnCargarArchivo);
            this.panelCopyTable.Controls.Add(this.label2);
            this.panelCopyTable.Controls.Add(this.cbxActividad);
            this.panelCopyTable.Controls.Add(this.label1);
            this.panelCopyTable.Controls.Add(this.cbxAsignatura);
            this.panelCopyTable.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelCopyTable.Location = new System.Drawing.Point(596, 56);
            this.panelCopyTable.Name = "panelCopyTable";
            this.panelCopyTable.Size = new System.Drawing.Size(204, 394);
            this.panelCopyTable.TabIndex = 11;
            this.panelCopyTable.Visible = false;
            // 
            // btnCopyTable
            // 
            this.btnCopyTable.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCopyTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyTable.Location = new System.Drawing.Point(477, 4);
            this.btnCopyTable.Name = "btnCopyTable";
            this.btnCopyTable.Size = new System.Drawing.Size(114, 48);
            this.btnCopyTable.TabIndex = 6;
            this.btnCopyTable.Text = "Copiar tabla desde otra actividad";
            this.btnCopyTable.UseVisualStyleBackColor = false;
            this.btnCopyTable.Click += new System.EventHandler(this.btnCopyTable_Click);
            // 
            // cbxAsignatura
            // 
            this.cbxAsignatura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAsignatura.FormattingEnabled = true;
            this.cbxAsignatura.Location = new System.Drawing.Point(15, 41);
            this.cbxAsignatura.Name = "cbxAsignatura";
            this.cbxAsignatura.Size = new System.Drawing.Size(177, 21);
            this.cbxAsignatura.TabIndex = 0;
            this.cbxAsignatura.SelectedIndexChanged += new System.EventHandler(this.cbxAsignatura_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asignatura";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Actividad";
            // 
            // cbxActividad
            // 
            this.cbxActividad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActividad.FormattingEnabled = true;
            this.cbxActividad.Location = new System.Drawing.Point(15, 103);
            this.cbxActividad.Name = "cbxActividad";
            this.cbxActividad.Size = new System.Drawing.Size(177, 21);
            this.cbxActividad.TabIndex = 2;
            // 
            // btnImportDataCancel
            // 
            this.btnImportDataCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnImportDataCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportDataCancel.Location = new System.Drawing.Point(108, 186);
            this.btnImportDataCancel.Name = "btnImportDataCancel";
            this.btnImportDataCancel.Size = new System.Drawing.Size(84, 23);
            this.btnImportDataCancel.TabIndex = 8;
            this.btnImportDataCancel.Text = "Cancelar";
            this.btnImportDataCancel.UseVisualStyleBackColor = false;
            this.btnImportDataCancel.Click += new System.EventHandler(this.btnImportDataCancel_Click);
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCargarArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarArchivo.Location = new System.Drawing.Point(15, 186);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(87, 23);
            this.btnCargarArchivo.TabIndex = 7;
            this.btnCargarArchivo.Text = "Copiar";
            this.btnCargarArchivo.UseVisualStyleBackColor = false;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // FrmBuildRubrica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelCopyTable);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmBuildRubrica";
            this.Text = "FrmBuildRubrica";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCopyTable.ResumeLayout(false);
            this.panelCopyTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelCopyTable;
        private System.Windows.Forms.Button btnCopyTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxActividad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxAsignatura;
        private System.Windows.Forms.Button btnImportDataCancel;
        private System.Windows.Forms.Button btnCargarArchivo;
    }
}