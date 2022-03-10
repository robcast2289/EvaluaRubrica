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
    public partial class FrmActividades : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();
        public int bloque = 1;
        public int codigoActividad;
        public string codigoAlumno;

        public FrmActividades()
        {
            InitializeComponent();
            radioButton1.CheckedChanged += new System.EventHandler(this.radioChange);
            radioButton2.CheckedChanged += new System.EventHandler(this.radioChange);
            radioButton3.CheckedChanged += new System.EventHandler(this.radioChange);
            radioButton4.CheckedChanged += new System.EventHandler(this.radioChange);
            // Codigo para seleccionar el bloque guardado
            radioButton1.Checked = true;
            llenaTabla();
        }

        public void llenaTabla(bool selectRow = false)
        {
            int actividadidSelected = 0;
            if (dgvActividades.CurrentRow != null && selectRow)
            {
                actividadidSelected = Convert.ToInt32(dgvActividades.CurrentRow.Cells["CODIGO"].Value);
            }
            conn.Open();
            string query = $"SELECT actividadid CODIGO, asignaturaid ASIGNATURA, bloque UNIDAD, descripcion DESCRIPCION, se_evaluara EVALUAR, tema TEMA, fecha_entrega FECHA, fecha_entrega_mejoramiento MEJORAMIENTO, punteo PUNTEO, tabla_configurada CONFIG FROM Actividades WHERE asignaturaid = {f1.codigoAsig} and bloque = {bloque}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            int sumaPunteos = 0;
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                sumaPunteos += Convert.ToInt32(dt.Rows[i]["PUNTEO"]);
            }            
            txtSumaPunteo.Text = $"{sumaPunteos}";
            lblAlert.Visible = (sumaPunteos != 100 ? true : false);
            

            dgvActividades.DataSource = dt;
            dgvActividades.Columns["CODIGO"].Visible = false;
            dgvActividades.Columns["ASIGNATURA"].Visible = false;
            dgvActividades.Columns["UNIDAD"].Visible = false;
            //dgvActividades.Columns["EVALUAR"].Visible = false;
            //dgvActividades.Columns["TEMA"].Visible = false;
            //dgvActividades.Columns["FECHA"].Visible = false;
            dgvActividades.Columns["MEJORAMIENTO"].Visible = false;
            dgvActividades.Columns["CONFIG"].Visible = false;

            if (selectRow)
            {
                foreach (DataGridViewRow item in dgvActividades.Rows)
                {
                    if (Convert.ToInt32(item.Cells["Codigo"].Value).Equals(actividadidSelected))
                    {
                        //item.Selected = true;
                        dgvActividades.Rows[item.Index].Cells[3].Selected = true;
                        guardarActividadSeleccionada();
                        break;
                    }
                }
            }            
        }

        public void llenaTablaAlumnos()
        {
            conn.Open();
            string query = $"SELECT alumnoid CODIGO, apellidos APELLIDOS, nombres NOMBRES FROM Alumnos WHERE asignaturaid = {f1.codigoAsig}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            dgvAlumnos.DataSource = dt;
        }

        private void limpiaCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtEvaluar.Text = string.Empty;
            txtTema.Text = string.Empty;
            nudPunteo.Value = 0;
            dtpFechaEntrega.Text = DateTime.Now.ToShortDateString();
            dtpFechaMejoramiento.Text = DateTime.Now.ToShortDateString();
            //configValoresCampos();
        }

        private void btnRubricaConfig_Click(object sender, EventArgs e)
        {
            if (dgvActividades.SelectedRows.Count > 0)
            {
                f1.openSubChildForm(new FrmBuildRubrica());
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una actividad para configurar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioChange(object sender, EventArgs e)
        {
            RadioButton rdbUnidad = (RadioButton)sender;

            if (rdbUnidad.Checked)
            {
                switch (rdbUnidad.Name)
                {
                    case "radioButton1":bloque = 1;break;
                    case "radioButton2":bloque = 2;break;
                    case "radioButton3":bloque = 3;break;
                    case "radioButton4":bloque = 4;break;                    
                }
            }
            tabControl1.SelectedIndex = 0;
            llenaTabla();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaCampos();
            txtDescripcion.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editarFila();
        }

        private void dgvActividades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvActividades.CurrentRow != null)
            {
                editarFila();
            }
        }

        private void dgvActividades_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvActividades.CurrentRow != null)
            {
                guardarActividadSeleccionada();
            }
        }

        private void guardarActividadSeleccionada()
        {
            codigoActividad = Convert.ToInt32(dgvActividades.CurrentRow.Cells["CODIGO"].Value);
            lblTituloActividad.Text = $"UNIDAD {bloque} - {Convert.ToString(dgvActividades.CurrentRow.Cells["DESCRIPCION"].Value).ToUpper()}";

            llenaTablaAlumnos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvActividades.SelectedRows.Count > 0)
            {
                string uid = Convert.ToString(dgvActividades.CurrentRow.Cells["CODIGO"].Value);
                string alumno = Convert.ToString(dgvActividades.CurrentRow.Cells["DESCRIPCION"].Value);
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show($"Deseas eliminar la actividad {alumno.ToUpper()}", "Confirmar", buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string query = $"DELETE FROM Actividades WHERE actividadid = {uid}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Actividad eliminada exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiaCampos();
                        llenaTabla();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay datos seleccionados para eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void editarFila()
        {
            limpiaCampos();
            if (dgvActividades.SelectedRows.Count > 0)
            {
                txtCodigo.Text = Convert.ToString(dgvActividades.CurrentRow.Cells["CODIGO"].Value);
                txtDescripcion.Text = Convert.ToString(dgvActividades.CurrentRow.Cells["DESCRIPCION"].Value);
                txtEvaluar.Text = Convert.ToString(dgvActividades.CurrentRow.Cells["EVALUAR"].Value);
                txtTema.Text = Convert.ToString(dgvActividades.CurrentRow.Cells["TEMA"].Value);
                nudPunteo.Value = Convert.ToInt32(dgvActividades.CurrentRow.Cells["PUNTEO"].Value);
                dtpFechaEntrega.Text = Convert.ToString(dgvActividades.CurrentRow.Cells["FECHA"].Value);
                dtpFechaMejoramiento.Text = Convert.ToString(dgvActividades.CurrentRow.Cells["MEJORAMIENTO"].Value);
            }
            else
            {
                MessageBox.Show("No hay datos seleccionados para editar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == string.Empty || nudPunteo.Value == 0)
            {
                MessageBox.Show("Debe llenar los datos obligatorios (*)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtCodigo.Text == string.Empty)
                {
                    try
                    {
                        conn.Open();
                        string query = $"INSERT INTO Actividades (bloque, asignaturaid, descripcion, se_evaluara, tema , fecha_entrega, fecha_entrega_mejoramiento, punteo, tabla_configurada) VALUES({bloque},{f1.codigoAsig},'{txtDescripcion.Text}','{txtEvaluar.Text}','{txtTema.Text}','{dtpFechaEntrega.Text}','{dtpFechaMejoramiento.Text}',{nudPunteo.Value},0)";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Actividad creado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiaCampos();
                        llenaTabla();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        conn.Open();
                        string query = $"UPDATE Actividades SET descripcion = '{txtDescripcion.Text}', se_evaluara = '{txtEvaluar.Text}', tema = '{txtTema.Text}', fecha_entrega = '{dtpFechaEntrega.Text}', fecha_entrega_mejoramiento = '{dtpFechaMejoramiento.Text}', punteo = {nudPunteo.Value} WHERE actividadid = {txtCodigo.Text}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Actividad actualizado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiaCampos();
                        llenaTabla(true);
                    }
                    catch (Exception ex)
                    {
                        conn.Close();

                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            if (dgvActividades.CurrentRow != null)
            {
                if (Convert.ToInt32(dgvActividades.CurrentRow.Cells["CONFIG"].Value) == 0)
                {
                    MessageBox.Show("Debes configurar la tabla de valores, en el boton de la esquina inferior derecha", "Espera!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    tabControl1.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("No hay actividades para calificar", "Espera!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tabControl1.SelectedIndex = 0;
            }                   
        }

        private void dgvAlumnos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlumnos.CurrentRow != null)
            {
                // Codigo para buscar las notas
                updateNotasAlumno();
            }
        }

        public void updateNotasAlumno()
        {
            codigoAlumno = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CODIGO"].Value);
            DataTable dt;
            for (int i = 0; i < 3; i++)
            {
                dt = calificaciones(i);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 0:
                            txtPunteoAct.Text = Convert.ToString(dt.Rows[0]["punteo_final"]);
                            txtFechaAct.Text = Convert.ToDateTime(dt.Rows[0]["fecha"]).ToShortDateString();                            
                            break;
                        case 1:
                            txtPunteoMej.Text = Convert.ToString(dt.Rows[0]["punteo_final"]);
                            txtFechaMej.Text = Convert.ToDateTime(dt.Rows[0]["fecha"]).ToShortDateString();
                            break;
                        case 2:
                            txtPunteoExt.Text = Convert.ToString(dt.Rows[0]["punteo_final"]);
                            txtFechaExt.Text = Convert.ToDateTime(dt.Rows[0]["fecha"]).ToShortDateString();                            
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            txtPunteoAct.Text = "";
                            txtFechaAct.Text = "";
                            break;
                        case 1:
                            txtPunteoMej.Text = "";
                            txtFechaMej.Text = "";
                            break;
                        case 2:
                            txtPunteoExt.Text = "";
                            txtFechaExt.Text = "";
                            break;
                    }
                }
            }

            // Valida boton Actividad
            if(txtFechaExt.Text != string.Empty)
            {
                btnCalificAct.Enabled = false;
                btnCalificAct.BackColor = Color.FromArgb(225, 225, 225);
                btnCalificAct.ForeColor = Color.White;
            }
            else
            {
                btnCalificAct.Enabled = true;
                if (txtFechaAct.Text != string.Empty)
                {
                    btnCalificAct.BackColor = Color.FromArgb(197, 75, 25);
                    btnCalificAct.ForeColor = Color.White;
                    btnCalificAct.Text = "Anular calificación";
                }
                else
                {
                    btnCalificAct.BackColor = Color.FromArgb(0, 120, 215);
                    btnCalificAct.ForeColor = Color.White;
                    btnCalificAct.Text = "Calificar";
                }
            }

            // Valida boton 2
            if(txtFechaAct.Text == string.Empty || txtFechaExt.Text != string.Empty)
            {
                btnCalificMej.Enabled = false;
                btnCalificMej.BackColor = Color.FromArgb(225, 225, 225);
                btnCalificMej.ForeColor = Color.White;
            }
            else
            {
                btnCalificMej.Enabled = true;
                if (txtFechaMej.Text != string.Empty)
                {
                    btnCalificMej.BackColor = Color.FromArgb(197, 75, 25);
                    btnCalificMej.ForeColor = Color.White;
                    btnCalificMej.Text = "Anular calificación";
                }
                else
                {
                    btnCalificMej.BackColor = Color.FromArgb(0, 120, 215);
                    btnCalificMej.ForeColor = Color.White;
                    btnCalificMej.Text = "Calificar";
                }
            }

            // Valida boton 3
            if(txtFechaAct.Text != string.Empty)
            {
                btnCalificExt.Enabled = false;
                btnCalificExt.BackColor = Color.FromArgb(225, 225, 225);
                btnCalificExt.ForeColor = Color.White;
            }
            else
            {
                btnCalificExt.Enabled = true;
                if (txtFechaExt.Text != string.Empty)
                {
                    btnCalificExt.BackColor = Color.FromArgb(197, 75, 25);
                    btnCalificExt.ForeColor = Color.White;
                    btnCalificExt.Text = "Anular calificación";
                }
                else
                {
                    btnCalificExt.BackColor = Color.FromArgb(0, 120, 215);
                    btnCalificExt.ForeColor = Color.White;
                    btnCalificExt.Text = "Calificar";
                }
            }
            
        }

        private DataTable calificaciones(int tipo)
        {
            conn.Open();
            string query = $"select * from Actividad_calificada where actividadid = {codigoActividad} and alumnoid = '{codigoAlumno}' and asignaturaid = {f1.codigoAsig} and tipo_evaluacion = {tipo};";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }

        private void btnCalificAct_Click(object sender, EventArgs e)
        {
            codigoAlumno = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CODIGO"].Value);
            if(txtFechaAct.Text == string.Empty)
            {
                FrmQualifyRubrica frm = new FrmQualifyRubrica(0);
                frm.Show();
            }
            else
            {
                borrarCalificacion(0);
            }
        }

        private void btnCalificMej_Click(object sender, EventArgs e)
        {
            codigoAlumno = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CODIGO"].Value);
            if (txtFechaMej.Text == string.Empty)
            {
                FrmQualifyRubrica frm = new FrmQualifyRubrica(1);
                frm.Show();
            }
            else
            {
                borrarCalificacion(1);
            }
        }

        private void btnCalificExt_Click(object sender, EventArgs e)
        {
            codigoAlumno = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CODIGO"].Value);
            if (txtFechaExt.Text == string.Empty)
            {
                FrmQualifyRubrica frm = new FrmQualifyRubrica(2);
                frm.Show();
            }
            else
            {
                borrarCalificacion(2);
            }
        }

        private void borrarCalificacion(int tipo_evaluacion)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show($"Deseas anular la calificacion?", "Confirmar", buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string query = $"DELETE FROM Actividad_calificada WHERE actividadid = {codigoActividad} and alumnoid = '{codigoAlumno}' and asignaturaid = {f1.codigoAsig} and tipo_evaluacion = {tipo_evaluacion}";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Calificacion anulada exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    updateNotasAlumno();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
