using System.Windows;
using RegistroPrestamos.Entities;
using RegistroPrestamos.BLL;

namespace RegistroPrestamos.UI.Registro
{
    public partial class rPrestamos : Window {
        Prestamo prestamo;

        public rPrestamos(){
            InitializeComponent();
            prestamo = new Prestamo();
            this.DataContext = prestamo;
        }

        public void BuscarBoton_Click(object sender, RoutedEventArgs e){
            var prestamo = PrestamoBLL.Buscar(this.ToInt(PrestamoIDTextBox.Text));

            if(prestamo != null)
                this.prestamo = prestamo;
            else{
                this.prestamo = new Prestamo();
                MessageBox.Show("No se encontró ningún registro", "Sin coincidencias", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.DataContext = this.prestamo;
        }

        private void Limpiar(){
            this.prestamo = new Prestamo();
            this.DataContext = this.prestamo;
        }

        private bool Validar(){
                
            if(FechaDatePicker.Text.Length == 0){
                MessageBox.Show("Introduzca una fecha válida", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;   
            
            } else if(PersonaTextBox.Text.Length == 0){
                MessageBox.Show("Introduzca el ID de una persona válida", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;                
            
            } else if(ConceptoTextBox.Text.Length == 0){
                MessageBox.Show("Introduzca un concepto", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;                
            
            } else if(MontoTextBox.Text.Length == 0){
                MessageBox.Show("Introduzca un monto", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;                
            
            } else if(!PersonaBLL.Existe(ToInt(PersonaTextBox.Text))){
                MessageBox.Show("El ID de la persona introducida no existe", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;

            } else
                return true;
        }
        public void NuevoBoton_Click(object sender, RoutedEventArgs e){
            Limpiar();
        }
        public void GuardarBoton_Click(object sender, RoutedEventArgs e){
            if(!Validar())
                return;

            var found = PrestamoBLL.Guardar(prestamo);

            if(found){
                Limpiar();
                MessageBox.Show("Registro guardado", "Guardado exitoso", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            
            } else 
                MessageBox.Show("Error", "Hubo un error al guardar", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void EliminarBoton_Click(object sender, RoutedEventArgs e){
            if(PrestamoBLL.Eliminar(this.ToInt(PrestamoIDTextBox.Text))){
                Limpiar();
                MessageBox.Show("Registro borrado", "Borrado exitoso", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            
            } else 
                MessageBox.Show("Error", "Hubo un error al borrar", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public int ToInt(string value)
        {
            int return_ = 0;

            int.TryParse(value, out return_);

            return return_;
        }
    }
}