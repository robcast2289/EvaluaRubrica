using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvaluaRubrica
{
    public partial class FrmAlumnos : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();
        bool updateRecord = false;

        public FrmAlumnos()
        {
            InitializeComponent();
            configValoresCampos();
            llenaTabla();
            panelImportFile.Location = panelDataUsers.Location;
            panelImportFile.Size = panelDataUsers.Size;
        }

        public void configValoresCampos()
        {
            cbxSexo.Items.Clear();
            ComboboxItem item = new ComboboxItem();
            item.Text = "Femenino";
            item.Value = "F";
            cbxSexo.Items.Add(item);
            item = new ComboboxItem();
            item.Text = "Masculino";
            item.Value = "M";
            cbxSexo.Items.Add(item);
            cbxSexo.SelectedIndex = 0;
        }

        public void llenaTabla()
        {
            conn.Open();
            string query = $"SELECT alumnoid CODIGO, nombres NOMBRES, apellidos APELLIDOS, sexo SEXO, telefono TELEFONO, email 'CORREO ELECTRONICO' FROM Alumnos WHERE asignaturaid = {f1.codigoAsig}";
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
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
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
            string query = $"SELECT alumnoid CODIGO, nombres NOMBRES, apellidos APELLIDOS, sexo SEXO, telefono TELEFONO, email 'CORREO ELECTRONICO' FROM Alumnos WHERE (alumnoid like '%{txtBuscar.Text}%' or nombres like '%{txtBuscar.Text}%' or apellidos like '%{txtBuscar.Text}%' or telefono like '%{txtBuscar.Text}%' or email like '%{txtBuscar.Text}%') and asignaturaid = {f1.codigoAsig}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            dgvAlumnos.DataSource = dt;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaCampos();
            updateRecord = false;
            txtCodigo.Enabled = true;
            txtCodigo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editarFila();
        }

        private void dgvAlumnos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editarFila();
        }

        private void editarFila()
        {
            limpiaCampos();
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                updateRecord = true;
                txtCodigo.Enabled = false;
                txtCodigo.Text = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CODIGO"].Value);
                txtNombres.Text = Convert.ToString(dgvAlumnos.CurrentRow.Cells["NOMBRES"].Value);
                txtApellidos.Text = Convert.ToString(dgvAlumnos.CurrentRow.Cells["APELLIDOS"].Value);
                cbxSexo.SelectedIndex = (Convert.ToString(dgvAlumnos.CurrentRow.Cells["SEXO"].Value) == "F" ? 0 : 1);
                txtTelefono.Text = Convert.ToString(dgvAlumnos.CurrentRow.Cells["TELEFONO"].Value);
                txtEmail.Text = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CORREO ELECTRONICO"].Value);
            }
            else
            {
                MessageBox.Show("No hay datos seleccionados para editar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                string uid = Convert.ToString(dgvAlumnos.CurrentRow.Cells["CODIGO"].Value);
                string alumno = Convert.ToString(dgvAlumnos.CurrentRow.Cells["NOMBRES"].Value) + " " + Convert.ToString(dgvAlumnos.CurrentRow.Cells["APELLIDOS"].Value);
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show($"Deseas eliminar al alumno {alumno.ToUpper()}", "Confirmar", buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string query = $"DELETE FROM Alumnos WHERE alumnoid = '{uid}' and asignaturaid = {f1.codigoAsig}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Alumno eliminado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (txtCodigo.Text == string.Empty || txtNombres.Text == string.Empty || txtApellidos.Text == string.Empty)
            {
                MessageBox.Show("Debe llenar los datos obligatorios (*)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (updateRecord == false)
                {
                    try
                    {
                        conn.Open();
                        string query = $"INSERT INTO Alumnos (alumnoid, nombres, apellidos, sexo, telefono, email , asignaturaid) VALUES('{txtCodigo.Text}','{txtNombres.Text}','{txtApellidos.Text}','{(cbxSexo.SelectedItem as ComboboxItem).Value}',{txtTelefono.Text},'{txtEmail.Text}',{f1.codigoAsig})";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Alumno creado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiaCampos();
                        llenaTabla();
                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if(updateRecord == true)
                {
                    try
                    {
                        conn.Open();
                        string query = $"UPDATE Alumnos SET nombres = '{txtNombres.Text}', apellidos = '{txtApellidos.Text}', sexo = '{(cbxSexo.SelectedItem as ComboboxItem).Value}', telefono = {txtTelefono.Text}, email = '{txtEmail.Text}' WHERE alumnoid = '{txtCodigo.Text}' and asignaturaid = {f1.codigoAsig}";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        
                        MessageBox.Show("Alumno actualizado exitosamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        updateRecord = false;
                        txtCodigo.Enabled = true;
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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiaCampos();
            updateRecord = false;
            txtCodigo.Enabled = true;
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Cargar archivo"; 
            ofd.Filter = "Archivos CSV |*.csv| Todos los archivos |*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                txtPathFile.Text = ofd.FileName;                
                btnAnalizar.Enabled = (txtPathFile.Text != string.Empty);
                btnCargarArchivo.Enabled = false;
            }
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            panelDataUsers.Visible = false;

           
            //panelImportFile.Size = new System.Drawing.Size(236, 584);
            //panelImportFile.Location = new Point(552, 48);
            //panelImportFile.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top);
            panelImportFile.Visible = true;
            btnAnalizar.Enabled = false;
            btnCargarArchivo.Enabled = false;

        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            int count = 0;
            string errores = "";
            try
            {
                using (StreamReader leer = new StreamReader(txtPathFile.Text, System.Text.Encoding.Default, false))
                {
                    while (!leer.EndOfStream)
                    {
                        count++;
                        if(count > 1) { 
                            string x = leer.ReadLine();
                            string[] registro = x.Split(';');

                            if (registro.Length != 6)
                            {
                                errores += $"la fila {count} tiene numero de columnas incorrecto \n";
                            }
                            else
                            {
                                if (registro[3] != "F" && registro[3] != "M")
                                {
                                    errores += $"la fila {count}, columna 4 solo puede contener F ó M \n";
                                }
                                if(registro[0].Replace(" ","") == "")
                                {
                                    errores += $"la fila {count}, columna 4; tiene que tener codigo \n";
                                }
                                if (registro[1].Replace(" ", "") == "")
                                {
                                    errores += $"la fila {count}, columna 4; tiene que tener nombres \n";
                                }
                                if (registro[2].Replace(" ", "") == "")
                                {
                                    errores += $"la fila {count}, columna 4 tiene que tener apellidos \n";
                                }
                            }
                        }
                    }
                }
                if (errores != string.Empty)
                {
                    errores = $"Se analizaron {count} registros, se encontraron los sigueintes errores \n" + errores;
                    lblResult.Text = errores;
                    btnCargarArchivo.Enabled = false;
                }
                else
                {
                    lblResult.Text = $"Se analizaron {count} registros correctamente!";
                    btnCargarArchivo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                        
        }

        private void btnImportDataCancel_Click(object sender, EventArgs e)
        {
            txtPathFile.Text = string.Empty;
            btnAnalizar.Enabled = false;
            btnCargarArchivo.Enabled = false;

            panelImportFile.Visible = false;
            panelDataUsers.Visible = true;            
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                using (StreamReader leer = new StreamReader(txtPathFile.Text, System.Text.Encoding.Default, false))
                {
                    while (!leer.EndOfStream)
                    {
                        count++;
                        if(count > 1) { 
                            string x = leer.ReadLine();
                            string[] registro = x.Split(';');

                            conn.Open();
                            string query = $"INSERT INTO Alumnos (alumnoid, nombres, apellidos, sexo, telefono, email, asignaturaid) VALUES('{registro[0]}','{registro[1]}','{registro[2]}','{registro[3]}','{registro[4]}','{registro[5]}',{f1.codigoAsig})";
                            SQLiteCommand cmd = new SQLiteCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show($"Se han creado {count} registros en la tabla de Alumnos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPathFile.Text = string.Empty;
                btnAnalizar.Enabled = false;
                btnCargarArchivo.Enabled = false;

                panelImportFile.Visible = false;
                panelDataUsers.Visible = true;
                llenaTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panelDataUsers.Visible = false;
            panelImportFile.Visible = true;
            btnAnalizar.Enabled = false;
            btnCargarArchivo.Enabled = false;
        }

        private void btnCreaEstructura_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Deseas crear la estructura de carpetas para el curso actual?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                conn.Open();
                string query = $"select a.descripcion || ' ' || a.grado || ' ' || a.nivel || ' ' || a.seccion ASIGNATURA, a.ciclo CICLO,p.ruta_carpetas RUTA from Asignaturas a, Parametros p where a.asignaturaid = {f1.codigoAsig}";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                DataTable dt = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Close();

                if (dt.Rows.Count == 1)
                {    // Valida ruta principal
                    //string PrincipalFolderPath = @dt.Rows[0]["RUTA"].ToString();
                    string PrincipalFolderPath = f1.Parms.ruta_carpetas;
                    string ciclo = dt.Rows[0]["CICLO"].ToString();
                    string asignatura = dt.Rows[0]["ASIGNATURA"].ToString();
                    if (PrincipalFolderPath != string.Empty && !Directory.Exists(PrincipalFolderPath))
                    {
                        MessageBox.Show("No existe la ruta de la carpeta principal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Crea ruta de Ciclo
                        string rutaCiclo = $@"{PrincipalFolderPath}\{ciclo}";
                        if (!Directory.Exists(rutaCiclo))
                        {
                            Directory.CreateDirectory(rutaCiclo);
                        }
                        // Crea ruta de asignatura
                        string rutaAsignatura = $@"{PrincipalFolderPath}\{ciclo}\{asignatura}";
                        if (!Directory.Exists(rutaAsignatura))
                        {
                            Directory.CreateDirectory(rutaAsignatura);
                        }

                        conn.Open();
                        query = $"SELECT nombres NOMBRES, apellidos APELLIDOS FROM Alumnos WHERE asignaturaid = {f1.codigoAsig}";
                        cmd = new SQLiteCommand(query, conn);
                        DataTable dtAlm = new DataTable();
                        SQLiteDataAdapter adapterAlm = new SQLiteDataAdapter(cmd);
                        adapterAlm.Fill(dtAlm);
                        conn.Close();
                        // Crea cada carpeta del alumno
                        foreach (DataRow row in dtAlm.Rows)
                        {
                            string alumno = row["APELLIDOS"].ToString() + ", " + row["NOMBRES"].ToString();

                            string rutaAlumno = $@"{PrincipalFolderPath}\{ciclo}\{asignatura}\{alumno}";
                            if (!Directory.Exists(rutaAlumno))
                            {
                                Directory.CreateDirectory(rutaAlumno);
                            }

                            // Bloques
                            if (!Directory.Exists(rutaAlumno + @"\Unidad I"))
                            {
                                Directory.CreateDirectory(rutaAlumno + @"\Unidad I");
                            }
                            if (!Directory.Exists(rutaAlumno + @"\Unidad II"))
                            {
                                Directory.CreateDirectory(rutaAlumno + @"\Unidad II");
                            }
                            if (!Directory.Exists(rutaAlumno + @"\Unidad III"))
                            {
                                Directory.CreateDirectory(rutaAlumno + @"\Unidad III");
                            }
                            if (!Directory.Exists(rutaAlumno + @"\Unidad IV"))
                            {
                                Directory.CreateDirectory(rutaAlumno + @"\Unidad IV");
                            }
                        }
                        MessageBox.Show("Estructura de archivos creada!!");
                    }
                }
            }
        }
    }
}
