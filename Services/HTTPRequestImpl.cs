using Newtonsoft.Json;
using ProyectGarantia.Models;
using System;

namespace ProyectGarantia.Services
{
    public class HTTPRequestImpl : IHTTPRequest
    {

        public async Task<string> ObtenerMensajeAsync()
        {
            HttpClient _client;
            try
            {

                _client = PreparedClient();
                string apiUrl = "https://190.99.17.152:10887/Api/Datoslotegarantias?Fchdesde=01/08/2023&Fchhasta=31/08/2023&Agencia=0125";
                //string apiUrl = "https://jsonplaceholder.typicode.com/users";
                HttpResponseMessage response = await _client.GetAsync(apiUrl);


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserializar la respuesta en un objeto
                    //DatosLoteGarantiasResponse objeto = JsonConvert.DeserializeObject<DatosLoteGarantiasResponse>(content);

                    // Aquí puedes trabajar con el objeto si es necesario

                    // Serializar el objeto de nuevo a JSON
                    //string jsonString = JsonConvert.SerializeObject(objeto, Formatting.Indented);
                    return content;

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