using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using DocumentFormat.OpenXml.Office2010.Excel;
using ProyectGarantia.Data.DataAcces;
using ProyectGarantia.Data.Interfaces;
using Newtonsoft.Json;
using static ProyectGarantia.Controllers.DocumentacionController;
using iTextSharp.text.pdf.draw;
using ProyectGarantia.Models;

namespace ProyectGarantia.Controllers
{
    public class DocumentacionController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IDADetalleLoteModelo DADetalleLoteModelo;
		public readonly IDAGarantia DAGarantia;

		public DocumentacionController(IWebHostEnvironment webHostEnvironment, IDADetalleLoteModelo DADetalleLoteModelo, IDAGarantia dAGarantia)
        {
            this.DADetalleLoteModelo = DADetalleLoteModelo;
            _webHostEnvironment = webHostEnvironment;
            DAGarantia = dAGarantia;
        }
        public IActionResult Index( int id)
        {
            var modelodetalle = DADetalleLoteModelo.GetIdDetalleLoteModelo(id);
            return View(modelodetalle);
        }

        [HttpPost]
        public ActionResult SalidaPrestamo(string AQuienSePresta, string MotivoPrestamo, string QuienEntrega, string GarantiasSeleccionadas)
        {
            List<GarantiaSeleccionada> garantiasSeleccionadas = JsonConvert.DeserializeObject<List<GarantiaSeleccionada>>(GarantiasSeleccionadas);

            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.DefaultCell.PaddingLeft = 2f;
            table.DefaultCell.PaddingRight = 2f;
            table.WidthPercentage = 100;

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img\\IDH HORIZONTAL.png");
            Image logo = Image.GetInstance(imagePath);
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleToFit(100f, 100f);
            logo.SetAbsolutePosition(10f, doc.PageSize.Height - 10f - logo.ScaledHeight);
            doc.Add(logo);

            Font font1 = new Font(FontFactory.GetFont("Arial", 14, Font.BOLD));
            Font font2 = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            Font font3 = new Font(FontFactory.GetFont("Arial", 12));


            Paragraph titulo1 = new Paragraph("IDH MICROFINANCIERA", font1);
            titulo1.Alignment = Element.ALIGN_CENTER;
            titulo1.SpacingAfter = 10f;
            doc.Add(titulo1);

            Paragraph titulo2 = new Paragraph("SALIDA POR PRESTAMO DE DOCUMENTOS", font2);
            titulo2.Alignment = Element.ALIGN_LEFT;
            titulo2.SpacingAfter = 10f;
            doc.Add(titulo2);

            Paragraph parrafo1 = new Paragraph("Entrega: " + QuienEntrega, font3);
            parrafo1.Alignment = Element.ALIGN_LEFT;
            parrafo1.SpacingAfter = 10f;
            doc.Add(parrafo1);

            Paragraph parrafo2 = new Paragraph("Prestamo a: " + AQuienSePresta, font3);
            parrafo2.Alignment = Element.ALIGN_LEFT;
            parrafo2.SpacingAfter = 10f;
            doc.Add(parrafo2);

            Paragraph parrafo3 = new Paragraph("Asunto: " + MotivoPrestamo, font3);
            parrafo3.Alignment = Element.ALIGN_LEFT;
            parrafo3.SpacingAfter = 10f;
            doc.Add(parrafo3);

            Paragraph parrafo5 = new Paragraph("Fecha del prestamo: " + DateTime.Now.ToString("dd-MM-yyyy"), font3);
            parrafo5.Alignment = Element.ALIGN_LEFT;
            parrafo5.SpacingAfter = 10f;
            doc.Add(parrafo5);

            FontFactory.RegisterDirectories();
            Font fontTitulo = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fontTitulo.Color = BaseColor.BLACK;

            Font fontDatos = new Font(FontFactory.GetFont("Arial", 11, Font.NORMAL));

            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph("#", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 10f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Nombre", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.MinimumHeight = 40f;
            cell.UseAscender = true;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Agencia", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Contrato", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Comentario", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 30f;
            table.AddCell(cell);

            int count = 0;
            foreach (var item in garantiasSeleccionadas)
            {
                DAGarantia.CambiarEstadoGarantia(item.id, 3);
                count++;
                cell = new PdfPCell(new Paragraph(count + "", fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 10f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.NombreAval, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 40f;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.MinimumHeight = 40f;
                cell.UseAscender = true;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Agencia, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Contrato, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Comentario, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 30f;
                table.AddCell(cell);
            }

            doc.Add(table);

            // Agregar un espacio en blanco al final del documento
            doc.Add(Chunk.NEWLINE);

            // Agregar línea de firma
            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.WHITE, Element.ALIGN_CENTER, -1);
            doc.Add(new Chunk(line));

            PdfPTable firmaTabla = new PdfPTable(4);

            PdfPCell firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph("______________________", fontDatos));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmacell.FixedHeight = 35f;
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph(QuienEntrega, fontTitulo));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.FixedHeight = 35f;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmaTabla.AddCell(firmacell);

            doc.Add(firmaTabla);

            doc.Close();
            writer.Close();

            // Devolver el PDF como un archivo descargable.
            return File(memoryStream.ToArray(), "application/pdf", "SalidaPrestamo.pdf");
        }

