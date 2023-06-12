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
        int quizID;
        string quizzz;
        Adminpanel admin = new Adminpanel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start scherm
            Start.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible; 
        }

        private void btnspeellijst_Click(object sender, RoutedEventArgs e)
        {
            
            homescreen.Visibility = Visibility.Hidden;
            Kieslijst.Visibility = Visibility.Visible;
            //leest het json file en maakt er een datagrid van
            var SaveDataList = json.readAllQuizes();

            dgAllQuizzes.ItemsSource = null;
            dgAllQuizzes.ItemsSource = SaveDataList;
        }

        private void Maakquiz_Click(object sender, RoutedEventArgs e)
        {
            homescreen.Visibility = Visibility.Hidden;
            maaklijst.Visibility = Visibility.Visible;

            //clear alle textboxes
            TitleQTB.Clear();
            Beschrijving.Clear();
            tijd.Clear();
            Vraagtextbox.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();  
            Antwoord4.Clear();
        }

        public void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //quiz id
            quizzz = File.ReadAllText("count.txt");
            quizID = int.Parse(quizzz);
            //opslaan quiz items
            currentQuiz = new quiz(quizID, TitleQTB.Text, Beschrijving.Text, tijd.Text);

            maaklijst.Visibility = Visibility.Hidden;
            maaklijst2.Visibility = Visibility.Visible;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            // Increment quizID
            quizID++;
            File.WriteAllText("count.txt", quizID.ToString());

            // Store answer options in an array
            string[] vraagOpties = new string[4];
            vraagOpties[0] = Antwoord1.Text;
            vraagOpties[1] = Antwoord2.Text;
            vraagOpties[2] = Antwoord3.Text;
            vraagOpties[3] = Antwoord4.Text;

            // Call newVraag method with the necessary parameters
            currentQuiz.newVraag(Vraagtextbox.Text, vraagOpties, int.Parse(checked_answer));

            // Display "Vraag opgeslagen" message
            MessageBox.Show("Vraag opgeslagen");
        }


        private void Quizopslaan_click(object sender, RoutedEventArgs e)
        {
            // Add listQuizVragen or the appropriate questions list to SaveDataList
            json.WriteDataToFile(currentQuiz);

            maaklijst2.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }

        private void onCheckBoxCheck(object sender, RoutedEventArgs e)
        {
            //checkbox opslaan van de goede antwoord
            Check1.IsChecked = false;
            Check2.IsChecked = false;
            Check3.IsChecked = false;
            Check4.IsChecked = false;

            CheckBox current = (CheckBox)sender;
            current.IsChecked = true;

            checked_answer = current.Name.Replace("Check", ""); 
            MessageBox.Show(checked_answer);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //clear alles knop
            Vraagtextbox.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();
            Antwoord4.Clear();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Back button
            maaklijst2.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }

        private void playbutton(object sender, RoutedEventArgs e)
        {
            // Hide the Kieslijst and show the Speel
            Kieslijst.Visibility = Visibility.Hidden;
            Speel.Visibility = Visibility.Visible;

            // Show the admin panel
            admin.Show();

            //Pakt de ID van de quiz
            Button button = sender as Button;
            var id = button.Tag;
            MessageBox.Show(Convert.ToString(id));
        }
    }
}
