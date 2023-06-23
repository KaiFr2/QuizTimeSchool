using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

public class vraag
{
    public int VraagID { get; set; }
    public string vraagtext { get; set; }
    public string[] antwoord { get; set; } = new string[4];
    public int correctAntwoord { get; set; }
    public string imagePath { get; set; } // New property for image path

    public vraag(int VraagID_, string vraagtext_, string[] antwoord_, int correctAntwoord_, string imagePath_) 
    {
        if (antwoord_ != null)
        {
            for (int i = 0; i < antwoord.Length; i++)
            {
                antwoord[i] = antwoord_[i];
            }
        }
        VraagID = VraagID_;
        vraagtext = vraagtext_;
        correctAntwoord = correctAntwoord_;
        imagePath = imagePath_; // Assign the image path to the property
    }
}
