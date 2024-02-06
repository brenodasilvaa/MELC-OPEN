using MELC.Core.Commons;
using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.DomainObjects.Materiais.Solidos
{
    internal static class SolidosFactory
    {
        internal static ICalculoSolidos GetCalculoSolidos(SolidoDto solido)
        {
            switch (solido.TipoSolido)
            {
                case TipoSolido.PerfilQuadrilatero:
                    return new CalculoPerfilQuadrilatero(solido);
                case TipoSolido.PerfilRedondo:
                    return new CalculoPerfilRedondo(solido);
                case TipoSolido.BarraRedonda:
                    return new CalculoBarraRedonda(solido);
                case TipoSolido.BarraSextavada:
                    return new CalculoBarraSextavada(solido);
                case TipoSolido.BarraQuadrilatera:
                    return new CalculoBarraQuadrilatera(solido);
                case TipoSolido.PerfilL:
                    return new CalculoPerfilL(solido);
                case TipoSolido.PerfilH:
                    return new CalculoPerfilH(solido);
                case TipoSolido.PerfilU:
                    return new CalculoPerfilU(solido);
                default:
                    throw new InvalidOperationException($"Não há implementação disponível para sólidos do tipo {solido.TipoSolido.GetDescription()}");
            }
        }
    }
}
