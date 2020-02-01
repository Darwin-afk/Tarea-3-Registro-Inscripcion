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
using Registro_Inscripcion.UI.ConsultasInscripciones;
using Registro_Inscripcion.UI.ConsultasPersonas;
using Registro_Inscripcion.UI.InscripcionesUi;
using Registro_Inscripcion.UI.Pagos;

namespace Registro_Inscripcion
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

        private void InscribirButton_Click(object sender, RoutedEventArgs e)
        {
            iPersona i = new iPersona();
            i.Show();
        }

        private void PagarButton_Click(object sender, RoutedEventArgs e)
        {
            pPersona p = new pPersona();
            p.Show();
        }

        private void ConsultarPersonasButton_Click(object sender, RoutedEventArgs e)
        {
            cPersona c = new cPersona();
            c.Show();
        }

        private void ConsultarInscripcionesButton_Click(object sender, RoutedEventArgs e)
        {
            cInscripciones ci = new cInscripciones();
            ci.Show();
        }
    }
}
