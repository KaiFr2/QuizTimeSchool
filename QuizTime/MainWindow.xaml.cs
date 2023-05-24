using QuizTime.Class;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Hidden;
            mode.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mode.Visibility = Visibility.Hidden;
            Speel.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mode.Visibility = Visibility.Hidden;
            maaklijst.Visibility = Visibility.Visible;
        }

        public void Button_Click_3(object sender, RoutedEventArgs e)
        {
            newQuiz = new quizopslaan(0, TitleQTB.Text, Beschrijving.Text, tijd.Text);

            maaklijst.Visibility = Visibility.Hidden;
            maaklijst2.Visibility = Visibility.Visible;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            List<vragenopslaan> listQuizVragen = new List<vragenopslaan>();

            listQuizVragen.Add(new vragenopslaan {quizid = newQuiz.iD, VraagID = currentID ,vraag1 = Vraag1.Text, antwoord1 = Antwoord1.Text, antwoord2 = Antwoord2.Text, antwoord3 = Antwoord3.Text, antwoord4 = Antwoord4.Text, check = checked_answer });

            currentID++; // VraagID

            var SaveDataList = json.ReadSavedDataFile();
            SaveDataList.AddRange(listQuizVragen);
            json.WriteDataToFile(SaveDataList);

            var list = json.ReadSavedDataFile();
        }   


        private void onCheckBoxCheck(object sender, RoutedEventArgs e)
        {
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
            Vraag1.Clear();
            Antwoord1.Clear();
            Antwoord2.Clear();
            Antwoord3.Clear();
            Antwoord4.Clear();
        }
    }
}
