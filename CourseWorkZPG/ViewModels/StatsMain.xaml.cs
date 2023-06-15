using CourseWorkZPG.BDModels;
using CourseWorkZPG.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CourseWorkZPG.ViewModels
{
    /// <summary>
    /// Interaction logic for StatsMain.xaml
    /// </summary>
    
    public partial class StatsMain : UserControl
    {
        private DispatcherTimer timer2;
        public StatsMain()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            InitializeComponent();
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(10);
            if (dbContext == null)
            {
                dbContext = new MyDbContext(optionsBuilder.Options);
                dbContext.Database.EnsureCreated();
            }
            var entries = dbContext.Journals.OrderByDescending(e => e.Id).Take(6).ToList();
            foreach (var entry in entries)
            {
                LB2.Items.Add($"{entry.Happening}");
            }
            var character = dbContext.PlayableChars.OrderBy(e => e.Id).Take(1);
            foreach (var item in character)
            {
                CharacterName.Content = $"Имя: {item.Name}";
                CharacterExp.Content = $"Опыт: {item.Exp}  / {(100 * 2 + 50 * (item.Lvl - 1)) * item.Lvl / 2}";
                CharacterLevel.Content = $"Уровень: {item.Lvl}";
            }
            timer2.Tick += Timer_Tick;
            timer2.Start();
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var entries = dbContext.Journals.OrderByDescending(e => e.Id).Take(6).ToList();
            LB2.Items.Clear();
            foreach (var entry in entries)
            {
                LB2.Items.Add($"{entry.Happening}");
            }
            var character = dbContext.PlayableChars.OrderBy(e => e.Id).Take(1);
            int exp, cur_lvl,curr_exp_cap;
            int gained = rnd.Next(1, 13);
            foreach (var item in character)
            {
                exp = (int)item.Exp + gained;
                cur_lvl = (int)item.Lvl;
                curr_exp_cap = (100*2+50*(cur_lvl-1))*cur_lvl/2;
                if (exp >= curr_exp_cap)
                {
                    item.Lvl = (int)item.Lvl + 1;
                    item.Exp = exp - curr_exp_cap;
                }
                else
                {
                    item.Exp = exp;
                }
                CharacterName.Content = $"Имя: {item.Name}";
                CharacterExp.Content = $"Опыт: {item.Exp}  / {curr_exp_cap}";
                CharacterLevel.Content = $"Уровень: {item.Lvl}";
            }
            await dbContext.SaveChangesAsync();
        }
        private async void Submit_Hint(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            PlayableChar player_ = dbContext.PlayableChars.FirstOrDefault(e => e.Id == 1);
            string x = CB1.Text;
            string y = HintInput.Text;
            FullDiary fd = new FullDiary();
            switch (x)
            {
                case "Пойти":
                    var locations_ = dbContext.Locations.OrderBy(x => x.Id).ToList();
                    bool isThere = false;
                    foreach (var location in locations_)
                    {
                        if (location.Name == y && player_.CurLoc != location.Id)
                        {
                            string happ = $"А что, пойти в '{location.Name}' звучит как отличная идея";
                            Journal journal_ = new Journal() { Happening = happ };
                            fd.LB1.Items.Add($"{journal_.Happening}");
                            dbContext.Journals.Add(journal_);
                            player_.CurLoc = location.Id;
                            await dbContext.SaveChangesAsync();
                        }
                        else if (player_.CurLoc == location.Id)
                        {
                            string happ2 = $"Так я это, уже в {location.Name}...";
                            Journal journal_ = new Journal() { Happening = happ2};
                            fd.LB1.Items.Add($"{journal_.Happening}");
                            dbContext.Journals.Add( journal_ );
                            await dbContext.SaveChangesAsync();
                        }
                    }
                    if (!isThere)
                    {
                        string happ3 = $"А это {y}, это вообще где?";
                        Journal journal_ = new Journal() { Happening = happ3 };
                        fd.LB1.Items.Add($"{journal_.Happening}");
                        dbContext.Journals.Add(journal_);
                        await dbContext.SaveChangesAsync();
                    }
                    break;
                case "Ограбь":
                    string happ4 = $"Где же это виданно, чтобы шут грабил!";
                    Journal journal2_ = new Journal() { Happening = happ4 };
                    fd.LB1.Items.Add($"{journal2_.Happening}");
                    dbContext.Journals.Add(journal2_);
                    await dbContext.SaveChangesAsync();
                    break;
                case "Сломай":
                    string happ5 = $"Ну раз ты советуешь, как найду {y}, то обязательно попробую!";
                    Journal journal3_ = new Journal() { Happening = happ5 };
                    fd.LB1.Items.Add($"{journal3_.Happening}");
                    dbContext.Journals.Add(journal3_);
                    await dbContext.SaveChangesAsync();
                    break;
                case "Заработай":
                    string happ6 = $"Попытаю удачи заработать чуть позже.";
                    Journal journal4_ = new Journal() { Happening = happ6 };
                    fd.LB1.Items.Add($"{journal4_.Happening}");
                    dbContext.Journals.Add(journal4_);
                    await dbContext.SaveChangesAsync();
                    break;
            }
        }
    }
}
