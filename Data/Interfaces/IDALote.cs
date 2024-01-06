using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Interfaces
{
    public interface IDALote
    {
        IEnumerable<Lote> GetLote();
        IEnumerable<Lote> GetLoteByEstado(EstadoLote estado);
        int InsertLote(Lote Lote);
        Lote GetIdLote(int id);
        Boolean UpdateLote(Lote Lote);
        Boolean DeleteLote(int id);
    }
}
