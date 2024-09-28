using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MenuPrincipal.PageReport
{
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();
        }

        // Método para generar reporte de libros
        private void GenerarReporteLibros(object sender, RoutedEventArgs e)
        {
            // Verificar si los elementos seleccionados son nulos
            if (comboBoxTema.SelectedItem == null || comboBoxAutor.SelectedItem == null || comboBoxEspecialidad.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un tema, autor y especialidad.");
                return;
            }

            string tema = ((ComboBoxItem)comboBoxTema.SelectedItem).Content.ToString();
            string autor = ((ComboBoxItem)comboBoxAutor.SelectedItem).Content.ToString();
            string especialidad = ((ComboBoxItem)comboBoxEspecialidad.SelectedItem).Content.ToString();

            string consultaSQL = "SELECT l.LibroID AS ID, l.Titulo, c.NombreCategoria AS Tema, a.NombreAutor AS Autor, e.NombreEspecialidad AS Especialidad " +
                                 "FROM Libros l " +
                                 "INNER JOIN Categorias c ON l.CategoriaID = c.CategoriaID " +
                                 "INNER JOIN Autores a ON l.AutorID = a.AutorID " +
                                 "INNER JOIN Especialidades e ON l.EspecialidadID = e.EspecialidadID " +
                                 "WHERE c.NombreCategoria = @Tema AND a.NombreAutor = @Autor AND e.NombreEspecialidad = @Especialidad";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consultaSQL, conDB);
                    cmd.Parameters.AddWithValue("@Tema", tema);
                    cmd.Parameters.AddWithValue("@Autor", autor);
                    cmd.Parameters.AddWithValue("@Especialidad", especialidad);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable librosTable = new DataTable();
                    adapter.Fill(librosTable);
                    dataGridLibros.ItemsSource = librosTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte de libros: " + ex.Message);
                }
            }
        }

        // Método para generar reporte de proveedores y empleados
        private void GenerarReporteProveedoresEmpleados()
        {
            // Verificar si los elementos seleccionados son nulos
            if (comboBoxProveedores.SelectedItem == null || comboBoxEmpleados.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un proveedor y un empleado.");
                return;
            }

            string proveedor = ((ComboBoxItem)comboBoxProveedores.SelectedItem).Content.ToString();
            string empleado = ((ComboBoxItem)comboBoxEmpleados.SelectedItem).Content.ToString();

            string consultaSQL = "SELECT p.ProveedorID AS ID, p.NombreProveedor AS Nombre, 'Proveedor' AS Tipo " +
                                 "FROM Proveedores p WHERE p.NombreProveedor = @Proveedor " +
                                 "UNION ALL " +
                                 "SELECT e.EmpleadoID AS ID, e.NombreCompleto AS Nombre, 'Empleado' AS Tipo " +
                                 "FROM Empleados e WHERE e.NombreCompleto = @Empleado";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consultaSQL, conDB);
                    cmd.Parameters.AddWithValue("@Proveedor", proveedor);
                    cmd.Parameters.AddWithValue("@Empleado", empleado);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable proveedoresEmpleadosTable = new DataTable();
                    adapter.Fill(proveedoresEmpleadosTable);
                    dataGridProveedoresEmpleados.ItemsSource = proveedoresEmpleadosTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte de proveedores y empleados: " + ex.Message);
                }
            }
        }

        // Método para generar reporte de análisis de compras
        private void GenerarReporteCompras(object sender, RoutedEventArgs e)
        {
            // Similar verifica como en otros métodos
            if (comboBoxPeriodoCompras.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un periodo.");
                return;
            }

            string periodo = ((ComboBoxItem)comboBoxPeriodoCompras.SelectedItem).Content.ToString();
            int meses = periodo.Contains("3 Meses") ? 3 : periodo.Contains("6 Meses") ? 6 : 12;

            string consultaSQL = "SELECT c.CompraID AS ID, c.NombreArticulo AS Articulo, c.FechaCompra, c.Cantidad, c.PrecioTotal " +
                                 "FROM Compras c " +
                                 "WHERE DATEDIFF(MONTH, c.FechaCompra, GETDATE()) <= @Meses";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consultaSQL, conDB);
                    cmd.Parameters.AddWithValue("@Meses", meses);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable comprasTable = new DataTable();
                    adapter.Fill(comprasTable);
                    dataGridCompras.ItemsSource = comprasTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el análisis de compras: " + ex.Message);
                }
            }
        }

        // Método para generar reporte de libros más prestados
        private void GenerarReporteLibrosMasPrestados(object sender, RoutedEventArgs e)
        {
            string consultaSQL = "SELECT l.LibroID AS ID, l.Titulo, COUNT(p.PrestamoID) AS CantidadPrestamos " +
                                 "FROM Prestamos p " +
                                 "INNER JOIN Libros l ON p.LibroID = l.LibroID " +
                                 "GROUP BY l.LibroID, l.Titulo " +
                                 "ORDER BY CantidadPrestamos DESC";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(consultaSQL, conDB);
                    DataTable librosMasPrestadosTable = new DataTable();
                    adapter.Fill(librosMasPrestadosTable);
                    dataGridLibrosMasPrestados.ItemsSource = librosMasPrestadosTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el reporte de libros más prestados: " + ex.Message);
                }
            }
        }

        // Método para generar la designación del lector y libro del mes
        private void GenerarLectorYLibroDelMes(object sender, RoutedEventArgs e)
        {
            string consultaSQLLector = "SELECT TOP 1 u.NombreCompleto, COUNT(p.PrestamoID) AS CantidadPrestamos " +
                                       "FROM Prestamos p " +
                                       "INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID " +
                                       "WHERE MONTH(p.FechaPrestamo) = MONTH(GETDATE()) " +
                                       "GROUP BY u.NombreCompleto " +
                                       "ORDER BY CantidadPrestamos DESC";

            string consultaSQLLibro = "SELECT TOP 1 l.Titulo, COUNT(p.PrestamoID) AS CantidadPrestamos " +
                                      "FROM Prestamos p " +
                                      "INNER JOIN Libros l ON p.LibroID = l.LibroID " +
                                      "WHERE MONTH(p.FechaPrestamo) = MONTH(GETDATE()) " +
                                      "GROUP BY l.Titulo " +
                                      "ORDER BY CantidadPrestamos DESC";

            using (SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB))
            {
                try
                {
                    SqlCommand cmdLector = new SqlCommand(consultaSQLLector, conDB);
                    SqlCommand cmdLibro = new SqlCommand(consultaSQLLibro, conDB);

                    conDB.Open();

                    // Lector del Mes
                    SqlDataReader lectorReader = cmdLector.ExecuteReader();
                    if (lectorReader.Read())
                    {
                        textBlockLectorDelMes.Text = "Lector del Mes: " + lectorReader["NombreCompleto"]?.ToString();
                    }
                    lectorReader.Close();

                    // Libro del Mes
                    SqlDataReader libroReader = cmdLibro.ExecuteReader();
                    if (libroReader.Read())
                    {
                        textBlockLibroDelMes.Text = "Libro del Mes: " + libroReader["Titulo"]?.ToString();
                    }
                    libroReader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar la designación del lector y libro del mes: " + ex.Message);
                }
                finally
                {
                    conDB.Close();
                }
            }
        }

        private void GenerarReporteProveedoresEmpleados(object sender, RoutedEventArgs e)
        {
            GenerarReporteProveedoresEmpleados();
        }
    }
}
