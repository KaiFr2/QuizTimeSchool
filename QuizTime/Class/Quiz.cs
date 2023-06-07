using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizTime.Class
{
    public class quiz
    {
        public int iD { get; set; }
        public string title { get; set; }

        public string beschrijving { get; set; }

        //Time for each question.
        public string tijd { get; set; }

        public List<vraag> vragen { get; set; } = new List<vraag>(); 
        public quiz(int iD, string title, string beschrijving, string tijd)
        {
            this.iD = iD;
            this.title = title;
            this.beschrijving = beschrijving;
            this.tijd = tijd;
        }

        public void newVraag(string vraagtext_, string[] antwoord_, int correctAntwoord_)
        {
            int newID;

            if (vragen.Count > 0)
            {
                // Get the latest ID from the existing questions
                newID = vragen.Max(q => q.VraagID) + 1;
            }
            else
            {
                // If there are no existing questions, start with ID 1
                newID = 0;
            }

            vragen.Add(new vraag(newID, vraagtext_, antwoord_, correctAntwoord_));
        }

    }
}
