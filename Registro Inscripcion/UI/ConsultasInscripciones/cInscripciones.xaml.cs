using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Registro_Inscripcion.Entidades;
using Registro_Inscripcion.BLL;
using System.Linq;

namespace Registro_Inscripcion.UI.ConsultasInscripciones
{
    /// <summary>
    /// Interaction logic for cInscripciones.xaml
    /// </summary>
    public partial class cInscripciones : Window
    {
        public cInscripciones()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Inscripciones>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltrarComboBox.SelectedIndex)
                {
                    case 0:
                        listado = InscripcionesBLL.GetList(p => true);
                        break;
                    case 1:
                        int id = Convert.ToInt32(CriterioTextBox.Text);
                        listado = InscripcionesBLL.GetList(p => p.InscripcionId == id);
                        break;
                    case 2:
                        int id2 = Convert.ToInt32(CriterioTextBox.Text);
                        listado = InscripcionesBLL.GetList(p => p.PersonaId == id2);
                        break;
                }
                if (DesdeDatePicker.SelectedDate != null && HastaDatePicker.SelectedDate != null)
                    listado = listado.Where(c => c.Fecha.Date >= DesdeDatePicker.SelectedDate.Value.Date && c.Fecha.Date <= HastaDatePicker.SelectedDate.Value.Date).ToList();
            }
            else
            {
                listado = InscripcionesBLL.GetList(p => true);
            }

            ConsultaDataGrid.ItemsSource = listado;
        }
    }
}
