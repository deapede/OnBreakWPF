using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    class Valorizacion
    {
        public static double UF = 27953.42;

        public int CalcularTotal(int precioBase, int asistentes, int personalAdicional)
        {
            double total = precioBase * UF;

            if (asistentes >= 1 && asistentes <= 20)
            {
                total = total + (UF * 3);
            }
            else if (asistentes >= 21 && asistentes <= 50)
            {
                total = total + (UF * 5);
            }
            else if (asistentes > 50)
            {
                total = total + (Math.Ceiling((double)(asistentes / 20)) * (2 * UF));
            }

            if (personalAdicional == 2)
            {
                total += (UF * 2);
            }
            else if (personalAdicional == 3)
            {
                total += (UF * 3);
            }
            else if (personalAdicional == 4)
            {
                total += (UF * 3.5);
            }
            else if (personalAdicional > 4)
            {
                total += (UF * 3.5) + ((personalAdicional - 4) * (UF * 0.5));
            }
            return (int)Math.Round(total);
        }
    }
}