        [HttpPost]
        public ActionResult SalidaCambioGarantia(string AutorizaCambio, string GarantiasSeleccionadas)
        {
            List<GarantiaSeleccionada> garantiasSeleccionadas = JsonConvert.DeserializeObject<List<GarantiaSeleccionada>>(GarantiasSeleccionadas);

            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.DefaultCell.PaddingLeft = 2f;
            table.DefaultCell.PaddingRight = 2f;
            table.WidthPercentage = 100;

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img\\IDH HORIZONTAL.png");
            Image logo = Image.GetInstance(imagePath);
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleToFit(100f, 100f);
            logo.SetAbsolutePosition(10f, doc.PageSize.Height - 10f - logo.ScaledHeight);
            doc.Add(logo);

            Font font1 = new Font(FontFactory.GetFont("Arial", 14, Font.BOLD));
            Font font2 = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            Font font3 = new Font(FontFactory.GetFont("Arial", 12));


            Paragraph titulo1 = new Paragraph("IDH MICROFINANCIERA", font1);
            titulo1.Alignment = Element.ALIGN_CENTER;
            titulo1.SpacingAfter = 10f;
            doc.Add(titulo1);

            Paragraph titulo2 = new Paragraph("SALIDA POR CAMBIO DE GARANTÍA", font2);
            titulo2.Alignment = Element.ALIGN_LEFT;
            titulo2.SpacingAfter = 10f;
            doc.Add(titulo2);

            Paragraph parrafo1 = new Paragraph("Autoriza el cambio: " + AutorizaCambio, font3);
            parrafo1.Alignment = Element.ALIGN_LEFT;
            parrafo1.SpacingAfter = 10f;
            doc.Add(parrafo1);

            Paragraph parrafo5 = new Paragraph("Fecha del cambio: " + DateTime.Now.ToString("dd-MM-yyyy"), font3);
            parrafo5.Alignment = Element.ALIGN_LEFT;
            parrafo5.SpacingAfter = 10f;
            doc.Add(parrafo5);

            FontFactory.RegisterDirectories();
            Font fontTitulo = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fontTitulo.Color = BaseColor.BLACK;

            Font fontDatos = new Font(FontFactory.GetFont("Arial", 11, Font.NORMAL));

            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph("#", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 10f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Nombre", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.MinimumHeight = 40f;
            cell.UseAscender = true;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Agencia", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Contrato", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Comentario", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 30f;
            table.AddCell(cell);

            int count = 0;
            foreach (var item in garantiasSeleccionadas)
            {
				DAGarantia.CambiarEstadoGarantia(item.id, 4);
				count++;
                cell = new PdfPCell(new Paragraph(count + "", fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 10f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.NombreAval, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 40f;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.MinimumHeight = 40f;
                cell.UseAscender = true;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Agencia, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Contrato, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Comentario, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 30f;
                table.AddCell(cell);
            }

            doc.Add(table);

            // Agregar un espacio en blanco al final del documento
            doc.Add(Chunk.NEWLINE);

            // Agregar línea de firma
            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.WHITE, Element.ALIGN_CENTER, -1);
            doc.Add(new Chunk(line));

            PdfPTable firmaTabla = new PdfPTable(4);

            PdfPCell firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph("______________________", fontDatos));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmacell.FixedHeight = 35f;
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph(AutorizaCambio, fontTitulo));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.FixedHeight = 35f;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmaTabla.AddCell(firmacell);

            doc.Add(firmaTabla);

            doc.Close();
            writer.Close();

            // Devolver el PDF como un archivo descargable.
            return File(memoryStream.ToArray(), "application/pdf", "SalidaCambioGarantia.pdf");
        }

