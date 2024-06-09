using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<TakenExam> TakenExams { get; set; }
    }
}
