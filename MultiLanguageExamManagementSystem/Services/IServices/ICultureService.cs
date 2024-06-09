using MultiLanguageExamManagementSystem.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiLanguageExamManagementSystem.Services.IServices
{
    public interface ICultureService
    {
        string GetString(string key, string culture);
        void AddOrUpdateLocalizationResource(string key, string value, string culture);
        void RemoveLocalizationResource(string key, string culture);

        IEnumerable<Language> GetAllLanguages();
        Language GetLanguageById(int id);
        void AddLanguage(Language language, string translationApiKey);
        void UpdateLanguage(Language language);
        void DeleteLanguage(int id);

        IEnumerable<LocalizationResource> GetLocalizationResources();
        LocalizationResource GetLocalizationResourceById(int id);
        void AddLocalizationResource(LocalizationResource localizationResource);
        void UpdateLocalizationResource(LocalizationResource localizationResource);
        void DeleteLocalizationResource(int id);
    }
}
