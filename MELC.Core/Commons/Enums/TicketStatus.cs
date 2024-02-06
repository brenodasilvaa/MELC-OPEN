using System.ComponentModel;

namespace MELC.Core.Commons.Enums
{
    public enum Status
    {
        [Description("Em fabricação")]
        InProgress = 0,
        [Description("Parado")]
        Stopped = 1,
        [Description("Finalizado")]
        Finished = 2,
        [Description("Abandonado")]
        Abandonned = 3,
        [Description("Faturado")]
        Billed = 4
    }
}
