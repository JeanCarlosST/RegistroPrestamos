 
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

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PersonaBLL.GetList(p => p.PersonaID == this.ToInt(CriterioTextBox.Text));
                        break;

                    // case 1:                       
                    //     listado = PersonaBLL.GetList(p => p.PersonaID.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
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

        public int ToInt(string value)
        {
            int return_ = 0;

            int.TryParse(value, out return_);

            return return_;
        }

        private void DatosDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}