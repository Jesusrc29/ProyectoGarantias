using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data;
using ProyectGarantia.Data.DataAcces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;
using System.Collections.Generic;
using System.Net.Mime;

namespace ProyectGarantia.Controllers
{
    public class DocumentoController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public readonly IDADocumento DADocumento;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentoController(ApplicationDbContext dbContext, IWebHostEnvironment hostingEnvironment, IDADocumento dADocumento)
        {
            _dbContext = dbContext;
            DADocumento = dADocumento;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int detalleLoteModeloId)
        {
            List<Documento> lista =  DADocumento.GetDocumentoPorDetalleLote(detalleLoteModeloId);
            if(lista != null)
            {
                return Json(lista);
            }
            else
            {
                return Json(null);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> SubirArchivos(int detalleLoteModeloId, string tipoDocumento, IFormFileCollection files)
        {
            try
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var nombreOriginal = file.FileName;
                        var nombreDocumento = GenerarNombreAletatorio(file.FileName);

                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "archivos", "documentos", nombreDocumento);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var documento = new Documento
                        {
                            DetalleLoteModeloId = detalleLoteModeloId,
                            NombreDocumento = nombreDocumento,
                            NombreOriginal = nombreOriginal,
                            TipoDocumento = tipoDocumento
                        };

                        _dbContext.Documento.Add(documento);
                    }
                }

                await _dbContext.SaveChangesAsync();

                return Json(new { exitoso = true });
            }
            catch (Exception ex)
            {
                return Json(new { exitoso = false, mensaje = ex.Message });
            }
        }

        public IActionResult DescargarDocumento(int documentoId)
        {
            var documento = DADocumento.GetIdDocumento(documentoId); 

            if (documento != null)
            {
                var filePath = Path.Combine(
                    _hostingEnvironment.WebRootPath,
                    "archivos",
                    "documentos",
                    documento.NombreDocumento
                );

                if (System.IO.File.Exists(filePath))
                {
                    var contentDisposition = new ContentDisposition
                    {
                        FileName = documento.NombreOriginal,
                        Inline = false, 
                    };

                    Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

                    return File(System.IO.File.OpenRead(filePath), "application/octet-stream"); 
                }
            }

            return NotFound();
        }


        private string GenerarNombreAletatorio(string filename)
        {
            string extn = Path.GetExtension(filename);
            Guid g = Guid.NewGuid();
            if (extn != ".exe")
            {
                string nombre = $"{g}{extn}";
                return nombre;

            }
            else
            {
                return $"No está permitido este tipo de archivos - Extensión : {extn}, no se ha subido ningún archivo";
            }
        }
    }
}
