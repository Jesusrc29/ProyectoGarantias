using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;

namespace ProyectGarantia.Data.DataAcces
{
    public class DADocumento : IDADocumento
    {
        private readonly ApplicationDbContext dbDocumento;

        public DADocumento(ApplicationDbContext dbContext)
        {
            dbDocumento = dbContext;
        }
        public IEnumerable<Documento> GetDocumento()
        {
            var listadoDocumento = new List<Documento>();

            {
                listadoDocumento = dbDocumento.Documento.ToList();
            }
            return listadoDocumento;
        }
        public int InsertDocumento(Documento Documento)
        {
            dbDocumento.Add(Documento);
            dbDocumento.SaveChanges();
            return Documento.DocumentoId;
        }

        public List<Documento> GetDocumentoPorDetalleLote(int id)
        {
            return dbDocumento.Documento.Where(item => item.DetalleLoteModeloId == id).ToList();
        }

        public Documento GetIdDocumento(int id)
        {
            return dbDocumento.Documento.Where(item => item.DocumentoId == id).FirstOrDefault();
        }

        public Boolean UpdateDocumento(Documento Documento)
        {
            dbDocumento.Documento.Attach(Documento);
            dbDocumento.Entry(Documento).State = EntityState.Modified;
            return dbDocumento.SaveChanges() != 0;
        }

        public Boolean DeleteDocumento(int id)
        {
            var entity = new Documento() { DocumentoId = id };
            dbDocumento.Documento.Attach(entity);
            dbDocumento.Documento.Remove(entity);
            return dbDocumento.SaveChanges() != 0;
        }
    }
}
