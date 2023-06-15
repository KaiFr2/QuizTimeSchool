using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizTime
{
    public partial class Adminpanel : Window
    {
        public static MainWindow MainWindowScherm;
        public int QuizId;
        public Adminpanel()
        {
            InitializeComponent();
        }


        private void Vorige_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex--; // Decrement the index to move to the previous question

            if (currentQuestionIndex >= 0 && currentQuestionIndex < MainWindowScherm.currentQuiz.vragen.Count)
            {
                // If there are valid previous questions, update the content of the labels with the respective question text

                // Update Antwoordbox0
                MainWindowScherm.Antwoordbox0.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;

                // Update Antwoordbox1
                MainWindowScherm.Antwoordbox1.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;

                // Update Antwoordbox2
                MainWindowScherm.Antwoordbox2.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;

                // Update Antwoordbox3
                MainWindowScherm.Antwoordbox3.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;
            }
            else
            {
                // If there are no previous questions or the index is invalid, display a message or perform any other desired action
                MessageBox.Show("No previous questions!");
            }
        }


        private int currentQuestionIndex = 0; // Keeps track of the current question index

        private void Volgende_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex++; // Increment the index to move to the next question

            if (currentQuestionIndex < MainWindowScherm.currentQuiz.vragen.Count)
            {
                // If there are more questions, update the content of the labels with the respective question text

                // Update Antwoordbox0
                MainWindowScherm.Antwoordbox0.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;

                // Update Antwoordbox1
                MainWindowScherm.Antwoordbox1.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;

                // Update Antwoordbox2
                MainWindowScherm.Antwoordbox2.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;

                // Update Antwoordbox3
                MainWindowScherm.Antwoordbox3.Content = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].vraagtext;
            }
            else
            {
                // If there are no more questions, display a message or perform any other desired action
                MessageBox.Show("No more questions!");
            }
        }



        private void Toon_Click(object sender, RoutedEventArgs e)
        {
           
        }


        private void Tijdstart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tijdstop_Click(object sender, RoutedEventArgs e)
        {

         }
    }
}
