namespace MELC.Core.DomainObjects.Dtos
{
    public class MaterialDesenhoDto
    {
        public Guid Id { get; set; }
        public Guid DesenhoId { get; set; }
        public Guid CriadoPorId { get; set; }
        public Guid MaterialId { get; set; }
        public Guid SolidoId { get; set; }
        public decimal Quantidade { get; set; }
        public double Peso { get; set; }
        public decimal Valor { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public DesenhoDto Desenho { get; set; }
        public UserDto CriadoPor { get; set; }
        public MaterialDto Material { get; set; }
        public SolidoDto Solido { get; set; }

        public RetornoDto<bool> AtualizarPropriedadesMaterialDesenho()
        {
            var calculoVolume = Solido.CalcularVolume();

            if (!calculoVolume.Success)
                return new RetornoDto<bool> { Success = false, Message = calculoVolume.Message };

            if (Solido.Volume <= 0)
                return new RetornoDto<bool> { Success = false, Message = "Não foi possível realizar esta operação, as dimensões estão incoerentes" };

            Peso = Material.Densidade * (Solido.Volume / 1000000) * (double)Quantidade;

            return new RetornoDto<bool> { Success = true };
        }
    }
}
