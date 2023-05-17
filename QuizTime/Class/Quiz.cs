using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTime.Class
{
    using System;
    using System.Collections.Generic;

    public class Quiz
    {
        public int Id { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(int id, List<Question> questions)
        {
            Id = id;
            Questions = questions;
        }
    }

    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(int id, string questionText, List<Answer> answers)
        {
            Id = id;
            QuestionText = questionText;
            Answers = answers;
        }
    }

    public class Answer
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(string answerText, bool isCorrect)
        {
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }

}
