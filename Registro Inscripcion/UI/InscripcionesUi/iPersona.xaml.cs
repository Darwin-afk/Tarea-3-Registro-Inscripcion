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

namespace Registro_Inscripcion.UI.InscripcionesUi
{
    /// <summary>
    /// Interaction logic for iPersona.xaml
    /// </summary>
    public partial class iPersona : Window
    {
        const int COSTO = 2600;

        public iPersona()
        {
            InitializeComponent();
            BalanceTextBox.Text = Convert.ToString(COSTO);
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas persona;
            Inscripciones inscripcion;
            bool paso = false;

            if (!validar())
                return;

            persona = llenaClaseP();
            inscripcion = llenaClaseI();

            if (Convert.ToInt32(PersonaIdTextBox.Text)== 0)
            {
                paso = PersonasBLL.Guardar(persona);
                inscripcion = ultimoRegistro(inscripcion);
                paso = InscripcionesBLL.Guardar(inscripcion);
            }
            else
            {
                if (!existeEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(InscripcionCheckBox.IsChecked == true)
                {
                    persona = aumentarBalance(persona);
                    paso = PersonasBLL.Modificar(persona);
                    paso = InscripcionesBLL.Guardar(inscripcion);
                }
                else
                {
                    paso = PersonasBLL.Modificar(persona);
                }
            }

            if (paso)
            {
                limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(PersonaIdTextBox.Text, out id);

            limpiar();

            persona = PersonasBLL.Buscar(id);

            if (persona != null)
            {
                MessageBox.Show("Persona Encontrada");
                llenaCampo(persona);
            }
            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(PersonaIdTextBox.Text, out id);

            limpiar();

            if (PersonasBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se puede eliminar una persona que no existe");
        }

        private void limpiar()
        {
            PersonaIdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CedulaTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            BalanceTextBox.Text = Convert.ToString(COSTO);
        }

        private void llenaCampo(Personas persona)
        {
            PersonaIdTextBox.Text = Convert.ToString(persona.PersonaId);
            NombreTextBox.Text = persona.Nombre;
            TelefonoTextBox.Text = persona.Telefono;
            CedulaTextBox.Text = persona.Cedula;
            DireccionTextBox.Text = persona.Direccion;
            FechaDatePicker.SelectedDate = persona.FechaNacimiento;
            BalanceTextBox.Text = Convert.ToString(persona.Balance);
        }

        private Personas llenaClaseP()
        {
            Personas persona = new Personas();
            persona.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            persona.Nombre = NombreTextBox.Text;
            persona.Telefono = TelefonoTextBox.Text;
            persona.Cedula = CedulaTextBox.Text;
            persona.Direccion = DireccionTextBox.Text;
            persona.FechaNacimiento = (DateTime)FechaDatePicker.SelectedDate;
            persona.Balance = COSTO;

            return persona;
        }

        private Inscripciones llenaClaseI()
        {
            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = 0;
            inscripcion.Fecha = DateTime.Now;
            inscripcion.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            inscripcion.Comentarios = "";
            inscripcion.Balance = COSTO;
            inscripcion.Monto = 0;

            return inscripcion;
        }

        private bool existeEnLaBaseDeDatos()
        {
            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(PersonaIdTextBox.Text));
            return (persona != null);
        }

        private Inscripciones ultimoRegistro(Inscripciones inscripcion)
        {
            inscripcion.PersonaId = PersonasBLL.ultimoRegistro();

            return inscripcion;
        }

        private Personas aumentarBalance(Personas persona1)
        {
            Personas persona2 = PersonasBLL.Buscar(Convert.ToInt32(persona1.PersonaId));
            persona1.Balance += persona2.Balance;

            return persona1;
        }

        private bool validar()
        {//En los campos numericos no se verificar los punto ya que no utilizar valores decimales
            bool paso = true;

            //ID
            if (string.IsNullOrWhiteSpace(PersonaIdTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < PersonaIdTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(PersonaIdTextBox.Text[i]) || Convert.ToInt32(PersonaIdTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Nombre
            if (NombreTextBox.Text == string.Empty)
                paso = false;

            //Telefono
            if (string.IsNullOrWhiteSpace(TelefonoTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < TelefonoTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(TelefonoTextBox.Text[i]) || Convert.ToInt32(TelefonoTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Direccion
            if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
                paso = false;

            //Cedula
            if (string.IsNullOrWhiteSpace(CedulaTextBox.Text.Replace("-", "")))
                paso = false;
            else
            {
                for (int i = 0; i < CedulaTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(CedulaTextBox.Text[i]))
                        paso = false;
                }
            }
            //Fecha
            if (FechaDatePicker.SelectedDate == null)
                paso = false;

            if (paso == false)
                MessageBox.Show("Datos invalidos");

            return paso;
        }
    }
}
