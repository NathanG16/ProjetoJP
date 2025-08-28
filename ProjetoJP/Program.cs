using ProjetoJP;
using ProjetoJP.Data;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Globalization;


Conexao db = new Conexao();

   db.Conectar();

AlunoRepositorio alunoRepositorio = new AlunoRepositorio(db.conn);

var testeMenu = "";
int menu = 0;

while (menu != 4)
{
    menu = MenuPrincipal();
    Console.Clear();
    switch (menu)
    {
        case 1:
            Administrador();
            break;
        case 2:
            Professor();
            break;
        case 3:
            Aluno();
            break;
        case 4:
            Console.WriteLine("ENCERRANDO PROGRAMA....");
            break;
    }
}
Console.ReadKey();
static int MenuPrincipal()
{
    Console.WriteLine("MENU DE OPÇÕES");
    Console.WriteLine("===================");
    Console.WriteLine("[1] Administrador");
    Console.WriteLine("[2] Professor");
    Console.WriteLine("[3] Aluno");
    Console.WriteLine("[4] Sair");

    int opcoes = int.Parse(Console.ReadLine());
    return opcoes;
}
void Administrador()
{
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
                ConsultarAluno();
                break;
            case 3:
                AlterarAluno();
                break;
            case 4:
                ExcluirAluno();
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

        Console.WriteLine("Nome Completo:");
        aluno.Nome = Console.ReadLine();

        Console.WriteLine("Idade:");
        aluno.Idade = int.Parse(Console.ReadLine());

        Console.WriteLine("CPF:");
        aluno.Cpf = Console.ReadLine();

        Console.WriteLine("Data de Nascimento:");
        aluno.DataNascimento = Console.ReadLine();

        Console.WriteLine("CEP:");
        aluno.Cep = Console.ReadLine();

        Console.WriteLine("Endereço:");
        aluno.Endereco = Console.ReadLine();

        Console.WriteLine("Número:");
        aluno.Numero = int.Parse(Console.ReadLine());

        Console.WriteLine("Bairro:");
        aluno.Bairro = Console.ReadLine();

        Console.WriteLine("Cidade:");
        aluno.Cidade = Console.ReadLine();

        Console.WriteLine("Estado:");
        aluno.Estado = Console.ReadLine();

        Console.WriteLine("Nota1 (pressione Enter para deixar em branco):");
        string nota1Input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nota1Input))
        {
            decimal nota1Value;
            if (decimal.TryParse(nota1Input.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out nota1Value))
            {
                aluno.Nota1 = nota1Value;
            }
            else
            {
                Console.WriteLine("Atenção: A nota 1 digitada não é um número válido.");
                aluno.Nota1 = null;
            }
        }
        else
        {
            aluno.Nota1 = null;
        }

        Console.WriteLine("Nota2 (pressione Enter para deixar em branco):");
        string nota2Input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(nota2Input))
        {
            decimal nota2Value;
            if (decimal.TryParse(nota2Input.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out nota2Value))
            {
                aluno.Nota2 = nota2Value;
            }
            else
            {
                Console.WriteLine("Atenção: A nota 2 digitada não é um número válido.");
                aluno.Nota2 = null;
            }
        }
        else
        {
            aluno.Nota2 = null;
        }

        AlunoRepositorio.InserirAluno(db, aluno);
        Console.WriteLine("Aluno inserido com sucesso!");
    }

    void ConsultarAluno()
    {
        Aluno aluno = new Aluno();

        Console.WriteLine("Informe o nome do aluno que deseja buscar");
        aluno.Nome = Console.ReadLine();

        var alunos = alunoRepositorio.BuscarAlunos(db, aluno);

        aluno = alunos.Where(x => x.Nome.Contains(aluno.Nome)).FirstOrDefault();

        Console.WriteLine($"Dados de {aluno.Nome}");
        Console.WriteLine($"Idade {aluno.Idade}");
        Console.WriteLine($"Cpf {aluno.Cpf}");
        Console.WriteLine($"Id {aluno.Id}");

        Console.ReadLine();
    }

    void AlterarAluno()
    {
        Aluno aluno = new Aluno();
        Console.Write("Informe o nome do aluno que deseja alterar: ");
        string nomeParaBuscar = Console.ReadLine();

        var alunos = alunoRepositorio.BuscarAlunos(db, aluno);
        aluno = alunos.Where(x => x.Nome.Contains(nomeParaBuscar)).FirstOrDefault();

        if (aluno != null)
        {
            Console.WriteLine($"Aluno encontrado, Digite os novos dados do {aluno.Nome} aperte enter se quiser manter algum dado");

            Console.WriteLine($"Nome:{aluno.Nome}");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                aluno.Nome = novoNome;
            }

            Console.WriteLine($"Idade:{aluno.Idade}");
            string novaIdade = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaIdade))
            {
                aluno.Idade = int.Parse(novaIdade);
            }

            Console.WriteLine($"Cpf:{aluno.Cpf}");
            string novoCpf = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoCpf))
            {
                aluno.Cpf = novoCpf;
            }
            Console.WriteLine($"Dados de {aluno.Nome} atualizados com sucesso!");
            string resultado = alunoRepositorio.EditarAluno(aluno);
        }
        else
        {
            Console.WriteLine($"Aluno com o nome {nomeParaBuscar} não encontrado.");
        }
        Console.ReadLine();
    }

    void ExcluirAluno()
    {
        Aluno aluno = new Aluno();
        Console.Write("Informe o nome do aluno que deseja excluir: ");
        string nomeParaBuscar = Console.ReadLine();

        var alunos = alunoRepositorio.BuscarAlunos(db, aluno);
        aluno = alunos.Where(x => x.Nome.Contains(nomeParaBuscar)).FirstOrDefault();
        if (aluno != null)
        {
            Console.WriteLine($"Aluno encontrado, deseja realmente excluir os dados de {aluno.Nome}?");
            Console.WriteLine("aperte 1 para sim e 2 para não");
            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                alunoRepositorio.ApagarAluno(db, aluno);
                Console.WriteLine("Dados excluidos aperte enter para voltar");
                Console.ReadLine();
            }
            if (opcao == 2)
            {
                Console.WriteLine("os dados foram mantidos");
            }

        }
        else
        {
            Console.WriteLine($"Aluno com o nome '{nomeParaBuscar}' não encontrado.");
        }
    }
}

