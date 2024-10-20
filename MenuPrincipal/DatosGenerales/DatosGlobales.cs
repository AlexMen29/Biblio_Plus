using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MenuPrincipal.DatosGenerales
{
    public class DatosGlobales
    {

        public DatosGlobales() { }


        public  string consultaAutor= "select NombreAutor from Autores";
        public string consultaCategoria = "select NombreCategoria from Categorias";
        public string consultaEdiorial= "select NombreEditorial from Editoriales";

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

    }
}
