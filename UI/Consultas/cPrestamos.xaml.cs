
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

        private void ConsultarButton_Click(object sender, RoutedEventArgs e){
            var listado = new List<Prestamo>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PrestamoBLL.GetList(p => p.PrestamoID == this.ToInt(CriterioTextBox.Text));
                        break;

                    // case 1:                       
                    //     listado = PrestamoBLL.GetList(p => p.PersonaID.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                    //     break;
                }
            }
            else
            {
                listado = PrestamoBLL.GetList(c => true);
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
    }
}