using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data.DataAcces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Data_Acces
{
    public class DALote : IDALote
    {
        private readonly ApplicationDbContext dbLote;

        public DALote(ApplicationDbContext dbContext)
        {
            dbLote = dbContext;
        }
        public IEnumerable<Lote> GetLote()
        {
            var listadoLote = new List<Lote>();
            
            {
                listadoLote = dbLote.Lote.ToList();
            }
            return listadoLote;
        }

        public IEnumerable<Lote> GetLoteByEstado(EstadoLote estado)
        {
            return dbLote.Lote.Where(lote => lote.Estado == estado).ToList();
        }

        public int InsertLote(Lote Lote)
        {
            dbLote.Add(Lote);
            dbLote.SaveChanges();
            return Lote.Id;
        }

        public Lote GetIdLote(int id)
        {
            return dbLote.Lote.Where(item => item.Id == id).FirstOrDefault();
        }

        public Boolean UpdateLote(Lote Lote)
        {
            dbLote.Lote.Attach(Lote);
            dbLote.Entry(Lote).State = EntityState.Modified;
            return dbLote.SaveChanges() != 0;
        }

        public Boolean DeleteLote(int id)
        {
            var entity = new Lote() { Id = id };
            dbLote.Lote.Attach(entity);
            dbLote.Lote.Remove(entity);
            return dbLote.SaveChanges() != 0;
        }
    }
}