void Professor()
{
    Aluno aluno = new Aluno();
    Console.Write("Informe o nome do aluno que deseja alterar: ");
    string nomeParaBuscar = Console.ReadLine();

    var alunos = alunoRepositorio.BuscarAlunos(db, aluno);
    aluno = alunos.Where(x => x.Nome.Contains(nomeParaBuscar)).FirstOrDefault();

    if (aluno != null)
    {
        Console.WriteLine($"Aluno encontrado, Digite os novos dados do {aluno.Nome} aperte enter se quiser manter algum dado");

        Console.WriteLine($"Nota 1:{aluno.Nota1}");
        string novaNota1 = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(novaNota1))
        {
            aluno.Nota1 = int.Parse(novaNota1);
        }

        Console.WriteLine($"Nota 2:{aluno.Nota2}");
        string novaNota2 = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(novaNota2))
        {
            aluno.Nota2 = int.Parse(novaNota2);
        }

        Console.WriteLine($"Notas atrualizadas de {aluno.Nome} com sucesso!");
        string resultado = AlunoRepositorio.InserirNota(db, aluno);
    }
    else
    {
        Console.WriteLine($"Aluno com o nome {nomeParaBuscar} não encontrado.");
    }
    Console.ReadLine();
}

void Aluno()
{
    // 1. Objeto para buscar o aluno pelo nome.
    Aluno alunoParaBuscar = new Aluno();

    Console.WriteLine("Informe o nome do aluno que deseja buscar:");
    alunoParaBuscar.Nome = Console.ReadLine();

    // 2. Busca a lista de alunos (com dados básicos).
    var listaDeAlunos = alunoRepositorio.BuscarAlunos(db, alunoParaBuscar);

    // 3. Encontra o aluno na lista.
    var alunoEncontrado = listaDeAlunos
        .Where(x => x.Nome.Contains(alunoParaBuscar.Nome))
        .FirstOrDefault();

    // 4. Se o aluno foi encontrado, busca as notas dele.
    if (alunoEncontrado != null)
    {
        // 5. Busca as notas. O seu método BuscarNota retorna uma lista.
        var listaDeNotas = alunoRepositorio.BuscarNota(db, alunoEncontrado);

        // 6. Se a busca por notas retornou algo, atualiza o objeto 'alunoEncontrado'.
        if (listaDeNotas != null && listaDeNotas.Any())
        {
            var notasDoAluno = listaDeNotas.FirstOrDefault();

            // Aqui, você atualiza as notas do objeto que você já tem.
            alunoEncontrado.Nota1 = notasDoAluno.Nota1;
            alunoEncontrado.Nota2 = notasDoAluno.Nota2;

            // 7. Agora, faz as verificações e mostra as notas.
            if (alunoEncontrado.Nota1.HasValue && alunoEncontrado.Nota2.HasValue)
            {
                double nota1 = (double)alunoEncontrado.Nota1.Value;
                double nota2 = (double)alunoEncontrado.Nota2.Value;
                double media = (nota1 + nota2) / 2;

                Console.WriteLine($"Nome: {alunoEncontrado.Nome}");
                Console.WriteLine($"Nota1: {nota1}");
                Console.WriteLine($"Nota2: {nota2}");
                Console.WriteLine($"Média: {media}");
            }
            else
            {
                Console.WriteLine($"As notas do aluno {alunoEncontrado.Nome} não foram cadastradas.");
            }
        }
        else
        {
            Console.WriteLine($"As notas do aluno {alunoEncontrado.Nome} não foram encontradas.");
        }
    }
    else
    {
        Console.WriteLine($"Aluno com o nome '{alunoParaBuscar.Nome}' não foi encontrado.");
    }

    Console.ReadLine();
}