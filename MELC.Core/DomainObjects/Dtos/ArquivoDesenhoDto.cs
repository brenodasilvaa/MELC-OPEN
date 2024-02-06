namespace MELC.Core.DomainObjects.Dtos
{
    public class ArquivoDesenhoDto
    {
        public Guid Id { get; set; }
        public Guid DesenhoId { get; set; }
        public string NomeArquivo { get; set; }
        public string CaminhoArquivo { get; set; }
        public string Extensao => Path.GetExtension(CaminhoArquivo).Trim();
        public string Base64 { get; set; }
        public DateTime Created { get; set; }
    }
}
