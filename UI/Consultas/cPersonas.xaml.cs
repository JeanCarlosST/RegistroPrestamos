 
using System;
using System.Windows;
using System.Collections.Generic;
using RegistroPrestamos.BLL;
using RegistroPrestamos.Entities;

namespace RegistroPrestamos.UI.Consulta
{
    public partial class cPersonas : Window
    {
        public cPersonas(){
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e){
            var listado = new List<Persona>();

            string criterio = CriterioTextBox.Text.Trim();
            if (criterio.Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PersonaBLL.GetList(p => p.PersonaID == Utilities.ToInt(CriterioTextBox.Text));
                        break;
                    
                    // Al buscar en cualquier tabla con string, da error
                    // case 1:                       
                    //     listado = PersonaBLL.GetList(p => p.Nombres.Contains(criterio, StringComparison.OrdinalIgnoreCase));
                    //     break;
                }
            }
            else
            {
                listado = PersonaBLL.GetList(c => true);
            }          

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}