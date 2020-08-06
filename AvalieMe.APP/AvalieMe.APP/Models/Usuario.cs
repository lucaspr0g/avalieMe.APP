namespace AvalieMe.APP.Models
{
    public class Usuario : RetornoAPI
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }
    }
}
