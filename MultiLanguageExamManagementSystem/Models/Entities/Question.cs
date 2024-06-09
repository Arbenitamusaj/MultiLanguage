using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Question
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Text { get; set; }
        [MaxLength(200)]
        public string CorrectAnswer { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
