using System.Windows;
using RegistroPrestamos.Entities;
using RegistroPrestamos.BLL;

namespace RegistroPrestamos.UI.Registro
{
    public partial class rPersonas : Window {
        Persona persona;

        public rPersonas(){
            InitializeComponent();
            persona = new Persona();
        }

        public void BuscarBoton_Click(object sender, RoutedEventArgs e){
            var persona = PersonaBLL.Buscar(this.ToInt(PersonaIDTextBox.Text));

            if(persona != null)
                this.persona = persona;
            else{
                this.persona = new Persona();
                MessageBox.Show("No se encontró ningún registro", "Sin coincidencias", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.DataContext = this.persona;
        }

        private void Limpiar(){
            this.persona = new Persona();
            this.DataContext = this.persona;
        }

        private bool Validar(){
                
            if(NombresTextBox.Text == "" /*|| NombresTextBox.Text.All(char.IsNumber)*/){
                MessageBox.Show("Introduzca un nombre válido", "Datos incorrectos", 
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

            var found = PersonaBLL.Guardar(persona);

            if(found){
                MessageBox.Show("Registro guardado", "Guardado exitoso", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            
            } else 
                MessageBox.Show("Error", "Hubo un error al guardar", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void EliminarBoton_Click(object sender, RoutedEventArgs e){
            if(PersonaBLL.Eliminar(this.ToInt(PersonaIDTextBox.Text))){
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