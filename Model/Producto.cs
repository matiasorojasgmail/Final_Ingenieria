using MVVMApplication.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApplication.Model
{
    public class Producto:NotificationClass
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio_Venta { get; set; }

        public int proveedorId;
        public int ProveedorId {
            get {
                return proveedorId;
            }
            set {
                proveedorId = value;
                OnProprtyChanged();
            }
        }
        [ForeignKey("ProveedorId")]
        public virtual Proveedor Proveedor { get; set; }
    }
}
