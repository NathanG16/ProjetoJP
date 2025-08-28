using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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
                string sql =
                            """
                    INSERT INTO Aluno (
                    Nome, 
                    Idade,
                    Cpf, 
                    DataNascimento, 
                    Cep, 
                    Endereco, 
                    Numero,
                    Bairro,
                    Cidade,
                    Estado,
                    Nota1, 
                    Nota2
                    ) VALUES (
                    @Nome, 
                    @Idade, 
                    @Cpf, 
                    @DataNascimento,
                    @Cep,
                    @Endereco, 
                    @Numero,
                    @Bairro,
                    @Cidade,
                    @Estado,
                    @Nota1, 
                    @Nota2
                    );
                    """;

                SqlCommand comando = new SqlCommand(sql, db.conn);
                comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                comando.Parameters.AddWithValue("@Idade", aluno.Idade);
                comando.Parameters.AddWithValue("@Cpf", aluno.Cpf);
                comando.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
                comando.Parameters.AddWithValue("@Cep", aluno.Cep);
                comando.Parameters.AddWithValue("@Endereco", aluno.Endereco);
                comando.Parameters.AddWithValue("@Numero", aluno.Numero);
                comando.Parameters.AddWithValue("@Bairro", aluno.Bairro);
                comando.Parameters.AddWithValue("@Cidade", aluno.Cidade);
                comando.Parameters.AddWithValue("@Estado", aluno.Estado);
                comando.Parameters.AddWithValue("@Nota1", (object?)aluno.Nota1 ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Nota2", (object?)aluno.Nota2 ?? DBNull.Value);

                if (comando.ExecuteNonQuery() > 0)
                {
                    return "Aluno inserido com sucesso!";
                }
                else
                {
                    return "Não foi possivel inserir Aluno!";
                }
            }
            catch (Exception ex)
            {
                return "Erro ao inserir Aluno: " + ex.Message;
            }
        }
        public List<Aluno> BuscarAlunos(Conexao db, Aluno aluno)
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            try
            {
                string sql = "select Id, Nome, Idade, Cpf from Aluno;";
                SqlCommand comando = new SqlCommand(sql, _conn);

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
                        var idDb = reader.GetInt32(reader.GetOrdinal("Id"));

                        alunos.Add(new Aluno()
                        {
                            Nome = nomeDb,
                            Idade = idadeDb,
                            Cpf = cpfDb,
                            Id = idDb
                        });

                    }
                    return alunos;
                }
            }
            catch (Exception e)
            {

                throw;
            }
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }
        public string EditarAluno(Aluno aluno)
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            try
            {
                string sql = @"Update Aluno
                             SET Nome = @Nome, Idade = @Idade, Cpf = @Cpf
                             Where Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, _conn))
                {
                    comando.Parameters.AddWithValue("@Nome", aluno.Nome);
                    comando.Parameters.AddWithValue("@Idade", aluno.Idade);
                    comando.Parameters.AddWithValue("@Cpf", aluno.Cpf);
                    comando.Parameters.AddWithValue("@Id", aluno.Id);

                    comando.ExecuteNonQuery();
                    return "Aluno editado com sucesso";
                }

            }
            catch (Exception e)
            {
                return "Erro ao editar Aluno";
            }
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public string ApagarAluno(Conexao db, Aluno aluno)
        {
            try
            {
                string sql = @"Delete From aluno
                             WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, db.conn))
                {
                    comando.Parameters.AddWithValue("@Id", aluno.Id);

                    comando.ExecuteNonQuery();
                    return "Aluno excluido com sucesso";
                }
            }
            catch (Exception e)
            {
                return "Erro ao excluir Aluno";
            }
        }

        public static string InserirNota(Conexao db, Aluno aluno)
        {
            try
            {
                // Query SQL corrigida com a cláusula WHERE
                string sql = @"UPDATE Aluno SET Nota1 = @Nota1, Nota2 = @Nota2 WHERE Id = @Id";

                using (SqlCommand comando = new SqlCommand(sql, db.conn))
                {
                    // Adicionando os parâmetros da nota
                    comando.Parameters.AddWithValue("@Nota1", aluno.Nota1);
                    comando.Parameters.AddWithValue("@Nota2", aluno.Nota2);
                    // Adicionando o parâmetro do ID para saber qual aluno atualizar
                    comando.Parameters.AddWithValue("@Id", aluno.Id);

                    comando.ExecuteNonQuery();
                    return "Aluno editado com sucesso";
                }
            }
            catch (Exception e)
            {
                // Logar o erro pode ser útil para depuração
                Console.WriteLine($"Erro: {e.Message}");
                return "Erro ao editar Aluno";
            }
        }
        public List<Aluno> BuscarNota(Conexao db, Aluno aluno)
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            try
            {
                string sql = "SELECT Nota1, Nota2 FROM Aluno WHERE Id = @Id;";

                using (SqlCommand comando = new SqlCommand(sql, _conn))
                {
                    comando.Parameters.AddWithValue("@Id", aluno.Id);
                    List<Aluno> notas = new List<Aluno>();

                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nota1Db = reader.GetDecimal(reader.GetOrdinal("Nota1"));
                            var nota2Db = reader.GetDecimal(reader.GetOrdinal("Nota2"));

                            notas.Add(new Aluno()
                            {
                                Nota1 = nota1Db,
                                Nota2 = nota2Db
                            });
                        }
                    }
                    return notas;
                }
            }
            catch (Exception e)
            {
                return new List<Aluno>();
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
        }
    }
}
