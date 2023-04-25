using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
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

namespace DiplomaApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            TranslateMethod();
        }

        public void TranslateMethod()
        {
            if (MainViewModel.CurrentLanguage == "English")
            {
                PlayButton.Content = "Play";
                ToTheoryButton.Content = "Theory";
                ToPuzzlesButton.Content = "Puzzles";
                ToHistoryButton.Content = "History";
                ToLanguageChanging.Content = "Language";
                AboutCreatorButton.Content = "Creator";
            }
            else if (MainViewModel.CurrentLanguage == "Russian")
            {
                PlayButton.Content = "Играть";
                ToTheoryButton.Content = "Теория";
                ToPuzzlesButton.Content = "Задачи";
                ToHistoryButton.Content = "История игры";
                ToLanguageChanging.Content = "Выбор языка";
                AboutCreatorButton.Content = "Об авторе";
            }
            else if (MainViewModel.CurrentLanguage == "Korean")
            {
                PlayButton.Content = "플레이";
                ToTheoryButton.Content = "이론";
                ToPuzzlesButton.Content = "체스 퍼즐";
                ToHistoryButton.Content = "체스 역사";
                ToLanguageChanging.Content = "언어";
                AboutCreatorButton.Content = "창조자";
            }
        }

        private void speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SpeechRecognitionTestLabel.Content = e.Result.Text;
            /*
            string FinalResult;
            FinalResult = Convert.ToString(e.Result);
            int i = Convert.ToInt(e.Result.Words[0].Text.ToLower());
            int j = Convert.ToInt(e.Result.Words[0].Text.ToLower());

            for(int i=0; i<FinalResult.Length; i++)
            {
                switch(j)
				{
					case "a":
                        j = 0;
						break;
					case "b":
                        j = 1;
						break;
					case "c":
                        j = 2;
                        break;
                    case "d":
                        j = 3;
						break;
					case "e":
                        j = 4;
						break;
					case "f":
                        j = 5;
						break;
                    case "g":
                        j = 6;
						break;
					case "h":
                        j = 7;
						break;
				}
                switch(i)
				{
					case "1":
                        i = 0;                        
						break;
					case "2":
                        i = 1;  
						break;
					case "3":
                        i = 2;  
                        break;
                    case "4":
                        i = 3;  
						break;
					case "5":
                        i = 4;  
						break;
					case "6":
                        i = 5;  
						break;
                    case "7":
                        i = 6;  
						break;
					case "8":
                        i = 7;  
						break;
                if(InsideBorder(i,j) == true && Board._area[i,j].PossibleMove == true)
                    CellCommand(cell,i,j);
                else
                    MessageBox.Show("Chosen move is impossible");
                break;
				}
            }
             */
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            speechRecognizer.Dispose();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow toPlayWindow = new();
            toPlayWindow.Show();
            Close();
        }

        private void ToTheoryButton_Click(object sender, RoutedEventArgs e)
        {
            RulesWindow rulesWindow = new();
            rulesWindow.Show();
            Close();
        }


        private void ToLanguageChanging_Click(object sender, RoutedEventArgs e)
        {
            LanguageSettingsWindow languageSettingsWindow = new();
            languageSettingsWindow.Show();
            Close();
        }

        private void ToHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new();
            historyWindow.Show();
            Close();
        }

        private void EnableListeningButton_Click(object sender, RoutedEventArgs e)
        {
            if (EnableListeningButton.IsChecked == true == true)
                speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            else
                speechRecognizer.RecognizeAsyncStop();
        }
    }
}
