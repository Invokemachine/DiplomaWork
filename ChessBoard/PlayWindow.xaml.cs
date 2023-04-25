
using DiplomaApp;

namespace ChessBoard
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow
    {
        public PlayWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            TranslationMethod();
        }

        public void TranslationMethod()
        {
            if (MainViewModel.CurrentLanguage == "Russian")
            {
                ClearButton.Content = "Очистить поле";
                NewGameButton.Content = "Новая игра";
            }
            else if (MainViewModel.CurrentLanguage == "Korean")
            {
                ClearButton.Content = "지우기";
                NewGameButton.Content = "새로운 게임";
            }
            else if(MainViewModel.CurrentLanguage == "English")
            {
                ClearButton.Content = "Clear";
                NewGameButton.Content = "New game";
            }
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
