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

namespace Registro_Inscripcion.UI.Pagos
{
    /// <summary>
    /// Interaction logic for pPersona.xaml
    /// </summary>
    public partial class pPersona : Window
    {
        public pPersona()
        {
            InitializeComponent();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Inscripciones inscripcion;
            bool paso = false;

            if (!validar())
                return;

            inscripcion = llenaClaseI();

            if (!existeEnLaBaseDeDatos())
            {
                MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            inscripcion = ingresarMonto(inscripcion);

            paso = InscripcionesBLL.Modificar(inscripcion);

            if (paso)
            {
                limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            { 
                MessageBox.Show("No fue posible guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Inscripciones inscripcion = new Inscripciones();
            int.TryParse(PersonaIdTextBox.Text, out id);

            limpiar();

            inscripcion = InscripcionesBLL.Buscar(id);

            if (inscripcion != null)
            {
                MessageBox.Show("Inscripcion Encontrada");
                llenaCampo(inscripcion);
            }
            else
            {
                MessageBox.Show("Inscripcion no Encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(InscripcionIdTextBox.Text, out id);

            limpiar();

            if (InscripcionesBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se puede eliminar una Inscripcion que no existe");
        }

        private void limpiar()
        {
            InscripcionIdTextBox.Text = string.Empty;
            FechaDatePicker.SelectedDate = DateTime.Now;
            PersonaIdTextBox.Text = string.Empty;
            NombreTextBox.Text = string.Empty;
            ComentariosTextBox.Text = string.Empty;
            BalanceTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
        }

        private void llenaCampo(Inscripciones inscripcion)
        {
            InscripcionIdTextBox.Text = Convert.ToString(inscripcion.InscripcionId);
            FechaDatePicker.SelectedDate = inscripcion.Fecha;
            PersonaIdTextBox.Text = Convert.ToString(inscripcion.PersonaId);
            NombreTextBox.Text = PersonasBLL.Buscar(inscripcion.PersonaId).Nombre;
            ComentariosTextBox.Text = inscripcion.Comentarios;
            BalanceTextBox.Text = Convert.ToString(inscripcion.Balance);
        }

        private Inscripciones llenaClaseI()
        {
            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = Convert.ToInt32(InscripcionIdTextBox.Text);
            inscripcion.Fecha = (DateTime)FechaDatePicker.SelectedDate;
            inscripcion.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            inscripcion.Comentarios = ComentariosTextBox.Text;
            inscripcion.Balance = Convert.ToInt32(BalanceTextBox.Text);
            inscripcion.Monto = Convert.ToInt32(MontoTextBox.Text);

            return inscripcion;
        }

        private bool existeEnLaBaseDeDatos()
        {
            Inscripciones inscripcion = InscripcionesBLL.Buscar(Convert.ToInt32(InscripcionIdTextBox.Text));
            return (inscripcion != null);
        }

        private Inscripciones ingresarMonto(Inscripciones inscripcion)
        {
            var persona = PersonasBLL.Buscar(inscripcion.PersonaId);
            persona.Balance -= inscripcion.Monto;
            PersonasBLL.Modificar(persona);

            inscripcion.Balance -= inscripcion.Monto;
            var inscripcionAntigua = InscripcionesBLL.Buscar(inscripcion.InscripcionId);
            inscripcion.Monto += inscripcionAntigua.Monto;

            return inscripcion;
        }

        private bool validar()
        {//En los campos numericos no se verificar los punto ya que no utilizar valores decimales
            bool paso = true;

            //InscripcionId
            if (string.IsNullOrWhiteSpace(InscripcionIdTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < InscripcionIdTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(InscripcionIdTextBox.Text[i]) || Convert.ToInt32(InscripcionIdTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            //Telefono
            if (string.IsNullOrWhiteSpace(MontoTextBox.Text))
                paso = false;
            else
            {
                for (int i = 0; i < MontoTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(MontoTextBox.Text[i]) || Convert.ToInt32(MontoTextBox.Text[i]) < 0)
                        paso = false;
                }
            }

            if (paso == false)
                MessageBox.Show("Datos invalidos");

            return paso;
        }
    }
}
