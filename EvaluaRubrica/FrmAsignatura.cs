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
    public partial class FrmAsignatura : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();

        public FrmAsignatura()
        {
            InitializeComponent();
            configValoresCampos();
            llenaTabla();
        }

        public void configValoresCampos()
        {
            nudCiclo.Maximum = DateTime.Now.Year + 1;
            nudCiclo.Minimum = DateTime.Now.Year - 5;
            nudCiclo.Value = DateTime.Now.Year;

            cbxExtado.Items.Clear();
            ComboboxItem item = new ComboboxItem();
            item.Text = "Activo";
            item.Value = 1;
            cbxExtado.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "Caducado";
            item.Value = 0;
            cbxExtado.Items.Add(item);
            cbxExtado.SelectedIndex = 0;
        }

        public void llenaTabla()
        {
            conn.Open();
            string query = "SELECT asignaturaid CODIGO, " +
                "descripcion DESCRIPCION, " +
                "ciclo CICLO, " +
                "nivel NIVEL, " +
                "grado GRADO, " +
                "seccion SECCION, " +
                "CASE estado " +
                "WHEN 0 THEN 'CADUCADO' " +
                "WHEN 1 THEN 'ACTIVO' " +
                "END ESTADO, " +
                "estado EV " +
                $"FROM asignaturas WHERE userid = {f1.codigoUsr}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            dgvAsignatura.DataSource = dt;
        }

        private void limpiaCampos()
        {
            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtNivel.Text = string.Empty;
            nudGrado.Value = 0;
            txtSeccion.Text = string.Empty;
            configValoresCampos();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                buscar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            conn.Open();
            string query = $"SELECT asignaturaid CODIGO, descripcion DESCRIPCION, ciclo CICLO, nivel NIVEL, grado GRADO, seccion SECCION, CASE estado WHEN 0 THEN 'CADUCADO' WHEN 1 THEN 'ACTIVO' END ESTADO, estado EV from Asignaturas where (descripcion like '%{txtBuscar.Text}%' or ciclo like '%{txtBuscar.Text}%' or nivel like '%{txtBuscar.Text}%') and userid = {f1.codigoUsr}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            dgvAsignatura.DataSource = dt;
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

        private void dgvAsignatura_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAsignatura.CurrentRow != null)
            {
                editarFila();
            }
        }

        private void editarFila()
        {
            limpiaCampos();
            if (dgvAsignatura.SelectedRows.Count > 0)
            {
                txtCodigo.Text = Convert.ToString(dgvAsignatura.CurrentRow.Cells["CODIGO"].Value);
                txtDescripcion.Text = Convert.ToString(dgvAsignatura.CurrentRow.Cells["DESCRIPCION"].Value);
                nudCiclo.Value = Convert.ToInt32(dgvAsignatura.CurrentRow.Cells["CICLO"].Value);
                txtNivel.Text = Convert.ToString(dgvAsignatura.CurrentRow.Cells["NIVEL"].Value);
                nudGrado.Value = Convert.ToInt32(dgvAsignatura.CurrentRow.Cells["GRADO"].Value);
                txtSeccion.Text = Convert.ToString(dgvAsignatura.CurrentRow.Cells["SECCION"].Value);
                cbxExtado.SelectedIndex = (Convert.ToString(dgvAsignatura.CurrentRow.Cells["ESTADO"].Value) == "ACTIVO" ? 0 : 1);
            }
            else
            {
                MessageBox.Show("No hay datos seleccionados para editar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAsignatura.SelectedRows.Count > 0)
            {
                string uid = Convert.ToString(dgvAsignatura.CurrentRow.Cells["CODIGO"].Value);
                string asignatura = Convert.ToString(dgvAsignatura.CurrentRow.Cells["DESCRIPCION"].Value);
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show($"Deseas eliminar la asignatura {asignatura}", "Confirmar", buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string query = $"DELETE FROM Asignaturas WHERE asignaturaid = {uid}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Asignatura eliminada exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           
            if (txtDescripcion.Text == string.Empty || txtNivel.Text == string.Empty || txtSeccion.Text == string.Empty)
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
                        string query = $"INSERT INTO asignaturas(userid, descripcion, ciclo, nivel, grado, seccion, estado) values({f1.codigoUsr},'{txtDescripcion.Text}',{nudCiclo.Value},'{txtNivel.Text}',{nudGrado.Value},'{txtSeccion.Text}',{(cbxExtado.SelectedItem as ComboboxItem).Value})";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        crearActividades();
                        MessageBox.Show("Asignatura creada exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        string query = $"UPDATE asignaturas SET descripcion = '{txtDescripcion.Text}', ciclo = {nudCiclo.Value}, nivel = '{txtNivel.Text}', grado = {nudGrado.Value}, seccion = '{txtSeccion.Text}', estado = {(cbxExtado.SelectedItem as ComboboxItem).Value} WHERE asignaturaid = {txtCodigo.Text}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Asignatura actualizada exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiaCampos();
                        llenaTabla();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                    
                }
                // Pendiiente si se activa esta funcion
                // f1.llenaCombo();
            }                        
        }

        private void crearActividades()
        {
            decimal punteoPorActividad = 0;
            punteoPorActividad = 100m / (f1.Parms.actividades_por_bloque > 0 ? f1.Parms.actividades_por_bloque : 1);

            int asignaturaid = ultimoRegistroId();

            for (int b = 1; b <= 4; b++)
            {
                for (int i = 1; i <= f1.Parms.actividades_por_bloque; i++)
                {
                    conn.Open();
                    string query = $"INSERT INTO Actividades (bloque, asignaturaid, descripcion, se_evaluara, tema , punteo, tabla_configurada) VALUES({b},{asignaturaid},'Actividad {i}','','',{punteoPorActividad},0)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }                        
        }

        private int ultimoRegistroId()
        {
            conn.Open();
            string query = $"select max(asignaturaid) asignaturaid from Asignaturas where userid = {f1.codigoUsr}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            return Convert.ToInt32(dt.Rows[0]["asignaturaid"]);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }
        
    }

    
}