        [HttpPost]
        public ActionResult IngresoCambioGarantia(string AutorizaCambio, DateTime FechaCambio, string GarantiasSeleccionadas)
        {
            List<GarantiaSeleccionada> garantiasSeleccionadas = JsonConvert.DeserializeObject<List<GarantiaSeleccionada>>(GarantiasSeleccionadas);

            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.DefaultCell.PaddingLeft = 2f;
            table.DefaultCell.PaddingRight = 2f;
            table.WidthPercentage = 100;

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img\\IDH HORIZONTAL.png");
            Image logo = Image.GetInstance(imagePath);
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleToFit(100f, 100f);
            logo.SetAbsolutePosition(10f, doc.PageSize.Height - 10f - logo.ScaledHeight);
            doc.Add(logo);

            Font font1 = new Font(FontFactory.GetFont("Arial", 14, Font.BOLD));
            Font font2 = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            Font font3 = new Font(FontFactory.GetFont("Arial", 12));


            Paragraph titulo1 = new Paragraph("IDH MICROFINANCIERA", font1);
            titulo1.Alignment = Element.ALIGN_CENTER;
            titulo1.SpacingAfter = 10f;
            doc.Add(titulo1);

            Paragraph titulo2 = new Paragraph("INGRESO POR CAMBIO DE GARANTÍA", font2);
            titulo2.Alignment = Element.ALIGN_LEFT;
            titulo2.SpacingAfter = 10f;
            doc.Add(titulo2);

            Paragraph parrafo1 = new Paragraph("Autoriza el cambio: " + AutorizaCambio, font3);
            parrafo1.Alignment = Element.ALIGN_LEFT;
            parrafo1.SpacingAfter = 10f;
            doc.Add(parrafo1);

            Paragraph parrafo5 = new Paragraph("Fecha del cambio: " + DateTime.Now.ToString("dd-MM-yyyy"), font3);
            parrafo5.Alignment = Element.ALIGN_LEFT;
            parrafo5.SpacingAfter = 10f;
            doc.Add(parrafo5);

            FontFactory.RegisterDirectories();
            Font fontTitulo = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fontTitulo.Color = BaseColor.BLACK;

            Font fontDatos = new Font(FontFactory.GetFont("Arial", 11, Font.NORMAL));

            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph("#", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 10f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Nombre", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.MinimumHeight = 40f;
            cell.UseAscender = true;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Agencia", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Contrato", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Comentario", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 30f;
            table.AddCell(cell);

            int count = 0;
            foreach (var item in garantiasSeleccionadas)
            {

                DAGarantia.CambiarEstadoGarantia(item.id, 5);
				count++;
                cell = new PdfPCell(new Paragraph(count + "", fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 10f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.NombreAval, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 40f;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.MinimumHeight = 40f;
                cell.UseAscender = true;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Agencia, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Contrato, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Comentario, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 30f;
                table.AddCell(cell);
            }

            doc.Add(table);

            doc.Add(Chunk.NEWLINE);

            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.WHITE, Element.ALIGN_CENTER, -1);
            doc.Add(new Chunk(line));

            PdfPTable firmaTabla = new PdfPTable(4);

            PdfPCell firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph("______________________", fontDatos));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmacell.FixedHeight = 35f;
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph(AutorizaCambio, fontTitulo));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.FixedHeight = 35f;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmaTabla.AddCell(firmacell);

            doc.Add(firmaTabla);

            doc.Close();
            writer.Close();

            return File(memoryStream.ToArray(), "application/pdf", "IngresoCambioGarantia.pdf");
        }

