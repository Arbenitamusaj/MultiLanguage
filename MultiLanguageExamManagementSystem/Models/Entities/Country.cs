using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
