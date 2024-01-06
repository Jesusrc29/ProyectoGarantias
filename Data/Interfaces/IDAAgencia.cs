using ProyectGarantia.Models;

namespace ProyectGarantia.Data.Interfaces
{
    public interface IDAAgencia
    {
        IEnumerable<Agencia> GetAgencia();

        int InsertAgencia(Agencia Agencia);
        Agencia GetIdAgencia(int id);
        Boolean UpdateAgencia(Agencia Agencia);
        Boolean DeleteAgencia(int id);
    }
}
