using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//SQL

using System.Data;
using System.Data.SqlClient;


namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region DECLARACION DE VARIABLES LOCALES
        //Conexion a la DB
        SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB);
      

       


        //variables para SQL Querys
        string consultaSQL = null;


        #endregion

        #region METODOS PERSONALIZADOS
        void EncontrarUsuario()
        {

            int resultado = 0;

            //aperturarBD
            if (conDB.State == ConnectionState.Closed)
            {
                conDB.Open();
                //Cosnulta para buscar usuario
                consultaSQL = null;

                consultaSQL = "SELECT DBO.FNENCONTRARUSUARIO(@User,@Password)";

                SqlCommand sqlCmd = new SqlCommand(consultaSQL, conDB);
                sqlCmd.CommandType = CommandType.Text;
                //enviar valores por paramtetros
                sqlCmd.Parameters.AddWithValue("@User", txtCorreo.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password.Trim());

                //ejecutar la consulta sql
                resultado = Convert.ToInt32(sqlCmd.ExecuteScalar());

                MessageBox.Show(resultado.ToString());

                //evaluar el resultado

                if (resultado == 1)
                {
                    //Instanciar formulario de usuario
                    //frmUsuarios frmUsu = new frmUsuarios();
                    //frmUsu.Show();
                    //this.Close();

                }
                else
                {
                    MessageBox.Show("Usuario no encontrado", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //cerrar la base de datos
                conDB.Close();

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EncontrarUsuario();
        }
    }
    #endregion



}