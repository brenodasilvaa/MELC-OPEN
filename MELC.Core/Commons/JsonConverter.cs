using System.Text.Json;

namespace MELC.Core.Commons
{
    public static class JsonConverter
    {
        public static string Serializar(object model)
        {
            return JsonSerializer.Serialize(model);
        }
    }
}
