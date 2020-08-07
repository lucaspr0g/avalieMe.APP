namespace AvalieMe.APP.Models
{
    public class NovoTeste
    {
        public long UserId { get; set; }
        public string Categoria { get; set; }
        public bool? MultiplasPessoas { get; set; }
        public string PosicaoPessoa { get; set; }
        public byte[] Imagem { get; set; }
    }
}
