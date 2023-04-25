using DiplomaApp;
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

namespace ChessBoard
{
    /// <summary>
    /// Логика взаимодействия для LanguageSettingsWindow.xaml
    /// </summary>
    public partial class LanguageSettingsWindow : Window
    {
        public LanguageSettingsWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            TranslateMethod();
            ButtonsDesign();
        }

        public void LanguageChanged(object sender)
        {
            if (sender.Equals(EnglishButton))
                MainViewModel.CurrentLanguage = "English";
            else if (sender.Equals(RussianButton))
                MainViewModel.CurrentLanguage = "Russian";
            else if (sender.Equals(KoreanButton))
                MainViewModel.CurrentLanguage = "Korean";
            if (MainViewModel.CurrentLanguage == "Russian")
            {
                EnglishButton.FontSize = 20;
                RussianButton.FontSize = 20;
                KoreanButton.FontSize = 20;
            }
            else
            {
                EnglishButton.FontSize = 25;
                RussianButton.FontSize = 25;
                KoreanButton.FontSize = 25;
            }
        }
        public void ButtonsDesign()
        {
            if (MainViewModel.CurrentLanguage == "Russian")
            {
                EnglishButton.FontSize = 20;
                RussianButton.FontSize = 20;
                KoreanButton.FontSize = 20;
            }
            else
            {
                EnglishButton.FontSize = 25;
                RussianButton.FontSize = 25;
                KoreanButton.FontSize = 25;
            }
        }

        public void DisableButtonsMethod()
        {
            if (MainViewModel.CurrentLanguage == "English")
            {
                EnglishButton.IsEnabled = false;
                RussianButton.IsEnabled = true;
                KoreanButton.IsEnabled = true;
            }
            else if (MainViewModel.CurrentLanguage == "Russian")
            {
                EnglishButton.IsEnabled = true;
                RussianButton.IsEnabled = false;
                KoreanButton.IsEnabled = true;
            }
            else if (MainViewModel.CurrentLanguage == "Korean")
            {
                EnglishButton.IsEnabled = true;
                RussianButton.IsEnabled = true;
                KoreanButton.IsEnabled = false;
            }
        }

        public void TranslateMethod()
        {
            if (MainViewModel.CurrentLanguage == "English")
            {
                EnglishButton.Content = "English";
                RussianButton.Content = "Russian (русский)";
                KoreanButton.Content = "Korean (한국어)";
                NotificationTextBlock.Text = "App has got only 3 languages yet but some new ones will be probably added in the next updates. In some languages translations may be slightly different however every piece of information saves the original meaning.";
                DisableButtonsMethod();

            }
            else if (MainViewModel.CurrentLanguage == "Russian")
            {
                EnglishButton.Content = "Английский (English)";
                RussianButton.Content = "Русский (Russian)";
                KoreanButton.Content = "Корейский (Korean)";
                NotificationTextBlock.Text = "На данный момент приложению доступны 3 языка, но, возможно, в следующих обновлениях буду добавлены новые. Перевод между некоторыми языками может слегка отличаться, тем не менее, вся информация сохраняет оригинальное значение.";
                DisableButtonsMethod();
            }
            else if (MainViewModel.CurrentLanguage == "Korean")
            {
                EnglishButton.Content = "영어 (English)";
                RussianButton.Content = "러시아어 (Russian)";
                KoreanButton.Content = "한국어 (Korean)";
                NotificationTextBlock.Text = "애플리케이션은 아직 3개 언어만 있지만 다음 업데이트에서 새로운 언어가 추가될 것입니다. 일부 언어에서는 번역이 약간 다를 수 있지만 모든 정보는 원래 의미를 저장합니다.";
                DisableButtonsMethod();
            }
        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            LanguageChanged(sender as Button);
            TranslateMethod();
        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {
            LanguageChanged(sender as Button);
            TranslateMethod();
        }

        private void KoreanButton_Click(object sender, RoutedEventArgs e)
        {
            LanguageChanged(sender as Button);
            TranslateMethod();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
