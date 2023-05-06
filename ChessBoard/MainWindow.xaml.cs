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
using System.Speech.Synthesis;

namespace DiplomaApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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

        private void AboutCreatorButton_Click(object sender, RoutedEventArgs e)
        {
            AboutCreator aboutCreator = new();
            aboutCreator.Show();
            Close();
        }

        private void ToPuzzlesButton_Click(object sender, RoutedEventArgs e)
        {
            PuzzlesWindow puzzlesWindow = new();
            puzzlesWindow.Show();
            Close();
        }
    }
}
