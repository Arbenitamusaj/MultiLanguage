using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Services.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationsController : ControllerBase
    {
        private readonly ILogger<LocalizationsController> _logger;
        private readonly ICultureService _cultureService;

        public LocalizationsController(ILogger<LocalizationsController> logger, ICultureService cultureService)
        {
            _logger = logger;
            _cultureService = cultureService;
        }

        [HttpGet("GetLocalizationResource")]
        public async Task<IActionResult> GetLocalizationResource(string key)
        {
            var culture = Request.Headers["Accept-Language"].ToString();
            var message = _cultureService.GetString(key, culture);
            return Ok(message);
        }

        [HttpGet("resources")]
        public async Task<ActionResult<IEnumerable<LocalizationResource>>> GetLocalizationResources()
        {
            var resources = _cultureService.GetLocalizationResources();
            return Ok(resources);
        }

        [HttpPost("resource")]
        public async Task<IActionResult> PostLocalizationResource(LocalizationResource resource)
        {
            _cultureService.AddLocalizationResource(resource);
            return CreatedAtAction(nameof(GetLocalizationResource), new { key = resource.Key }, resource);
        }

        [HttpPut("resource/{id}")]
        public async Task<IActionResult> PutLocalizationResource(int id, LocalizationResource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest();
            }

            _cultureService.UpdateLocalizationResource(resource);
            return NoContent();
        }

        [HttpDelete("resource/{id}")]
        public async Task<IActionResult> DeleteLocalizationResource(int id)
        {
            _cultureService.DeleteLocalizationResource(id);
            return NoContent();
        }

        [HttpGet("languages")]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            var languages = _cultureService.GetAllLanguages();
            return Ok(languages);
        }

        [HttpGet("language/{id}")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {
            var language = _cultureService.GetLanguageById(id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }

        [HttpPost("language")]
        public async Task<IActionResult> PostLanguage(Language language, [FromHeader] string translationApiKey)
        {
            _cultureService.AddLanguage(language, translationApiKey);
            return CreatedAtAction(nameof(GetLanguage), new { id = language.Id }, language);
        }

        [HttpPut("language/{id}")]
        public async Task<IActionResult> PutLanguage(int id, Language language)
        {
            if (id != language.Id)
            {
                return BadRequest();
            }

            _cultureService.UpdateLanguage(language);
            return NoContent();
        }

        [HttpDelete("language/{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            _cultureService.DeleteLanguage(id);
            return NoContent();
        }
    }
}
