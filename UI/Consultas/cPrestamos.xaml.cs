
using System;
using System.Windows;
using System.Collections.Generic;
using RegistroPrestamos.BLL;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.UI.Consulta
{
    public partial class cPrestamos : Window
    {
        public cPrestamos(){
            InitializeComponent();
        }

        private void ConsultarBoton_Click(object sender, RoutedEventArgs e){
            var listado = new List<Prestamos>(); 

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PrestamoBLL.GetList(p => p.PrestamoId == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                    case 1:
                        listado = PrestamoBLL.GetList(p => p.PersonaId == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                    case 2:
                        bool fechaValidada = ValidarFecha(CriterioTextBox.Text);

                        if(!fechaValidada){
                            MessageBox.Show("Introduzca un fecha válida", "Datos incorrectos", 
                                             MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;   
                        }

                        listado = PrestamoBLL.GetList(p => p.Fecha.Equals(DateTime.Parse(CriterioTextBox.Text)));
                        break;
                        
                    case 3:                       
                         listado = PrestamoBLL.GetList(p => p.Concepto.ToLower().Contains(CriterioTextBox.Text.ToLower()));
                         break;
                }
            }
            else
            {
                listado = PrestamoBLL.GetList(c => true);
            }          

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }

        private bool ValidarFecha(string date){
            try{
                DateTime.Parse(date);
                return true;
            } catch{
                return false;
            }
        }

    }
}