using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

using System.Data;
using System.Data.SqlClient;

namespace MenuPrincipal.DatosGenerales
{
    public class DatosGlobales
    {

        public DatosGlobales() { }


        public  string consultaAutor= "select NombreAutor from Autores";
        public string consultaCategoria = "select NombreCategoria from Categorias";
        public string consultaEdiorial= "select NombreEditorial from Editoriales";
        public string consultaTipoUsuario= "select Tipo from TipoUsuario";
        public string consultaCarrera = "select NombreCarrera from Carrera";
        public string consultarEdicion = "select ISBN from Ediciones";
        public string consultarProveedores = "select NombreProveedor from Proveedores";


        public BitmapImage ConvertirABitmapImage(byte[] imageBytes)
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

        public void LlenarBoxFiltros(string consulta, ComboBox elementoBox, string columna)
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show($"Error inesperado: {e.Message}");
            }
        }


    }
}
