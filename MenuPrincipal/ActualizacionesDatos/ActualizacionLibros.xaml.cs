using MenuPrincipal.BD.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MenuPrincipal.ActualizacionesDatos
{
    /// <summary>
    /// Lógica de interacción para ActualizacionLibros.xaml
    /// </summary>
    public partial class ActualizacionLibros : Window
    {
        private DetallesLibros Libros;
        public ActualizacionLibros(DetallesLibros Libros)
        {
            InitializeComponent();
            this.Libros = Libros;

            CargarDatos();
        }


        public void CargarDatos()
        {
            EditTituloTextBox.Text = Libros.Titulo;
            EditAutorTextBox.Text = Libros.Autor;
            EditEditorialTextBox.Text = Libros.Editorial;
            EditCategoriaTextBox.Text = Libros.Categoria;
            EditEdicionTextBox.Text = Libros.Edicion;
            EditStockTextBox.Text = Libros.StockActual.ToString();

            // Mostrar el panel de edición
        }



    }
}
