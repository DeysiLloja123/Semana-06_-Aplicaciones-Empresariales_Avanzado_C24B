using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Semana_06
{
    public partial class MainWindow : Window

    {
        private string connectionString = "Data Source=LAB1504-27\\SQLEXPRESS;Initial Catalog=Neptuno;User Id=UserTecsup;Password=123";// Reemplaza con tu cadena de conexión

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InsertarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_InsertarEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Asignar valores de los campos de texto a los parámetros del procedimiento almacenado
                    command.Parameters.AddWithValue("@IdEmpleado", int.Parse(txtIdEmpleado.Text));
                    command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    command.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);
                    command.Parameters.AddWithValue("@FechaNacimiento", dpFechaNacimiento.SelectedDate);
                    command.Parameters.AddWithValue("@FechaContratacion", dpFechaContratacion.SelectedDate);
                    command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                    command.Parameters.AddWithValue("@Region", txtRegion.Text);
                    command.Parameters.AddWithValue("@CodPostal", txtCodPostal.Text);
                    command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    command.Parameters.AddWithValue("@TelDomicilio", txtTelDomicilio.Text);
                    command.Parameters.AddWithValue("@Extension", txtExtension.Text);
                    command.Parameters.AddWithValue("@Notas", txtNotas.Text);
                    command.Parameters.AddWithValue("@Jefe", int.Parse(txtJefe.Text));
                    command.Parameters.AddWithValue("@SueldoBasico", decimal.Parse(txtSueldoBasico.Text));

                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado insertado correctamente.");
                    ClearText();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar empleado: " + ex.Message);
            }
        }

        private void ClearText()
        {
            txtIdEmpleado.Clear();
            txtApellidos.Clear();
            txtNombre.Clear();
            txtCargo.Clear();
            txtTratamiento.Clear();
            dpFechaNacimiento.SelectedDate = null;
            dpFechaContratacion.SelectedDate = null;
            txtDireccion.Clear();
            txtCiudad.Clear();
            txtRegion.Clear();
            txtCodPostal.Clear();
            txtPais.Clear();
            txtTelDomicilio.Clear();
            txtExtension.Clear();
            txtNotas.Clear();
            txtJefe.Clear();
            txtSueldoBasico.Clear();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_ListEmpleados1", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("IdEmpleado"));
                        string apellidos = reader.IsDBNull(reader.GetOrdinal("Apellidos")) ? "" : reader.GetString(reader.GetOrdinal("Apellidos"));
                        string nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                        string cargo = reader.IsDBNull(reader.GetOrdinal("Cargo")) ? "" : reader.GetString(reader.GetOrdinal("Cargo"));
                        string tratamiento = reader.IsDBNull(reader.GetOrdinal("Tratamiento")) ? "" : reader.GetString(reader.GetOrdinal("Tratamiento"));
                        DateTime fechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"));
                        DateTime fechaContratacion = reader.IsDBNull(reader.GetOrdinal("FechaContratacion")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaContratacion"));
                        string direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? "" : reader.GetString(reader.GetOrdinal("Direccion"));
                        string ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? "" : reader.GetString(reader.GetOrdinal("Ciudad"));
                        string region = reader.IsDBNull(reader.GetOrdinal("Region")) ? "" : reader.GetString(reader.GetOrdinal("Region"));
                        string codPostal = reader.IsDBNull(reader.GetOrdinal("CodPostal")) ? "" : reader.GetString(reader.GetOrdinal("CodPostal"));
                        string pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? "" : reader.GetString(reader.GetOrdinal("Pais"));
                        string telDomicilio = reader.IsDBNull(reader.GetOrdinal("TelDomicilio")) ? "" : reader.GetString(reader.GetOrdinal("TelDomicilio"));
                        string extension = reader.IsDBNull(reader.GetOrdinal("Extension")) ? "" : reader.GetString(reader.GetOrdinal("Extension"));
                        string notas = reader.IsDBNull(reader.GetOrdinal("Notas")) ? "" : reader.GetString(reader.GetOrdinal("Notas"));
                        int jefe = reader.IsDBNull(reader.GetOrdinal("Jefe")) ? 0 : reader.GetInt32(reader.GetOrdinal("Jefe"));
                        decimal sueldoBasico = reader.IsDBNull(reader.GetOrdinal("SueldoBasico")) ? 0 : reader.GetDecimal(reader.GetOrdinal("SueldoBasico"));

                        empleados.Add(new Empleado(id, apellidos, nombre, cargo, tratamiento, fechaNacimiento,
                                                    fechaContratacion, direccion, ciudad, region, codPostal, pais,
                                                    telDomicilio, extension, notas, jefe, sueldoBasico));
                    }

                    reader.Close();
                }

                resultGrid.ItemsSource = empleados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_ActualizarEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdEmpleado", int.Parse(txtIdEmpleado.Text));
                    command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    command.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);
                    command.Parameters.AddWithValue("@FechaNacimiento", dpFechaNacimiento.SelectedDate);
                    command.Parameters.AddWithValue("@FechaContratacion", dpFechaContratacion.SelectedDate);
                    command.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                    command.Parameters.AddWithValue("@Region", txtRegion.Text);
                    command.Parameters.AddWithValue("@CodPostal", txtCodPostal.Text);
                    command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    command.Parameters.AddWithValue("@TelDomicilio", txtTelDomicilio.Text);
                    command.Parameters.AddWithValue("@Extension", txtExtension.Text);
                    command.Parameters.AddWithValue("@Notas", txtNotas.Text);
                    command.Parameters.AddWithValue("@Jefe", int.Parse(txtJefe.Text));
                    command.Parameters.AddWithValue("@SueldoBasico", decimal.Parse(txtSueldoBasico.Text));

                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado actualizado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar empleado: " + ex.Message);
            }
        }

        private void resultGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (resultGrid.SelectedItem != null)
            {
                Empleado empleadoSeleccionado = (Empleado)resultGrid.SelectedItem;

                // Actualizar los campos del formulario con los datos del empleado seleccionado
                txtIdEmpleado.Text = empleadoSeleccionado.IdEmpleado.ToString();
                txtApellidos.Text = empleadoSeleccionado.Apellidos;
                txtNombre.Text = empleadoSeleccionado.Nombre;
                txtCargo.Text = empleadoSeleccionado.Cargo;
                txtTratamiento.Text = empleadoSeleccionado.Tratamiento;
                dpFechaNacimiento.SelectedDate = empleadoSeleccionado.FechaNacimiento;
                dpFechaContratacion.SelectedDate = empleadoSeleccionado.FechaContratacion;
                txtDireccion.Text = empleadoSeleccionado.Direccion;
                txtCiudad.Text = empleadoSeleccionado.Ciudad;
                txtRegion.Text = empleadoSeleccionado.Region;
                txtCodPostal.Text = empleadoSeleccionado.CodPostal;
                txtPais.Text = empleadoSeleccionado.Pais;
                txtTelDomicilio.Text = empleadoSeleccionado.TelDomicilio;
                txtExtension.Text = empleadoSeleccionado.Extension;
                txtNotas.Text = empleadoSeleccionado.Notas;
                txtJefe.Text = empleadoSeleccionado.Jefe.ToString();
                txtSueldoBasico.Text = empleadoSeleccionado.SueldoBasico.ToString();
            }
        }



        private void EliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("USP_ElimiarEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Asignar el valor del IdEmpleado al parámetro del procedimiento almacenado
                    command.Parameters.AddWithValue("@IdEmpleado", int.Parse(txtIdEmpleado.Text));

                    command.ExecuteNonQuery();

                    MessageBox.Show("Empleado eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar empleado: " + ex.Message);
            }
        }


    }
}
