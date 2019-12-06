using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class ListadoItems
    {
        public enum tipoactividad
        {
            Agropecuaria, Mineria, Manufactura, Comercio, Hoteleria, Alimentos, Transporte, Servicios
        }

        public enum tipoempresa
        {
            SPA, EIRL, LIMITADA, SA
        }

        public enum  tipoevento
        {
            CoffeeBreak, Cocktail, Cenas           
        }

        public enum enumCoffeeBreak
        {
            CB001, CB002, CB003
        }
        public enum enumCocktail
        {
            CO001, CO002
        }
        public enum enumCenas
        {
            CE001, CE002
        }


    }
}
