using Microsoft.Win32;
using Newtonsoft.Json;
using QuizTime.Class;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace QuizTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string checked_answer = "";
        private int currentID = 1;
        public quiz currentQuiz;
        private DispatcherTimer quizTimer;
        int quizID;
        string quizzz;
        string imagePath;
        //Adminpanel adminWindow = new Adminpanel();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Begin scherm knop
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            maaklijst.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }

        //Het knop om naar het datagrid te gaan en json ophalen en alle quizzes laten zien
        private void btnspeellijst_Click(object sender, RoutedEventArgs e)
        {

            homescreen.Visibility = Visibility.Hidden;
            Kieslijst.Visibility = Visibility.Visible;
            //leest het json file en maakt er een datagrid van
            var SaveDataList = json.readAllQuizes();

            dgAllQuizzes.ItemsSource = null;
            dgAllQuizzes.ItemsSource = SaveDataList;
        }


        //Het opslaan van de quiz gegevens en dan door gaan naar de vragen van de quiz
        private void Maakquiz_Click(object sender, RoutedEventArgs e)
        {
            homescreen.Visibility = Visibility.Hidden;
            maaklijst.Visibility = Visibility.Visible;

            //clear alle textboxes
            TitleQTB.Clear();
            Beschrijving.Clear();
            tijd.Clear();
            Vraagtextbox.Clear();
            Antwoord0.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();
        }

        //Het optellen van de quizID
        public void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Check if all textboxes have valid input
            if (string.IsNullOrWhiteSpace(TitleQTB.Text))
            {
                MessageBox.Show("Vul de titel in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Beschrijving.Text))
            {
                MessageBox.Show("Vul een beschrijving.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tijd.Text))
            {
                MessageBox.Show("Vul een tijd in.");
                return;
            }

            // Additional validation logic if needed

            //quiz id
            quizzz = File.ReadAllText("count.txt");
            quizID = int.Parse(quizzz);
            //opslaan quiz items
            currentQuiz = new quiz(quizID, TitleQTB.Text, Beschrijving.Text, tijd.Text);

            maaklijst.Visibility = Visibility.Hidden;
            maaklijst2.Visibility = Visibility.Visible;
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files  (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;

                QuizImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        //Het opslaan van de vragen
        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            // Check if all textboxes have valid input
            if (string.IsNullOrWhiteSpace(Vraagtextbox.Text))
            {
                MessageBox.Show("Vul een vraag in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Antwoord0.Text) || string.IsNullOrWhiteSpace(Antwoord1.Text) || string.IsNullOrWhiteSpace(Antwoord2.Text) || string.IsNullOrWhiteSpace(Antwoord3.Text))
            {
                MessageBox.Show("Vul alle antwoorden in.");
                return;
            }

            // Check if a correct answer is selected
            if (!(Check0.IsChecked == true || Check1.IsChecked == true || Check2.IsChecked == true || Check3.IsChecked == true))
            {
                MessageBox.Show("Kies een correct antwoord.");
                return;
            }

            // Check if an image is selected
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                MessageBox.Show("Selecteer een afbeelding.");
                return;
            }

            // Additional validation logic if needed

            // Store answer options in an array
            string[] vraagOpties = new string[4];
            vraagOpties[0] = Antwoord0.Text;
            vraagOpties[1] = Antwoord1.Text;
            vraagOpties[2] = Antwoord2.Text;
            vraagOpties[3] = Antwoord3.Text;

            // Call newVraag method with the necessary parameters, including the image path
            currentQuiz.newVraag(Vraagtextbox.Text, vraagOpties, int.Parse(checked_answer), imagePath);

            // Clear the image path after assigning it to the question
            imagePath = string.Empty;

            // Clear the input fields for the next question
            Vraagtextbox.Clear();
            Antwoord0.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();

            // Clear the selected answer checkbox
            Check0.IsChecked = false;
            Check1.IsChecked = false;
            Check2.IsChecked = false;
            Check3.IsChecked = false;

            // Clear the displayed image
            QuizImage.Source = null;

            // Display "Vraag opgeslagen" message
            MessageBox.Show("Vraag opgeslagen");
        }




        //Het opsturen van de quiz data naar json.

        private void Quizopslaan_click(object sender, RoutedEventArgs e)
        {
            // Add listQuizVragen or the appropriate questions list to SaveDataList
            json.WriteDataToFile(currentQuiz);

            maaklijst2.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }



        //Checkbox checken knop
        private void onCheckBoxCheck(object sender, RoutedEventArgs e)
        {
            //checkbox opslaan van de goede antwoord
            Check0.IsChecked = false;
            Check1.IsChecked = false;
            Check2.IsChecked = false;
            Check3.IsChecked = false;

            CheckBox current = (CheckBox)sender;
            current.IsChecked = true;

            checked_answer = current.Name.Replace("Check", "");
        }

        //Textboxes clearen
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Clear the image path
            imagePath = string.Empty;

            // Clear the image source
            QuizImage.Source = null;

            // Clear all the text fields
            Vraagtextbox.Clear();
            Antwoord0.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();
        }


        //Terug naar home button
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Back button
            maaklijst2.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }

       
        //Het spelen van de quiz knop
        private void playbutton(object sender, RoutedEventArgs e)
        {
            // Hide the Kieslijst and show the Speel
            Kieslijst.Visibility = Visibility.Hidden;
            Speel.Visibility = Visibility.Visible;

            // Show the admin panel
            Adminpanel ControlPanelWindow = new Adminpanel();
            Adminpanel.MainWindowScherm = this;
            ControlPanelWindow.Show();

            // Pakt de ID van de quiz
            Button button = sender as Button;
            var id = button.Tag;

            // Roept de functie op voor het ophalen van de juiste quiz ID
            currentQuiz = json.FetchQuiz((int)id);

            // displayed het vraag
            Vraagstelling.Content = currentQuiz.vragen[0].vraagtext;

            // Variable for time
            var timerValue = currentQuiz.tijd;

            // Timer display
            TijdLabel.Content = "Tijd: " + timerValue;

            string imagePath = currentQuiz.vragen[0].imagePath;
            if (!string.IsNullOrEmpty(imagePath))
            {
                Afbeeldinggame.Source = new BitmapImage(new Uri(imagePath));
            }

            ControlPanelWindow.UpdateQuestionCounter();

            // loop waar de antwoorden doorheen worden gekeken en dat in de labels worden gezet
            for (int i = 0; i < currentQuiz.vragen[0].antwoord.Length; i++)
            {
                var lbl = Speel.FindName("Antwoordbox" + i.ToString()) as Label;
                lbl.Content = currentQuiz.vragen[0].antwoord[i];
            }
        }


        private int currentIndex = 0; // Track the current question index

        private int currentQuestionIndex = 0;

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuiz != null && currentQuiz.vragen.Count > 0)
            {
                currentQuestionIndex++;

                if (currentQuestionIndex < currentQuiz.vragen.Count)
                {
                    // Display the next question and answers
                    UpdateQuestionAndAnswers(currentQuestionIndex);
                }
                else
                {
                    // No more questions
                    MessageBox.Show("Er zijn geen vragen meer.");
                }
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuiz != null && currentQuiz.vragen.Count > 0)
            {
                currentQuestionIndex--;

                if (currentQuestionIndex >= 0)
                {
                    // Display the previous question and answers
                    UpdateQuestionAndAnswers(currentQuestionIndex);
                }
                else
                {
                    // No previous questions
                    MessageBox.Show("Geen vorige vragen meer.");
                }
            }
        }

        private void UpdateQuestionAndAnswers(int questionIndex)
        {
            var question = currentQuiz.vragen[questionIndex];

            // Update the question text
            EditVraag.Text = question.vraagtext;

            // Update the answer textboxes and checkboxes
            if (question.antwoord.Length >= 4)
            {
                EditAntwoord0.Text = question.antwoord[0];
                EditAntwoord1.Text = question.antwoord[1];
                EditAntwoord2.Text = question.antwoord[2];
                EditAntwoord3.Text = question.antwoord[3];

                // Optionally, set the correct checkboxes based on the correctAntwoord value
                EditCheck0.IsChecked = question.correctAntwoord == 0;
                EditCheck1.IsChecked = question.correctAntwoord == 1;
                EditCheck2.IsChecked = question.correctAntwoord == 2;
                EditCheck3.IsChecked = question.correctAntwoord == 3;
            }
        }

        private void EditUploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                // Update the image source of the image label or control
                Afbeeldingbewerk.Source = new BitmapImage(new Uri(imagePath));

                // Optionally, you can update the image path in the current question object
                var question = currentQuiz.vragen[currentQuestionIndex];
                question.imagePath = imagePath;
            }
        }

        private void Bewerkclick(object sender, RoutedEventArgs e)
        {
            Kieslijst.Visibility = Visibility.Hidden;
            Bewerkpg.Visibility = Visibility.Visible;

            // Get the selected quiz ID from the data grid
            Button button = sender as Button;
            if (button != null && button.Tag is int id)
            {
                currentQuiz = json.FetchQuiz(id);

                // Display the quiz details
                if (currentQuiz != null)
                {
                    // Display the title, description, and time
                    TitleEdit.Text = currentQuiz.title;
                    BeschrijvingEdit.Text = currentQuiz.beschrijving;
                    TijdEdit.Text = currentQuiz.tijd;

                    // Display the question and answers in the textboxes and checkboxes
                    if (currentQuiz.vragen.Count > 0)
                    {
                        // Assuming only one question is present
                        var question = currentQuiz.vragen[0];

                        // Display the question text
                        EditVraag.Text = question.vraagtext;

                        // Display the answers in the textboxes
                        EditAntwoord0.Text = question.antwoord[0];
                        EditAntwoord1.Text = question.antwoord[1];
                        EditAntwoord2.Text = question.antwoord[2];
                        EditAntwoord3.Text = question.antwoord[3];

                        // Optionally, set the correct checkboxes based on the correctAntwoord value
                        EditCheck0.IsChecked = question.correctAntwoord == 0;
                        EditCheck1.IsChecked = question.correctAntwoord == 1;
                        EditCheck2.IsChecked = question.correctAntwoord == 2;
                        EditCheck3.IsChecked = question.correctAntwoord == 3;

                        // Load the image for the question
                        Afbeeldingbewerk.Source = new BitmapImage(new Uri(question.imagePath));
                    }
                }
            }
        }


        private void OpslaanEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuiz != null)
            {
                currentQuiz.title = TitleEdit.Text;
                currentQuiz.beschrijving = BeschrijvingEdit.Text;

                var question = currentQuiz.vragen[currentQuestionIndex];

                question.vraagtext = EditVraag.Text;

                if (question.antwoord.Length >= 4)
                {
                    question.antwoord[0] = EditAntwoord0.Text;
                    question.antwoord[1] = EditAntwoord1.Text;
                    question.antwoord[2] = EditAntwoord2.Text;
                    question.antwoord[3] = EditAntwoord3.Text;

                    if (EditCheck0.IsChecked == true)
                        question.correctAntwoord = 0;
                    else if (EditCheck1.IsChecked == true)
                        question.correctAntwoord = 1;
                    else if (EditCheck2.IsChecked == true)
                        question.correctAntwoord = 2;
                    else if (EditCheck3.IsChecked == true)
                        question.correctAntwoord = 3;
                }

                // Save the updated image path
                question.imagePath = Afbeeldingbewerk.Source.ToString();

                // Save the updated currentQuiz object to the JSON file
                json.WriteDataToFile(currentQuiz);

                MessageBox.Show("Wijzigingen zijn opgeslagen.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Kieslijst.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }



        /*private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                    
                // Call the function to save the image path to JSON
                SaveImagePathToJson(imagePath);

                // Display the selected image in the Image control
                QuizImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }



        private void SaveImagePathToJson(string imagePath)
        {
            if (currentQuiz != null && currentQuiz.vragen.Count > 0)
            {
                int currentQuestionIndex = 0; // Assuming you are updating the first question
                currentQuiz.vragen[currentQuestionIndex].imagePath = imagePath;

                // Save the updated JSON data
                json.WriteDataToFile(currentQuiz);
            }
        }*/
    }
}
