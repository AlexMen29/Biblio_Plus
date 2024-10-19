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


        public  string consultaAutor= "SELECT DISTINCT A.NombreAutor FROM Libros L JOIN DetallesLibros DL ON L.DetallesID = DL.DetallesID JOIN Autores A ON DL.AutorID = A.AutorID";
        public string consultaEdiorial= "SELECT DISTINCT E.NombreEditorial FROM Libros L  JOIN DetallesLibros DL ON L.DetallesID = DL.DetallesID JOIN Editoriales E ON DL.EditorialID = E.EditorialID";
        public string consultaCategoria= "SELECT DISTINCT C.NombreCategoria  FROM Libros L JOIN DetallesLibros DL ON L.DetallesID = DL.DetallesID JOIN Categorias C ON DL.CategoriaID = C.CategoriaID";

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
