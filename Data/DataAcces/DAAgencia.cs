using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data.DataAcces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Data_Acces
{
    public class DAAgencia : IDAAgencia
    {
        private readonly ApplicationDbContext dbAgencia;

        public DAAgencia(ApplicationDbContext dbContext)
        {
            dbAgencia = dbContext;
        }
        public IEnumerable<Agencia> GetAgencia()
        {
            var listadoAgencia = new List<Agencia>();

            {
                listadoAgencia = dbAgencia.Agencia.ToList();
            }
            return listadoAgencia;
        }
        public int InsertAgencia(Agencia Agencia)
        {
            dbAgencia.Add(Agencia);
            dbAgencia.SaveChanges();
            return Agencia.Id;
        }

        public Agencia GetIdAgencia(int id)
        {
            return dbAgencia.Agencia.Where(item => item.Id == id).FirstOrDefault();
        }

        public Boolean UpdateAgencia(Agencia Agencia)
        {
            dbAgencia.Agencia.Attach(Agencia);
            dbAgencia.Entry(Agencia).State = EntityState.Modified;
            return dbAgencia.SaveChanges() != 0;
        }

        public Boolean DeleteAgencia(int id)
        {
            var entity = new Agencia() { Id = id };
            dbAgencia.Agencia.Attach(entity);
            dbAgencia.Agencia.Remove(entity);
            return dbAgencia.SaveChanges() != 0;
        }
    }
}
