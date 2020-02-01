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

namespace Registro_Inscripcion.UI.ConsultasPersonas
{
    /// <summary>
    /// Interaction logic for cPersona.xaml
    /// </summary>
    public partial class cPersona : Window
    {
        public cPersona()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Personas>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltrarComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PersonasBLL.GetList(p => true);
                        break;
                    case 1:
                        int id = Convert.ToInt32(CriterioTextBox.Text);
                        listado = PersonasBLL.GetList(p => p.PersonaId == id);
                        break;
                    case 2:
                        listado = PersonasBLL.GetList(p => p.Nombre.Contains(CriterioTextBox.Text));
                        break;
                    case 3:
                        listado = PersonasBLL.GetList(p => p.Cedula.Contains(CriterioTextBox.Text));
                        break;
                    case 4:
                        listado = PersonasBLL.GetList(p => p.Direccion.Contains(CriterioTextBox.Text));
                        break;
                }
                if (DesdeDatePicker.SelectedDate != null && HastaDatePicker.SelectedDate != null)
                    listado = listado.Where(c => c.FechaNacimiento.Date >= DesdeDatePicker.SelectedDate.Value.Date && c.FechaNacimiento.Date <= HastaDatePicker.SelectedDate.Value.Date).ToList();
            }
            else
            {
                listado = PersonasBLL.GetList(p => true);
            }

            ConsultaDataGrid.ItemsSource = listado;
        }
    }
}
