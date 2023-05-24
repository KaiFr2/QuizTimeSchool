using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTime.Class
{
    public class quizopslaan
    {
        public int iD { get; set; }
        public string title { get; set; }

        public string beschrijving { get; set; }

        public string tijd { get; set; }

        public quizopslaan(int iD, string title, string beschrijving, string tijd)
        {
            this.iD = iD;
            this.title = title;
            this.beschrijving = beschrijving;
            this.tijd = tijd;
        }   
    }
}
