using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace MultiLanguageExamManagementSystem.Helpers
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var acceptLanguageHeader = context.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrEmpty(acceptLanguageHeader))
            {
                var cultures = acceptLanguageHeader.Split(',')
                    .Select(StringWithQualityHeaderValue.Parse)
                    .OrderByDescending(s => s.Quality ?? 1)
                    .Select(s => s.Value.ToString())
                    .ToList();

                if (cultures.Any())
                {
                    var culture = new CultureInfo(cultures.First());
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
            }

            await _next(context);
        }
    }
}
