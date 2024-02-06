using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MELC.WebApp.MVC.Extensions;

namespace MELC.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected T ObterObjeto<T>(string dado, bool caseInsensitive = true)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = caseInsensitive
            };

            return JsonSerializer.Deserialize<T>(dado, options);
        }

        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado), 
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                case System.Net.HttpStatusCode.Forbidden:
                case System.Net.HttpStatusCode.NotFound:
                case System.Net.HttpStatusCode.InternalServerError:
                    throw new CustomHttpRequestException(response.StatusCode);

                case System.Net.HttpStatusCode.BadRequest:
                    return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
