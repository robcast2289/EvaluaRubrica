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
    public partial class FrmUsers : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();

        public FrmUsers()
        {
            InitializeComponent();
            llenaTablaUsuarios();
        }
        
        public void llenaTablaUsuarios()
        {
            conn.Open();
            string query = "SELECT userid CODIGO, username NOMBRE, userdpi DPI from Users";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            dgvUsers.DataSource = dt;
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
            string query = $"SELECT userid CODIGO, username NOMBRE, userdpi DPI from Users where username like '%{txtBuscar.Text}%' or userdpi like '%{txtBuscar.Text}%'";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            dgvUsers.DataSource = dt;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaCampos();
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editarFila();
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                editarFila();
            }
        }

        private void editarFila()
        {
            limpiaCampos();
            if (dgvUsers.SelectedRows.Count > 0)
            {
                txtCodigo.Text = Convert.ToString(dgvUsers.CurrentRow.Cells["CODIGO"].Value);
                txtNombre.Text = Convert.ToString(dgvUsers.CurrentRow.Cells["NOMBRE"].Value);
                txtDPI.Text = Convert.ToString(dgvUsers.CurrentRow.Cells["DPI"].Value);
            }
            else
            {
                MessageBox.Show("No hay datos seleccionados para editar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int cu = 0;
                if (f1 != null)
                {
                    cu = f1.codigoUsr;
                }
            
                string uid = Convert.ToString(dgvUsers.CurrentRow.Cells["CODIGO"].Value);
                string uname = Convert.ToString(dgvUsers.CurrentRow.Cells["NOMBRE"].Value);

                if (cu.ToString() != uid)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show($"Deseas eliminar al usuario {uname}", "Confirmar", buttons, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            conn.Open();
                            string query = $"DELETE FROM Users WHERE userid = {uid}";
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("Usuario eliminado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiaCampos();
                            llenaTablaUsuarios();
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
                    MessageBox.Show("No es posible borrar el usuario actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay datos seleccionados para eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void limpiaCampos()
        {
            txtCodigo.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDPI.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtCodigo.Text == string.Empty)
            {
                if(txtNombre.Text == string.Empty && txtDPI.Text == string.Empty)
                {
                    MessageBox.Show("Debe llenar los datos obligatorios (*)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        conn.Open();
                        string query = String.Format("INSERT INTO Users(username, userdpi) values('{0}','{1}')", txtNombre.Text, txtDPI.Text);
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Usuario creado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiaCampos();
                        llenaTablaUsuarios();
                    }
                    catch(Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
            else
            {
                if (txtNombre.Text == string.Empty && txtDPI.Text == string.Empty)
                {
                    MessageBox.Show("Debe llenar los datos obligatorios (*)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        conn.Open();
                        string query = $"UPDATE Users SET username = '{txtNombre.Text}', userdpi = '{txtDPI.Text}' WHERE userid = {txtCodigo.Text}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Usuario actualizado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        llenaTablaUsuarios();
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
        
    }
}
