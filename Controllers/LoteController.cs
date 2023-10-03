using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data;
using ProyectGarantia.Data.Data_Acces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;
using ProyectGarantia.Services;
using X.PagedList;
using static ProyectGarantia.Data.ApplicationDbContext;

namespace ProyectGarantia.Controllers
{
    //[ApiController]
    //[Route("/[Lote]")]
    public class LoteController : Controller
    {
        public readonly IDALote DALote;
        public readonly IHTTPRequest _httpRequest;
        //crear objeto para obtener el nombre del usuario logueado
        private readonly UserManager<Usuario> usuario;
        private readonly ApplicationDbContext _dbAgencia;

        //[Route("WebAppOpe")]

        public LoteController(IDALote DALote, IHTTPRequest httpRequest, UserManager<Usuario> usuario, ApplicationDbContext dbAgencia)

        {
            this.DALote = DALote;
            _httpRequest = httpRequest;
            this.usuario = usuario;
            _dbAgencia = dbAgencia;
        }

        public async Task<IActionResult> Prueba()
        {
            var informacionServicio = await _httpRequest.ObtenerMensajeAsync();
            return Ok(informacionServicio);
        }
        public IActionResult Index(int page = 1)
        {
            //var model = new DALote();
            var pageNumber = page;
            var informacionDB = DALote.GetLote();
            var Datos = informacionDB.OrderByDescending(x => x.Id).ToList().ToPagedList(pageNumber, 8);
            return View(Datos);
        }
        //Http Request
        [HttpGet("WebAppServicio")]
        public IActionResult Servicio()
        {
            var informacionServicio = _httpRequest.ObtenerMensajeAsync();
            return View(informacionServicio);
        }

        //static async Task Main(string[] args)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        try
        //        {
        //            // Definir la URL de la API
        //            string apiUrl = "https://api.example.com/data";

        //            // Hacer la petición GET
        //            HttpResponseMessage response = await client.GetAsync(apiUrl);

        //            // Verificar si la petición fue exitosa
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Leer el contenido de la respuesta
        //                string content = await response.Content.ReadAsStringAsync();
        //                Console.WriteLine(content);
        //            }
        //            else
        //            {
        //                Console.WriteLine($"Error: {response.StatusCode}");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Excepción capturada: {ex.Message}");
        //        }
        //    }
        //}

        //public IActionResult Create()
        public async Task<IActionResult> CreateAsync()
        {
            //var Lote=new DALote();
            //ViewBag.Lote = DALote.GetLote();
            // Obtener el nombre del usuario logueado
            var usuarioLogueado = await usuario.GetUserAsync(User);
            var codAgenciaID = usuarioLogueado.AgenciaId;

            // Intentar obtener el código de agencia
            var codAgencia = _dbAgencia.Agencias.SingleOrDefault(c => c.Id == codAgenciaID)?.Codigo;

            // Variable del número correlativo
            string numeroCorrelativo = "";

            if (!string.IsNullOrEmpty(codAgencia))
            {
                var contadorAgencia = _dbAgencia.ContadorLotes.SingleOrDefault(c => c.CodAgencia == codAgencia);

                if (contadorAgencia == null)
                {
                    contadorAgencia = new ContadorLotes { CodAgencia = codAgencia, Contador = 1 };
                    _dbAgencia.ContadorLotes.Add(contadorAgencia);
                }
                else
                {
                    if (contadorAgencia.Contador > 99998)
                    {
                        // Cambiar el formato a D6 y reiniciar el contador a 1
                        contadorAgencia.Contador = 1;
                        numeroCorrelativo = $"{codAgencia:D4}-{contadorAgencia.Contador:D6}";
                    }
                    else
                    {
                        contadorAgencia.Contador++;
                        numeroCorrelativo = $"{codAgencia:D4}-{contadorAgencia.Contador:D5}";
                    }
                }
                //Actualizar el contador en la base de datos
                await _dbAgencia.SaveChangesAsync();
            }

            // Asignar el número correlativo a ViewBag
            ViewBag.NumeroCorrelativo = numeroCorrelativo;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Lote Lote)
        //public async Task<IActionResult> CreateAsync(Lote Lote)
        {
            Lote.Id = 0;
            Lote.FechaCreacion = DateOnly.FromDateTime(DateTime.Now.Date);
            var model = DALote.InsertLote(Lote);
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
            //var detLote = new DALote();
            var modelodetalle = DALote.GetIdLote(id);
            return View(modelodetalle);
        }

        public IActionResult Edit(int id)
        {
            //var Lote = new DALote();
            //ViewBag.Lote = DALote.GetLote();

            //var Lote = new DALote();
            var model = DALote.GetIdLote(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Lote Lote)
        {
            //Lote.FechaModificacion = DateTime.Now;
            //var model = new DALote();
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

        public IActionResult Delete(int id)
        {
            //var LoteDelete = new DALote();
            var resultado = DALote.DeleteLote(id);
            return RedirectToAction("Index");
        }
    }
}
