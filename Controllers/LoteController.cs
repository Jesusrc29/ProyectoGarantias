using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using BarcodeLib;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.Host;
using Newtonsoft.Json;
using ProyectGarantia.Data;
using ProyectGarantia.Data.DataAcces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;
using ProyectGarantia.Services;
using X.PagedList;
using BarcodeLib;
using static ProyectGarantia.Data.ApplicationDbContext;
using static ProyectGarantia.Models.DetalleLoteModelo;
using Barcode = BarcodeLib.Barcode;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Crypto = System.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace ProyectGarantia.Controllers
{
    [Authorize]
    public class LoteController : Controller
    {
        public readonly IDALote DALote;
        public readonly IHTTPRequest _httpRequest;
        public readonly IDAAgencia DAAgencia;
        public readonly IDADetalleLoteModelo DADetalleLote;
        private readonly IDAGarantia DAGarantia;
        private readonly IDADocumento DADocumento;
        private readonly UserManager<Usuario> usuario;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _dbAgencia;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public LoteController(IDAAgencia DAAgencia, IDALote DALote, IHTTPRequest httpRequest,
                                UserManager<Usuario> usuario, ApplicationDbContext dbAgencia,
                                IDADetalleLoteModelo DADetalleLote, IWebHostEnvironment webHostEnvironment, 
                                IDAGarantia DAGarantia, IDADocumento dADocumento, SignInManager<Usuario> signInManager)
        {
            this.DALote = DALote;
            _httpRequest = httpRequest;
            this.usuario = usuario;
            this.DAAgencia = DAAgencia;
            this.DADetalleLote = DADetalleLote;
            _dbAgencia = dbAgencia;
            _webHostEnvironment = webHostEnvironment;
            this.DAGarantia = DAGarantia;
            this.DADocumento = dADocumento;
            _signInManager = signInManager;
        }

        public IActionResult Index(int page = 1)
        {
            var pageNumber = page;
            var informacionDB = DALote.GetLoteByEstado(EstadoLote.Enviado);
            var Datos = informacionDB.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, 8);
            return View(Datos);
        }

        public IActionResult LotesEnviados(int page = 1)
        {
            var pageNumber = page;
            var informacionDB = DALote.GetLoteByEstado(EstadoLote.EnCurso);
            var Datos = informacionDB.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, 8);
            return View(Datos);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var usuarioLogueado = await usuario.GetUserAsync(User);
            var codAgenciaID = usuarioLogueado.AgenciaId;

            var codAgencia = _dbAgencia.Agencias.FirstOrDefault(c => c.Id == codAgenciaID)?.Codigo;

            string numeroCorrelativo = "";

            if (!string.IsNullOrEmpty(codAgencia))
            {
                var contadorAgencia = _dbAgencia.ContadorLotes.FirstOrDefault(c => c.CodAgencia == codAgencia);

                if (contadorAgencia == null)
                {
                    contadorAgencia = new ContadorLotes { CodAgencia = codAgencia, Contador = 1 };
                    _dbAgencia.ContadorLotes.Add(contadorAgencia);
                }
                else
                {
                    if (contadorAgencia.Contador > 999998)
                    {
                        
                        contadorAgencia.Contador = 1;
                        numeroCorrelativo = $"{codAgencia:D4}-{contadorAgencia.Contador:D7}";
                    }
                    else
                    {
                        contadorAgencia.Contador++;
                        numeroCorrelativo = $"{codAgencia:D4}-{contadorAgencia.Contador:D6}";
                    }
                }


                bool esContrato = false; 
                if (!string.IsNullOrEmpty(codAgencia))
                {
                    esContrato = true;  
                }

                string tipoDocumento = esContrato ? "CON" : "PAG";
                string numeroCorrelativoDoc = $"{tipoDocumento}-{codAgencia}-{contadorAgencia.Contador:D6}";

                await _dbAgencia.SaveChangesAsync();
            }

            ViewBag.NumeroCorrelativo = numeroCorrelativo;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lote Lote)
        {
            var usuarioLogueado = await usuario.GetUserAsync(User);
            int AgenciaId = usuarioLogueado.AgenciaId;
            
            var codAgencia = _dbAgencia.Agencias.SingleOrDefault(c => c.Id == AgenciaId)?.Codigo;
            Lote.Id = 0;
            Lote.FechaCreacion = DateOnly.FromDateTime(DateTime.Now.Date);
            var model = DALote.InsertLote(Lote);

            if (model > 0)
            {
                var nombre = DAAgencia.GetIdAgencia(AgenciaId).Nombre;
                var api = await _httpRequest.ObtenerMensajeAsync(codAgencia, Lote.FechaDesde, Lote.FechaHasta);

                List<DetalleLoteModelo> lista = new List<DetalleLoteModelo>();
                foreach (var item in api)
                {
                    DetalleLoteModelo obj = new DetalleLoteModelo();
                    
                    obj.Id = 0;
                    obj.LoteId = model;
                    obj.FechaOtorgada = DateOnly.FromDateTime(item.Fecha_otorgamiento);
                    obj.NombreCliente = item.Nombres + " " + item.Apellidos;
                    obj.NombreAsesor = item.Asesor;
                    obj.CentroCosto = nombre;
                    obj.NumeroCorrelativo = Lote.NumeroCorrelativo;
                    obj.NombreCreador = Lote.NombreCreador;
                    obj.NumPrestamo = item.Prestamo;


                    decimal montoTotal = 0;
                    int countGarantias = 0;
                    foreach (var i in item.Cgarantia)
                    {
                        if (i.TpAval == 2 && i.Prestamo == item.Prestamo)
                        {
                            Garantia objGarantia = new Garantia();
                            objGarantia.Id = 0;
                            objGarantia.CorrGarantia = i.Cod_aval;

                            objGarantia.PrestamoId = obj.Id;
                            objGarantia.NombreAval = i.Nombre_aval;
                            objGarantia.MontoGarantia = (double)i.Monto_garantia;
                            objGarantia.DescripAval = i.Descrip_aval;

                            if (obj.Garantias == null)
                            {
                                obj.Garantias = new List<Garantia>();
                            }

                            obj.Garantias.Add(objGarantia);

                            montoTotal += i.Monto_garantia;
                            countGarantias++;

                            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "archivos", "codigoBarras",objGarantia.CorrGarantia + ".png");
                            if (!System.IO.File.Exists(filePath))
                            {
                                System.Drawing.Image imagenCodigo;
                                BarcodeLib.TYPE tipoCodigo = BarcodeLib.TYPE.CODE128;

                                Barcode codBarras = new Barcode();
                                codBarras.IncludeLabel = true;
                                codBarras.LabelPosition = LabelPositions.BOTTOMCENTER;

                                imagenCodigo = codBarras.Encode(tipoCodigo, objGarantia.CorrGarantia, System.Drawing.Color.Black, System.Drawing.Color.White, 300, 100);

                                imagenCodigo.Save(filePath);
                            }
                        }
                    }


                    obj.ValorGarantia = montoTotal;
                    obj.CantidadGarantias = countGarantias;
                    lista.Add(obj);
                }

                int rptaDetalleLote = DADetalleLote.InsertListDetalleLoteModelo(lista);
                if (rptaDetalleLote > 0)
                {
                    return RedirectToAction("Index");
                    
                }
                else
                {
                    return View(model);
                }

            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            return RedirectToAction("Index", "DetalleLoteModelo", new { id = id });
        }

        public IActionResult Edit(int id)
        {
            var model = DALote.GetIdLote(id);

            return View(model);
        }

        public IActionResult Enviar(int id)
        {
            Lote lote = DALote.GetIdLote(id);
            lote.Estado = EstadoLote.EnCurso;
            DetalleLoteModelo detalleLote = DADetalleLote.GetDetalleLoteModeloPorLote(id).FirstOrDefault();

            detalleLote.FechaEnvio = DateOnly.FromDateTime(DateTime.Now);

            var resultadoLote = DALote.UpdateLote(lote);
            var resultadoDetalleLote = DADetalleLote.UpdateDetalleLoteModelo(detalleLote); 

            if (resultadoLote && resultadoDetalleLote)
            {
                TempData["SuccessMessage"] = "El envío se realizó correctamente.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(resultadoLote);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AprobarLote(int id, bool isAjax, string password)
        {
            Lote lote = DALote.GetIdLote(id);
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (lote == null)
            {
                return NotFound();
            }

            if (lote.Estado == EstadoLote.Aprobado)
            {
                return RedirectToAction("LotesEnviados");
            }

            bool todosPrestamosAprobados = VerificarPrestamosAprobados(id);

            if (todosPrestamosAprobados)
            {
                lote.Estado = EstadoLote.Aprobado;
                DALote.UpdateLote(lote);
                if (isAjax)
                {
                    return Json(new { success = true, requirePassword=false, redirectUrl = Url.Action("LotesEnviados") });
                }
                else
                {
                    return RedirectToAction("LotesEnviados");
                }
            }
            else
            {
                if (isAjax)
                {
                    return Json(new { success = true, message = "Aprobación exitosa", requirePassword = true });
                }
                else
                {
                    return View("LotesEnviados");
                }
            }
        }

        private bool VerificarPrestamosAprobados(int loteId)
        {
            var prestamos = DADetalleLote.GetDetalleLoteModeloPorLote(loteId);

            return prestamos != null && prestamos.All(p => p.Estado == DetalleLoteModelo.EstadoPrestamo.Aprobado);
        }

        [HttpGet]
        public async Task<IActionResult> VerificarContrasenaAdmin(int id, string password)
        {
            try
            {
                var listaususarios = await usuario.Users.ToListAsync();
                                
                var adminUsers = listaususarios.Where(user =>usuario.IsInRoleAsync(user, "Supervisora").Result)
                     .ToList();

                foreach (var item in adminUsers)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(item, password, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        Lote lote = DALote.GetIdLote(id);
                        lote.Estado = EstadoLote.Aprobado;
                        DALote.UpdateLote(lote);
                        return Json(true);
                    }
                }
                return Json(false);
            }
            catch (Exception ex)
            {

                return Json(false);
            }
        }


        [HttpPost]
        public IActionResult Edit(Lote Lote)
        {
            var resultado = DALote.UpdateLote(Lote);
            if (resultado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(resultado);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            List<DetalleLoteModelo> detalle = (List<DetalleLoteModelo>)DADetalleLote.GetDetalleLoteModeloPorLote(id);
            if (detalle.Count() > 0)
            {
                foreach (var item in detalle)
                {
                    List<Documento> docs = DADocumento.GetDocumentoPorDetalleLote(item.Id);
                    if (docs.Count() > 0)
                    {
                        foreach (var doc in docs)
                        {
                            await BorrarArchivo(doc.NombreDocumento);
                        }
                    }
                }
            }
            var lote = DALote.GetIdLote(id);
            _dbAgencia.Entry(lote).State = EntityState.Detached;

            var resultado = DALote.DeleteLote(id);
            return RedirectToAction("LotesEnviados");
        }

        public async Task<ActionResult> GenerarReporte(DateTime fechaInicio, DateTime fechaFin, string estado, string formato)
        {
            var usuarioLogueado = await usuario.GetUserAsync(User);
            int AgenciaId = usuarioLogueado.AgenciaId;
            var nombAgencia = _dbAgencia.Agencias.SingleOrDefault(c => c.Id == AgenciaId)?.Nombre;

            List<Lote> lotes = ObtenerDatosReporte(fechaInicio, fechaFin, estado);

            if (formato == "PDF")
            {
                return await CrearPDF(nombAgencia, lotes, fechaInicio, fechaFin);
            }
            else if (formato == "Excel")
            {
                return await CrearExcel(nombAgencia, lotes, fechaInicio, fechaFin);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public List<Lote> ObtenerDatosReporte(DateTime fechaInicio, DateTime fechaFin, string estado)
        {
            if (estado == "Todos")
            {
                var lotes = _dbAgencia.Lote
                        .Where(l => l.FechaCreacion >= DateOnly.FromDateTime(fechaInicio) && l.FechaCreacion <= DateOnly.FromDateTime(fechaFin))
                        .OrderBy(l => l.FechaCreacion)
                        .ToList();
                return lotes;
            }
            else
            {
                EstadoLote estadoEnum = (EstadoLote)Enum.Parse(typeof(EstadoLote), estado, true);

                var lotes = _dbAgencia.Lote
                        .Where(l => l.FechaCreacion >= DateOnly.FromDateTime(fechaInicio) && l.FechaCreacion <= DateOnly.FromDateTime(fechaFin)
                        && l.Estado == estadoEnum)
                        .OrderBy(l => l.FechaCreacion)
                        .ToList();
                return lotes;
            }

        }

        public async Task<ActionResult> CrearPDF(string nombAgencia, List<Lote> lotes, DateTime fechaInicio, DateTime fechaFin)
        {
            Document doc = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

            doc.Open();

            PdfPTable table = new PdfPTable(5);

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

            Paragraph titulo2 = new Paragraph("REPORTE DE LOTES", font1);
            titulo2.Alignment = Element.ALIGN_CENTER;
            titulo2.SpacingAfter = 10f;
            doc.Add(titulo2);

            Paragraph subtitulo1 = new Paragraph();
            subtitulo1.FirstLineIndent = 204;

            Chunk leftText1 = new Chunk("Agencia : ", font2);

            Chunk rightText1 = new Chunk(nombAgencia, font3);


            subtitulo1.Add(leftText1);
            subtitulo1.Add(rightText1);
            doc.Add(subtitulo1);

            Paragraph subtitulo2 = new Paragraph();
            subtitulo2.FirstLineIndent = 214;

            Chunk leftText2 = new Chunk("Rango : ", font2);

            Chunk rightText2 = new Chunk(fechaInicio.ToString("dd/MM/yyyy") + " - " + fechaFin.ToString("dd/MM/yyyy"), font3);


            subtitulo2.Add(leftText2);
            subtitulo2.Add(rightText2);
            doc.Add(subtitulo2);


            FontFactory.RegisterDirectories();
            Font fontTitulo = new Font(FontFactory.GetFont("Arial", 12, Font.BOLD));
            fontTitulo.Color = BaseColor.BLACK;

            Font fontDatos = new Font(FontFactory.GetFont("Arial", 11, Font.NORMAL));

            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph("ID", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Número Correlativo", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Estado", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Fecha de Creación", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 40f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Nombre del Creador", fontTitulo));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.FixedHeight = 35f;
            cell.Border = PdfPCell.BOTTOM_BORDER;
            table.AddCell(cell);

            foreach (var lote in lotes)
            {
                cell = new PdfPCell(new Paragraph(lote.Id.ToString(), fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 20f;
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(lote.NumeroCorrelativo, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 20f;
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(lote.Estado.ToString(), fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 20f;
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(lote.FechaCreacion.ToString(), fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 20f;
                cell.Border = PdfPCell.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(lote.NombreCreador, fontDatos));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = PdfPCell.NO_BORDER;
                cell.FixedHeight = 20f;
                table.AddCell(cell);
            }

            doc.Add(table);

            PdfPTable tbFooter = new PdfPTable(3);
            tbFooter.TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
            tbFooter.DefaultCell.Border = 0;

            tbFooter.AddCell(new Paragraph());
            tbFooter.AddCell(new Paragraph());

            cell = new PdfPCell(new Paragraph("Página " + writer.PageNumber));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            tbFooter.AddCell(cell);

            tbFooter.WriteSelectedRows(0, -1, doc.LeftMargin, writer.PageSize.GetBottom(doc.BottomMargin) - 5, writer.DirectContent);

            doc.Close();
            writer.Close();

            return File(memoryStream.ToArray(), "application/pdf", "ReporteLotes.pdf");
        }

        public async Task<ActionResult> CrearExcel(string nombAgencia, List<Lote> lotes, DateTime fechaInicio, DateTime fechaFin)
        {
            System.Data.DataTable dTabiertas = new System.Data.DataTable();

            dTabiertas.Columns.Add("ID", typeof(string));
            dTabiertas.Columns.Add("Número Correlativo", typeof(string));
            dTabiertas.Columns.Add("Estado", typeof(string));
            dTabiertas.Columns.Add("Fecha Creación", typeof(string));
            dTabiertas.Columns.Add("Agencia", typeof(string));
            dTabiertas.Columns.Add("Creador", typeof(string));

            if (lotes.Count > 0)
            {
                lotes.ForEach(x =>
                {
                    dTabiertas.Rows.Add(x.Id, x.NumeroCorrelativo, x.Estado, x.FechaCreacion, nombAgencia, x.NombreCreador);
                });
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(dTabiertas, "Listado Lotes");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    var raro = File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{"Listado Lotes"}.xlsx");
                    return raro;
                }
            }
        }

        private async Task<bool> BorrarArchivo(string nombreDocumento)
        {
            try
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "archivos", "documentos", nombreDocumento);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);

                    var documento = _dbAgencia.Documento.FirstOrDefault(d => d.NombreDocumento == nombreDocumento);
                    if (documento != null)
                    {
                        _dbAgencia.Documento.Remove(documento);
                        await _dbAgencia.SaveChangesAsync();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
