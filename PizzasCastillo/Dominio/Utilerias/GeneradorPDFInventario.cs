using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Kernel.Events;
using iText.Layout.Element;
using iText.Kernel.Colors;
using Style = iText.Layout.Style;
using iText.IO.Font.Constants;
using Dominio.Entidades;
using iText.IO.Font;

namespace Dominio.Utilerias
{
    public class GeneradorPDFInventario
    {
        private ReporteInventario nuevoReporte;

        public GeneradorPDFInventario(ReporteInventario reporte)
        {
            this.nuevoReporte = reporte;
        }

        public bool GenerarPDFReporteInventario(String finalPath)
        {
            bool isGenerated = false;

            try
            {
                PdfWriter writer;
                writer = new PdfWriter(finalPath);
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(50, 35, 50, 35);

                document.Add(Encabezado().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT));
                document.Add(GenerarTablaInformacionGeneral(nuevoReporte).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(GenerarTablaDeProductos(nuevoReporte).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(new AreaBreak());
                document.Add(GenerarTablaFirma().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));

                document.Close();
                isGenerated = true;
            }
            catch (IOException ex)
            {

                throw ex;
            }

            return isGenerated;
        }

        private Table GenerarTablaInformacionGeneral(ReporteInventario reporte)
        {
            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

            Style styleLabelText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table informationTable = new Table(4).SetWidth(500).SetMargin(10);

            Cell informationCell = new Cell(1, 4).Add(new Paragraph("Datos del Reporte"));
            informationTable.AddHeaderCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 1).Add(new Paragraph("Nombre del reporte informe: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell(2, 2).Add(new Paragraph(reporte.Nombre));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell(2, 3).Add(new Paragraph("Fecha del reporte: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell(2, 4).Add(new Paragraph(DateTime.Now.ToString("HH:mm MM/dd/yyyy")));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell(3, 4).Add(new Paragraph("Datos del Empleado"));
            informationTable.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(4,1).Add(new Paragraph("Numero de Empleado: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell(4,2).Add(new Paragraph(reporte.GeneradoPor.NumeroEmpleado));
            informationCell = new Cell(4,3).Add(new Paragraph("Nombre: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell(4,4).Add(new Paragraph(reporte.GeneradoPor.NombreCompleto));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell(5,1).Add(new Paragraph("Telefono: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell(5,2).Add(new Paragraph(reporte.GeneradoPor.Telefono));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell(5,3).Add(new Paragraph("Tipo de Empleado: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell(5,4).Add(new Paragraph(reporte.GeneradoPor.TipoUsuario.Nombre));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            return informationTable;
        }

        private Table GenerarTablaDeProductos(ReporteInventario reporte)
        {

            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table productosReportados = new Table(6).SetWidth(600).SetMargin(10);
            Cell informationCell = new Cell(1, 6).Add(new Paragraph("Tabla de Productos en Inventario"));
            productosReportados.AddHeaderCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 1).Add(new Paragraph("Código Barra"));
            productosReportados.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 2).Add(new Paragraph("Nombre"));
            productosReportados.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 3).Add(new Paragraph("Cantidad en Inventario"));
            productosReportados.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 4).Add(new Paragraph("Unidad de Medida"));
            productosReportados.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 5).Add(new Paragraph("Cantidad Real"));
            productosReportados.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 6).Add(new Paragraph("Comentarios"));
            productosReportados.AddCell(informationCell.AddStyle(styleHeaderText));

            List<Reporta> listaArticulos = reporte.Reporta;
            int contadorFilas = 3;

            foreach (Reporta reportado in listaArticulos)
            {
                informationCell = new Cell(contadorFilas, 1).Add(new Paragraph(reportado.Producto.CodigoBarra));
                productosReportados.AddCell(informationCell.AddStyle(styleText));

                informationCell = new Cell(contadorFilas, 2).Add(new Paragraph(reportado.Producto.Nombre));
                productosReportados.AddCell(informationCell.AddStyle(styleText));

                informationCell = new Cell(contadorFilas, 3).Add(new Paragraph(reportado.CantidadEnInventario.ToString()));
                productosReportados.AddCell(informationCell.AddStyle(styleText));

                informationCell = new Cell(contadorFilas, 4).Add(new Paragraph(reportado.Producto.UnidadDeMedida));
                productosReportados.AddCell(informationCell.AddStyle(styleText));

                if(reportado.CantidadReal == 0)
                {
                    informationCell = new Cell(contadorFilas, 5).Add(new Paragraph("-"));
                }
                else
                {
                    informationCell = new Cell(contadorFilas, 5).Add(new Paragraph(reportado.CantidadReal.ToString()));
                }
                
                productosReportados.AddCell(informationCell.AddStyle(styleText));

                if (String.IsNullOrEmpty(reportado.Comentario))
                {
                    informationCell = new Cell(contadorFilas, 6).Add(new Paragraph(" "));
                }
                else
                {
                    informationCell = new Cell(contadorFilas, 6).Add(new Paragraph(reportado.Comentario));
                }
                
                productosReportados.AddCell(informationCell.AddStyle(styleText));
                contadorFilas++;
            }
            return productosReportados;
        }

        private Table GenerarTablaFirma()
        {
            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table signatureTable = new Table(2).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell(3, 1).SetHeight(210);
            signatureTable.AddCell(informationCell);

            informationCell = new Cell().SetHeight(70);
            signatureTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Vo. Bo. Nombre, Puesto y Firma del Empleado Responsable de realizar inventario "));
            signatureTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell();
            signatureTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Nombre(s) y Firma(s) de los Empleados miembros del equipo de inventario (si aplica)"));
            signatureTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph("Sello PIZZAS CASTILLO"));
            signatureTable.AddCell(informationCell.AddStyle(styleHeaderText));

            return signatureTable;
        }

        private Paragraph Encabezado()
        {
            try
            {
                PdfFont ffont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                Paragraph p = new Paragraph("Reporte de Inventario PIZZAS CASTILLO")
                .SetFont(ffont)
                .SetFontSize(15)
                .SetItalic();
                return p;
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}