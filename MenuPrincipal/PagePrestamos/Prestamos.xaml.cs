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
        // Constructor
        public Prestamos()
        {
            InitializeComponent();
            CargarClasificacionPrestamos();
            CargarControlPagos();
        }

        // Método para cargar la clasificación de préstamos en el DataGrid
        private void CargarClasificacionPrestamos()
        {
            string consultaSQL = "SELECT p.PrestamoID AS ID, l.Titulo, p.TipoPrestamo, p.FechaPrestamo, p.FechaDevolucion " +
                                 "FROM Prestamos p " +
                                 "INNER JOIN Libros l ON p.LibroID = l.LibroID";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(consultaSQL, conDB);
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
            string consultaSQL = "SELECT pp.PrestamoID AS ID, l.Titulo, pr.TipoPrestamo, pp.PeriodoPago AS Periodo, pp.Monto " +
                                 "FROM PagosPrestamos pp " +
                                 "INNER JOIN Prestamos pr ON pp.PrestamoID = pr.PrestamoID " +
                                 "INNER JOIN Libros l ON pr.LibroID = l.LibroID";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(consultaSQL, conDB);
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
            string consultaSQL = $"SELECT p.PrestamoID AS ID, l.Titulo, p.TipoPrestamo, p.FechaPrestamo, p.FechaDevolucion " +
                                 $"FROM Prestamos p " +
                                 $"INNER JOIN Libros l ON p.LibroID = l.LibroID " +
                                 $"WHERE p.TipoPrestamo = '{tipoPrestamo}'";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(consultaSQL, conDB);
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
