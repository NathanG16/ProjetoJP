namespace ProjetoJP
{
    public class Pessoa
    {
        public int codigo { get; set; }
        public string   nome { get; set; }
        public string cpf { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
