using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class ContratosCollection : List<Contratos>
    {
        public Contratos BuscarPorNumeroContrato(string numerocontrato)
        {
            try
            {
                Contratos cli = this.First(p => p.Numerocontrato == numerocontrato);
                return cli;
            }
            catch
            {
                return null;
            }

        }
    }
}
