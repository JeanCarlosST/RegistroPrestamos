
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
                        listado = PrestamoBLL.GetList(p => p.PrestamoID == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                    case 1:
                        listado = PrestamoBLL.GetList(p => p.PrestamoID == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                    // Al buscar en cualquier tabla con string, da error
                    // case 2:                       
                    //     listado = PrestamoBLL.GetList(p => p.Concepto.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
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

    }
}