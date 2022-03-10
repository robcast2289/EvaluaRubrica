using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace EvaluaRubrica
{
    class GeneraDocumentos
    {
        SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        string[] nameMonth = new string[12] { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agostro", "septiembre", "octubre", "noviembre", "diciembre" };

        public void createRubricaPdf(int actividadid, string alumnoid, int asignaturaid, int tipo_evaluacion, string rutaGuardar)
        {
            MemoryStream byteStream = new MemoryStream();

            Document document = new Document();
            document.SetMargins(40, 40, 35, 5);
            document.AddAuthor("EvaluaRubrica");
            document.AddCreator("EvaluaRubrica");
            document.AddCreationDate();
            document.AddTitle("Rúbrica");
            PdfWriter writer = PdfWriter.GetInstance(document, byteStream);
            document.SetPageSize(PageSize.LETTER);
            document.Open();

            // Capa 1 - imagen
            PdfLayer layer = new PdfLayer("layerImage", writer);
            PdfContentByte cb = writer.DirectContent;
            cb.BeginLayer(layer);
            //iTextSharp.text.Image logoPeq = iTextSharp.text.Image.GetInstance($@"C:\Users\DynamicsAdmin\Documents\TODOS LOS CURSOS\BelenImagen.jpeg");
            System.Drawing.Bitmap image = global::EvaluaRubrica.Properties.Resources.BelenImagen;
            iTextSharp.text.Image logoPeq = iTextSharp.text.Image.GetInstance(image,System.Drawing.Imaging.ImageFormat.Jpeg);            
            logoPeq.SetAbsolutePosition(30, document.PageSize.Height - Convert.ToInt16(logoPeq.Height * 0.30) - 40);
            logoPeq.ScalePercent(30);
            cb.AddImage(logoPeq);
            cb.EndLayer();
            // Fin capa 1            

            //Capa 2 - Datos
            layer = new PdfLayer("layerData", writer);
            cb.BeginLayer(layer);

            DataTable encabezado = getDataPdf(actividadid, alumnoid);
            DataTable calificado = getDataCalificadoPdf(actividadid, alumnoid, tipo_evaluacion);
            float espacio = 70f;
            document.Add(paragraphNew("INSTITUTO NORMAL CENTRAL PARA SEÑORITAS 'BELEN'", 12, true, espacio));
            document.Add(paragraphNew($"{Convert.ToString(encabezado.Rows[0]["GRADO"])}", 8, false, espacio));
            document.Add(paragraphNew($"{Convert.ToString(encabezado.Rows[0]["ASIGNATURA"])}", 8, false, espacio));
            document.Add(paragraphNew($"Profesor:   {Convert.ToString(encabezado.Rows[0]["CATEDRATICO"])}", 8, false, espacio));
            document.Add(paragraphNew($"Ciclo       {Convert.ToString(encabezado.Rows[0]["CICLO"])}", 8, false, espacio));
            DateTime fechaCalificado = Convert.ToDateTime(Convert.ToString(calificado.Rows[0]["FECHA"]));
            document.Add(paragraphNew($"Guatemala, {fechaCalificado.Day} de {nameMonth[fechaCalificado.Month - 1]} de {fechaCalificado.Year}", 8, false, espacio));

            if(tipo_evaluacion == 1)
                document.Add(paragraphNew($"MEJORAMIENTO", 12, false, 0, 30f, "center"));
            if (tipo_evaluacion == 2)
                document.Add(paragraphNew($"EXTEMPORANEO", 12, false, 0, 30f, "center"));

            document.Add(paragraphNew($"RUBRICA PARA EVALUAR {Convert.ToString(encabezado.Rows[0]["EVALUAR"])}", 12, false, 0, 30f, "center"));
            document.Add(paragraphNew($"TEMA: {Convert.ToString(encabezado.Rows[0]["TEMA"])}", 12, false, 0, 0, "center"));
            document.Add(paragraphNew($"Valor: {Convert.ToString(encabezado.Rows[0]["PUNTEO_MAXIMO"])}pts", 12, false, 0, 0, "center"));

            document.Add(paragraphNew($"Fecha de entrega: {Convert.ToDateTime(encabezado.Rows[0]["FECHA_ENTREGA"]).ToShortDateString()}", 12, false, 0, 35f, "center"));

            document.Add(paragraphNew($"{Convert.ToString(encabezado.Rows[0]["APELLIDOS_ALUMNO"])}, {Convert.ToString(encabezado.Rows[0]["NOMBRE_ALUMNO"])}", 14, false, 0, 35f, "center"));
            // espacio
            document.Add(paragraphNew(" ", 14, false, 0, 35f));

            //// TABLA
            PdfPCell cell;
            int columnas = 0, filas = 0;
            columnas = Convert.ToInt32(encabezado.Rows[0]["COLUMNAS"]);
            filas = Convert.ToInt32(encabezado.Rows[0]["FILAS"]);
            string[,] textos = getDataDetallePdf(actividadid, columnas + 1, filas + 1);
            bool[,] marcado = getDataDetCalificadoPdf(actividadid, alumnoid, tipo_evaluacion, columnas + 1, filas + 1);
            PdfPTable table = new PdfPTable(columnas + 1);
            for (int y = 0; y <= filas; y++)
            {
                for (int x = 0; x <= columnas; x++)
                {
                    cell = new PdfPCell(paragraphNew($"{textos[x, y]}", 8, false, 0, 0, "center"));
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    if(marcado[x,y])
                        cell.BackgroundColor = new BaseColor(System.Drawing.Color.FromArgb(254, 187, 103));
                    table.AddCell(cell);
                }
            }
            document.Add(table);
            //// FIN TABLA

            document.Add(paragraphNew($"PUNTEO: {Convert.ToString(calificado.Rows[0]["PUNTEO_FIN"])}", 10, false, 50f, 35f));
            if(Convert.ToString(calificado.Rows[0]["OBSERVACIONES"]) != string.Empty)
            {
                document.Add(paragraphNew($"Observaciones: {Convert.ToString(calificado.Rows[0]["OBSERVACIONES"])}", 10, false, 50f, 25f));
            }                        

            cb.EndLayer();
            document.Close();

            byte[] arch = byteStream.GetBuffer();
            string nombreArchivo = "";
            switch (tipo_evaluacion)
            {
                case 0: nombreArchivo = $"{Convert.ToString(encabezado.Rows[0]["ACTIVIDAD"])}"; break;
                case 1: nombreArchivo = $"{Convert.ToString(encabezado.Rows[0]["ACTIVIDAD"])} - mejoramiento"; break;
                case 2: nombreArchivo = $"{Convert.ToString(encabezado.Rows[0]["ACTIVIDAD"])} - extemporaneo"; break;
            }
            guardarAchivo(rutaGuardar,nombreArchivo,"pdf",arch);
            byteStream.Dispose();
        }

        private Paragraph paragraphNew(string text, int fontSize, bool fontBold = false, float spaceLeft = 0, float spaceTop = 0, string align = "left")
        {
            iTextSharp.text.Font myfont1 = new iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, fontSize, (fontBold ? iTextSharp.text.Font.BOLD : iTextSharp.text.Font.NORMAL)));

            Paragraph myParagraph = new Paragraph(text, myfont1);
            switch (align)
            {
                case "left": myParagraph.Alignment = Element.ALIGN_LEFT; break;
                case "right": myParagraph.Alignment = Element.ALIGN_RIGHT; break;
                case "center": myParagraph.Alignment = Element.ALIGN_CENTER; break;
            }
            if (spaceTop != 0)
                myParagraph.Leading = spaceTop;
            if (spaceLeft != 0)
                myParagraph.IndentationLeft = spaceLeft;

            return myParagraph;
        }

        private DataTable getDataPdf(int actividadid, string alumnoid)
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

            return dt;
        }

        private string[,] getDataDetallePdf(int actividadid,int columnas, int filas)
        {
            conn.Open();
            string query = $"SELECT actividadid CODIGO, columna COLUMNA, fila FILA, texto TEXTO, valor VALOR FROM Actividades_detalle WHERE actividadid = {actividadid}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            string[,] detalle = new string[columnas, filas];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int x = Convert.ToInt32(dt.Rows[i]["COLUMNA"]);
                int y = Convert.ToInt32(dt.Rows[i]["FILA"]);

                detalle[x, y] = Convert.ToString(dt.Rows[i]["TEXTO"]);

                if (x > 0 && y > 0)
                {
                    detalle[x, y] = detalle[x, y] + $"\n{Convert.ToDecimal(dt.Rows[i]["VALOR"])}";
                }
            }

            return detalle;
        }

        private DataTable getDataCalificadoPdf(int actividadid, string alumnoid, int tipo_evaluacion)
        {
            conn.Open();
            string query = $@"select fecha FECHA, punteo PUNTEO, punteo_final PUNTEO_FIN, observaciones OBSERVACIONES from Actividad_calificada where actividadid = {actividadid} and alumnoid = '{alumnoid}' and tipo_evaluacion = {tipo_evaluacion}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }

        private bool[,] getDataDetCalificadoPdf(int actividadid, string alumnoid, int tipo_evaluacion, int columnas, int filas)
        {
            conn.Open();
            string query = $"select columna COLUMNA, fila FILA, marcado MARCADO from Actividad_calificada_detalle where actividadid = {actividadid} and alumnoid = '{alumnoid}' and tipo_evaluacion = {tipo_evaluacion}";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();

            bool[,] detalle = new bool[columnas, filas];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int x = Convert.ToInt32(dt.Rows[i]["COLUMNA"]);
                int y = Convert.ToInt32(dt.Rows[i]["FILA"]);

                detalle[x, y] = (Convert.ToInt32(dt.Rows[i]["MARCADO"]) == 1 ? true : false);
            }

            return detalle;
        }

        private void guardarAchivo(string ruta, string nombreArchivo, string ext, byte[] archivo)
        {
            int x = 1;
            while (File.Exists($@"{ruta}\{nombreArchivo}.{ext}"))
            {
                x++;
                if(x > 2)
                {
                    nombreArchivo = nombreArchivo.Replace($"_{x-1}", $"_{x}");
                }
                else
                {
                    nombreArchivo += $"_{x}";
                }                
            }

            File.WriteAllBytes($@"{ruta}\{nombreArchivo}.{ext}", archivo);
        }
    }
}
