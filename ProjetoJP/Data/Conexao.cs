using System.Data.SqlClient;

namespace ProjetoJP.Data
{
    public class Conexao
    {
        //public SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\katit\\Documents\\Teste\\MeuBanco.mdf;Integrated Security=True;TrustServerCertificate=True;");
        public SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=bancoJP;Data Source=DESKTOP-PC9AHS8\\SQLEXPRESS;");
        public void Conectar()
        {
            conn.Open();    
        }

        public void Desconectar()
        {
            conn.Close();
        }
    }
}
