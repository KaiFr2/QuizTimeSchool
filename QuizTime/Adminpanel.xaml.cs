﻿using System;
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
using System.Windows.Threading;

namespace QuizTime
{
    public partial class Adminpanel : Window
    {
        public static MainWindow MainWindowScherm;
        public int QuizId;
        private bool isTimerPaused = false;

        public Adminpanel()
        {
            InitializeComponent();
        }

        private int currentQuestionIndex = 0; // Keeps track of the current question index


        private void Vorige_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex--; // Decrement the index to move to the previous question

            if (currentQuestionIndex >= 0 && currentQuestionIndex < MainWindowScherm.currentQuiz.vragen.Count)
            {
                // If there are valid previous questions, update the content of the labels with the respective question text and answer options
                var currentQuestion = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex];

                // Update Vraagstelling
                MainWindowScherm.Vraagstelling.Content = currentQuestion.vraagtext;

                // Update Antwoordbox0
                MainWindowScherm.Antwoordbox0.Content = currentQuestion.antwoord[0];

                // Update Antwoordbox1
                MainWindowScherm.Antwoordbox1.Content = currentQuestion.antwoord[1];

                // Update Antwoordbox2
                MainWindowScherm.Antwoordbox2.Content = currentQuestion.antwoord[2];

                // Update Antwoordbox3
                MainWindowScherm.Antwoordbox3.Content = currentQuestion.antwoord[3];

                // Reset label visibility
                ResetLabelVisibility();

                // Update image
                UpdateQuestionImage(currentQuestion.imagePath);

                UpdateQuestionCounter();
            }
            else
            {
                // If there are no previous questions or the index is invalid, display a message or perform any other desired action
                MessageBox.Show("Geen vorige vragen meer!");
            }
        }

        private void Volgende_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex++; // Increment the index to move to the next question

            if (currentQuestionIndex < MainWindowScherm.currentQuiz.vragen.Count)
            {
                // If there are more questions, update the content of the labels with the respective question text and answer options
                var currentQuestion = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex];

                // Update Vraagstelling
                MainWindowScherm.Vraagstelling.Content = currentQuestion.vraagtext;

                // Update Antwoordbox0
                MainWindowScherm.Antwoordbox0.Content = currentQuestion.antwoord[0];

                // Update Antwoordbox1
                MainWindowScherm.Antwoordbox1.Content = currentQuestion.antwoord[1];

                // Update Antwoordbox2
                MainWindowScherm.Antwoordbox2.Content = currentQuestion.antwoord[2];

                // Update Antwoordbox3
                MainWindowScherm.Antwoordbox3.Content = currentQuestion.antwoord[3];

                // Reset label visibility
                ResetLabelVisibility();

                // Update image
                UpdateQuestionImage(currentQuestion.imagePath);

                UpdateQuestionCounter();
            }
            else
            {
                // If there are no more questions, display a message or perform any other desired action
                MessageBox.Show("Geen vragen meer!");
            }
        }

        private void UpdateQuestionImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                BitmapImage image = new BitmapImage(new Uri(imagePath));
                MainWindowScherm.Afbeeldinggame.Source = image;
            }
            else
            {
                MainWindowScherm.Afbeeldinggame.Source = null;
            }
        }






        private void Toon_Click(object sender, RoutedEventArgs e)
        {
            // Get the correct answer index from the current question
            int correctAnswerIndex = MainWindowScherm.currentQuiz.vragen[currentQuestionIndex].correctAntwoord;

            // Show all labels
            MainWindowScherm.Antwoordbox0.Visibility = Visibility.Visible;
            MainWindowScherm.Antwoordbox1.Visibility = Visibility.Visible;
            MainWindowScherm.Antwoordbox2.Visibility = Visibility.Visible;
            MainWindowScherm.Antwoordbox3.Visibility = Visibility.Visible;

            // Hide the incorrect labels
            switch (correctAnswerIndex)
            {
                case 0:
                    MainWindowScherm.Antwoordbox1.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox2.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox3.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    MainWindowScherm.Antwoordbox0.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox2.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox3.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    MainWindowScherm.Antwoordbox0.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox1.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox3.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    MainWindowScherm.Antwoordbox0.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox1.Visibility = Visibility.Collapsed;
                    MainWindowScherm.Antwoordbox2.Visibility = Visibility.Collapsed;
                    break;
            }

            // Force update of the UI layout
            MainWindowScherm.UpdateLayout();
        }
        private void ResetLabelVisibility()
        {
            // Reset the visibility of all labels to Visible
            MainWindowScherm.Antwoordbox0.Visibility = Visibility.Visible;
            MainWindowScherm.Antwoordbox1.Visibility = Visibility.Visible;
            MainWindowScherm.Antwoordbox2.Visibility = Visibility.Visible;
            MainWindowScherm.Antwoordbox3.Visibility = Visibility.Visible; 
        }

        public void UpdateQuestionCounter()
        {
            // Update the content of the question counter label
            int currentQuestionNumber = currentQuestionIndex + 1;
            int totalQuestions = MainWindowScherm.currentQuiz.vragen.Count;
            MainWindowScherm.QuestionCounterLabel.Content = $"Question {currentQuestionNumber} of {totalQuestions}";
        }
        private DispatcherTimer quizTimer; // Declare a DispatcherTimer for the quiz timer
        private TimeSpan timeRemaining; // Declare a variable to track the remaining time

        private void Tijdstart_Click(object sender, RoutedEventArgs e)
        {
            // Get the time value from JSON (assuming it's stored as a string)
            string timeInSecondsString = MainWindowScherm.currentQuiz.tijd;

            // Convert the string to an integer
            if (int.TryParse(timeInSecondsString, out int timeInSeconds))
            {
                // Create a TimeSpan object from the timeInSeconds value
                timeRemaining = TimeSpan.FromSeconds(timeInSeconds);

                // Initialize and start the quiz timer
                quizTimer = new DispatcherTimer();
                quizTimer.Interval = TimeSpan.FromSeconds(1);
                quizTimer.Tick += QuizTimer_Tick;
                quizTimer.Start();
            }
            else
            {
                // Handle invalid time format (e.g., display an error message)
            }
        }
        private void Tijdstop_Click(object sender, RoutedEventArgs e)
        {
            // Pause the quiz timer
            if (quizTimer != null && quizTimer.IsEnabled)
            {
                quizTimer.Stop();
                isTimerPaused = true;
            }
        }

        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            // Decrement the time remaining by one second
            timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1));

            // Update the timer display
            MainWindowScherm.TijdLabel.Content = "Tijd: " + timeRemaining.ToString();

            // Check if the timer has reached zero
            if (timeRemaining.TotalSeconds <= 0)
            {
                // Stop the timer
                quizTimer.Stop();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindowScherm.homescreen.Visibility = Visibility.Visible;
            MainWindowScherm.Speel.Visibility = Visibility.Hidden;
        }
    }
}
