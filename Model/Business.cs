using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApplication.Model
{
    public class Business
    {
        FarmaDB _dbContext = null;
        public Business()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory",
             Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            _dbContext = new FarmaDB();
        }

        internal IEnumerable<Proveedor> GetAllProveedores()
        {
            return _dbContext.Proveedor.ToList();
        }

        internal IEnumerable<Producto> Get()
        {
            return _dbContext.Producto.Include("Proveedor").ToList();
        }

        internal void Delete(Producto producto)
        {
            try
            {
                _dbContext.Producto.Remove(producto);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        internal void Update(Producto updatedProducto)
        {
            CheckValidations(updatedProducto);
            if (updatedProducto.Id > 0)
            {
                Producto selectedProducto = _dbContext.Producto.First(p => p.Id == updatedProducto.Id);
                selectedProducto.Nombre = updatedProducto.Nombre;
                selectedProducto.Descripcion = updatedProducto.Descripcion;
                selectedProducto.Precio_Venta = updatedProducto.Precio_Venta;
                selectedProducto.ProveedorId = updatedProducto.ProveedorId;
            }
            else
            {
                _dbContext.Producto.Add(updatedProducto);
            }

            _dbContext.SaveChanges();
        }

        private void CheckValidations(Producto producto)
        {
            if(producto == null)
            {
                
                throw new Exception("Completar Formulario");
            }

            if (string.IsNullOrEmpty(producto.Nombre))
            {
                
                throw new Exception("Ingresar Medicamento");
            }
            else if (producto.Precio_Venta<=0)
            {
                
                throw new Exception("Verificar Precio");
            }
            else if ((int)producto.ProveedorId == 0)
            {
                
                throw new Exception("Seleccione Proveedor");
            }
        }
    }
}
