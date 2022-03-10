namespace EvaluaRubrica
{
    partial class FrmParametros
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
            this.gbxRutaCarpetas = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRutaCarpetas = new System.Windows.Forms.TextBox();
            this.lblRutaCarpetas = new System.Windows.Forms.Label();
            this.gbxActividades = new System.Windows.Forms.GroupBox();
            this.nudActividades = new System.Windows.Forms.NumericUpDown();
            this.lblActividades = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.gbxPorcentajes = new System.Windows.Forms.GroupBox();
            this.chbRoundUp = new System.Windows.Forms.CheckBox();
            this.lblRound = new System.Windows.Forms.Label();
            this.nudPctExtemporaneo = new System.Windows.Forms.NumericUpDown();
            this.lblPctExtemporaneo = new System.Windows.Forms.Label();
            this.nudPctMejoramiento = new System.Windows.Forms.NumericUpDown();
            this.lblPctMejoramiento = new System.Windows.Forms.Label();
            this.chbActConMej = new System.Windows.Forms.CheckBox();
            this.lblActConMej = new System.Windows.Forms.Label();
            this.gbxRutaCarpetas.SuspendLayout();
            this.gbxActividades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudActividades)).BeginInit();
            this.gbxPorcentajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPctExtemporaneo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPctMejoramiento)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxRutaCarpetas
            // 
            this.gbxRutaCarpetas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxRutaCarpetas.Controls.Add(this.button1);
            this.gbxRutaCarpetas.Controls.Add(this.txtRutaCarpetas);
            this.gbxRutaCarpetas.Controls.Add(this.lblRutaCarpetas);
            this.gbxRutaCarpetas.Location = new System.Drawing.Point(12, 52);
            this.gbxRutaCarpetas.Name = "gbxRutaCarpetas";
            this.gbxRutaCarpetas.Size = new System.Drawing.Size(776, 70);
            this.gbxRutaCarpetas.TabIndex = 0;
            this.gbxRutaCarpetas.TabStop = false;
            this.gbxRutaCarpetas.Text = "Ruta de archivos";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::EvaluaRubrica.Properties.Resources.folder_open2;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(566, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRutaCarpetas
            // 
            this.txtRutaCarpetas.Location = new System.Drawing.Point(260, 30);
            this.txtRutaCarpetas.Name = "txtRutaCarpetas";
            this.txtRutaCarpetas.ReadOnly = true;
            this.txtRutaCarpetas.Size = new System.Drawing.Size(300, 20);
            this.txtRutaCarpetas.TabIndex = 1;
            // 
            // lblRutaCarpetas
            // 
            this.lblRutaCarpetas.AutoSize = true;
            this.lblRutaCarpetas.Location = new System.Drawing.Point(12, 30);
            this.lblRutaCarpetas.Name = "lblRutaCarpetas";
            this.lblRutaCarpetas.Size = new System.Drawing.Size(133, 13);
            this.lblRutaCarpetas.TabIndex = 0;
            this.lblRutaCarpetas.Text = "Ruta principal de archivos:";
            // 
            // gbxActividades
            // 
            this.gbxActividades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxActividades.Controls.Add(this.nudActividades);
            this.gbxActividades.Controls.Add(this.lblActividades);
            this.gbxActividades.Location = new System.Drawing.Point(12, 128);
            this.gbxActividades.Name = "gbxActividades";
            this.gbxActividades.Size = new System.Drawing.Size(776, 79);
            this.gbxActividades.TabIndex = 1;
            this.gbxActividades.TabStop = false;
            this.gbxActividades.Text = "Bloques y actividades";
            // 
            // nudActividades
            // 
            this.nudActividades.Location = new System.Drawing.Point(260, 32);
            this.nudActividades.Name = "nudActividades";
            this.nudActividades.Size = new System.Drawing.Size(80, 20);
            this.nudActividades.TabIndex = 1;
            // 
            // lblActividades
            // 
            this.lblActividades.AutoSize = true;
            this.lblActividades.Location = new System.Drawing.Point(15, 32);
            this.lblActividades.Name = "lblActividades";
            this.lblActividades.Size = new System.Drawing.Size(174, 13);
            this.lblActividades.TabIndex = 0;
            this.lblActividades.Text = "Cantidad de actividades por bloque";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(331, 29);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "PARAMETROS GENERALES";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Location = new System.Drawing.Point(647, 384);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(141, 32);
            this.btnGuardar.TabIndex = 38;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // gbxPorcentajes
            // 
            this.gbxPorcentajes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxPorcentajes.Controls.Add(this.chbActConMej);
            this.gbxPorcentajes.Controls.Add(this.lblActConMej);
            this.gbxPorcentajes.Controls.Add(this.chbRoundUp);
            this.gbxPorcentajes.Controls.Add(this.lblRound);
            this.gbxPorcentajes.Controls.Add(this.nudPctExtemporaneo);
            this.gbxPorcentajes.Controls.Add(this.lblPctExtemporaneo);
            this.gbxPorcentajes.Controls.Add(this.nudPctMejoramiento);
            this.gbxPorcentajes.Controls.Add(this.lblPctMejoramiento);
            this.gbxPorcentajes.Location = new System.Drawing.Point(12, 213);
            this.gbxPorcentajes.Name = "gbxPorcentajes";
            this.gbxPorcentajes.Size = new System.Drawing.Size(776, 155);
            this.gbxPorcentajes.TabIndex = 2;
            this.gbxPorcentajes.TabStop = false;
            this.gbxPorcentajes.Text = "Porcentajes de nota";
            // 
            // chbRoundUp
            // 
            this.chbRoundUp.AutoSize = true;
            this.chbRoundUp.Location = new System.Drawing.Point(260, 98);
            this.chbRoundUp.Name = "chbRoundUp";
            this.chbRoundUp.Size = new System.Drawing.Size(15, 14);
            this.chbRoundUp.TabIndex = 5;
            this.chbRoundUp.UseVisualStyleBackColor = true;
            // 
            // lblRound
            // 
            this.lblRound.AutoSize = true;
            this.lblRound.Location = new System.Drawing.Point(15, 98);
            this.lblRound.Name = "lblRound";
            this.lblRound.Size = new System.Drawing.Size(182, 13);
            this.lblRound.TabIndex = 4;
            this.lblRound.Text = "Redondear al numero entero superior";
            // 
            // nudPctExtemporaneo
            // 
            this.nudPctExtemporaneo.Location = new System.Drawing.Point(260, 65);
            this.nudPctExtemporaneo.Name = "nudPctExtemporaneo";
            this.nudPctExtemporaneo.Size = new System.Drawing.Size(80, 20);
            this.nudPctExtemporaneo.TabIndex = 3;
            // 
            // lblPctExtemporaneo
            // 
            this.lblPctExtemporaneo.AutoSize = true;
            this.lblPctExtemporaneo.Location = new System.Drawing.Point(15, 67);
            this.lblPctExtemporaneo.Name = "lblPctExtemporaneo";
            this.lblPctExtemporaneo.Size = new System.Drawing.Size(191, 13);
            this.lblPctExtemporaneo.TabIndex = 2;
            this.lblPctExtemporaneo.Text = "Porcentaje de nota para extemporaneo";
            // 
            // nudPctMejoramiento
            // 
            this.nudPctMejoramiento.Location = new System.Drawing.Point(260, 30);
            this.nudPctMejoramiento.Name = "nudPctMejoramiento";
            this.nudPctMejoramiento.Size = new System.Drawing.Size(80, 20);
            this.nudPctMejoramiento.TabIndex = 1;
            // 
            // lblPctMejoramiento
            // 
            this.lblPctMejoramiento.AutoSize = true;
            this.lblPctMejoramiento.Location = new System.Drawing.Point(15, 32);
            this.lblPctMejoramiento.Name = "lblPctMejoramiento";
            this.lblPctMejoramiento.Size = new System.Drawing.Size(186, 13);
            this.lblPctMejoramiento.TabIndex = 0;
            this.lblPctMejoramiento.Text = "Porcentaje de nota para mejoramiento";
            // 
            // chbActConMej
            // 
            this.chbActConMej.AutoSize = true;
            this.chbActConMej.Location = new System.Drawing.Point(260, 126);
            this.chbActConMej.Name = "chbActConMej";
            this.chbActConMej.Size = new System.Drawing.Size(15, 14);
            this.chbActConMej.TabIndex = 7;
            this.chbActConMej.UseVisualStyleBackColor = true;
            // 
            // lblActConMej
            // 
            this.lblActConMej.AutoSize = true;
            this.lblActConMej.Location = new System.Drawing.Point(15, 127);
            this.lblActConMej.Name = "lblActConMej";
            this.lblActConMej.Size = new System.Drawing.Size(220, 13);
            this.lblActConMej.TabIndex = 6;
            this.lblActConMej.Text = "Sumar la nota de la actividad al mejoramiento";
            // 
            // FrmParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 516);
            this.Controls.Add(this.gbxPorcentajes);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.gbxActividades);
            this.Controls.Add(this.gbxRutaCarpetas);
            this.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.Name = "FrmParametros";
            this.Text = "FrmParametros";
            this.gbxRutaCarpetas.ResumeLayout(false);
            this.gbxRutaCarpetas.PerformLayout();
            this.gbxActividades.ResumeLayout(false);
            this.gbxActividades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudActividades)).EndInit();
            this.gbxPorcentajes.ResumeLayout(false);
            this.gbxPorcentajes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPctExtemporaneo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPctMejoramiento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxRutaCarpetas;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRutaCarpetas;
        private System.Windows.Forms.Label lblRutaCarpetas;
        private System.Windows.Forms.GroupBox gbxActividades;
        private System.Windows.Forms.NumericUpDown nudActividades;
        private System.Windows.Forms.Label lblActividades;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox gbxPorcentajes;
        private System.Windows.Forms.NumericUpDown nudPctExtemporaneo;
        private System.Windows.Forms.Label lblPctExtemporaneo;
        private System.Windows.Forms.NumericUpDown nudPctMejoramiento;
        private System.Windows.Forms.Label lblPctMejoramiento;
        private System.Windows.Forms.CheckBox chbRoundUp;
        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.CheckBox chbActConMej;
        private System.Windows.Forms.Label lblActConMej;
    }
}