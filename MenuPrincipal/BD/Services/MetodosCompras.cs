using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


using System.Data.SqlClient;

namespace MenuPrincipal.BD.Services
{
    internal class MetodosCompras
    {

        public static int RegistrarCompraCompleta(ComprasModel compra)
        {
            int compraIDGenerado = 0;

            try
            {
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_RegistrarCompra";

                        // Agregar parámetros al comando usando el objeto de tipo ComprasModel
                        command.Parameters.AddWithValue("@Cantidad", compra.Cantidad);
                        command.Parameters.AddWithValue("@CostoUnidad", compra.CostoUnidad);
                        command.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);
                        command.Parameters.AddWithValue("@CostoTotal", compra.CostoTotal);
                        command.Parameters.AddWithValue("@EdicionID", compra.EdicionID);
                        command.Parameters.AddWithValue("@ProveedorID", compra.ProveedorID);
                        command.Parameters.AddWithValue("@DetallesID", compra.DetallesID);

                        // Ejecutar el comando y obtener el ID de compra generado
                        compraIDGenerado = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocurrió un error: " + e.Message, "Error de consulta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return compraIDGenerado;
        }
    }




}
