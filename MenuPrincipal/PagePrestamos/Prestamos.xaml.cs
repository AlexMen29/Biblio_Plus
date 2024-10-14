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
            string consultaSQL = @"SELECT 
    p.PrestamoID AS ID, 
    l.Titulo, 
    p.TipoPrestamo, 
    p.FechaPrestamo, 
    p.FechaDevolucion
FROM 
    Prestamos p
INNER JOIN 
    Solicitudes s ON p.SolicitudID = s.SolicitudID
INNER JOIN 
    Libros l ON s.LibroID = l.LibroID;
"; // Asegúrate de que 'LibroID' es correcto

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
            string consultaSQL = @"SELECT 
    pp.PagoID AS ID, 
    l.Titulo, 
    pr.TipoPrestamo, 
    pp.FechaPago AS Periodo, 
    pp.ValorPagar AS Monto
FROM 
    PagosPrestamos pp
INNER JOIN 
    Prestamos pr ON pp.PrestamoID = pr.PrestamoID
INNER JOIN 
    Solicitudes s ON pr.SolicitudID = s.SolicitudID
INNER JOIN 
    Libros l ON s.LibroID = l.LibroID;
"; // Asegúrate de que 'LibroID' es correcto

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
            string tipoPrestamo = ((ComboBoxItem)comboBoxTipoPrestamo.SelectedItem)?.Content?.ToString();

            // Verifica si se ha seleccionado un tipo de préstamo
            if (string.IsNullOrEmpty(tipoPrestamo))
            {
                MessageBox.Show("Por favor, selecciona un tipo de préstamo.");
                return;
            }

            string consultaSQL = $@"SELECT 
    p.PrestamoID AS ID, 
    l.Titulo, 
    p.TipoPrestamo, 
    p.FechaPrestamo, 
    p.FechaDevolucion
FROM 
    Prestamos p
INNER JOIN 
    Solicitudes s ON p.SolicitudID = s.SolicitudID
INNER JOIN 
    Libros l ON s.LibroID = l.LibroID
WHERE 
    p.TipoPrestamo = 'Tipo de préstamo que quieres filtrar';  -- Cambia esto por el tipo específico
";  // Uso de parámetros para evitar inyecciones SQL

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(consultaSQL, conDB);
                    adapter.SelectCommand.Parameters.AddWithValue("@tipoPrestamo", tipoPrestamo); // Asignar el parámetro

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
            string periodoPago = ((ComboBoxItem)comboBoxPeriodoPago.SelectedItem)?.Content?.ToString();
            string monto = textBoxMonto.Text;

            // Verificación básica de entrada
            if (string.IsNullOrEmpty(periodoPago) || string.IsNullOrEmpty(monto))
            {
                MessageBox.Show("Por favor, completa el período de pago y el monto.");
                return;
            }

            if (decimal.TryParse(monto, out decimal montoDecimal))
            {
                // Aquí puedes agregar la lógica adicional para calcular el pago si es necesario
                MessageBox.Show($"Pago calculado para el período: {periodoPago}, con un monto de {montoDecimal:C}");
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un monto válido.");
            }
        }
    }
}
