using iTextSharp.text.pdf.qrcode;
using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data.Data_Acces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;

namespace ProyectGarantia.Data.DataAcces
{
    public class DAGarantia: IDAGarantia
    {
        private readonly ApplicationDbContext dbGarantia;

        public DAGarantia(ApplicationDbContext dbContext)
        {
            dbGarantia = dbContext;
        }

        public Garantia GetIdGarantia(int id)
        {
            return dbGarantia.Garantia.Where(item => item.Id == id).FirstOrDefault();
        }

        public int InsertListGarantias(List<Garantia> garantias)
        {
            foreach (var garantia in garantias)
            {
                dbGarantia.Add(garantia);
            }

            dbGarantia.SaveChanges();

            return garantias.FirstOrDefault()?.PrestamoId ?? 0;
        }

        public int InsertGarantias(Garantia garantia)
        {
            dbGarantia.Add(garantia);
            
            dbGarantia.SaveChanges();

            return garantia.PrestamoId;
        }

        public Boolean CambiarEstadoGarantia(int idGarantia, int estado)
        {
            Garantia garantia = dbGarantia.Garantia.Where(x => x.Id == idGarantia).FirstOrDefault();
            switch (estado)
            {
                case 0:
                    garantia.Estado = EstadoGarantia.Activa;
                    break;
				case 1:
					garantia.Estado = EstadoGarantia.Inactiva;
					break;
				case 2:
					garantia.Estado = EstadoGarantia.EnDemanda;
					break;
				case 3:
					garantia.Estado = EstadoGarantia.SalidaPrestamoDocumentacion;
					break;
				case 4:
					garantia.Estado = EstadoGarantia.SalidaCambio;
					break;
				case 5:
					garantia.Estado = EstadoGarantia.IngresoCambio;
                    break;
				case 6:
					garantia.Estado = EstadoGarantia.BajaCancelacion;
					break;

			}
			dbGarantia.Garantia.Attach(garantia);
			dbGarantia.Entry(garantia).State = EntityState.Modified;
			return dbGarantia.SaveChanges() != 0;
		}

    }
}
