using ProjetoJP;
using ProjetoJP.Data;
using System.Data.SqlClient;


Conexao db = new Conexao();

   db.Conectar();

AlunoRepositorio alunoRepositorio = new AlunoRepositorio(db.conn);

var teste = "";
int opcoes = 0;

while (opcoes != 5)
{
    opcoes = Menu();
    Console.Clear();
    switch (opcoes)
    {
        case 1:
            CadastrarAluno();
            break;
        case 2:

            break;
        case 3:

            break;
        case 4:

            break;
        case 5:
            Console.WriteLine("ENCERRANDO PROGRAMA....");
            break;
    }
}
Console.ReadKey();
static int Menu()
{
    Console.WriteLine("MENU DE OPÇÕES");
    Console.WriteLine("===================");
    Console.WriteLine("[1] Cadastrar Aluno");
    Console.WriteLine("[2] Consultar Aluno");
    Console.WriteLine("[3] Alterar dados do aluno");
    Console.WriteLine("[4] Excluir Aluno");
    Console.WriteLine("[5] Sair");

    int opcoes = int.Parse(Console.ReadLine());
    return opcoes;
}

void CadastrarAluno()
{
    Aluno aluno = new Aluno();

    Console.WriteLine("Preencha os dados solicitados do Aluno");

    Console.WriteLine("Nome Completo");
    aluno.nome = Console.ReadLine();

    Console.WriteLine("Idade");
    aluno.idade = int.Parse(Console.ReadLine());

    Console.WriteLine("Cpf");
    aluno.cpf = Console.ReadLine();

    AlunoRepositorio.InserirAluno(db, aluno);

}

void ConsultarAluno()
{
    List<Aluno> listaDeAlunos = new List<Aluno>();
    Console.WriteLine("Digite o cpf do aluno:");
    string cpfAluno = Console.ReadLine();

    foreach (var aluno in listaDeAlunos)
    {
        if (aluno.cpf == cpfAluno)
        {
            Console.WriteLine("ALUNO ENCONTRADO");
            Console.WriteLine($"Nome:{aluno.nome}");
            Console.WriteLine($"Idade:{aluno.idade}");
            Console.WriteLine($"Data de nascimento:{aluno.dataNascimento}");
            Console.WriteLine($"Cpf:{aluno.cpf}");
            Console.WriteLine($"Cep:{aluno.cep}");
            Console.WriteLine($"Endereço:{aluno.endereco}");
            Console.WriteLine($"Numero:{aluno.numero}");
            Console.WriteLine($"Bairro:{aluno.bairro}");
            Console.WriteLine($"Cidade:{aluno.cidade}");
            Console.WriteLine($"Estado:{aluno.estado}");
            Console.WriteLine($"Nota 1:{aluno.nota1}");
            Console.WriteLine($"Nota 2:{aluno.nota2}");
            Console.WriteLine($"Media:{aluno.media}");
        }
    }
    AlunoRepositorio.BuscarAlunos(db);
}