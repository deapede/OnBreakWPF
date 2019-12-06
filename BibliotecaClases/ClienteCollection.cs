using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{

    public class ClienteCollection : List<Clientes>
    {
        public Clientes BuscarPorRut(string rut)
        {
            try
            {
                Clientes cli = this.First(p => p.Rut == rut);
                return cli;
            }
            catch
            {
                return null;
            }

        }
    }
}
