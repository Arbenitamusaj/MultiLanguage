using MultiLanguageExamManagementSystem.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Username { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Exam> CreatedExams { get; set; }
        public ICollection<TakenExam> TakenExams { get; set; }
    }
}
