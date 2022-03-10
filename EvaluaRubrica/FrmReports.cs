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
using word = Microsoft.Office.Interop.Word;

namespace EvaluaRubrica
{
    public partial class FrmReports : Form
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        Form1 f1 = Application.OpenForms.OfType<Form1>().SingleOrDefault();

        public FrmReports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable datosGenerales, actividadesColum, notasAlumnos;

            datosGenerales = generales();
            actividadesColum = actividadesCol();
            notasAlumnos = notas(Convert.ToInt32(nudBloqueRep.Value));

            int actividades = 6;
            int actividadesSist = Convert.ToInt32(datosGenerales.Rows[0]["CANT_ACTIV"]);
            int filas = Convert.ToInt32(datosGenerales.Rows[0]["CANT_ALUM"]);
            int iRowCount = filas + 5;
            int iColCount = (actividades * 4) + 4;

            

            object ObjMiss = System.Reflection.Missing.Value;
            word.Application ObjWord = new word.Application();
            word.Document ObjDoc = ObjWord.Documents.Add(ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);
            ObjDoc.Activate();
            ObjDoc.PageSetup.Orientation = word.WdOrientation.wdOrientLandscape;
            ObjDoc.PageSetup.LeftMargin = 30;
            ObjDoc.PageSetup.RightMargin = 30;
            ObjDoc.PageSetup.TopMargin = 40;
            ObjDoc.PageSetup.BottomMargin = 40;
            float widthPa = ObjDoc.PageSetup.PageWidth;
            //ObjWord.Selection.Orientation = word.WdTextOrientation.wdTextOrientationHorizontal;
            //ObjWord.Selection.Font.Color = word.WdColor.wdColorBlue;
            ObjWord.Selection.Font.Color = word.WdColor.wdColorBlack;
            //ObjWord.Selection.TypeText($"Ancho de pagina {widthPa}");

            object objEndOfDoc = "\\endofdoc";
            word.Range WordRange = ObjDoc.Bookmarks.get_Item(ref objEndOfDoc).Range;
            Microsoft.Office.Interop.Word.Table wordTable;
            wordTable = ObjDoc.Tables.Add(WordRange, iRowCount, iColCount, ref ObjMiss, ref ObjMiss);
            // autoajuste de ancho de las celdas
            for (int i = 1; i <= iColCount; i++)
            {
                wordTable.Columns[i].Width = (ObjDoc.PageSetup.PageWidth - (ObjDoc.PageSetup.LeftMargin + ObjDoc.PageSetup.RightMargin) - 200) / (iColCount - 1);
                for (int j = 1; j <= iRowCount; j++)
                {
                    wordTable.Cell(j, i).Range.Font.Size = 9;
                    wordTable.Cell(j, i).Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wordTable.Cell(j, i).VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    wordTable.Cell(j, i).Range.ParagraphFormat.SpaceAfter = 0;
                    wordTable.Cell(j, i).Range.ParagraphFormat.SpaceBefore = 0;
                }
            }
            wordTable.Columns[2].Width = 200;
            wordTable.Rows[5].Height = 100;
            // fin autoajuste

            // combina celdas
            word.Cell cellMerge;
            for (int rHead = 1; rHead < 5; rHead++)
            {
                cellMerge = wordTable.Cell(rHead, 1);
                cellMerge.Merge(wordTable.Cell(rHead, cellMerge.ColumnIndex + 1));
                for (int act = 0; act < actividades; act++)
                {
                    cellMerge = wordTable.Cell(rHead, act + 2);
                    cellMerge.Merge(wordTable.Cell(rHead, cellMerge.ColumnIndex + 3));                    
                }                
            }
            cellMerge = wordTable.Cell(1, actividades + 3);
            cellMerge.Merge(wordTable.Cell(cellMerge.RowIndex + 4, iColCount));
            // fin combina celdas
            
