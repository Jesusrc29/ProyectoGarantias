using ProyectGarantia.Models;

namespace ProyectGarantia.Services
{
    public interface IHTTPRequest
    {
        Task<List<DatosLoteGarantiasResponse>> ObtenerMensajeAsync(String CodAgencia, DateTime FechaDesde, DateTime FechaHasta);
    }
}
