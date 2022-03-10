using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace EvaluaRubrica
{
    public partial class Form1 : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        private Form activeForm = null;
        public int codigoUsr = 0;
        public int codigoAsig = 0;
        public Parametros Parms = new Parametros();

        public Form1()
        {
            InitializeComponent();
            IniciaApp();
            customizeDesign();

        }
        private void customizeDesign()
        {
            panelConfigSubmenu.Visible = false;
            panelData.Visible = false;
        }

        public void IniciaApp()
        {
            panelSideMenu.Visible = false;
            panelFilter.Visible = false;
            panelChildFormContent.Visible = false;

            Panel fondo = new Panel();
            fondo.Name = "panelFondo";
            fondo.Dock = DockStyle.Fill;
            fondo.TabIndex = 90;
            fondo.Location = new System.Drawing.Point(0, 0);
            fondo.Size = new System.Drawing.Size(250, 100);
            fondo.BackColor = Color.MintCream;
            fondo.BringToFront();
            Controls.Add(fondo);

            conn.Open();
            string query = "SELECT * from Users";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count == 0)
            {
                fondo.Controls.Add(createFirstUser());
            }
            else
            {
                listUsers(dt);
            }
        }

        public GroupBox createFirstUser()
        {
            GroupBox grbx = new GroupBox();
            grbx.Size = new System.Drawing.Size(300, 191);
            grbx.Text = "Crea primer usuario";
            grbx.Location = new System.Drawing.Point((this.Size.Width - grbx.Size.Width) / 2, (this.Size.Height - grbx.Size.Height) / 2);
            grbx.BackColor = Color.LightSteelBlue;
            grbx.Anchor = AnchorStyles.None;

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre completo";
            lblNombre.Size = new System.Drawing.Size(119, 17);
            lblNombre.Location = new System.Drawing.Point(16, 23);

            Label lblDPI = new Label();
            lblDPI.Text = "DPI";
            lblDPI.Size = new System.Drawing.Size(30, 17);
            lblDPI.Location = new System.Drawing.Point(16, 86);

            TextBox txtNombre = new TextBox();
            txtNombre.Size = new System.Drawing.Size(262, 23);
            txtNombre.Location = new System.Drawing.Point(19, 43);
            txtNombre.MaxLength = 120;

            TextBox txtDPI = new TextBox();
            txtDPI.Size = new System.Drawing.Size(262, 23);
            txtDPI.Location = new System.Drawing.Point(19, 106);
            txtDPI.MaxLength = 13;

            Button btnAceptar = new Button();
            btnAceptar.Text = "Aceptar";
            btnAceptar.Size = new System.Drawing.Size(75, 41);
            btnAceptar.Location = new System.Drawing.Point(206, 135);
            btnAceptar.BackColor = Color.MediumSeaGreen;
            btnAceptar.Click += new System.EventHandler(this.btnAceptarNewUser);

            grbx.Controls.Add(lblNombre);
            grbx.Controls.Add(lblDPI);
            grbx.Controls.Add(txtNombre);
            grbx.Controls.Add(txtDPI);
            grbx.Controls.Add(btnAceptar);

            return grbx;
        }

        public void listUsers(DataTable _dt)
        {
            int anchoTotal = (_dt.Rows.Count * 140) + ((_dt.Rows.Count - 1) * 40);
            anchoTotal = (anchoTotal > 1040 ? 1040 : anchoTotal);
            for (int x = 1; x <= _dt.Rows.Count && x <= 6; x++)
            {
                Panel panelUser = new Panel();
                panelUser.Size = new System.Drawing.Size(140, 150);
                if (anchoTotal <= 500)
                {
                    panelUser.Location = new System.Drawing.Point(((this.Size.Width - anchoTotal) / 2) + (180 * (x - 1)), (this.Size.Height - panelUser.Size.Height) / 2);
                }
                else
                {
                    if (x <= 3)
                    {
                        panelUser.Location = new System.Drawing.Point(((this.Size.Width - 500) / 2) + (180 * (x - 1)), (this.Size.Height - (panelUser.Size.Height * 2 + 40)) / 2);
                    }
                    else if (x > 3 && x <= 6)
                    {
                        panelUser.Location = new System.Drawing.Point(((this.Size.Width - (anchoTotal - 540)) / 2) + (180 * ((x - 3) - 1)), ((this.Size.Height - panelUser.Size.Height + 190) / 2));
                    }
                }
                panelUser.Name = "PanelUser" + _dt.Rows[x - 1]["userid"].ToString().ToUpper();
                //panelUser.BackColor = Color.Red;
                panelUser.Anchor = AnchorStyles.None;

                PictureBox pbxUser = new PictureBox();
                pbxUser.Size = new System.Drawing.Size(134, 103);
                pbxUser.Location = new System.Drawing.Point(3, 3);
                pbxUser.Name = "pbxUser" + _dt.Rows[x - 1]["userid"].ToString().ToUpper();
                pbxUser.Image = EvaluaRubrica.Properties.Resources.user_icon;
                pbxUser.SizeMode = PictureBoxSizeMode.Zoom;
                pbxUser.Click += new System.EventHandler(this.btnSelectUser);

                Label lblUser = new Label();
                lblUser.Size = new System.Drawing.Size(134, 41);
                lblUser.Location = new System.Drawing.Point(3, 109);
                lblUser.Name = "lblUser" + _dt.Rows[x - 1]["userid"].ToString().ToUpper();
                lblUser.AutoSize = false;
                lblUser.TextAlign = ContentAlignment.MiddleCenter;
                lblUser.Text = _dt.Rows[x - 1]["username"].ToString().ToUpper();
                lblUser.Font = new Font("Microsoft Sans Serif", 9);
                lblUser.Click += new System.EventHandler(this.btnSelectUser);

                panelUser.Controls.Add(pbxUser);
                panelUser.Controls.Add(lblUser);

                Controls[3].Controls.Add(panelUser);

            }


        }

        public void iniciaDatos()
        {
            conn.Open();
            string query = $"SELECT * from Users where userid = {codigoUsr}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                // Llena el nombre del Usuario
                lblUserName.Text = dt.Rows[0]["username"].ToString();
                lblUserName.Location = new Point((panelFilter.Width - lblUserName.Width) / 2, lblUserName.Location.Y);

                // Llena el combo de las asignaturas
                llenaCombo();
            }

            obtenerParametros();
        }

        public void llenaCombo()
        {
            conn.Open();
            string query = $"SELECT asignaturaid Value, descripcion || ' ' || ciclo || ' - ' || grado || ' ' || nivel || ' ' || seccion Text from Asignaturas where userid = {codigoUsr} and estado = 1 order by descripcion";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            cbxSelectAsig.DataSource = dt.DefaultView;
            cbxSelectAsig.DisplayMember = "Text";
        }

        private void hideSubMenu()
        {
            panelConfigSubmenu.Visible = (panelConfigSubmenu.Visible ? false : false);
            panelData.Visible = (panelData.Visible ? false : false);
        }

        private void showSubMenu(Panel _subMenu)
        {
            if (!_subMenu.Visible)
            {
                hideSubMenu();
                _subMenu.Visible = true;
            }
            else
            {
                _subMenu.Visible = false;
            }
        }



        private void btnAceptarNewUser(object sender, EventArgs e)
        {
            //showSubMenu(panelConfigSubmenu);
            //Controls[3].Controls[0].Controls[0].Text = Controls[3].Controls[0].Controls[2].Text;

            conn.Open();
            string query = String.Format("INSERT INTO Users(username, userdpi) values('{0}','{1}')", Controls[3].Controls[0].Controls[2].Text, Controls[3].Controls[0].Controls[3].Text);
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Controls[3].Controls.Remove(((Button)sender).Parent);
            IniciaApp();
        }

        private void btnSelectUser(object sender, EventArgs e)
        {
            int codigoUsuario = 0;

            Type obj = sender.GetType();
            string algo = obj.ToString();
            if (sender.GetType().ToString() == "System.Windows.Forms.PictureBox")
            {
                PictureBox pxbx = (PictureBox)sender;
                codigoUsuario = Int32.Parse(pxbx.Name.Replace("pbxUser", ""));
            }
            if (sender.GetType().ToString() == "System.Windows.Forms.Label")
            {
                Label pxbx = (Label)sender;
                codigoUsuario = Int32.Parse(pxbx.Name.Replace("lblUser", ""));
            }

            Controls[3].Visible = false;

            panelChildFormContent.Visible = true;
            panelFilter.Visible = true;
            panelSideMenu.Visible = true;

            codigoUsr = codigoUsuario;
            iniciaDatos();
        }



        private void openChildForm(Form _childForm, Button _btnClick)
        {
            if (activeForm != null)
            {
                closeAllChildForm();
            }
            _btnClick.BackColor = System.Drawing.Color.FromArgb(254, 187, 103);
            activeForm = _childForm;
            _childForm.TopLevel = false;
            _childForm.FormBorderStyle = FormBorderStyle.None;
            _childForm.Dock = DockStyle.Fill;
            panelChildFormContent.Controls.Add(_childForm);
            panelChildFormContent.Tag = _childForm;
            _childForm.BringToFront();
            _childForm.Show();
        }

        public void openSubChildForm(Form _childForm)
        {
            _childForm.TopLevel = false;
            _childForm.FormBorderStyle = FormBorderStyle.None;
            _childForm.Dock = DockStyle.Fill;
            panelChildFormContent.Controls.Add(_childForm);
            panelChildFormContent.Tag = _childForm;
            _childForm.BringToFront();
            _childForm.Show();
        }

        private void closeAllChildForm()
        {

            while (panelChildFormContent.Controls.Count > 0)
            {
                ((Form)panelChildFormContent.Controls[0]).Close();
            }

            btnAlumnos.BackColor = Color.LightSteelBlue;
            btnUsers.BackColor = Color.LightSteelBlue;
            btnCourses.BackColor = Color.LightSteelBlue;
            btnGeneralConfig.BackColor = Color.LightSteelBlue;
            btnActividades.BackColor = Color.LightSteelBlue;
            btnReportes.BackColor = Color.LightSteelBlue;
        }



        private void cbxSelectAsig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                closeAllChildForm();
            }

            codigoAsig = Convert.ToInt32((cbxSelectAsig.SelectedItem as DataRowView).Row.ItemArray[0]);

            //MessageBox.Show(codigoAsig.ToString(), "algo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void obtenerParametros()
        {
            conn.Open();
            string query = $"SELECT * from Parametros";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if(dt.Rows.Count > 0) { 
                Parms.ruta_carpetas = Convert.ToString(dt.Rows[0]["ruta_carpetas"]);
                Parms.actividades_por_bloque = Convert.ToInt32(dt.Rows[0]["actividades_por_bloque"]);
                Parms.prct_mejoramiento = Convert.ToInt32(dt.Rows[0]["prct_mejoramiento"]);
                Parms.prct_extemporaneo = Convert.ToInt32(dt.Rows[0]["prct_extemporaneo"]);
                Parms.redondear_arriba = (Convert.ToInt32(dt.Rows[0]["redondear_arriba"]) == 1 ? true : false);
                Parms.activ_con_mejor = (Convert.ToInt32(dt.Rows[0]["actividad_mas_mejoramiento"]) == 1 ? true : false);
            }
        }


        #region Botones de Menu

        #region Datos
        private void btnData_Click(object sender, EventArgs e)
        {
            showSubMenu(panelData);
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            if (codigoAsig != 0)
            {
                openChildForm(new FrmAlumnos(), (Button)sender);
            }
            else
            {
                MessageBox.Show("Debes tener seleccionado una asignatura para esta opción", "Restringido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActividades_Click(object sender, EventArgs e)
        {
            if (codigoAsig != 0)
            {
                //openChildForm(new FrmBuildRubrica(), (Button)sender);
                openChildForm(new FrmActividades(), (Button)sender);
            }
            else
            {
                MessageBox.Show("Debes tener seleccionado una asignatura para esta opción", "Restringido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (codigoAsig != 0)
            {
                //openChildForm(new FrmBuildRubrica(), (Button)sender);
                openChildForm(new FrmReports(), (Button)sender);
            }
            else
            {
                MessageBox.Show("Debes tener seleccionado una asignatura para esta opción", "Restringido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion


        #region Configuracion
        private void btnConfigMenu_Click(object sender, EventArgs e)
        {
            showSubMenu(panelConfigSubmenu);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmUsers(), (Button)sender);
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmAsignatura(), (Button)sender);
        }

        private void btnGeneralConfig_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmParametros(), (Button)sender);
        }
        #endregion


        private void btnLogoutMenu_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                closeAllChildForm();
            }

            Controls.RemoveAt(3);
            codigoAsig = 0;
            codigoUsr = 0;
            IniciaApp();
        }


        #endregion
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class Parametros
    {
        public string ruta_carpetas { get; set; }
        public int actividades_por_bloque { get; set; }
        public int prct_mejoramiento { get; set; }
        public int prct_extemporaneo { get; set; }
        public bool redondear_arriba { get; set; }
        public bool activ_con_mejor { get; set; }
    }
}