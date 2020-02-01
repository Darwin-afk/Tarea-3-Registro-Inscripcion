using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Registro_Inscripcion.Entidades
{
    public class Inscripciones
    {
        [Key]
        public int InscripcionId { get; set; }
        public DateTime Fecha { get; set; }
        public int PersonaId { get; set; }
        public string Comentarios { get; set; }
        public int Balance { get; set; }
        public int Monto { get; set; }

        public Inscripciones()
        {
            InscripcionId = 0;
            Fecha = DateTime.Now;
            PersonaId = 0;
            Comentarios = string.Empty;
            Balance = 0;
            Monto = 0;
        }
    }
}
