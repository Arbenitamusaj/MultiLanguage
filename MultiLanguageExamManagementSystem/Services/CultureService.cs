
using MultiLanguageExamManagementSystem.Data.UnitOfWork;
using MultiLanguageExamManagementSystem.Models.Entities;
using MultiLanguageExamManagementSystem.Services.IServices;


namespace MultiLanguageExamManagementSystem.Services
{
    public class CultureService : ICultureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CultureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region String Localization

        public string GetString(string key, string culture)
        {
            var resource = _unitOfWork.Repository<LocalizationResource>()
                                        .GetByCondition(lr => lr.Key == key && lr.Language.LanguageCode == culture)
                                        .FirstOrDefault();
            return resource?.Value;
        }

        public void AddOrUpdateLocalizationResource(string key, string value, string culture)
        {
            var language = _unitOfWork.Repository<Language>().GetByCondition(l => l.LanguageCode == culture).FirstOrDefault();
            if (language == null) return;

            var resource = _unitOfWork.Repository<LocalizationResource>()
                                        .GetByCondition(lr => lr.Key == key && lr.LanguageId == language.Id)
                                        .FirstOrDefault();

            if (resource == null)
            {
                resource = new LocalizationResource
                {
                    Key = key,
                    Value = value,
                    LanguageId = language.Id,
                    Language = language
                };
                _unitOfWork.Repository<LocalizationResource>().Create(resource);
            }
            else
            {
                resource.Value = value;
                _unitOfWork.Repository<LocalizationResource>().Update(resource);
            }

            _unitOfWork.Complete();
        }

        public void RemoveLocalizationResource(string key, string culture)
        {
            var language = _unitOfWork.Repository<Language>().GetByCondition(l => l.LanguageCode == culture).FirstOrDefault();
            if (language == null) return;

            var resource = _unitOfWork.Repository<LocalizationResource>()
                                        .GetByCondition(lr => lr.Key == key && lr.LanguageId == language.Id)
                                        .FirstOrDefault();
            if (resource != null)
            {
                _unitOfWork.Repository<LocalizationResource>().Delete(resource);
                _unitOfWork.Complete();
            }
        }

        #endregion

        #region Languages

        public IEnumerable<Language> GetAllLanguages()
        {
            return _unitOfWork.Repository<Language>().GetAll();
        }

        public Language GetLanguageById(int id)
        {
            return _unitOfWork.Repository<Language>().GetByCondition(l => l.Id == id).FirstOrDefault();
        }

        public void AddLanguage(Language language, string translationApiKey)
        {
            _unitOfWork.Repository<Language>().Create(language);
            _unitOfWork.Complete();
            SeedLanguageWithTranslations(language, translationApiKey);
        }

        public void UpdateLanguage(Language language)
        {
            _unitOfWork.Repository<Language>().Update(language);
            _unitOfWork.Complete();
        }

        public void DeleteLanguage(int id)
        {
            var language = _unitOfWork.Repository<Language>().GetByCondition(l => l.Id == id).FirstOrDefault();
            if (language != null)
            {
                _unitOfWork.Repository<Language>().Delete(language);
                _unitOfWork.Complete();
            }
        }

        #endregion

        #region Localization Resources

        public IEnumerable<LocalizationResource> GetLocalizationResources()
        {
            return _unitOfWork.Repository<LocalizationResource>().GetAll();
        }

        public LocalizationResource GetLocalizationResourceById(int id)
        {
            return _unitOfWork.Repository<LocalizationResource>().GetByCondition(lr => lr.Id == id).FirstOrDefault();
        }

        public void AddLocalizationResource(LocalizationResource localizationResource)
        {
            _unitOfWork.Repository<LocalizationResource>().Create(localizationResource);
            _unitOfWork.Complete();
            TranslateAndSeedResource(localizationResource);
        }

        public void UpdateLocalizationResource(LocalizationResource localizationResource)
        {
            _unitOfWork.Repository<LocalizationResource>().Update(localizationResource);
            _unitOfWork.Complete();
            TranslateAndSeedResource(localizationResource);
        }

        public void DeleteLocalizationResource(int id)
        {
            var resource = _unitOfWork.Repository<LocalizationResource>().GetByCondition(lr => lr.Id == id).FirstOrDefault();
            if (resource != null)
            {
                _unitOfWork.Repository<LocalizationResource>().Delete(resource);
                _unitOfWork.Complete();
            }
        }

        private void SeedLanguageWithTranslations(Language language, string translationApiKey)
        {
            var existingResources = _unitOfWork.Repository<LocalizationResource>().GetByCondition(lr => lr.Language.LanguageCode == "en");
            foreach (var resource in existingResources)
            {
                var translatedText = TranslationService.TranslateText(resource.Value, "en", language.LanguageCode, translationApiKey).Result;
                AddOrUpdateLocalizationResource(resource.Key, translatedText, language.LanguageCode);
            }
        }

        private void TranslateAndSeedResource(LocalizationResource resource)
        {
            var languages = _unitOfWork.Repository<Language>().GetAll();
            foreach (var lang in languages)
            {
                if (lang.LanguageCode != "en")
                {
                    var translatedText = TranslationService.TranslateText(resource.Value, "en", lang.LanguageCode, "YourTranslationApiKey").Result;
                    AddOrUpdateLocalizationResource(resource.Key, translatedText, lang.LanguageCode);
                }
            }
        }

        #endregion
    }
}
