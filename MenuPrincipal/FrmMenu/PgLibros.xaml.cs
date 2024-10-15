using MenuPrincipal.BD.Models;
using MenuPrincipal.BD.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

namespace MenuPrincipal.FrmMenu
{
    /// <summary>
    /// Lógica de interacción para PgLibros.xaml
    /// </summary>
    public partial class PgLibros : Page
    {
        private DataTable dataTable;
        public PgLibros()
        {
            InitializeComponent();
            CargarTodosLosElementos();
            CargarDatos();


            LlenarBoxFiltros("SELECT DISTINCT A.NombreAutor FROM Libros L JOIN DetallesLibros DL ON L.DetallesID = DL.DetallesID JOIN Autores A ON DL.AutorID = A.AutorID", AutorComboBox,"NombreAutor");
            LlenarBoxFiltros("SELECT DISTINCT E.NombreEditorial FROM Libros L  JOIN DetallesLibros DL ON L.DetallesID = DL.DetallesID JOIN Editoriales E ON DL.EditorialID = E.EditorialID", EditorialComboBox, "NombreEditorial");
            LlenarBoxFiltros("SELECT DISTINCT C.NombreCategoria  FROM Libros L JOIN DetallesLibros DL ON L.DetallesID = DL.DetallesID JOIN Categorias C ON DL.CategoriaID = C.CategoriaID", CategoriaComboBox,"NombreCategoria");
        }

        public List<DetallesLibros> ListaDataGrid;

        SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB);


        //Funcion creada para llamar solo una vez para cargar todos los elementos
        private void CargarTodosLosElementos()
        {
            CargarElementosPorGenero("SELECT TOP 3* FROM LIBROS ORDER BY LibroID DESC", contentGrid);
            CargarElementosPorGenero("SELECT *FROM LIBROS WHERE EditorialID=2", contenGridDrama);
        }

        //funcion que carga elementos segun genero
        private void CargarElementosPorGenero(string query,ItemsControl contenGrid)
        {
            try
            {
                conDB.Open();           
                SqlCommand command = new SqlCommand(query, conDB);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                // Crear una nueva columna para las imágenes
                dataTable.Columns.Add("ImageData", typeof(BitmapImage));

                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["imagen"] != DBNull.Value)
                    {
                        byte[] imageBytes = (byte[])row["imagen"];
                        BitmapImage bitmapImage = ConvertirABitmapImage(imageBytes);
                        row["ImageData"] = bitmapImage;
                    }
                }

                // Asignar los datos al ItemsControl
                contenGrid.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}");
            }
            finally
            {
                conDB.Close();
            }
        }
        private BitmapImage ConvertirABitmapImage(byte[] imageBytes)
        {
            using (var ms = new System.IO.MemoryStream(imageBytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }






        #region ADMINISTRADOR

        public void CargarDatos()
        {
            ListaDataGrid = MetodosDetallesLibros.MostrarLibros();
            LibrosDataGrid.ItemsSource = ListaDataGrid;

        }




        public void LlenarBoxFiltros(string consulta, ComboBox elementoBox, string columna)
        {
            //Lista con valores correspondientes a ComboBox
            List<string> Lista = new List<string>();
            using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                conn.Open();

                using (var command = new SqlCommand(consulta, conn))
                {
                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(dr[columna].ToString());
                        }
                        Lista.Add("Ninguno");
                    }
                }
            }

            elementoBox.ItemsSource = Lista; // Asigna la lista al ComboBox
        }


        public void AplicarFiltro()
        {
            // Obtenemos la lista completa de libros
            List<DetallesLibros> librosFiltrados = MetodosDetallesLibros.MostrarLibros();

            // Filtramos por autor si hay un valor seleccionado
            if (AutorComboBox.SelectedItem != null && AutorComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string autorSeleccionado = AutorComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Autor.Equals(autorSeleccionado, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filtramos por editorial si hay un valor seleccionado
            if (EditorialComboBox.SelectedItem != null && EditorialComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string editorialSeleccionada = EditorialComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Editorial.Equals(editorialSeleccionada, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filtramos por categoría si hay un valor seleccionado
            if (CategoriaComboBox.SelectedItem != null && CategoriaComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string categoriaSeleccionada = CategoriaComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Categoria.Equals(categoriaSeleccionada, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (StockComboBox.SelectedItem != null && ((ComboBoxItem)StockComboBox.SelectedItem).Content.ToString() != "Ninguno")
            {
           

                string stockSeleccionado = ((ComboBoxItem)StockComboBox.SelectedItem).Content.ToString();

                if (stockSeleccionado == "Mayor a Menor")
                {
                    librosFiltrados = librosFiltrados
                        .OrderByDescending(libro => libro.StockActual) // Ordenar de mayor a menor
                        .ToList();
                }
                else if (stockSeleccionado == "Menor a Mayor")
                {
                    librosFiltrados = librosFiltrados
                        .OrderBy(libro => libro.StockActual) // Ordenar de menor a mayor
                        .ToList();
                }
            }


            // Asignamos los libros filtrados al DataGrid
            LibrosDataGrid.ItemsSource = librosFiltrados;

          
        }



        private void AutorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
            AplicarFiltro();
        }
        private void EditorialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltro();
        }

        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltro();
        }

        private void StockComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            AplicarFiltro();
        }



        #endregion

        private void btnQuitarFiltros_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFiltros();
        }
        public void LimpiarFiltros()
        {
            AutorComboBox.SelectedIndex = -1;
            EditorialComboBox.SelectedIndex = -1;
            CategoriaComboBox.SelectedIndex = -1;
            StockComboBox.SelectedIndex = -1;
        }

    }






}
