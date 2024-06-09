using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MultiLanguageExamManagementSystem.Services
{
    public static class TranslationService
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<string> TranslateText(string text, string sourceLang, string targetLang, string apiKey)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var content = new StringContent(
                $"{{\"text\":\"{text}\", \"source_lang\":\"{sourceLang}\", \"target_lang\":\"{targetLang}\"}}",
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("https://api.deepl.com/v2/translate", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseBody);
                return json["translations"]?[0]?["text"]?.ToString();
            }
            return text;
        }
    }
}
