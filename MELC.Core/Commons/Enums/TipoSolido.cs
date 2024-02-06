using System.ComponentModel;

namespace MELC.Core.Commons.Enums
{
    public enum TipoSolido
    {
        [Description("Tubo quadrado/retangular")]
        PerfilQuadrilatero = 0,
        [Description("Tubo redondo")]
        PerfilRedondo = 1,
        [Description("Barra redonda")]
        BarraRedonda = 2,
        [Description("Barra sextavada")]
        BarraSextavada = 3,
        [Description("Barra/Chapa quadrada/retangular")]
        BarraQuadrilatera = 4,
        [Description("Perfil L")]
        PerfilL = 5,
        [Description("Perfil H")]
        PerfilH = 6,
        [Description("Perfil U")]
        PerfilU = 7
    }
}
