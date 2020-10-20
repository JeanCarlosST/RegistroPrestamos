using System.Windows;
using RegistroPrestamos.Entities;
using RegistroPrestamos.BLL;
using System;

namespace RegistroPrestamos.UI.Registro
{
    public partial class rPrestamos : Window {
        Prestamos prestamo;

        public rPrestamos(){
            InitializeComponent();
            prestamo = new Prestamos();
            prestamo.Fecha = DateTime.Now;
            this.DataContext = prestamo;

            PersonaComboBox.ItemsSource = PersonaBLL.GetList(p => true);
            PersonaComboBox.SelectedValuePath = "PersonaId";
            PersonaComboBox.DisplayMemberPath = "Nombres";
        }

        public void BuscarBoton_Click(object sender, RoutedEventArgs e){
            var prestamo = PrestamoBLL.Buscar(Utilities.ToInt(PrestamoIdTextBox.Text));

            if(prestamo != null)
                this.prestamo = prestamo;
            else{
                this.prestamo = new Prestamos();
                MessageBox.Show("No se encontró ningún registro", "Sin coincidencias", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.DataContext = this.prestamo;
        }

        private void Limpiar(){
            this.prestamo = new Prestamos();
            this.DataContext = this.prestamo;
        }

        private bool Validar(){
                
            DateTime date;
            if(FechaDatePicker.Text.Length == 0){
                MessageBox.Show("Introduzca una fecha", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;   

            } else if(!DateTime.TryParse(FechaDatePicker.Text, out date)){
                MessageBox.Show("Seleccione a una fecha válida", "Datos incorrectos", 
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;  
            

            } else if(PersonaComboBox.SelectedIndex == -1){
                MessageBox.Show("Seleccione a una persona", "Datos incorrectos", 
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
            if(PrestamoBLL.Eliminar(Utilities.ToInt(PrestamoIdTextBox.Text))){
                Limpiar();
                MessageBox.Show("Registro borrado", "Borrado exitoso", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            
            } else 
                MessageBox.Show("Error", "Hubo un error al borrar", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public String CleanSymbols(string text)
        {
            text = text.Replace(" ", "");
            text = text.Replace("-", "");
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            if(text.StartsWith("+1"))
                text = text.Replace("+1", "");

            return text;
        }
    }
}