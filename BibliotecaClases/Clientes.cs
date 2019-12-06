using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Clientes
    {
        private string rut;
        private string razonsocial;
        private string nombrecontacto;
        private string emailcontacto;
        private string direccion;
        private string telefono;
        private string tipoactividad;
        private string tipoempresa;

        public Clientes(string rut, string razonsocial, string nombrecontacto, string emailcontacto, string direccion, string telefono, string tipoactividad, string tipoempresa)
        {
            this.rut = rut;
            this.razonsocial = razonsocial;
            this.nombrecontacto = nombrecontacto;
            this.emailcontacto = emailcontacto;
            this.direccion = direccion;
            this.telefono = telefono;
            this.tipoactividad = tipoactividad;
            this.tipoempresa = tipoempresa;
        }

        public Clientes()
        {

        }

        public string Rut { get => rut; set => rut = value; }
        public string Razonsocial { get => razonsocial; set => razonsocial = value; }
        public string Nombrecontacto { get => nombrecontacto; set => nombrecontacto = value; }
        public string Emailcontacto { get => emailcontacto; set => emailcontacto = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Tipoactividad { get => tipoactividad; set => tipoactividad = value; }
        public string Tipoempresa { get => tipoempresa; set => tipoempresa = value; }

        
    }
}
