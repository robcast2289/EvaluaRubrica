using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class FrmQualifyRubrica : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();
        FrmActividades f2 = Application.OpenForms.OfType<FrmActividades>().SingleOrDefault();
        string[] nameMonth = new string[12] { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agostro", "septiembre", "octubre", "noviembre", "diciembre" };
        int filas = 0, columnas = 0;
        Control[,] ctrls;
        decimal[,] valores;
        bool[,] calificado;
        decimal punteo_max;
        int btnWidth = 120, btnHeight = 60;
        int eVertical = 250;
        int eHorizontal = 0;
        int eEntreControles = 2;
        public bool formValid = true;

        decimal punteoAcumulado = 0, punteoFinal = 0;
        string rutaCarpetaAlumno = "";

        int asignaturaid;
        string alumnoid;
        int actividadid;
        int bloque;
        public int tipo_evaluacion;
        public int prct_actividades;


        public FrmQualifyRubrica(int tipo_eval)
        {
            InitializeComponent();
            tipo_evaluacion = tipo_eval;
            initVariables();            
            dataInicial();
        }

        private void initVariables()
        {
            asignaturaid = f1.codigoAsig;
            alumnoid = f2.codigoAlumno;
            actividadid = f2.codigoActividad;
            bloque = f2.bloque;
        }

        public void dataInicial()
        {
            conn.Open();
            string query = $@"select 
acti.descripcion ACTIVIDAD
,asig.descripcion ASIGNATURA
,asig.grado || ' ' || asig.nivel || ' ' || asig.seccion GRADO
,user.username CATEDRATICO
,asig.ciclo CICLO
,acti.se_evaluara EVALUAR
,acti.tema TEMA
,acti.punteo PUNTEO_MAXIMO
,alm.nombres NOMBRE_ALUMNO
,alm.apellidos APELLIDOS_ALUMNO
,acti.fecha_entrega FECHA_ENTREGA
,acti.fecha_entrega_mejoramiento FECHA_ENTREGA_MEJORAMIENTO
,(select max(columna) columnas from Actividades_detalle ad where ad.actividadid = acti.actividadid) COLUMNAS
,(select max(fila) filas from Actividades_detalle ad where ad.actividadid = acti.actividadid) FILAS
from Actividades acti, Asignaturas asig, Users user, Alumnos alm
where acti.asignaturaid = asig.asignaturaid 
and asig.userid = user.userid
and alm.asignaturaid = asig.asignaturaid
and alm.alumnoid = '{alumnoid}'
and acti.actividadid = {actividadid}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (validaDatos(dt))
            {

                columnas = Convert.ToInt32(dt.Rows[0]["COLUMNAS"]);
                filas = Convert.ToInt32(dt.Rows[0]["FILAS"]);
                ctrls = new Control[columnas + 1, filas + 1];
                valores = new decimal[columnas + 1, filas + 1];
                calificado = new bool[columnas + 1, filas + 1];

                lblGrado.Text = Convert.ToString(dt.Rows[0]["GRADO"]);
                lblAsignatura.Text = Convert.ToString(dt.Rows[0]["ASIGNATURA"]);
                lblCatedraticoTxt.Text = Convert.ToString(dt.Rows[0]["CATEDRATICO"]);
                lblCicloTxt.Text = Convert.ToString(dt.Rows[0]["CICLO"]);
                //lblFecha.Text = $"Guatemala, {DateTime.Now.Day} de {nameMonth[DateTime.Now.Month-1]} de {DateTime.Now.Year}";
                lblFecha.Text = "Fecha";

                lblEvaluar.Text = $"RUBRICA PARA EVALUAR {Convert.ToString(dt.Rows[0]["EVALUAR"])}";
                lblTema.Text = $"TEMA {Convert.ToString(dt.Rows[0]["TEMA"])}";
                lblPunteoMax.Text = $"VALOR: {Convert.ToString(dt.Rows[0]["PUNTEO_MAXIMO"])}pts.";
                if (tipo_evaluacion == 0)
                    lblFechaEntrega.Text = $"Fecha de entrega: {Convert.ToDateTime(dt.Rows[0]["FECHA_ENTREGA"]).ToShortDateString()}";
                if (tipo_evaluacion == 1)
                    lblFechaEntrega.Text = $"Fecha de entrega: {Convert.ToDateTime(dt.Rows[0]["FECHA_ENTREGA_MEJORAMIENTO"]).ToShortDateString()}";
                lblEvaluar.Location = new Point((this.Width - lblEvaluar.Width) / 2, lblEvaluar.Location.Y);
                lblTema.Location = new Point((this.Width - lblTema.Width) / 2, lblTema.Location.Y);
                lblPunteoMax.Location = new Point((this.Width - lblPunteoMax.Width) / 2, lblPunteoMax.Location.Y);
                lblFechaEntrega.Location = new Point((this.Width - lblFechaEntrega.Width) / 2, lblFechaEntrega.Location.Y);
                punteo_max = Convert.ToDecimal(dt.Rows[0]["PUNTEO_MAXIMO"]);

                lblAlumnoNombre.Text = $"{Convert.ToString(dt.Rows[0]["APELLIDOS_ALUMNO"])}, {Convert.ToString(dt.Rows[0]["NOMBRE_ALUMNO"])}";
                lblAlumnoNombre.Location = new Point((this.Width - lblAlumnoNombre.Width) / 2, lblAlumnoNombre.Location.Y);

                if (tipo_evaluacion == 1 || tipo_evaluacion == 2)
                {
                    lblPunteoFinal.Visible = true;
                    txtPunteoFinal.Visible = true;
                    if (tipo_evaluacion == 1)
                    {
                        prct_actividades = f1.Parms.prct_mejoramiento;
                        lblPunteoFinal.Text = $"Mejoramiento\n{prct_actividades}%";
                    }
                    if (tipo_evaluacion == 2)
                    {
                        prct_actividades = f1.Parms.prct_extemporaneo;
                        lblPunteoFinal.Text = $"Extemporaneo\n{prct_actividades}%";
                    }
                }

                llenaTabla();
            }
        }

        bool validaDatos(DataTable dt)
        {
            bool ret = true;
            string mensaje = "Debe llenar los siguientes datos de la actividad:\n ";
            if(Convert.ToString(dt.Rows[0]["EVALUAR"]) == string.Empty)
            {
                mensaje += "Evaluar\n ";
                ret = false;
            }
            if (Convert.ToString(dt.Rows[0]["TEMA"]) == string.Empty)
            {
                mensaje += "Tema\n ";
                ret = false;
            }
            if (dt.Rows[0]["FECHA_ENTREGA"] == DBNull.Value)
            {
                mensaje += "Fecha entrega\n ";
                ret = false;
            }
            if (dt.Rows[0]["FECHA_ENTREGA_MEJORAMIENTO"] == DBNull.Value)
            {
                mensaje += "Fecha entrega mejoramiento\n ";
                ret = false;
            }
            if(ret == false)
            {
                formValid = false;
                MessageBox.Show(mensaje, "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ret;
        }

        private void llenaTabla()
        {
            conn.Open();
            string query = $"SELECT actividadid CODIGO, columna COLUMNA, fila FILA, texto TEXTO, valor VALOR FROM Actividades_detalle WHERE actividadid = {actividadid}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            generaControles();
            actualizaDisenio();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int x = Convert.ToInt32(dt.Rows[i]["COLUMNA"]);
                int y = Convert.ToInt32(dt.Rows[i]["FILA"]);

                ctrls[x, y].Text = Convert.ToString(dt.Rows[i]["TEXTO"]);

                if (x > 0 && y > 0)
                {
                    ctrls[x, y].Text = ctrls[x, y].Text + $"\n{Convert.ToDecimal(dt.Rows[i]["VALOR"])}";
                    valores[x, y] = Convert.ToDecimal(dt.Rows[i]["VALOR"]);
                }
            }

        }

        private void generaControles()
        {
            for (int y = 0; y <= filas; y++)
            {
                for (int x = 0; x <= columnas; x++)
                {
                    if (x != 0 && y != 0)
                    {
                        ctrls[x, y] = createButton(x, y);
                    }
                    else
                    {
                        ctrls[x, y] = createLabel(x, y);
                    }
                }
            }
        }

        private void actualizaDisenio()
        {
            for (int y = 0; y <= filas; y++)
            {
                for (int x = 0; x <= columnas; x++)
                {
                    if (x != 0 && y != 0)
                    {
                        ((Button)ctrls[x, y]).Size = new System.Drawing.Size(btnWidth, btnHeight);
                        ((Button)ctrls[x, y]).Location = locationControls(x, y, "btn");
                    }
                    else
                    {
                        ((Label)ctrls[x, y]).Size = new System.Drawing.Size(btnWidth, btnHeight);
                        ((Label)ctrls[x, y]).Location = locationControls(x, y, "lbl");
                    }
                }
            }
            this.AutoScroll = true;
            this.SetAutoScrollMargin(5, 5);
        }

        private Button createButton(int x, int y)
        {
            Button btnDesc = new Button();
            btnDesc.Size = new System.Drawing.Size(btnWidth, btnHeight);
            btnDesc.Text = "";
            btnDesc.Font = new System.Drawing.Font(btnDesc.Font.FontFamily, Convert.ToInt16(6));
            btnDesc.Anchor = AnchorStyles.Top;
            btnDesc.Name = $"Boton{x}_{y}";
            btnDesc.FlatStyle = FlatStyle.Flat;
            btnDesc.BackColor = Color.FromArgb(202, 202, 202);
            btnDesc.UseCompatibleTextRendering = true;
            btnDesc.Click += new System.EventHandler(this.btnMatriz_Click);
            addControlsToWindow(btnDesc);

            return btnDesc;
        }

        private Label createLabel(int x, int y)
        {
            Label lblPts = new Label();
            lblPts.Font = new System.Drawing.Font(lblPts.Font.FontFamily, Convert.ToInt16(7),FontStyle.Bold);
            lblPts.Anchor = AnchorStyles.Top;
            lblPts.Text = "";
            lblPts.Name = $"Label{x}_{y}";
            lblPts.AutoSize = false;
            lblPts.TextAlign = ContentAlignment.MiddleCenter;
            lblPts.BorderStyle = BorderStyle.FixedSingle;
            lblPts.BackColor = Color.White;
            addControlsToWindow(lblPts);

            return lblPts;
        }

        private void addControlsToWindow(Control _control)
        {
            Controls.Add(_control);
        }        

        private Point locationControls(int _x, int _y, string _ctrl)
        {
            int x = (((this.Width - ((columnas + 1) * (btnWidth + eEntreControles) - eEntreControles)) / 2) + ((btnWidth + eEntreControles) * _x));
            int y = (eVertical + ((btnHeight + eEntreControles) * _y));

            return new System.Drawing.Point(x, y);
        }        

        private void btnMatriz_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            string[] cordenadas = new string[2];
            int x, y;
            cordenadas = name.Replace("Boton", "").Split('_');
            x = Convert.ToInt32(cordenadas[0]);
            y = Convert.ToInt32(cordenadas[1]);
            if(calificado[x,y] == false)
            {
                calificado[x, y] = true;
                punteoAcumulado += valores[x, y];
                ((Button)sender).BackColor = Color.FromArgb(254, 187, 103);
            }
            else
            {
                calificado[x, y] = false; ;
                punteoAcumulado -= valores[x, y];
                ((Button)sender).BackColor = Color.FromArgb(202, 202, 202);
            }
            punteoFinal = punteoAcumulado * (tipo_evaluacion == 0 ? 1m : (prct_actividades / 100m)); // cambiar 50 por variable
            if(tipo_evaluacion != 0 && f1.Parms.redondear_arriba)
            {
                punteoFinal = Math.Ceiling(punteoFinal);
            }            
            txtPunteo.Text = Convert.ToString(punteoAcumulado);
            txtPunteoFinal.Text = Convert.ToString(punteoFinal);

            lblMensaje.Visible = (punteoFinal > punteo_max);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool error = false;
            error = validaExisteRubrica();

            if (!error) {
                error = validaExisteRutaAlumno();
            }

            if (!error)
            {
                try
                {
                    conn.Open();
                    string query = $"INSERT INTO Actividad_calificada (actividadid, alumnoid, asignaturaid, tipo_evaluacion, fecha, punteo, punteo_final, observaciones) VALUES({actividadid},'{alumnoid}',{asignaturaid},{tipo_evaluacion},'{dtpFecha.Text}',{punteoAcumulado},{punteoFinal},'{txtObservaciones.Text}')";
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

            if (!error)
            {
                for (int y = 0; y <= filas; y++)
                {
                    for (int x = 0; x <= columnas; x++)
                    {
                        if(calificado[x, y] == true) { 
                            try
                            {
                                conn.Open();
                                string query = $"INSERT INTO Actividad_calificada_detalle (actividadid, alumnoid, asignaturaid, tipo_evaluacion, columna, fila, marcado) VALUES({actividadid},'{alumnoid}',{asignaturaid},{tipo_evaluacion},{x},{y},{1})";
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
                }
            }

            if (!error)
            {
                try
                {
                    GeneraDocumentos gd = new GeneraDocumentos();
                    gd.createRubricaPdf(actividadid, alumnoid, asignaturaid, tipo_evaluacion, rutaCarpetaAlumno);
                    MessageBox.Show("Se guardó la calificacion y el documento correctamente!!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f2.updateNotasAlumno();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Se guardo la calificación pero el documento tuvo provlemas para guardarse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //MessageBox.Show("Se ha guradado correctamente la rubrica", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hubo errores al guardar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validaExisteRubrica()
        {
            bool error = false;

            conn.Open();
            string query = $@"select * from Actividad_calificada where actividadid = {actividadid} and alumnoid = '{alumnoid}' and asignaturaid = {asignaturaid} and tipo_evaluacion = {tipo_evaluacion}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("La actividad ya se encuntra calificada!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error = true;
            }
            return error;
        }

        private bool validaExisteRutaAlumno()
        {
            bool error = false;

            conn.Open();
            string query = $@"select 
a.descripcion || ' ' || a.grado || ' ' || a.nivel || ' ' || a.seccion ASIGNATURA
,a.ciclo CICLO
,p.ruta_carpetas RUTA 
,u.apellidos || ', ' || u.nombres ALUMNO
from Asignaturas a, Parametros p, Alumnos u
where a.asignaturaid = {asignaturaid}
and a.asignaturaid = u.asignaturaid
and u.alumnoid = '{alumnoid}'";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 1)
            {    // Valida ruta principal
                string PrincipalFolderPath = @dt.Rows[0]["RUTA"].ToString();
                string ciclo = dt.Rows[0]["CICLO"].ToString();
                string asignatura = dt.Rows[0]["ASIGNATURA"].ToString();
                string alumno = dt.Rows[0]["ALUMNO"].ToString();
                string Bloque = "";
                switch (bloque)
                {
                    case 1: Bloque = "Unidad I"; break;
                    case 2: Bloque = "Unidad II"; break;
                    case 3: Bloque = "Unidad III"; break;
                    case 4: Bloque = "Unidad IV"; break;
                }
                string rutaAlumnoFinal = $@"{PrincipalFolderPath}\{ciclo}\{asignatura}\{alumno}\{Bloque}";
                if (PrincipalFolderPath != string.Empty && !Directory.Exists(rutaAlumnoFinal))
                {
                    MessageBox.Show("No existe la ruta de la carpeta del alumno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error = true;
                }
                rutaCarpetaAlumno = rutaAlumnoFinal;
            }
            return error;
        }

    }
}
