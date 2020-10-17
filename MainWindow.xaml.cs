using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RegistroPrestamos.UI.Registro;
using RegistroPrestamos.UI.Consulta;

namespace RegistroPrestamos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void rPrestamosMenuItem_Click(object sender, RoutedEventArgs e){
            rPrestamos rPrestamos = new rPrestamos();
            rPrestamos.Show();
        }
        public void rPersonasMenuItem_Click(object sender, RoutedEventArgs e){
            rPersonas rPersonas = new rPersonas();
            rPersonas.Show();
        }
        public void rMorasMenuItem_Click(object sender, RoutedEventArgs e){
            rMoras rMoras = new rMoras();
            rMoras.Show();
        }
        public void cPrestamosMenuItem_Click(object sender, RoutedEventArgs e){
            cPrestamos cPrestamos = new cPrestamos();
            cPrestamos.Show();
        }
        public void cPersonasMenuItem_Click(object sender, RoutedEventArgs e){
            cPersonas cPersonas = new cPersonas();
            cPersonas.Show();
        }
        public void cMorasMenuItem_Click(object sender, RoutedEventArgs e){
            
        }
        
    }
}