            // titulos
            wordTable.Cell(1, 1).Range.Text = "INSTITUTO NORMAL CENTRAL PARA SEÑORITAS 'BELEN'";
            wordTable.Cell(2, 1).Range.Text = $"Asignatura grado nivel y seccion \n Subárea: {datosGenerales.Rows[0]["ASIGNATURA"]}";
            wordTable.Cell(3, 1).Range.Text = $"Catedrática(o): {datosGenerales.Rows[0]["CATEDRATICO"]}";
            wordTable.Cell(4, 1).Range.Text = $"Ciclo Escolar: {datosGenerales.Rows[0]["CICLO"]} \t Bloque: {nudBloqueRep.Value}";
            wordTable.Cell(5, 1).Range.Text = "Clave";
            wordTable.Cell(5, 2).Range.Text = "Apellidos y nombres";
            wordTable.Cell(1, actividades + 3).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
            wordTable.Cell(1, actividades + 3).Range.Text = "Total sumatoria de todas las actividades";

            for (int x = 1; x <= actividades; x++)
            {                
                wordTable.Cell(1, x + 1).Range.Text = $"ACTIVIDAD No. {x}";                
                wordTable.Cell(2, x + 1).Range.Text = $"{(actividadesColum.Rows.Count >= x ? actividadesColum.Rows[x - 1]["DESCRIPCION"] : "")}";
                wordTable.Cell(3, x + 1).Range.Font.Size = 8;
                wordTable.Cell(3, x + 1).Range.Text = $"Fecha entrega \n{(actividadesColum.Rows.Count >= x ? actividadesColum.Rows[x-1]["FECHA_ENTREGA"] : "")}";
                wordTable.Cell(4, x + 1).Range.Font.Size = 8;
                wordTable.Cell(4, x + 1).Range.Text = $"Fecha mejoramiento \n{(actividadesColum.Rows.Count >= x ? actividadesColum.Rows[x - 1]["FECHA_MEJOR"] : "")}";

                wordTable.Cell(5, ((x-1) * 4) + 3).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
                wordTable.Cell(5, ((x - 1) * 4) + 3).Range.Text = "Puntos";
                wordTable.Cell(5, ((x - 1) * 4) + 3 + 1).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
                wordTable.Cell(5, ((x - 1) * 4) + 3 + 1).Range.Text = $"Pt. Mejor. {f1.Parms.prct_mejoramiento}%";
                wordTable.Cell(5, ((x - 1) * 4) + 3 + 2).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
                wordTable.Cell(5, ((x - 1) * 4) + 3 + 2).Range.Text = $"Fuera tiempo {f1.Parms.prct_extemporaneo}%";
                wordTable.Cell(5, ((x - 1) * 4) + 3 + 3).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
                wordTable.Cell(5, ((x - 1) * 4) + 3 + 3).Range.Text = "Total";
            }
            // fin titulos

            // Datos
            string clave = "";
            int actividadId = 0;
            int actividadOrden = 0;
            int totalActividad = 0;
            int totalUnidad = 0;
            int linea = 5;
            for (int x = 0; x < notasAlumnos.Rows.Count; x++)
            {
                if(Convert.ToString(notasAlumnos.Rows[x]["CLAVE"]) != clave)
                {
                    // total ultima actividad y total unidad
                    if (actividadOrden != 0 && clave != "")
                    {
                        wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 3).Range.Text = (totalActividad != 0 ? Convert.ToString(totalActividad) : "");
                        totalUnidad += totalActividad;
                        wordTable.Cell(linea, (actividades * 4) + 4).Range.Text = (totalUnidad != 0 ? Convert.ToString(totalUnidad) : "");
                        totalUnidad = 0;
                        totalActividad = 0;
                    }
                    // fin totales
                    clave = Convert.ToString(notasAlumnos.Rows[x]["CLAVE"]);
                    linea++;
                    wordTable.Cell(linea, 1).Range.Text = clave;
                    wordTable.Cell(linea, 2).Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphLeft;
                    wordTable.Cell(linea, 2).Range.Text = $"{notasAlumnos.Rows[x]["APELLIDOS"]}, {notasAlumnos.Rows[x]["NOMBRES"]}";
                    actividadOrden = 0;
                }
                if(Convert.ToInt32(notasAlumnos.Rows[x]["actividadid"]) != actividadId)
                {
                    // total Activdad anterior
                    if(actividadOrden != 0) {
                        wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 3).Range.Text = (totalActividad != 0 ? Convert.ToString(totalActividad) : "");
                        totalUnidad += totalActividad;
                        totalActividad = 0;
                    }
                    // fin total actividad

