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
    
    public partial class ReporteEstado
    {
        public int IdReporteEstado { get; set; }
        public int IdProyecto { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    
        public virtual Proyecto Proyecto { get; set; }
    }
}
