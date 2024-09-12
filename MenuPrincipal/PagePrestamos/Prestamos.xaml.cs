using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MenuPrincipal.PagePrestamos
{
    public partial class Prestamos : Page
    {
        // Cadena de conexión a la base de datos
        private string connectionString = "Server=DESKTOP-HO4DRLK\\SQLEXPRESS;Initial Catalog=Biblioteca;Integrated Security=True;Encrypt=False;";
        //Data Source=DESKTOP-HO4DRLK\SQLEXPRESS;Initial Catalog=Biblioteca;Integrated Security=True;Encrypt=False
        public Prestamos()
        {
            InitializeComponent();
            CargarClasificacionPrestamos();
            CargarControlPagos();
        }

        // Método para cargar la clasificación de préstamos en el DataGrid
        private void CargarClasificacionPrestamos()
        {
            string query = "SELECT p.PrestamoID AS ID, l.Titulo, p.TipoPrestamo, p.FechaPrestamo, p.FechaDevolucion " +
                           "FROM Prestamos p " +
                           "INNER JOIN Libros l ON p.LibroID = l.LibroID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable prestamosTable = new DataTable();
                    adapter.Fill(prestamosTable);
                    dataGridPrestamos.ItemsSource = prestamosTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los préstamos: " + ex.Message);
                }
            }
        }


        // Método para cargar el control de pagos en el DataGrid
        private void CargarControlPagos()
        {
            string query = "SELECT pp.PrestamoID AS ID, l.Titulo, pr.TipoPrestamo, pp.PeriodoPago AS Periodo, pp.Monto " +
                           "FROM PagosPrestamos pp " +
                           "INNER JOIN Prestamos pr ON pp.PrestamoID = pr.PrestamoID " +
                           "INNER JOIN Libros l ON pr.LibroID = l.LibroID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable pagosTable = new DataTable();
                    adapter.Fill(pagosTable);
                    dataGridPagos.ItemsSource = pagosTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los pagos: " + ex.Message);
                }
            }
        }


        // Evento del botón para generar la clasificación de préstamos
        private void BtnGenerarClasificacion_Click(object sender, RoutedEventArgs e)
        {
            string tipoPrestamo = ((ComboBoxItem)comboBoxTipoPrestamo.SelectedItem).Content.ToString();
            string query = $"SELECT p.PrestamoID AS ID, l.Titulo, p.TipoPrestamo, p.FechaPrestamo, p.FechaDevolucion " +
                           $"FROM Prestamos p " +
                           $"INNER JOIN Libros l ON p.LibroID = l.LibroID " +
                           $"WHERE p.TipoPrestamo = '{tipoPrestamo}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable prestamosTable = new DataTable();
                    adapter.Fill(prestamosTable);
                    dataGridPrestamos.ItemsSource = prestamosTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar la clasificación de préstamos: " + ex.Message);
                }
            }
        }

        // Evento del botón para calcular el pago
        private void BtnCalcularPago_Click(object sender, RoutedEventArgs e)
        {
            string periodoPago = ((ComboBoxItem)comboBoxPeriodoPago.SelectedItem).Content.ToString();
            string monto = textBoxMonto.Text;

            // Aquí puedes agregar la lógica adicional para calcular el pago si es necesario

            MessageBox.Show($"Pago calculado para el período: {periodoPago}, con un monto de {monto}");
        }
    }
}
