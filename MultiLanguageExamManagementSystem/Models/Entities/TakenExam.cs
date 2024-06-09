using System.ComponentModel.DataAnnotations;

namespace MultiLanguageExamManagementSystem.Models.Entities
{
    public class TakenExam
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public bool IsCompleted { get; set; }
    }
}
