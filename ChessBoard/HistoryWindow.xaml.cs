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
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            TranslateMethod();
        }

        public void TranslateMethod()
        {
            if (MainViewModel.CurrentLanguage == "English")
            {
                TitleLabel.Content = "The history of chess";
                FirstParagraphTextBlock.Text = "Chess originated from the two-player Indian war game, Chatarung, which dates back to 600 A.D. In 1000 A.D, chess spread to Europe by Persian traders. " +
                    "The piece next to the king was called a ferz in Persian, defined as a male counselor to the king. The Europeans concocted a more romantic imagery, and changed the ferz to a queen.";
                SecondParagraphTextBlock.Text = "At that time, the queen was the weakest piece on the board. The bishop was also a short-range piece. Because the queen and bishop were so weak, the game was much slower than it is today." +
                    " It took a long time for a player to develop the pieces and even longer to checkmate the enemy king.";
                ThirdParagraphTextBlock.Text = "Medieval chessplayers often started out with midgame starting positions to speed up the game. Medieval chess was extremely popular. At the end of the 15th century, the rules underwent a sudden sea change. " +
                    "The queen transformed from the weakest piece on the board to the strongest! At the same time, the bishop became the long-range piece that it is today. These changes quickened the games pace. The battle was intensified. Mistakes were harshly punished, " +
                    "tabiyas (from Hindi ~ 'A position in the opening of a game that occurs after a sequence of moves that is heavily standardized') were no longer necessary, and violent checkmates were executed much more often than before. " +
                    "The inventor of these changes is unknown; probably the new rules were not thought up by an individual, but came about from collective experimentation. " +
                    "These new rules were standardized by the 16th century advent of mass production and the printing press. The faster paced game was more suitable for organized play, chess notation, codified rules, and strategy books.";
                ForthParagraphTextBlock.Text = "American chess was fortuitously trumpeted by founding father and chess aficionado Benjamin Franklin, who in 1750 penned The Morals of Chess. Franklins article praises the social and intellectual development that chess inspires. " +
                    "Franklin himself was known to while many hours away on chess, especially against beautiful women.";
            }
            if (MainViewModel.CurrentLanguage == "Russian")
            {
                TitleLabel.Content = "История шахмат";
                FirstParagraphTextBlock.Text = "Основой-предком для шахмат послужила игра для двоих 'Чатарунг', созданная в Индии примерно в 600 году Н.Э. В 1000-х годах Н.Э., шахматы распространились по Европе персидскими торговцами." +
                    "Фигура рядом с королём была названа 'Ферзь' (изн. муж. род), обозначала главного консула короля. Европейцы немного романтизировали игру, заменив ферзя на королеву. " +
                    "Тем не менее, на территории России и большинства стран-соседей с русскоговорящим населением игроки и по сей день предпочитают персидский термин";
                SecondParagraphTextBlock.Text = "На тот момент ферзь или королева были слабейшей фигурой на доске. Слон так же являлся короткоходящей фигурой. По причине того, что ферзь и слон были такими слабыми, игра была намного медленнее, чем она в наши дни." +
                    " Развитие фигур занимало очень длительное время, не говоря о том, чтобы поставить мат вражескому королю.";
                ThirdParagraphTextBlock.Text = "Средневековые игроки зачастую начинали игру с миттельшпиля, чтобы ускорить игру. Тогда шахматы были невероятно популярны. К концу 15-го века, правила подверглись неожиданным изменениям." +
                    "Ферзь превратился из самой слабой фигуры на доске в самую сильную! В то же время, слон стал дальноходящим, каким является сейчас. Эти изменения повлияли на скорость игры. Сражения стали интенсивнее. Ошибки могли быть беспощадно наказаны," +
                    "табии (с хинди ~ 'позиция, возникающая только после строгой, стандартизированной последовательности ходов') были больше необязательны и жёсткие матовые комбинации гораздо чаще заканчивали игру, чем раньше. " +
                    "Основатель этих изменений неизвестен, возможно, новые правила были придуманы не одним человеком, а определённым коллективом в ходе экспериментирования с правилами." +
                    "Новые правила стали стандартом к 16-му веку в виду массовой популяризации. Быстрый вариант игры допускал более организированную игру, заметки, кодифицированные правила и создание книг с различными стратегиями.";
                ForthParagraphTextBlock.Text = "В Древнюю Русь шахматная игра проникла с Востока (предположительно, каспийско-волжским путём) не позднее VIII—IX веках. Это подтверждается археологическими находками и лингвистическими данными: " +
                    "русские термины «шахматы», «слон», «ферзь» — восточного происхождения." +
                    "Несмотря на то, что церковь до середины XVII в. вела упорную борьбу по искоренению шахмат на Руси как «бесовских игрищ», они были популярны среди ремесленников, торговцев, служилых людей, бояр. Шахматами увлекались Иван Грозный, " +
                    "Борис Годунов, Алексей Михайлович и другие государственные деятели. ";
            }
            if (MainViewModel.CurrentLanguage == "Korean")
            {
                TitleLabel.Content = "체스의 역사";
                FirstParagraphTextBlock.Text = "체스는 서기 600년으로 거슬러 올라가는 2인용 인디언 전쟁 게임인 차타룽에서 유래했습니다. 서기 1000년에 체스는 페르시아 상인들에 의해 유럽으로 퍼졌습니다. " +
                    "왕 옆에 있는 조각은 페르시아어로 페르즈(ferz)라고 불렀는데, 왕의 조언자 남성으로 정의되었습니다. 유럽인들은 더 낭만적인 이미지를 만들어냈고 페르즈를 여왕으로 되었어요.";
                SecondParagraphTextBlock.Text = "그 당시 여왕은 보드에서 가장 약한 조각이었습니다. 비숍도 단거리 피스였다. 여왕과 비숍이 너무 약해서 게임이 지금보다 훨씬 느렸습니다. 플레이어가 조각을 개발하는 데 오랜 시간이 걸렸고 적의 왕을 체크메이트하는 데는 더 오래 걸렸습니다." +
                    "중세 체스 선수들은 종종 게임 속도를 높이기 위해 중간 게임 시작 위치에서 시작했습니다.";
                ThirdParagraphTextBlock.Text = "중세 체스는 매우 인기가 있었습니다. 15세기 말에 규칙은 급격한 변화를 겪었습니다." +
                    "최약체에서 최강자로 변신한 여왕! 동시에 비숍은 오늘날의 원거리 피스가 되었다. 이러한 변화는 게임 속도를 빠르게 했습니다. 전투가 격화되었습니다. 실수는 가혹하게 처벌되었고, 타비야는 더 이상 필요하지 않았으며, 폭력적인 장군은 이전보다 훨씬 더 자주 처형되었습니다." +
                    " 이러한 변경 사항을 발명한 사람은 알 수 없습니다. 아마도 새로운 규칙은 개인이 생각한 것이 아니라 집단 실험에서 나온 것입니다." +
                    "이 새로운 규칙은 16세기 대량 생산과 인쇄기의 등장으로 표준화되었습니다. 더 빠른 속도의 게임은 조직화된 플레이, 체스 표기법, 성문화된 규칙 및 전략 서적에 더 적합했어요.";
                ForthParagraphTextBlock.Text = "이렇게 체스는 유로파에 인기를 얻었지만 한국의 체스가 중국에서 왔습니다. 유럽 체스와 마찬가지로 중국 체스 조각은 처음에는 훨씬 약했습니다." +
                    "발견된 가장 오래된 장기 조각은 고려 말(1265년에서 1268년 사이)에 강화도로 가는 길에서 마도(대한민국) 바다에 가라앉은 오래된 난파선(소위 난파선 마도 N°3)에서 발견되었습니다. 당시 임시 자본.";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
