using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Contratos
    {
        private string numerocontrato;
        private string creacion;
        private string rutclientecontrato;
        private string termino;
        private string fechahorainicio;
        private string fechahoratermino;
        private string modalidad;
        private int tipoevento;
        private string observaciones;
        private int asistentes;
        private int personaladicional;
        private float valortotalcontrato;

        public Contratos(string numerocontrato, string creacion, string termino, string rutclientecontrato, string fechahorainicio, string fechahoratermino, string modalidad, int tipoevento, string observaciones, int asistentes, 
                            int personaladicional, float valortotalcontrato)
        {
            this.numerocontrato = numerocontrato;
            this.creacion = creacion;
            this.termino = termino;
            this.rutclientecontrato = rutclientecontrato;
            this.fechahorainicio = fechahorainicio;
            this.fechahoratermino = fechahoratermino;
            this.modalidad = modalidad;
            this.tipoevento = tipoevento;
            this.observaciones = observaciones;
            this.asistentes = asistentes;
            this.personaladicional = personaladicional;
            this.valortotalcontrato = valortotalcontrato;
        }

        public Contratos(){

        }


        public string Numerocontrato { get => numerocontrato; set => numerocontrato = value; }
        public string Rutclientecontrato { get => rutclientecontrato; set => rutclientecontrato = value; }
        public string Creacion { get => creacion; set => creacion = value; }
        public string Termino { get => termino; set => termino = value; }
        public string Fechahorainicio { get => fechahorainicio; set => fechahorainicio = value; }
        public string Fechahoratermino { get => fechahoratermino; set => fechahoratermino = value; }
        public int Tipoevento { get => tipoevento; set => tipoevento = value; }
        public string Modalidad { get => modalidad; set => modalidad = value; }
        public int Asistentes { get=> asistentes; set=> asistentes = value; }
        public int Personaladicional { get => personaladicional;set=> personaladicional = value; }
        public float Valortotalcontrato { get => valortotalcontrato; set => valortotalcontrato = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
    }
}
