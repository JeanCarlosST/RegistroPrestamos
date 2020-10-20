using System.Windows;
using RegistroPrestamos.Entities;
using RegistroPrestamos.BLL;
using System.Linq;
using System.Windows.Documents;
using System.Collections.Generic;
using System;

namespace RegistroPrestamos.UI.Registro
{
    public partial class rMoras : Window 
    {
        Moras mora;
        public rMoras(){
            InitializeComponent();
            mora = new Moras();
            this.DataContext = mora;

            PrestamoComboBox.ItemsSource = PrestamoBLL.GetList(p => true);
            PrestamoComboBox.SelectedValuePath = "PrestamoId";
            PrestamoComboBox.DisplayMemberPath = "Concepto";
        }

        private void Limpiar()
        {
            mora = new Moras();
            Actualizar();
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = mora;
        }

        private bool ExisteDB()
        {
            mora = MorasBLL.Buscar(Utilities.ToInt(MoraIdTextBox.Text));

            return (mora != null);
        }
        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            Moras anterior = MorasBLL.Buscar(Utilities.ToInt(MoraIdTextBox.Text));

            if(anterior != null)
            {
                mora = anterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("Mora no encontrada.", "Registro de moras");
            }
        }

        private void AgregarBoton_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidarDetalle())
                return;

            MorasDetalle detalle = new MorasDetalle(
                Utilities.ToInt(MoraIdTextBox.Text),
                Utilities.ToInt(PrestamoComboBox.SelectedValue.ToString()),
                Utilities.ToInt(ValorMoraTextBox.Text)
            );

            mora.Detalle.Add(detalle);
            mora.Total += detalle.Valor;

            Actualizar();

            PrestamoComboBox.SelectedIndex = -1;
            ValorMoraTextBox.Clear();
        }

        private void RemoverBoton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                MorasDetalle detalle = (MorasDetalle)DetalleDataGrid.SelectedItem;

                mora.Total -= detalle.Valor;
                mora.Detalle.Remove(detalle);
                
                Actualizar();
            }
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidarMora())
                return;

            bool paso = MorasBLL.Guardar(mora);

            if(paso)
            {
                Limpiar();
                MessageBox.Show("Mora guardada con éxito.", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No fue posible guardar.", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            int id = Utilities.ToInt(MoraIdTextBox.Text);

            Limpiar();

            if (MorasBLL.Eliminar(id))
                MessageBox.Show("Mora eliminada.", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se pudo eliminar la mora", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool ValidarDetalle()
        {
            decimal v;
            if(!Decimal.TryParse(ValorMoraTextBox.Text, out v))
            {
                 MessageBox.Show("Ingrese un valor que contenga un número válido", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;   
            }

            if(ValorMoraTextBox.Text.Length == 0 || Utilities.ToDecimal(ValorMoraTextBox.Text) == 0)
            {
                MessageBox.Show("Ingrese un valor que sea mayor a cero.", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(PrestamoComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un préstamo", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool ValidarMora()
        {
            DateTime fecha;

            if(!DateTime.TryParse(FechaDatePicker.Text, out fecha))
            {
                MessageBox.Show("Ingrese una fecha válida", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(mora.Detalle.Count == 0)
            {
                MessageBox.Show("Ingrese por lo menos un préstamo", "Registro de moras", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}