//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBSR2
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecursoProyecto
    {
        public int IdRecursoProyecto { get; set; }
        public int IdRecurso { get; set; }
        public int IdProyecto { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public Nullable<System.DateTime> FechaHasta { get; set; }
        public byte Horas { get; set; }
        public byte HorasFacturacion { get; set; }
        public string Observaciones { get; set; }
    
        public virtual Proyecto Proyecto { get; set; }
        public virtual Recurso Recurso { get; set; }
    }
}