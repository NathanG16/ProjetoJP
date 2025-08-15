using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJP.Data
{
    public class AlunoRepositorio
    {
        private SqlConnection _conn;
        public AlunoRepositorio(SqlConnection conn)
        {
            _conn = conn;
        }

        public static string InserirAluno(Conexao db, Aluno aluno)
        {
            try
            {
                string sql = $"INSERT INTO Aluno (nome, idade, cpf) VALUES('{aluno.nome}', {aluno.idade}, '{aluno.cpf}')";
                SqlCommand comando = new SqlCommand(sql, db.conn);

                comando.ExecuteNonQuery();

                return "Aluno inserido com sucesso!";
            }
            catch (Exception e)
            {

                return "Erro ao inserir Aluno";
            }
        }
        public static List<Aluno> BuscarAlunos(Conexao db)
        {

            string sql = "select Nome, Idade, Cpf from Aluno";
            SqlCommand comando = new SqlCommand(sql, db.conn);

            List<Aluno> alunos = new List<Aluno>();

            using (var reader = comando.ExecuteReader())
            {
                //cria um leitor do ADO.net

                while (reader.Read())
                {///vai lendo cada item do resultado do select
                 ///retorna cada item encontrado
                    var nomeDb = reader.GetString(reader.GetOrdinal("Nome"));
                    var idadeDb = reader.GetInt32(reader.GetOrdinal("Idade"));
                    var cpfDb = reader.GetString(reader.GetOrdinal("Cpf"));

                    alunos.Add(new Aluno()
                    {
                        nome = nomeDb,
                        idade = idadeDb,
                        cpf = cpfDb
                    });

                }
                return alunos;
            }
        }
        public static string EditarAluno(Conexao db, Aluno aluno)
        {
            try
            {
                string sql = @"Update aluno
                             SET nome = @nome, idade = @idade, cpf = @Cpf
                             Where Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, db.conn))
                {
                    comando.Parameters.AddWithValue("@nome", aluno.nome);
                    comando.Parameters.AddWithValue("@idade", aluno.idade);
                    comando.Parameters.AddWithValue("@cpf", aluno.cpf);
                    comando.Parameters.AddWithValue("@id", aluno.id);

                    comando.ExecuteNonQuery();
                    return "Aluno editado com sucesso";
                }

            }
            catch (Exception e)
            {
                return "Erro ao editar Aluno";
            }
    }
    }
}