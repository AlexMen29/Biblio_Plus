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
using System.Data;
using System.Data.SqlClient;

namespace MenuPrincipal.PageUsuarios
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Page
    {
        public Usuarios()
        {
            InitializeComponent();
        }
        //CONEXION  A LA BASE DE DATOS 
        SqlConnection condb = new SqlConnection(Properties.Settings.Default.conexionDB);

        //Variable consulata sql 
        String consultaSQL = null;

        void MostrarUsuario()
        {
            //consulta sql 
            consultaSQL = null;
            consultaSQL = "SELECT Carnet,NombreCompleto,Carrera,Correo,Telefono,FechaRegistro FROM USUARIOS";
            condb.Open();
            //creando elemento sqladapter
            SqlDataAdapter da = new SqlDataAdapter(consultaSQL, condb);
            // crear data table
            DataTable dt = new DataTable();
            // llenar table 
            da.Fill(dt);
            // proceder a llenado de datagrid
            dataGridUsuarios.ItemsSource = dt.DefaultView;
            //cerrar 
            condb.Close();

        }

        private void BuscarUsuarios()
        {
            string carnet = txtCarnet.Text;
            string nombreCompleto = txtNombre.Text;
            string carrera = txbCarrera.Text;
            string correo = txtCorreo.Text;

            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.conexionDB))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    // Consulta SQL dinámica
                    string query = "SELECT * FROM USUARIOS WHERE 1=1"; // 1=1 para no tener que revisar si es el primer filtro

                    if (!string.IsNullOrEmpty(carnet))
                    {
                        query += " AND Carnet LIKE @Carnet";
                        cmd.Parameters.AddWithValue("@Carnet", "%" + carnet + "%");
                    }
                    if (!string.IsNullOrEmpty(nombreCompleto))
                    {
                        query += " AND NombreCompleto LIKE @NombreCompleto";
                        cmd.Parameters.AddWithValue("@NombreCompleto", "%" + nombreCompleto + "%");
                    }
                    if (!string.IsNullOrEmpty(carrera))
                    {
                        query += " AND Carrera LIKE @Carrera";
                        cmd.Parameters.AddWithValue("@Carrera", "%" + carrera + "%");
                    }
                    if (!string.IsNullOrEmpty(correo))
                    {
                        query += " AND Correo LIKE @Correo";
                        cmd.Parameters.AddWithValue("@Correo", "%" + correo + "%");
                    }

                    cmd.CommandText = query;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridUsuarios.ItemsSource = dt.DefaultView; // Mostrar resultados en el DataGrid
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar usuarios: " + ex.Message);
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarUsuario();
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            BuscarUsuarios();
        }
    }
}
