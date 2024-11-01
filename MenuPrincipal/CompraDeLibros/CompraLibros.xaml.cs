using MenuPrincipal.BD;
using MenuPrincipal.BD.Services;
using MenuPrincipal.DatosGenerales;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MenuPrincipal.CompraDeLibros
{
    /// <summary>
    /// Lógica de interacción para CompraLibros.xaml
    /// </summary>
    public partial class CompraLibros : Page
    {
        DatosGlobales datos = new DatosGlobales();
        public CompraLibros()
        {
            InitializeComponent();

            datos.LlenarBoxFiltros(datos.consultarEdicion, SeleccionarEdiccionBox, "ISBN");
            datos.LlenarBoxFiltros(datos.consultarProveedores, SelecionarProveedorBox, "NombreProveedor");
            DateFecha.SelectedDate = DateTime.Today;

        }

        private void calcularTotal()
        {
            try
            {
                costoTotaltxt.Text ="$"+ (Convert.ToDecimal(cantidadLibrotxt.Text) * Convert.ToDecimal(costoUnidadtxt.Text)).ToString();

            }
            catch (Exception e)
            {
                costoTotaltxt.Text = "...";
            }
        }
        private void cantidadLibrotxt_SelectionChanged(object sender, RoutedEventArgs e)
        {
            calcularTotal();
        }

        private void costoUnidadtxt_SelectionChanged(object sender, RoutedEventArgs e)
        {
            calcularTotal();
        }

        private void btnComprar_Click(object sender, RoutedEventArgs e)
        {
            //id 0:Edicion ; id 1:Proveerdor; id 1: detalleLibro;
            int[] id = IdProveedorEdicionDetalle();

            ComprasModel datosCompra = new ComprasModel();
            //datosCompra.Cantidad=



            int res = MetodosCompras.RegistrarCompraCompleta(datosCompra);

            if (res > 0)
            {
                MessageBox.Show("Compra registrada exitosamente con ID: " + res, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Hubo un problema al registrar la compra", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            

            MessageBox.Show($"id Edicion {id[0]} Proveedor {id[1]}Detalle{id[2]}" );
        }


        private int[] IdProveedorEdicionDetalle()
        {
            int[] ids= new int[3];

            //obtenemos id correspondiente a edicion
            string consultaEdicion = "SELECT EdicionID FROM Ediciones WHERE ISBN = @ISBN";
            ids[0] = ObtenerIdLocal(consultaEdicion, "@ISBN", SeleccionarEdiccionBox, 0);


            //obtenemos id correspondiente a proveedor
            string consultaProveedores = "select ProveedorID from Proveedores where NombreProveedor=@Nombre";
            ids[1] = ObtenerIdLocal(consultaProveedores, "@Nombre", SelecionarProveedorBox,0);

            string consultaDetalle = "select DetallesID from DetallesLibros where EdicionID=@ediccionID";
            int ediccionID = ids[0];
            ids[2] = ObtenerIdLocal(consultaDetalle, "@ediccionID", SelecionarProveedorBox, ediccionID);

            return ids;
        }
        private int ObtenerIdLocal(string consulta,string clave, ComboBox valor,int Edicion)
        {
            if (Edicion > 0)
            {
                string consultaSql = consulta;
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { clave, Edicion } // Reemplaza con el ISBN correspondiente
                };

                // Llamar al método y obtener el resultado
                int id = MetodosCRUD.ObtenerId(consultaSql, parametros);
                return id;
            }
            else
            {
                string consultaSql = consulta;
                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { clave, valor.Text } // Reemplaza con el ISBN correspondiente
                };

                // Llamar al método y obtener el resultado
                int id = MetodosCRUD.ObtenerId(consultaSql, parametros);
                return id;
            }
        }
    }
}
