using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizTime.Class
{
    public  class vraag
    {   
        public int VraagID { get; set; }
        public string vraagtext { get; set; }
        public string[] antwoord { get; set; } = new string[4];
        public int correctAntwoord { get; set; }

        public vraag (int VraagiD_, string vraagtext_, string[] antwoord_, int correctAntwoord_)
        {
            /*for (int i = 0; i < antwoord.Length; i++)
            {
                antwoord[i] = antwoord_[i];
            }*/
            VraagID = VraagiD_;
            vraagtext = vraagtext_;
            correctAntwoord = correctAntwoord_;
        }
        

    }
}
