using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Interfaces
{
    public interface IDADocumento
    {
        IEnumerable<Documento> GetDocumento();

        int InsertDocumento(Documento Documento);
        public List<Documento> GetDocumentoPorDetalleLote(int id);
        Documento GetIdDocumento(int id);
        Boolean UpdateDocumento(Documento Documento);
        Boolean DeleteDocumento(int id);
    }
}
