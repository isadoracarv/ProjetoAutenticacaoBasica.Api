namespace ProjetoAutenticacaoBasica.Api.Models.DTOs
{
    public class PassageiroDtos
    {
    }

    public class CriarPassageiroDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }

    public class InfoPassageiroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