        [HttpPost]
        public ActionResult CancelaciónPrestamo(string AutorizaCambio, string GarantiasSeleccionadas)
        {
            List<GarantiaSeleccionada> garantiasSeleccionadas = JsonConvert.DeserializeObject<List<GarantiaSeleccionada>>(GarantiasSeleccionadas);

            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.DefaultCell.PaddingLeft = 2f;
            table.DefaultCell.PaddingRight = 2f; 
            table.WidthPercentage = 100;

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img\\IDH HORIZONTAL.png");
            Image logo = Image.GetInstance(imagePath);
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleToFit(100f, 100f);
            logo.SetAbsolutePosition(10f, doc.PageSize.Height - 10f - logo.ScaledHeight);
            doc.Add(logo);

            Font font1 = new Font(FontFactory.GetFont("Arial", 14, Font.BOLD));
            Font font2 = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            Font font3 = new Font(FontFactory.GetFont("Arial", 12));


            Paragraph titulo1 = new Paragraph("IDH MICROFINANCIERA", font1);
            titulo1.Alignment = Element.ALIGN_CENTER;
            titulo1.SpacingAfter = 10f;
            doc.Add(titulo1);

            Paragraph titulo2 = new Paragraph("BAJA POR CANCELACIÓN DE PRESTAMO", font2);
            titulo2.Alignment = Element.ALIGN_LEFT;
            titulo2.SpacingAfter = 10f;
            doc.Add(titulo2);

            Paragraph parrafo1 = new Paragraph("Autoriza el cambio: " + AutorizaCambio, font3);
            parrafo1.Alignment = Element.ALIGN_LEFT;
            parrafo1.SpacingAfter = 10f;
            doc.Add(parrafo1);

            Paragraph parrafo5 = new Paragraph("Fecha de la cancelación: " + DateTime.Now.ToString("dd-MM-yyyy"), font3);
            parrafo5.Alignment = Element.ALIGN_LEFT;
            parrafo5.SpacingAfter = 10f;
            doc.Add(parrafo5);

            FontFactory.RegisterDirectories();
            Font fontTitulo = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fontTitulo.Color = BaseColor.BLACK;

            Font fontDatos = new Font(FontFactory.GetFont("Arial", 11, Font.NORMAL));

            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph("#", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 10f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Nombre", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.MinimumHeight = 40f; 
            cell.UseAscender = true;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Agencia", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Contrato", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 25f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Comentario", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            cell.FixedHeight = 30f;
            table.AddCell(cell);

            int count = 0;
            foreach (var item in garantiasSeleccionadas)
            {
				DAGarantia.CambiarEstadoGarantia(item.id, 6);
				count++;
                cell = new PdfPCell(new Paragraph(count + "", fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 10f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.NombreAval, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 40f;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.MinimumHeight = 40f;
                cell.UseAscender = true;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Agencia, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Contrato, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(item.Comentario, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.BOTTOM_BORDER;
                cell.FixedHeight = 30f;
                table.AddCell(cell);
            }

            doc.Add(table);

            // Agregar un espacio en blanco al final del documento
            doc.Add(Chunk.NEWLINE);

            // Agregar línea de firma
            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.WHITE, Element.ALIGN_CENTER, -1);
            doc.Add(new Chunk(line));

            PdfPTable firmaTabla = new PdfPTable(4);

            PdfPCell firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph("______________________", fontDatos));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmacell.FixedHeight = 35f;
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell();
            firmacell.Border = PdfPCell.NO_BORDER;

            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);
            firmaTabla.AddCell(firmacell);

            firmacell = new PdfPCell(new Paragraph(AutorizaCambio, fontTitulo));
            firmacell.HorizontalAlignment = Element.ALIGN_CENTER;
            firmacell.VerticalAlignment = Element.ALIGN_MIDDLE;
            firmacell.FixedHeight = 35f;
            firmacell.Border = PdfPCell.NO_BORDER;
            firmaTabla.AddCell(firmacell);

            doc.Add(firmaTabla);

            doc.Close();
            writer.Close();

            // Devolver el PDF como un archivo descargable.
            return File(memoryStream.ToArray(), "application/pdf", "CancelacionPrestamo.pdf");
        }

        public class GarantiaSeleccionada
        {
            public string NombreAval { get; set; }
            public string Agencia { get; set; }
            public string Contrato { get; set; }
            public string Comentario { get; set; }
            public string Descripcion { get; set; }
            public string Nombre { get; set; }
            public string Valor { get; set; }

            public int id { get; set; }

        }
    }
}
