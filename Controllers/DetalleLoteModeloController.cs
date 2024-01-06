using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BarcodeLib;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;
using ProyectGarantia.Models.ViewModel;
using ProyectGarantia.Services;
using X.PagedList;
using static ProyectGarantia.Data.ApplicationDbContext;

namespace ProyectGarantia.Controllers
{
    public class DetalleLoteModeloController : Controller
    {
        public readonly IDALote DALote;
        public readonly IDADetalleLoteModelo DADetalleLoteModelo;
        public readonly IDADocumento DADocumento;
        public readonly IHTTPRequest _httpRequest;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly UserManager<Usuario> usuario;
        private readonly ApplicationDbContext _dbAgencia;

        public DetalleLoteModeloController(IDADetalleLoteModelo DADetalleLoteModelo, IDALote DALote,
                                            IHTTPRequest httpRequest, UserManager<Usuario> usuario,
                                            ApplicationDbContext dbAgencia, IDADocumento dADocumento,
                                            IWebHostEnvironment hostingEnvironment)

        {
            this.DADetalleLoteModelo = DADetalleLoteModelo;
            this.DALote = DALote;
            _httpRequest = httpRequest;
            this.usuario = usuario;
            _dbAgencia = dbAgencia;
            DADocumento = dADocumento;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index(int id, string buscar, int page = 1)
        {
            var pageNumber = page;

            var lista = DADetalleLoteModelo.GetDetalleLoteModeloPorLote(id);

            if (!String.IsNullOrEmpty(buscar))
            {
                lista = lista.Where(x => x.NombreCliente.Contains(buscar.ToUpper()) || x.NombreAsesor.ToUpper().Contains(buscar.ToUpper())).ToList();
            }
            var Datos = lista.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, 12);
            return View(Datos);
        }

        public IActionResult Create()
        {
            ViewBag.Lote = DALote.GetLote();

            return View();
        }

