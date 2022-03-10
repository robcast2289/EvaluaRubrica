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
    public partial class FrmParametros : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();

        public FrmParametros()
        {
            InitializeComponent();
            validaTablaParametros();
        }

        private void validaTablaParametros()
        {
            conn.Open();
            string query = $"SELECT ruta_carpetas, actividades_por_bloque, prct_mejoramiento, prct_extemporaneo, redondear_arriba, actividad_mas_mejoramiento FROM Parametros";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if(dt.Rows.Count == 0)
            {
                try
                {
                    conn.Open();
                    query = $"INSERT INTO Parametros (ruta_carpetas, actividades_por_bloque, prct_mejoramiento, prct_extemporaneo, redondear_arriba, actividad_mas_mejoramiento) VALUES('',4,0,0,0,0)";
                    cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    nudActividades.Value = 4;

                    f1.obtenerParametros();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtRutaCarpetas.Text = dt.Rows[0]["ruta_carpetas"].ToString();
                nudActividades.Value = Convert.ToInt32(dt.Rows[0]["actividades_por_bloque"]);
                nudPctMejoramiento.Value = Convert.ToInt32(dt.Rows[0]["prct_mejoramiento"]);
                nudPctExtemporaneo.Value = Convert.ToInt32(dt.Rows[0]["prct_extemporaneo"]);
                chbRoundUp.Checked = (Convert.ToInt32(dt.Rows[0]["redondear_arriba"]) == 1 ? true : false);
                chbActConMej.Checked = (Convert.ToInt32(dt.Rows[0]["actividad_mas_mejoramiento"]) == 1 ? true : false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtRutaCarpetas.Text = fbd.SelectedPath;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = $@"UPDATE Parametros SET 
ruta_carpetas = '{txtRutaCarpetas.Text}', 
actividades_por_bloque = {nudActividades.Value},
prct_mejoramiento = {nudPctMejoramiento.Value},
prct_extemporaneo = {nudPctExtemporaneo.Value},
redondear_arriba = {(chbRoundUp.Checked ? 1 : 0)}, 
actividad_mas_mejoramiento = {(chbActConMej.Checked ? 1 : 0)}";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                f1.obtenerParametros();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
