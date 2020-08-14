using MVVMApplication.Infra;
using MVVMApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApplication.ViewModel
{
    public class MainWindowViewModel:NotificationClass
    {
        Business _business;
        public EventHandler ShowMessageBox = delegate { };
        public List<Proveedor> ListaProveedores { get; set; }

        public MainWindowViewModel()
        {
            try
            {
                _business = new Business();
                ProductosCollection = new ObservableCollection<Producto>(_business.Get());
                ListaProveedores = new List<Proveedor>(_business.GetAllProveedores());
                SelectedProducto = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Propiedades

        private bool _editMode;
        public bool EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
                OnProprtyChanged();
            }
        }


        private bool _canAdd;
        public bool CanAdd
        {
            get
            {
                return _canAdd;
            }
            set
            {
                _canAdd = value;
                OnProprtyChanged();
            }
        }

        private bool _canDelete;
        public bool CanDelete
        {
            get
            {
                return _canDelete;
            }
            set
            {
                _canDelete = value;
                OnProprtyChanged();
            }
        }

        private ObservableCollection<Producto> productosCollection;
        public ObservableCollection<Producto> ProductosCollection
        {
            get { return productosCollection; }
            set
            {
                productosCollection = value;
                OnProprtyChanged();
            }
        }

        private Producto _selectedProducto;
        public Producto SelectedProducto
        {
            get
            {
                return _selectedProducto;
            }
            set
            {
                _selectedProducto = value;
                EditMode = _selectedProducto != null;
                CanDelete = _selectedProducto == null ? false : (_selectedProducto.Id > 0);
                CanAdd = _selectedProducto == null;
                OnProprtyChanged();
            }
        }
        #endregion

        #region Command CancelAction
        public RelayCommand CancelAction
        {
            get
            {
                return new RelayCommand(Cancel, true);
            }
        }

        private void Cancel()
        {
            try
            {
                SelectedProducto = null;
            }
            catch (Exception ex)
            {
                ShowMessageBox(this, new MessageEventArgs()
                {
                    Message = ex.Message
                });
            }

        }
        #endregion

        #region Command Add

        public RelayCommand Add
        {
            get
            {
                return new RelayCommand(AddPerson, true);
            }        
        }

        private void AddPerson()
        {
            try
            {
                SelectedProducto = new Producto();                       
            }
            catch (Exception ex)
            {
                ShowMessageBox(this, new MessageEventArgs()
                {
                    Message = ex.Message
                });
            }            
        }
        #endregion

        #region Command Save
        public RelayCommand Save
        {
            get
            {
                return new RelayCommand(SaveProducto, true);
            }
        }

        private void SaveProducto()
        {
            try
            {
                _business.Update(SelectedProducto);
                ProductosCollection = new ObservableCollection<Producto>(_business.Get());
                ShowMessageBox(this, new MessageEventArgs()
                {
                    Message = "Medicamento Agregado"
                });

                SelectedProducto = null;
            }
            catch (Exception ex)
            {
                ShowMessageBox(this, new MessageEventArgs()
                {
                    Message = ex.Message
                });
            }               
          
        }

        #endregion

        #region Command Delete
        public RelayCommand Delete
        {
            get
            {
                return new RelayCommand(DeletePerson, true);
            }
        }

        private void DeletePerson()
        {
            _business.Delete(SelectedProducto);
            SelectedProducto = null;
            ProductosCollection = new ObservableCollection<Producto>(_business.Get());
        }
        #endregion
    }
}
