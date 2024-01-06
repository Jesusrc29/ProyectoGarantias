using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Interfaces
{
    public interface IDAGarantia
    {
        public Garantia GetIdGarantia(int id);
        int InsertListGarantias(List<Garantia> garantias);
        public int InsertGarantias(Garantia garantia);
        public Boolean CambiarEstadoGarantia(int idGarantia, int estado);
    }
}
