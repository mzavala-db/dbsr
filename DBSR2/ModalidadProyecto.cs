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
    
    public partial class ModalidadProyecto
    {
        public ModalidadProyecto()
        {
            this.Proyecto = new HashSet<Proyecto>();
        }
    
        public byte IdModalidadProyecto { get; set; }
        public string Nombre { get; set; }
    
        public virtual ICollection<Proyecto> Proyecto { get; set; }
    }
}
