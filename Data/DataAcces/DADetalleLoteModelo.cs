using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data.Data_Acces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;

namespace ProyectGarantia.Data.DataAcces
{
    public class DADetalleLoteModelo : IDADetalleLoteModelo
    {
        private readonly ApplicationDbContext dbDetalleLoteModelo;

        public DADetalleLoteModelo(ApplicationDbContext dbContext)
        {
            dbDetalleLoteModelo = dbContext;
        }

        public IEnumerable<DetalleLoteModelo> GetDetalleLoteModelo()
        {
            return dbDetalleLoteModelo.DetalleLoteModelo.Include(item => item.Lote).ToList();
        }

        public IEnumerable<DetalleLoteModelo> GetDetalleLoteModeloPorLote(int loteId)
        {
            return dbDetalleLoteModelo.DetalleLoteModelo.Where(x => x.LoteId == loteId).Include(item => item.Lote).ToList();
        }

        public int InsertDetalleLoteModelo(DetalleLoteModelo DetalleLoteModelo)
        {
            dbDetalleLoteModelo.Add(DetalleLoteModelo);
            dbDetalleLoteModelo.SaveChanges();
            return DetalleLoteModelo.Id;
        }

        public int InsertListDetalleLoteModelo(List<DetalleLoteModelo> DetalleLoteModelos)
        {
            foreach (var item in DetalleLoteModelos)
            {
                dbDetalleLoteModelo.Add(item);
            }
            dbDetalleLoteModelo.SaveChanges();
            return DetalleLoteModelos.FirstOrDefault().LoteId;
        }
        public Boolean UpdateDetalleLote(DetalleLoteModelo DetalleLoteModelo)
        {
            var existingDetalleLote = dbDetalleLoteModelo.DetalleLoteModelo.Find(DetalleLoteModelo.Id);

            if (existingDetalleLote != null)
            {
                existingDetalleLote.GVCopiaRevision = DetalleLoteModelo.GVCopiaRevision;
                existingDetalleLote.GVDocOriginal = DetalleLoteModelo.GVDocOriginal;
                existingDetalleLote.GVTraspaso = DetalleLoteModelo.GVTraspaso;
                existingDetalleLote.GHAvaluo = DetalleLoteModelo.GHAvaluo;
                existingDetalleLote.GHEscritura = DetalleLoteModelo.GHEscritura;
                existingDetalleLote.GHRevisionIP = DetalleLoteModelo.GHRevisionIP;
                existingDetalleLote.Contrato = DetalleLoteModelo.Contrato;
                existingDetalleLote.Pagare = DetalleLoteModelo.Pagare;
                existingDetalleLote.NumContrato = DetalleLoteModelo.NumContrato;
                existingDetalleLote.NumPagare = DetalleLoteModelo.NumPagare;
                existingDetalleLote.Estado = DetalleLoteModelo.Estado;
                dbDetalleLoteModelo.Entry(existingDetalleLote).State = EntityState.Modified;

                return dbDetalleLoteModelo.SaveChanges() != 0;
            }
            return false;
        }
        public DetalleLoteModelo GetIdDetalleLoteModelo(int id)
        {
            return dbDetalleLoteModelo.DetalleLoteModelo.Where(item => item.Id == id).Include(x => x.Garantias).FirstOrDefault();
        }

        public Boolean UpdateDetalleLoteModelo(DetalleLoteModelo DetalleLoteModelo)
        {
            dbDetalleLoteModelo.DetalleLoteModelo.Attach(DetalleLoteModelo);
            dbDetalleLoteModelo.Entry(DetalleLoteModelo).State = EntityState.Modified;
            return dbDetalleLoteModelo.SaveChanges() != 0;
        }

        public Boolean DeleteDetalleLoteModelo(int id)
        {
            var entity = new DetalleLoteModelo() { Id = id };
            dbDetalleLoteModelo.DetalleLoteModelo.Attach(entity);
            dbDetalleLoteModelo.DetalleLoteModelo.Remove(entity);
            return dbDetalleLoteModelo.SaveChanges() != 0;
        }
    }
}
