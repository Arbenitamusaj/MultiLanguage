using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LanguageCode { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public bool IsDefault { get; set; }

        public bool IsSelected { get; set; }
    }
}