                    actividadOrden++;
                    actividadId = Convert.ToInt32(notasAlumnos.Rows[x]["actividadid"]);
                }
                if(actividadOrden <= 6) { // HASTA UN MAXIMO DE 6 ACTIVIDADES MUESTRA EL REPORTE
                    if(Convert.ToString(notasAlumnos.Rows[x]["tipo_evaluacion"]) != "" ) { 
                        switch (Convert.ToInt32(notasAlumnos.Rows[x]["tipo_evaluacion"]))
                        {
                            case 0:
                                wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3).Range.Text = Convert.ToString(notasAlumnos.Rows[x]["punteo_final"]);
                                totalActividad = Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                break;
                            case 1:
                                wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 1).Range.Text = Convert.ToString(notasAlumnos.Rows[x]["punteo_final"]);
                                if (f1.Parms.activ_con_mejor)
                                {
                                    totalActividad += Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                }
                                else
                                {
                                    totalActividad = Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                }
                                break;
                            case 2:
                                wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 2).Range.Text = Convert.ToString(notasAlumnos.Rows[x]["punteo_final"]);
                                totalActividad = Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                break;
                        }
                    }
                }
            }
            // fin datos

            wordTable.Borders.Enable = 1;
            ObjWord.Visible = true;
        }

        private DataTable generales()
        {
            DataTable dt;
            conn.Open();
            string query = $@"SELECT 
usr.username CATEDRATICO
,asig.descripcion ASIGNATURA
,asig.ciclo CICLO
,asig.grado GRADO
,asig.nivel NIVEL
,asig.seccion SECCION
,(select count(*) CANT_ACTIV from Actividades act where act.asignaturaid = asig.asignaturaid and act.bloque = {nudBloqueRep.Value}) CANT_ACTIV
,(select count(*) CANT_ALUM from Alumnos alm where alm.asignaturaid = asig.asignaturaid) CANT_ALUM
FROM Users usr, Asignaturas asig
WHERE 
asig.userid = usr.userid
and usr.userid = {f1.codigoUsr}
and asig.asignaturaid = {f1.codigoAsig}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        private DataTable actividadesCol()
        {
            DataTable dt;
            conn.Open();
            string query = $@"select 
descripcion DESCRIPCION
,fecha_entrega FECHA_ENTREGA
,fecha_entrega_mejoramiento FECHA_MEJOR 
from Actividades
where asignaturaid = {f1.codigoAsig} 
and bloque = {nudBloqueRep.Value} 
order by actividadid";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        private DataTable notas(int bloque)
        {
            DataTable dt;
            conn.Open();
            string query = $@"select 
alm.alumnoid CLAVE
,alm.apellidos APELLIDOS
,alm.nombres NOMBRES 
,act.actividadid
,act.descripcion
,acal.tipo_evaluacion
,acal.punteo_final
from Alumnos alm
LEFT JOIN Actividades act on act.asignaturaid = alm.asignaturaid
LEFT JOIN Actividad_calificada acal on acal.actividadid = act.actividadid and acal.alumnoid == alm.alumnoid
where alm.asignaturaid = {f1.codigoAsig}
and act.bloque = {bloque}
order by alm.apellidos";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        private void btnPromedios_Click(object sender, EventArgs e)
        {
            llenaTablaTemporal();

            DataTable datosGenerales, notasProm;

            datosGenerales = generales();
            notasProm = notasPromedio();

            int actividades = 4;
            int actividadesSist = Convert.ToInt32(datosGenerales.Rows[0]["CANT_ACTIV"]);
            int filas = notasProm.Rows.Count;
            int iRowCount = filas + 5;
            int iColCount = (actividades) + 5;



            object ObjMiss = System.Reflection.Missing.Value;
            word.Application ObjWord = new word.Application();
            word.Document ObjDoc = ObjWord.Documents.Add(ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);
            ObjDoc.Activate();
            ObjDoc.PageSetup.Orientation = word.WdOrientation.wdOrientPortrait;
            ObjDoc.PageSetup.LeftMargin = 30;
            ObjDoc.PageSetup.RightMargin = 30;
            ObjDoc.PageSetup.TopMargin = 40;
            ObjDoc.PageSetup.BottomMargin = 40;
            float widthPa = ObjDoc.PageSetup.PageWidth;
            //ObjWord.Selection.Orientation = word.WdTextOrientation.wdTextOrientationHorizontal;
            //ObjWord.Selection.Font.Color = word.WdColor.wdColorBlue;
            ObjWord.Selection.Font.Color = word.WdColor.wdColorBlack;

            object start = 0;
            object end = 0;
            word.Range tableLocation = ObjDoc.Range(ref start, ref end);
            Microsoft.Office.Interop.Word.Table encTable;
            encTable = ObjDoc.Tables.Add(tableLocation, 2, 2);
            encTable = ObjDoc.Tables[1];
            encTable.Range.Font.Size = 12;
            encTable.Range.Font.Bold = 1;
            encTable.Borders.Enable = 0;

            encTable.Cell(1, 1).Range.Text = $"Asignatura: {datosGenerales.Rows[0]["ASIGNATURA"]}";
            encTable.Cell(1, 2).Range.Text = $"Grado y sección: {datosGenerales.Rows[0]["GRADO"]} {datosGenerales.Rows[0]["NIVEL"]} {datosGenerales.Rows[0]["SECCION"]}";
            encTable.Cell(2,1).Range.Text = $"Catedrática(o): {datosGenerales.Rows[0]["CATEDRATICO"]}";

            encTable.Range.Select();
            ObjWord.Selection.MoveDown(word.WdUnits.wdLine,1);
            ObjWord.Selection.TypeParagraph();
            //ObjWord.Selection.TypeText($"Ancho de pagina {widthPa}");

            object objEndOfDoc = "\\endofdoc";
            word.Range WordRange = ObjDoc.Bookmarks.get_Item(ref objEndOfDoc).Range;
            tableLocation = ObjDoc.Range(0, 0);
            Microsoft.Office.Interop.Word.Table wordTable;
            wordTable = ObjDoc.Tables.Add(ObjWord.Selection.Range, iRowCount, iColCount);
            //wordTable = ObjDoc.Tables[2];
            // autoajuste de ancho de las celdas
            for (int i = 1; i <= iColCount; i++)
            {
                wordTable.Columns[i].Width = (ObjDoc.PageSetup.PageWidth - (ObjDoc.PageSetup.LeftMargin + ObjDoc.PageSetup.RightMargin) - 200) / (iColCount - 1);
                for (int j = 1; j <= iRowCount; j++)
                {
                    wordTable.Cell(j, i).Range.Font.Size = 9;
                    wordTable.Cell(j, i).Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
                    wordTable.Cell(j, i).VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    wordTable.Cell(j, i).Range.ParagraphFormat.SpaceAfter = 0;
                    wordTable.Cell(j, i).Range.ParagraphFormat.SpaceBefore = 0;
                }
            }
            wordTable.Columns[1].Width = 25;
            wordTable.Columns[2].Width = 50;
            wordTable.Columns[3].Width = 150;
            wordTable.Columns[9].Width = 100;
            wordTable.Rows[1].Height = 50;
            // fin autoajuste

            // titulos
            wordTable.Cell(1, 1).Range.Text = "No.";
            wordTable.Cell(1, 2).Range.Text = "Código";
            wordTable.Cell(1, 3).Range.Text = "Nombre";
            wordTable.Cell(1, 4).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
            wordTable.Cell(1, 5).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
            wordTable.Cell(1, 6).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
            wordTable.Cell(1, 7).Range.Orientation = word.WdTextOrientation.wdTextOrientationVerticalFarEast;
            wordTable.Cell(1, 4).Range.Text = "Unidad 1";
            wordTable.Cell(1, 5).Range.Text = "Unidad 2";
            wordTable.Cell(1, 6).Range.Text = "Unidad 3";
            wordTable.Cell(1, 7).Range.Text = "Unidad 4";
            wordTable.Cell(1, 8).Range.Text = "NOTA FINAL";
            wordTable.Cell(1, 9).Range.Text = "Observaciones";

            // Datos
            int linea = 2;
            for (int x = 0; x < notasProm.Rows.Count; x++)
            {
                decimal promedio = 0;
                wordTable.Cell(linea, 1).Range.Text = Convert.ToString(x+1);
                wordTable.Cell(linea, 2).Range.Text = $"{notasProm.Rows[x]["alumnoid"]}";
                wordTable.Cell(linea, 3).Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphLeft;
                wordTable.Cell(linea, 3).Range.Text = $"{notasProm.Rows[x]["alumno_nombre"]}";                
                wordTable.Cell(linea, 4).Range.Text = (Convert.ToInt32(notasProm.Rows[x]["bloque1"]) != 0 ? Convert.ToString(notasProm.Rows[x]["bloque1"]) : "");
                wordTable.Cell(linea, 5).Range.Text = (Convert.ToInt32(notasProm.Rows[x]["bloque2"]) != 0 ? Convert.ToString(notasProm.Rows[x]["bloque2"]) : "");
                wordTable.Cell(linea, 6).Range.Text = (Convert.ToInt32(notasProm.Rows[x]["bloque3"]) != 0 ? Convert.ToString(notasProm.Rows[x]["bloque3"]) : "");
                wordTable.Cell(linea, 7).Range.Text = (Convert.ToInt32(notasProm.Rows[x]["bloque4"]) != 0 ? Convert.ToString(notasProm.Rows[x]["bloque4"]) : "");
                promedio = Math.Ceiling(Convert.ToDecimal((Convert.ToInt32(notasProm.Rows[x]["bloque1"]) + Convert.ToInt32(notasProm.Rows[x]["bloque2"]) + Convert.ToInt32(notasProm.Rows[x]["bloque3"]) + Convert.ToInt32(notasProm.Rows[x]["bloque4"])) / 4));
                wordTable.Cell(linea, 8).Range.Text = (promedio != 0 ? Convert.ToString(promedio) : "");                
                if(promedio != 0)
                {
                    if(promedio >= 60)
                        wordTable.Cell(linea, 9).Range.Text = "APROBADO";
                    else
                        wordTable.Cell(linea, 9).Range.Text = "NO APROBADO";
                }
                linea++;
            }
            // fin datos

            wordTable.Borders.Enable = 1;
            ObjWord.Visible = true;
        }

        private DataTable notasPromedio()
        {
            DataTable dt;
            conn.Open();
            string query = $@"select * from TmpReportPromedio";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        private void vaciarTablaTemporal()
        {
            try
            {
                conn.Open();
                string query = $"DELETE FROM TmpReportPromedio";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void insertAlumnoTmp(string alumnoid, string nombre, int bloque, decimal valor)
        {
            decimal b1 = 0, b2 = 0, b3 = 0, b4 = 0;
            switch (bloque)
            {
                case 1: b1 = valor; break;
                case 2: b2 = valor; break;
                case 3: b3 = valor; break;
                case 4: b4 = valor; break;
                default: break;
            }
            try
            {
                conn.Open();
                string query = $"insert into TmpReportPromedio(alumnoid,alumno_nombre,bloque1,bloque2,bloque3,bloque4) values('{alumnoid}','{nombre}',{b1},{b2},{b3},{b4})";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateAlumnoTmp(string alumnoid, int bloque, decimal valor)
        {
            try
            {
                conn.Open();
                string query = $"UPDATE TmpReportPromedio SET bloque{bloque} = {valor} where alumnoid = '{alumnoid}'";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool buscaAlumnoTmp(string alumnoid)
        {
            DataTable dt;
            bool existe = false;
            conn.Open();
            string query = $@"select * from TmpReportPromedio where alumnoid = '{alumnoid}'";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
                existe = true;

            return existe;
        }

        private void llenaTablaTemporal()
        {
            vaciarTablaTemporal();
            DataTable notasAlumnos;
            

            string clave = "";
            int actividadId = 0;
            int actividadOrden = 0;
            int totalActividad = 0;
            int totalUnidad = 0;
            int linea = 2;
            for (int unidad = 1; unidad <= 4; unidad++)
            {
                notasAlumnos = notas(unidad);

                for (int x = 0; x < notasAlumnos.Rows.Count; x++)
                {
                    if (Convert.ToString(notasAlumnos.Rows[x]["CLAVE"]) != clave)
                    {
                        // total unidad
                        if (actividadOrden != 0 && clave != "")
                        {
                            //wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 3).Range.Text = (totalActividad != 0 ? Convert.ToString(totalActividad) : "");
                            totalUnidad += totalActividad;
                            //wordTable.Cell(linea, 4).Range.Text = (totalUnidad != 0 ? Convert.ToString(totalUnidad) : "");
                            updateAlumnoTmp(clave, unidad, totalUnidad);
                            totalUnidad = 0;
                            totalActividad = 0;
                        }
                        // fin totales
                        clave = Convert.ToString(notasAlumnos.Rows[x]["CLAVE"]);
                        linea++;
                        if (!buscaAlumnoTmp(clave))
                        {
                            insertAlumnoTmp(clave, $"{notasAlumnos.Rows[x]["APELLIDOS"]}, {notasAlumnos.Rows[x]["NOMBRES"]}", unidad, 0);
                        }
                        /*wordTable.Cell(linea, 2).Range.Text = clave;
                        wordTable.Cell(linea, 3).Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphLeft;
                        wordTable.Cell(linea, 3).Range.Text = $"{notasAlumnos.Rows[x]["APELLIDOS"]}, {notasAlumnos.Rows[x]["NOMBRES"]}";*/
                        actividadOrden = 0;
                    }
                    if (Convert.ToInt32(notasAlumnos.Rows[x]["actividadid"]) != actividadId)
                    {
                        // total Activdad anterior
                        if (actividadOrden != 0)
                        {
                            //wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 3).Range.Text = (totalActividad != 0 ? Convert.ToString(totalActividad) : "");
                            totalUnidad += totalActividad;
                            totalActividad = 0;
                        }
                        // fin total actividad

                        actividadOrden++;
                        actividadId = Convert.ToInt32(notasAlumnos.Rows[x]["actividadid"]);
                    }
                    if (actividadOrden <= 6)
                    { // HASTA UN MAXIMO DE 6 ACTIVIDADES MUESTRA EL REPORTE
                        if (Convert.ToString(notasAlumnos.Rows[x]["tipo_evaluacion"]) != "")
                        {
                            switch (Convert.ToInt32(notasAlumnos.Rows[x]["tipo_evaluacion"]))
                            {
                                case 0:
                                    //wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3).Range.Text = Convert.ToString(notasAlumnos.Rows[x]["punteo_final"]);
                                    totalActividad = Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                    break;
                                case 1:
                                    //wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 1).Range.Text = Convert.ToString(notasAlumnos.Rows[x]["punteo_final"]);
                                    if (f1.Parms.activ_con_mejor)
                                    {
                                        totalActividad += Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                    }
                                    else
                                    {
                                        totalActividad = Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                    }
                                    break;
                                case 2:
                                    //wordTable.Cell(linea, ((actividadOrden - 1) * 4) + 3 + 2).Range.Text = Convert.ToString(notasAlumnos.Rows[x]["punteo_final"]);
                                    totalActividad = Convert.ToInt32(notasAlumnos.Rows[x]["punteo_final"]);
                                    break;
                            }
                        }
                    }
                }
            }
            
            
        }
    }
}
