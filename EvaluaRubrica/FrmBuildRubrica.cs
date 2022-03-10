using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvaluaRubrica
{
    public partial class FrmBuildRubrica : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();
        FrmActividades f2 = Application.OpenForms.OfType<FrmActividades>().SingleOrDefault();

        TextBox[,] txtsleyendas = new TextBox[10,10];
        NumericUpDown[,] nudPunteos = new NumericUpDown[10, 10];
        Label[,] lblPunteos = new Label[10, 10];
        int filas = 1, columnas = 1;
        Graphics graficador;
        Timer timer1;

        int eVertical = 50;
        int eHorizontal = 0;
        int eEntreControles = 30;
        int txtWidth = 100;
        int txtHeight = 40;
        int nudWidth = 50;
        int nudHeight = 20;


        public FrmBuildRubrica()
        {
            InitializeComponent();
            graficador = this.CreateGraphics();
            llenaTabla();
            //graficador = pictureBox1.CreateGraphics();
            timer1 = new Timer();
            timer1.Enabled = false;
            timer1.Interval = 10;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer1.Start();

                                    
            
        }

        public void llenaTabla()
        {
            limpiaControles();
            conn.Open();
            string query = $"SELECT actividadid CODIGO, columna COLUMNA, fila FILA, texto TEXTO, valor VALOR FROM Actividades_detalle WHERE actividadid = {f2.codigoActividad}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(Convert.ToInt32(dt.Rows[i]["COLUMNA"]) > columnas)
                    {
                        columnas = Convert.ToInt32(dt.Rows[i]["COLUMNA"]);
                    }
                    if (Convert.ToInt32(dt.Rows[i]["FILA"]) > filas)
                    {
                        filas = Convert.ToInt32(dt.Rows[i]["FILA"]);
                    }
                }
                generaControles();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int x = Convert.ToInt32(dt.Rows[i]["COLUMNA"]);
                    int y = Convert.ToInt32(dt.Rows[i]["FILA"]);

                    txtsleyendas[x,y].Text = Convert.ToString(dt.Rows[i]["TEXTO"]);

                    if(x > 0 && y > 0)
                    {
                        nudPunteos[x,y].Value = Convert.ToInt32(dt.Rows[i]["VALOR"]);
                    }                   
                }

            }
            else
            {
                generaControles();
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //graficador = this.CreateGraphics();
            //graficador.Clear(Color.FromArgb(240, 240, 240));
            //dibujaLineas(graficador);
            actualizaDisenio();
            timer1.Stop();
        }

        private void actualizaDisenio()
        {
            for (int y = 0; y <= filas; y++)
            {
                for (int x = 0; x <= columnas; x++)
                {
                    ((TextBox)txtsleyendas[x, y]).Size = new System.Drawing.Size(txtWidth, txtHeight);
                    ((TextBox)txtsleyendas[x, y]).Location = locationControls(x, y, "txt");
                    if (x != 0 && y != 0)
                    {
                        ((NumericUpDown)nudPunteos[x, y]).Size = new System.Drawing.Size(50, 20);
                        ((NumericUpDown)nudPunteos[x, y]).Location = locationControls(x, y, "nud");
                        ((Label)lblPunteos[x, y]).Location = locationControls(x, y, "lbl");
                    }
                }
            }
            panel2.AutoScroll = true;
            panel2.SetAutoScrollMargin(5, 5);
        }

        public void generaControles()
        {
            for(int y = 0; y <= filas; y++)
            {
                for (int x = 0; x <= columnas; x++)
                {
                    txtsleyendas[x,y] = createTextBox(x,y);

                    if(x != 0 && y != 0)
                    { 
                        nudPunteos[x, y] = createNumericUpDown(x,y);
                        lblPunteos[x, y] = createLabel(x,y);
                    }
                }
            }            
            dibujaLineas(graficador);
            #region comentado
            /*Button btnNewColumn = new Button();
            btnNewColumn.Size = new System.Drawing.Size(60, 20);
            //btnNewColumn.Location = new System.Drawing.Point(((this.Width - ((columnas + 1) * 135 - 35)) / 2 + (135 * (columnas + 1))), (100));
            btnNewColumn.Location = new System.Drawing.Point(30, 10);
            //btnNewColumn.Anchor = AnchorStyles.None;
            btnNewColumn.AutoSize = false;
            btnNewColumn.Text = "+ COL";
            btnNewColumn.Name = $"btnNewColumn";
            btnNewColumn.Click += new System.EventHandler(this.newColumn);
            Controls.Add(btnNewColumn);

            Button btnNewRow = new Button();
            btnNewRow.Size = new System.Drawing.Size(60, 20);
            //btnNewRow.Location = new System.Drawing.Point(((this.Width - ((columnas + 1) * 135 - 35)) / 2), (100 + (75 * (filas + 1))));
            btnNewRow.Location = new System.Drawing.Point(30, 40);
            //btnNewRow.Anchor = AnchorStyles.None;
            btnNewRow.AutoSize = false;
            btnNewRow.Text = "+ FIL";
            btnNewRow.Name = $"btnNewRow";
            btnNewRow.Click += new System.EventHandler(this.newRow);
            Controls.Add(btnNewRow);

            Button btnDelColumn = new Button();
            btnDelColumn.Size = new System.Drawing.Size(60, 20);
            //btnNewColumn.Location = new System.Drawing.Point(((this.Width - ((columnas + 1) * 135 - 35)) / 2 + (135 * (columnas + 1))), (100));
            btnDelColumn.Location = new System.Drawing.Point(100, 10);
            //btnNewColumn.Anchor = AnchorStyles.None;
            btnDelColumn.AutoSize = false;
            btnDelColumn.Text = "- COL";
            btnDelColumn.Name = $"btnDelColumn";
            btnDelColumn.Click += new System.EventHandler(this.delColumn);
            Controls.Add(btnDelColumn);

            Button btnDelRow = new Button();
            btnDelRow.Size = new System.Drawing.Size(60, 20);
            //btnNewRow.Location = new System.Drawing.Point(((this.Width - ((columnas + 1) * 135 - 35)) / 2), (100 + (75 * (filas + 1))));
            btnDelRow.Location = new System.Drawing.Point(100, 40);
            //btnNewRow.Anchor = AnchorStyles.None;
            btnDelRow.AutoSize = false;
            btnDelRow.Text = "- FIL";
            btnDelRow.Name = $"btnDelRow";
            btnDelRow.Click += new System.EventHandler(this.delRow);
            Controls.Add(btnDelRow);*/
            #endregion
        }

        public void limpiaControles()
        {
            txtsleyendas = new TextBox[10, 10];
            nudPunteos = new NumericUpDown[10, 10];
            lblPunteos = new Label[10, 10];
            filas = 1;
            columnas = 1;
            while (panel2.Controls.Count > 0)
            {
                panel2.Controls.RemoveAt(0);
            }
        }

        private void newColumn(object sender, EventArgs e)
        {
            if (columnas == 9)
            {
                MessageBox.Show("No se pueden agregar mas de 10 columnas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                columnas += 1;
                for (int y = 0; y <= filas; y++)
                {
                    txtsleyendas[columnas, y] = createTextBox(columnas, y);

                    if (y != 0)
                    {
                        nudPunteos[columnas, y] = createNumericUpDown(columnas, y);
                        lblPunteos[columnas, y] = createLabel(columnas, y);
                    }
                }
                actualizaDisenio();
            }
        }

        private void newRow(object sender, EventArgs e)
        {
            if(filas == 9)
            {
                MessageBox.Show("No se pueden agregar mas de 10 filas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { 
                filas += 1;
                for (int x = 0; x <= columnas; x++)
                {
                    txtsleyendas[x, filas] = createTextBox(x, filas);

                    if (x != 0)
                    {
                        nudPunteos[x, filas] = createNumericUpDown(x, filas, true);
                        lblPunteos[x, filas] = createLabel(x, filas);
                    }                        
                }
                actualizaDisenio();
            }
        }

        private void delColumn(object sender, EventArgs e)
        {
            if (columnas == 1)
            {
                MessageBox.Show("Debe existir minimo una columna de valoración", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int y = 0; y <= filas; y++)
                {
                    removeControlsToWindow(txtsleyendas[columnas, y]);
                    removeControlsToWindow(nudPunteos[columnas, y]);
                    removeControlsToWindow(lblPunteos[columnas, y]);                                            
                }
                columnas -= 1;
                actualizaDisenio();
            }
        }

        private void delRow(object sender, EventArgs e)
        {
            if (filas == 1)
            {
                MessageBox.Show("Debe existir minimo una fila de valoración", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int x = 0; x <= columnas; x++)
                {
                    removeControlsToWindow(txtsleyendas[x, filas]);
                    removeControlsToWindow(nudPunteos[x, filas]);
                    removeControlsToWindow(lblPunteos[x, filas]);                        
                }
                filas -= 1;
                actualizaDisenio();
            }
        }

        private TextBox createTextBox(int x, int y)
        {
            TextBox txtDesc = new TextBox();
            txtDesc.Size = new System.Drawing.Size(txtWidth, txtHeight);
            //txtDesc.Location = new System.Drawing.Point(((this.Width - ((columnas + 1) * (textBoxWidth + eEntreControles) - eEntreControles)) / 2 + ((textBoxWidth + eEntreControles) * x)), (eVertical + ((textBoxHeight + eEntreControles) * y)));
            //txtDesc.Location = locationControls(x, y, "txt");
            txtDesc.MaxLength = 120;
            txtDesc.Multiline = true;
            txtDesc.Font = new Font(txtDesc.Font.FontFamily, Convert.ToInt16(7.5));
            txtDesc.Anchor = AnchorStyles.Top;
            txtDesc.Name = $"Texto{x}_{y}";
            txtDesc.BackColor = (x != 0 && y != 0 ? Color.FromArgb(202, 202, 202) : Color.White);
            //Controls.Add(txtDesc);
            addControlsToWindow(txtDesc);

            return txtDesc;
        }

        private NumericUpDown createNumericUpDown(int x, int y, bool newRow = false)
        {
            NumericUpDown nudPts = new NumericUpDown();
            nudPts.Size = new System.Drawing.Size(nudWidth, nudHeight);
            //nudPts.Location = new System.Drawing.Point((((this.Width - ((columnas + 1) * (textBoxWidth + eEntreControles) - eEntreControles)) / 2) + ((textBoxWidth + eEntreControles) * x) + 50), (eVertical + ((textBoxHeight + eEntreControles) * y) + 40));
            //nudPts.Location = locationControls(x, y, "nud");
            nudPts.Font = new Font(nudPts.Font.FontFamily, Convert.ToInt16(7.5));
            nudPts.Anchor = AnchorStyles.Top;
            if (newRow) nudPts.Value = nudPunteos[x, y - 1].Value;
            nudPts.DecimalPlaces = 1;
            nudPts.Name = $"Punteo{x}_{y}";
            //Controls.Add(nudPts);
            addControlsToWindow(nudPts);

            return nudPts;
        }

        private Label createLabel(int x, int y)
        {
            Label lblPts = new Label();
            //lblPts.Location = new System.Drawing.Point((((this.Width - ((columnas + 1) * (textBoxWidth + eEntreControles) - eEntreControles)) / 2) + ((textBoxWidth + eEntreControles) * x)), (eVertical + ((textBoxHeight + eEntreControles) * y) + 40));
            //lblPts.Location = locationControls(x, y, "lbl");
            lblPts.Font = new Font(lblPts.Font.FontFamily, Convert.ToInt16(7.5));
            lblPts.Anchor = AnchorStyles.Top;
            lblPts.Text = "Valor: ";
            lblPts.Name = $"lblPunteo{x}_{y}";
            //Controls.Add(lblPts);
            addControlsToWindow(lblPts);

            return lblPts;
        }

        private Point locationControls(int _x, int _y, string _ctrl)
        {
            int x = (((this.Width - ((columnas + 1) * (txtWidth + eEntreControles) - eEntreControles)) / 2) + ((txtWidth + eEntreControles) * _x));
            int y = (eVertical + ((txtHeight + eEntreControles) * _y));

            switch (_ctrl)
            {
                case "nud":
                    x += txtWidth - nudWidth;
                    y += txtHeight;
                    break;
                case "lbl":
                    y += txtHeight;
                    break;
            }

            return new System.Drawing.Point(x, y);
        }

        private void addControlsToWindow(Control _control)
        {
            panel2.Controls.Add(_control);
        }

        private void removeControlsToWindow(Control _control)
        {
            panel2.Controls.Remove(_control);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            eliminaData();
            string texto = "";
            decimal valor = 0;
            bool error = false;
            for (int y = 0; y <= filas; y++)
            {
                for (int x = 0; x <= columnas; x++)
                {
                    texto = txtsleyendas[x, y].Text;
                    if (x > 0 && y > 0)
                        valor = nudPunteos[x, y].Value;
                    else
                        valor = 0;

                    try
                    {
                        conn.Open();
                        string query = $"INSERT INTO Actividades_detalle (actividadid, columna, fila, texto, valor) VALUES({f2.codigoActividad},{x},{y},'{texto}',{valor})";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();                        
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        error = true;                        
                    }
                }
            }

            if (!error)
            {
                error = actualizaTablaActividad();
            }

            if (!error)
            {
                f2.llenaTabla(true);
                MessageBox.Show("Se ha guradado correctamente la tabla de valores", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hubo errores al guardar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool actualizaTablaActividad()
        {
            bool error = false;
            try
            {
                conn.Open();
                string query = $"UPDATE Actividades SET tabla_configurada = 1 WHERE actividadid = {f2.codigoActividad}";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                error = true;
            }
            return error;
        }

        private void eliminaData()
        {
            try
            {
                conn.Open();
                string query = $"DELETE FROM Actividades_detalle WHERE actividadid = {f2.codigoActividad}";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void btnCopyTable_Click(object sender, EventArgs e)
        {
            panelCopyTable.Visible = true;
            llenaCombo();
        }

        private void btnImportDataCancel_Click(object sender, EventArgs e)
        {
            panelCopyTable.Visible = false;
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            int codigoAsig = Convert.ToInt32((cbxAsignatura.SelectedItem as DataRowView).Row.ItemArray[0]);
            int codigoActi = Convert.ToInt32((cbxActividad.SelectedItem as DataRowView).Row.ItemArray[0]);

            ///
            eliminaData();
            bool error = false;
            try
            {
                conn.Open();
                string query = $@"insert into Actividades_detalle (actividadid,columna,fila,texto,valor)
select {f2.codigoActividad}, acdet.columna, acdet.fila, acdet.texto,acdet.valor 
from Actividades_detalle acdet 
where acdet.actividadid = {codigoActi} ";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                error = true;
            }

            if (!error)
            {
                error = actualizaTablaActividad();
            }

            if (!error)
            {
                f2.llenaTabla(true);
                MessageBox.Show("Se ha guradado correctamente la tabla de valores", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hubo errores al guardar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            panelCopyTable.Visible = false;

            llenaTabla();
            timer1.Start();
        }

        public void llenaCombo()
        {
            conn.Open();
            string query = $"SELECT asignaturaid Value, descripcion || ' ' || ciclo || ' - ' || grado || ' ' || nivel || ' ' || seccion Text from Asignaturas where userid = {f1.codigoUsr} and estado = 1 order by descripcion";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            cbxAsignatura.DataSource = dt.DefaultView;
            cbxAsignatura.DisplayMember = "Text";
        }

        public void llenaCombo2(int asig)
        {
            conn.Open();
            string query = $@"select actividadid Value, 'Bloque ' || bloque || ' - ' || descripcion Text 
from Actividades
where asignaturaid = {asig}
and tabla_configurada = 1 
order by bloque";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            cbxActividad.DataSource = dt.DefaultView;
            cbxActividad.DisplayMember = "Text";
        }

        private void cbxAsignatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoAsig = Convert.ToInt32((cbxAsignatura.SelectedItem as DataRowView).Row.ItemArray[0]);

            llenaCombo2(codigoAsig);
        }

        private void dibujaLineas(Graphics g)
        {
            
            // Linea horizontal
            Point p1 = new Point(((this.Width - ((columnas + 1) * 135 - 35)) / 2), 155), p2 = new Point((this.Width - ((columnas + 1) * 135 - 35)) / 2 + (135 * (columnas + 1)) - 35, 155);
            g.DrawLine(new Pen(Color.Black, 4), p1, p2);

            // Linea Vertical
            Point p3 = new Point(((this.Width - ((columnas - 1) * 135 - 15)) / 2), 100), p4 = new Point(((this.Width - ((columnas - 1) * 135 - 15)) / 2), 100 + (75 * (filas + 1)) - 35);
            g.DrawLine(new Pen(Color.Black, 4), p3, p4);
        }
    }
}