        [HttpPost]
        public IActionResult Create(DetalleLoteModelo DetalleLoteModelo)
        {
            DetalleLoteModelo.Id = 0;
            var model = DADetalleLoteModelo.InsertDetalleLoteModelo(DetalleLoteModelo);
            if (model > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Details(int id)
        {
            var modelodetalle = DADetalleLoteModelo.GetIdDetalleLoteModelo(id);
            var documentos = DADocumento.GetDocumentoPorDetalleLote(id);

            var viewModel = new DetalleLoteModeloConDocumentos
            {
                DetalleLoteModelo = modelodetalle,
                Documentos = documentos
            };

            return View(viewModel);
        }

        public IActionResult Edit(int loteId,int id)
        {
            ViewBag.Lote = DALote.GetLote();

            var model = DADetalleLoteModelo.GetIdDetalleLoteModelo(id);
            model.LoteId = loteId;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DetalleLoteModelo DetalleLoteModelo, int loteId)
        {
            bool validacion = true;
            List<Documento> docs = DADocumento.GetDocumentoPorDetalleLote(DetalleLoteModelo.Id);
            if (DetalleLoteModelo.GVDocOriginal)
            {
                var doc = docs.Where(x => x.TipoDocumento == "GVDocOriginal").FirstOrDefault();
                if(doc == null) { validacion=false; }
            }
            if (DetalleLoteModelo.GVCopiaRevision)
            {
                var doc = docs.Where(x => x.TipoDocumento == "GVCopiaRevision").FirstOrDefault();
                if (doc == null) { validacion = false; }
            }
            if (DetalleLoteModelo.GVTraspaso)
            {
                var doc = docs.Where(x => x.TipoDocumento == "GVTraspaso").FirstOrDefault();
                if (doc == null) { validacion = false; }
            }
            if (DetalleLoteModelo.GHEscritura)
            {
                var doc = docs.Where(x => x.TipoDocumento == "GHEscritura").FirstOrDefault();
                if (doc == null) { validacion = false; }
            }
            if (DetalleLoteModelo.GHAvaluo)
            {
                var doc = docs.Where(x => x.TipoDocumento == "GHAvaluo").FirstOrDefault();
                if (doc == null) { validacion = false; }
            }
            if (DetalleLoteModelo.GHRevisionIP)
            {
                var doc = docs.Where(x => x.TipoDocumento == "GHRevisionIP").FirstOrDefault();
                if (doc == null) { validacion = false; }
            }

            if(validacion)
            {
                var numPrestamo = DetalleLoteModelo.NumPrestamo;
                if (DetalleLoteModelo.Contrato)
                {
                    DetalleLoteModelo.NumContrato = "CON-" + numPrestamo;
                }

                if (DetalleLoteModelo.Pagare)
                {
                    DetalleLoteModelo.NumPagare = "PAG-" + numPrestamo;
                }

                var resultado = DADetalleLoteModelo.UpdateDetalleLote(DetalleLoteModelo);

                if (resultado)
                {
                    return Json(new { exitoso = true, redirectUrl = Url.Action("Index", "Lote") });
                }
                else
                {
                    return View(resultado);
                }
            }
            else
            {
                return new JsonResult(new { exitoso = false, mensaje = "Debe subir los archivos necesarios para las garantías" });
            }
            
        }

        public IActionResult Delete(int id)
        {
            var resultado = DADetalleLoteModelo.DeleteDetalleLoteModelo(id);
            return RedirectToAction("Index");
        }

        public IActionResult BtnAceptar(int id)
        {
            DetalleLoteModelo detalleLoteModelo = DADetalleLoteModelo.GetIdDetalleLoteModelo(id);

            detalleLoteModelo.Estado = DetalleLoteModelo.EstadoPrestamo.Aprobado;

            var resultadoDetalleLote = DADetalleLoteModelo.UpdateDetalleLoteModelo(detalleLoteModelo);

            if (resultadoDetalleLote)
            {
                return RedirectToAction("Index", new { id = detalleLoteModelo.LoteId });

            }
            else
            {
                return View(resultadoDetalleLote);
            }
        }

        public IActionResult BtnRechazar(int id)
        {
            DetalleLoteModelo detalleLoteModelo = DADetalleLoteModelo.GetIdDetalleLoteModelo(id);

            detalleLoteModelo.Estado = DetalleLoteModelo.EstadoPrestamo.Rechazado;

            var resultadoDetalleLote = DADetalleLoteModelo.UpdateDetalleLoteModelo(detalleLoteModelo);

            if (resultadoDetalleLote)
            {
                return RedirectToAction("Index", new { id = detalleLoteModelo.LoteId });

            }
            else
            {
                return View(resultadoDetalleLote);
            }
        }

        public IActionResult DescargarCodigoBarras(string correlativo)
        {
            EliminarArchivosCodigoBarras();

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "archivos", "codigoBarras", correlativo + ".png");
            System.Drawing.Image imagenCodigo;
            BarcodeLib.TYPE tipoCodigo = BarcodeLib.TYPE.CODE128;

            Barcode codBarras = new Barcode();
            codBarras.IncludeLabel = true;
            codBarras.LabelPosition = LabelPositions.BOTTOMCENTER;

            imagenCodigo = codBarras.Encode(tipoCodigo, correlativo, System.Drawing.Color.Black, System.Drawing.Color.White, 300, 100);

            imagenCodigo.Save(filePath);


            var contentDisposition = new ContentDisposition
            {
                FileName = correlativo + ".png",
                Inline = false, 
            };

            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            var imagenStream = System.IO.File.OpenRead(filePath);
            var imagen = File(imagenStream, "application/octet-stream"); 

            return imagen;
        }

        public void EliminarArchivosCodigoBarras()
        {
            var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "archivos", "codigoBarras");

            try
            {
                var files = Directory.GetFiles(folderPath);

                foreach (var file in files)
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

}
