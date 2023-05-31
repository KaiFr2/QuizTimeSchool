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
        public quizopslaan newQuiz;
        int quizID;
        string quizzz;
        int selectid;

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
            var SaveDataList = json.ReadSavedDataFile();
            vragenopslaan vragenopslaan = new vragenopslaan();

            dgAllQuizzes.ItemsSource = null;
            dgAllQuizzes.ItemsSource = SaveDataList;
        }

        private void Maakquiz_Click(object sender, RoutedEventArgs e)
        {
            homescreen.Visibility = Visibility.Hidden;
            maaklijst.Visibility = Visibility.Visible;
            currentID = 1;

            //cleared alle textboxes
            TitleQTB.Clear();
            Beschrijving.Clear();
            tijd.Clear();
            Vraag1.Clear();
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
            //opslaan van de quiz
            newQuiz = new quizopslaan(quizID, TitleQTB.Text, Beschrijving.Text, tijd.Text);

            maaklijst.Visibility = Visibility.Hidden;
            maaklijst2.Visibility = Visibility.Visible;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            //opslaan van de vragen
            quizID++;
            File.WriteAllText("count.txt", quizID.ToString());
            List<vragenopslaan> listQuizVragen = new List<vragenopslaan>();

            listQuizVragen.Add(new vragenopslaan {quizid = newQuiz.iD, VraagID = currentID, title = TitleQTB.Text  ,vraag1 = Vraag1.Text, antwoord1 = Antwoord1.Text, antwoord2 = Antwoord2.Text, antwoord3 = Antwoord3.Text, antwoord4 = Antwoord4.Text, check = checked_answer });

            currentID++; // VraagID

            var SaveDataList = json.ReadSavedDataFile();
            SaveDataList.AddRange(listQuizVragen);
            json.WriteDataToFile(SaveDataList);

            var list = json.ReadSavedDataFile();

            MessageBox.Show("Vraag opgeslagen", "Message", MessageBoxButton.OK, MessageBoxImage.Information);


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

        private void Volgende_Click(object sender, RoutedEventArgs e)
        {
            //clear knop
            Vraag1.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();
            Antwoord4.Clear();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            maaklijst2.Visibility = Visibility.Hidden;
            homescreen.Visibility = Visibility.Visible;
        }

        private void btnbewerklijst_Click(object sender, RoutedEventArgs e)
        {
            //bewerk pagina WIP
            homescreen.Visibility = Visibility.Hidden;
            Kieslijst.Visibility = Visibility.Visible;

            var SaveDataList = json.ReadSavedDataFile();
            vragenopslaan vragenopslaan = new vragenopslaan();

            dgAllQuizzes.ItemsSource = null;
            dgAllQuizzes.ItemsSource = SaveDataList;
        }

        private void playbutton(object sender, RoutedEventArgs e)
        {
            //knop om quiz te spelen
            Kieslijst.Visibility = Visibility.Hidden;
            Speel.Visibility = Visibility.Visible;

            var button = sender as Button;

            int tempquizid = (int)button.CommandParameter;

            selectid = tempquizid;
        }
    }
}
