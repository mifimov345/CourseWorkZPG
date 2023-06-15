using CourseWorkZPG.BDModels;
using CourseWorkZPG.DBModels;
using CourseWorkZPG.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace CourseWorkZPG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyDbContext _db;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "Host=localhost; Port=5432; Database=ZPG_3; Username=postgres; Password=root;";
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            _db = new MyDbContext(optionsBuilder.Options);
            Initialize(_db).GetAwaiter();
            FullDiary fullDiary = new FullDiary();
            fullDiary.Loaded += DiaryButton_Click;
            DownPanel dp = new DownPanel();
            var uc1 = dp.Content as DownPanel;
        }

        public string locId;

        public static async Task Initialize(MyDbContext db)
        {
            try
            {
                if (db.Items.Any())
                {
                    Debug.WriteLine("Database already contains items.");
                    return;
                }
                Debug.WriteLine("Adding new items to database.");
                Random rnd = new Random();
                string[] namings = new string[] {"Горшок", "Зозо", "Бомбурчтомпбампсель", "Акатош", "Феропонт", "Шеогоррат", "Борокгданн", "Йорунд", "Шаркбой", "Бенчхолль", "Барагрунн", "Хачанури"};
                string naming = namings[rnd.Next(0,13)];
                var playableChar = new PlayableChar() {Id = 1, Exp = 0, Lvl = 1, Name = naming, CurLoc = rnd.Next(1,4) };
                await db.PlayableChars.AddAsync(playableChar);
                var Location1 = new Location() { Name = "Темный лес" };
                var Location2 = new Location() { Name = "Городской базар"};
                var Location3 = new Location() { Name = "Деревня" };
                await db.Locations.AddRangeAsync(Location1,Location2,Location3);
                var item1 = new Item() { Name = "Ржавый щит", Description = "Ржавый сломанный щит", Location_ = Location1 };
                var item2 = new Item() { Name = "Дырявый ботинок", Description = "Бархатные тяги...", Location_ = Location1 };
                var item3 = new Item() { Name = "Странная книга", Description = "Написана на неизвестном языке", Location_ = Location2 };
                var item4 = new Item() { Name = "Тупое лезвие", Description = "Тупое лезвие какого-то предмета", Location_ = Location2 };
                var item5 = new Item() { Name = "Разбитая кружка", Description = "В ней могло быть пиво...", Location_ = Location2 };
                var item6 = new Item() { Name = "Голова быка", Description = "Отрезанная голова, дурной подарок", Location_ = Location3 };
                var item7 = new Item() { Name = "Камень", Description = "Обычный камень", Location_ = Location3 };
                var item8 = new Item() { Name = "Светящаяся бутылка", Description = "Ха, в ней молния", Location_ = Location3 };
                //общие предметы среди локаций
                var item9 = new Item() { Name = "Плакат о розыске", Description = "Его телега же сбила...", Location_ = Location1 };
                var item10 = new Item() { Name = "Плакат о розыске", Description = "Его телега же сбила...", Location_ = Location2 };
                var item11 = new Item() { Name = "Плакат о розыске", Description = "Его телега же сбила...", Location_ = Location3 };
                var item12 = new Item() { Name = "Дырявые штаны", Description = "Да у меня и свои есть", Location_ = Location1 };
                var item13 = new Item() { Name = "Дырявые штаны", Description = "Да у меня и свои есть", Location_ = Location2 };
                var item14 = new Item() { Name = "Дырявые штаны", Description = "Да у меня и свои есть", Location_ = Location3 };
                var item15 = new Item() { Name = "Палка", Description = "Кривая", Location_ = Location1 };
                var item16 = new Item() { Name = "Палка", Description = "Кривая", Location_ = Location2 };
                var item17 = new Item() { Name = "Палка", Description = "Кривая", Location_ = Location3 };
                var item18 = new Item() { Name = "Горшок", Description = "Это что, я...", Location_ = Location1 };
                var item19 = new Item() { Name = "Горшок", Description = "Это что, я...", Location_ = Location2 };
                var item20 = new Item() { Name = "Горшок", Description = "Это что, я...", Location_ = Location3 };
                var item21 = new Item() { Name = "Кость", Description = "Из шкафа выпало?", Location_ = Location1 };
                var item22 = new Item() { Name = "Кость", Description = "Из шкафа выпало?", Location_ = Location2 };
                var item23 = new Item() { Name = "Кость", Description = "Из шкафа выпало?", Location_ = Location3 };
                await db.Items.AddRangeAsync(item1, item2, item3, item4, item5, item6, item7,item8,item9, item10, item11, item12, item13, item14, item15, item16, item17, item18, item19, item20, item21, item22, item23);
                var npc1 = new NPC() {Name = "Лесник", Reward = item2, Location_ = Location1 };
                var npc2 = new NPC() { Name = "Труп", Reward = item1, Location_ = Location1 };
                var npc3 = new NPC() { Name = "Странный торговец", Reward = item3, Location_ = Location2 };
                var npc4 = new NPC() { Name = "Пьяница", Reward = item5, Location_ = Location2 };
                var npc5 = new NPC() { Name = "Бедняк", Reward = item4, Location_ = Location2 };
                var npc6 = new NPC() { Name = "Мясник", Reward = item6, Location_ = Location3 };
                var npc7 = new NPC() { Name = "Деревенский ребенок", Reward = item7, Location_ = Location3 };
                var npc8 = new NPC() { Name = "Дурак", Reward = item8, Location_ = Location3 };
                await db.NPCS.AddRangeAsync(npc1,npc2,npc3,npc4,npc5,npc6,npc7,npc8);
                var evented1 = new Evented() {Followup = "Я нашел", Ending = ",а оно мне надо?" };
                var evented2 = new Evented() { Followup = "Я получил от", Ending = ", может вернуть?" };
                var evented3 = new Evented() { Followup = "В меня кинули", Ending = "может пригодится?" };
                var evented4 = new Evented() { Followup = "Мне подарили", Ending = ", а что, приятно." };
                var evented5 = new Evented() { Followup = "Точно, пойду в", Ending = "выглядит интересно." };
                var evented6 = new Evented() { Followup = "Я купил у", Ending = ", достойная покупка!" };
                var evented7 = new Evented() { Followup = "Я увидел", Ending = "лучше отойти подальше." };
                var evented8 = new Evented() { Followup = "странно поздоровался и дал мне", Ending = ",некрасиво отказываться от такого." };
                await db.Eventeds.AddRangeAsync(evented1,evented2,evented3,evented4,evented5,evented6, evented7, evented8);
                var journal = new Journal() { Happening = "Король заскучал от моих представлений и прогнал в мир искать вдохновения" };
                await db.Journals.AddAsync(journal);
                await db.SaveChangesAsync();
                var journal2 = new Journal() { Happening = $"Сперва отправимся в хмм {db.Locations.FirstOrDefault(e => e.Id == playableChar.CurLoc).Name}" };
                await db.Journals.AddAsync(journal2);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }


        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new StatsMain();
            }
        }

        private void DiaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                var fullDiary = new FullDiary();
                mainViewModel.SelectedView = fullDiary;
            }
        }

        private void DescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new ProgramDescription();
            }
        }

        private void ChangeAccButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new ChoiceOfAccount();
            }
        }
    }
}
