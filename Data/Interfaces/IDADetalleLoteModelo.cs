using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Interfaces
{
    public interface IDADetalleLoteModelo
    {
        IEnumerable<DetalleLoteModelo> GetDetalleLoteModelo();
        public IEnumerable<DetalleLoteModelo> GetDetalleLoteModeloPorLote(int loteId);
        int InsertDetalleLoteModelo(DetalleLoteModelo DetalleLoteModelo);
        int InsertListDetalleLoteModelo(List<DetalleLoteModelo> DetalleLoteModelos);
        DetalleLoteModelo GetIdDetalleLoteModelo(int id);
        Boolean UpdateDetalleLote(DetalleLoteModelo DetalleLoteModelo);
        Boolean UpdateDetalleLoteModelo(DetalleLoteModelo DetalleLoteModelo);
        Boolean DeleteDetalleLoteModelo(int id);
    }
}
