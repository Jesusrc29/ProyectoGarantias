using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using ProyectGarantia.Data;
using ProyectGarantia.Models;
using System;
using static ProyectGarantia.Data.ApplicationDbContext;

namespace ProyectGarantia.Services
{
    public class HTTPRequestImpl : IHTTPRequest
    {
        public async Task<List<DatosLoteGarantiasResponse>> ObtenerMensajeAsync(String CodAgencia, DateTime FechaDesde, DateTime FechaHasta)

        {

            HttpClient _client;
            try
            {
                
                _client = PreparedClient();
                string apiUrl = $"https://190.4.17.10:1089/Api/Datoslotegarantias?Fchdesde={FechaDesde:dd/MM/yyyy}&Fchhasta={FechaHasta:dd/MM/yyyy}&Agencia={CodAgencia}";
                
                HttpResponseMessage response = await _client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<DatosLoteGarantiasResponse> prestamos = JsonConvert.DeserializeObject<List<DatosLoteGarantiasResponse>>(json);
                    return prestamos;

                }
                else
                {
                    throw new HttpRequestException("No se obtuvo correctamente los datos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en petición:"+ ex);
            }


        }
        public static HttpClient PreparedClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(handler);
            return client;
        }

    }
}