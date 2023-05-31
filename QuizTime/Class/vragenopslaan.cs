using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTime.Class
{
    internal class vragenopslaan
    {
        public int quizid { get; set; }
        public int VraagID { get; set; }

        public string title { get; set; }
        public string vraag1 { get; set; }
        public string antwoord1 { get; set; }
        public string antwoord2 { get; set; }
        public string antwoord3 { get; set; }
        public string antwoord4 { get; set; }

        public string check { get; set; }
    }
}
