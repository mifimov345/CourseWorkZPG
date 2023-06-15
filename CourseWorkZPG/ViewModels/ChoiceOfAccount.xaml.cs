using CourseWorkZPG.BDModels;
using CourseWorkZPG.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CourseWorkZPG.ViewModels
{
    /// <summary>
    /// Interaction logic for ChoiceOfAccount.xaml
    /// </summary>
    public partial class ChoiceOfAccount : UserControl
    {
        public ChoiceOfAccount()
        {
            InitializeComponent();
            DataContext = this;
            //Adding
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var loclist = dbContext.Locations.Where(x => x.Id > 3).ToList();
            var itlist = dbContext.Items.Where(x => x.Id > 23).ToList();
            var npclist = dbContext.NPCS.Where(x => x.Id > 8).ToList();
            if (loclist.Count > 0)
            {
                foreach (var loc in loclist)
                {
                    CBC.Items.Add(loc.Name);
                }
            }
            if (itlist.Count > 0)
            {
                foreach (var it in itlist)
                {
                    CBC.Items.Add(it.Name);
                }
            }
            if (npclist.Count > 0)
            {
                foreach (var npc in npclist)
                {
                    CBC.Items.Add(npc.Name);
                }
            }
            //Bindings
            CBA.DropDownClosed += CBA_DropDownClosed;
            CBA.SelectionChanged += OnUIElementValueChanged;
            TBAName.TextChanged += OnUIElementValueChanged;
            TBADescription.TextChanged += OnUIElementValueChanged;
            CBALocations.SelectionChanged += OnUIElementValueChanged;
            CBAItems.SelectionChanged += OnUIElementValueChanged;
            //Removing
            CBD.DropDownClosed+= CBD_DropdownClosed2;
            CBD.SelectionChanged += OnUIElementValueChanged2;
            CBDVariable.SelectionChanged += OnUIElementValueChanged2;
            //Changing
            CBC.DropDownClosed += CBC_DropDownClosed3;
            CBC.SelectionChanged += OnUIElementValueChanged3;
            ChangingVariable.SelectionChanged += OnUIElementValueChanged3;
            ChangingName.TextChanged += OnUIElementValueChanged3;
            ChangingInput2.TextChanged += OnUIElementValueChanged3;
            ChangeCB.DropDownClosed += ChangeCB_DropDownClosed;
            ChangeCB.SelectionChanged += OnUIElementValueChanged3;

        }

        private void ChangeCB_DropDownClosed(object? sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var loclist = dbContext.Locations.Where(x => x.Id > 3).ToList();
            var itlist = dbContext.Items.Where(x => x.Id > 23).ToList();
            var npclist = dbContext.NPCS.Where(x => x.Id > 8).ToList();
            Location chloc = (Location)dbContext.Locations.Where(x => x.Name == ChangeCB.Text).FirstOrDefault();
            ChangingVariable.Items.Clear();
            foreach (var item in itlist)
            {
                if (item.Location_ == chloc)
                {
                    ChangingVariable.Items.Add(item.Name);
                }
            }

        }

        private void CBC_DropDownClosed3(object? sender, EventArgs e)
        {
            if (CBC.HasItems)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
                optionsBuilder.UseNpgsql("Postgres");
                var dbContext = new MyDbContext(optionsBuilder.Options);
                var loclist = dbContext.Locations.OrderBy(x => x.Id).ToList();
                var itlist = dbContext.Items.OrderBy(x => x.Id).ToList();
                var npclist = dbContext.NPCS.OrderBy(x => x.Id).ToList();
                var Itnames = dbContext.Items.OrderBy(x => x.Name).ToList();
                var NPCNames = dbContext.NPCS.OrderBy(x => x.Name).ToList();
                var Locnames = dbContext.Locations.OrderBy(x => x.Name).ToList();
                int matchingItems = 0;
                int matchingNpcs = 0;
                int matchingLocations = 0;
                foreach (var item in Itnames)
                {
                    if (item.Name == CBC.Text)
                    {
                        matchingItems++;
                    }
                }
                foreach (var npcs in NPCNames)
                {
                    if (npcs.Name == CBC.Text)
                    {
                        matchingNpcs++;
                    }
                }
                foreach (var location in Locnames)
                {
                    if (location.Name == CBC.Text)
                    {
                        matchingLocations++;
                    }
                }
                ChangeCB.Items.Clear();
                ChangingVariable.Items.Clear();
                string startpoint = string.Empty;
                string startpoint2 = string.Empty;
                if (matchingItems > 0)
                {
                    Item chitem = (Item)dbContext.Items.Where(x => x.Name == CBC.Text).FirstOrDefault();
                    ChangingName.IsEnabled = true;
                    ChangingName.Text = chitem.Name;
                    ChangingInput2.IsEnabled = true;
                    ChangingInput2.Text = chitem.Description;
                    ChangeCB.IsEnabled = true;
                    foreach (var loc in loclist)
                    {
                        if (loc == chitem.Location_)
                        {
                            startpoint = loc.Name;
                        }
                        ChangeCB.Items.Add(loc.Name);
                    }
                    ChangeCB.SelectedItem = startpoint;
                }
                else if (matchingNpcs > 0)
                {
                    NPC chnpc = (NPC)dbContext.NPCS.Where(x => x.Name == CBC.Text).FirstOrDefault();
                    Location chloc = new Location();
                    ChangingName.IsEnabled = true;
                    ChangingName.Text = chnpc.Name;
                    ChangeCB.IsEnabled = true;
                    foreach (var loc in loclist)
                    {
                        if (loc == chnpc.Location_)
                        {
                            chloc = loc;
                            startpoint = loc.Name;
                        }
                        ChangeCB.Items.Add(loc.Name);
                    }
                    ChangeCB.SelectedItem = startpoint;
                    ChangingVariable.IsEnabled = true;
                    foreach (var item in itlist)
                    {
                        if (item.Location_ == chloc)
                        {
                            ChangingVariable.Items.Add(item.Name);
                        }
                        if (chnpc.Reward == item)
                        {
                            startpoint = item.Name;
                        }
                    }
                    ChangingVariable.SelectedItem = startpoint;
                }
                else if (matchingLocations > 0)
                {
                    Location chloc = (Location)dbContext.Locations.Where(x => x.Name == CBC.Text).FirstOrDefault();
                    ChangingName.IsEnabled = true;
                    ChangingName.Text = chloc.Name;
                    ChangeCB.IsEnabled = true;
                    foreach (var loc in loclist)
                    {
                        if (loc == chloc)
                        {
                            chloc = loc;
                            startpoint = loc.Name;
                        }
                        ChangeCB.Items.Add(loc.Name);
                    }
                    ChangeCB.SelectedItem = startpoint;
                }
            }
        }

        private void OnUIElementValueChanged3(object sender, EventArgs e)
        {
            UpdateButtonState3();
        }

        private void UpdateButtonState3()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var loclist = dbContext.Locations.Where(x => x.Id > 3).ToList();
            var itlist = dbContext.Items.Where(x => x.Id > 23).ToList();
            var npclist = dbContext.NPCS.Where(x => x.Id > 8).ToList();
            var Itnames = dbContext.Items.OrderBy(x => x.Name).ToList();
            var NPCNames = dbContext.NPCS.OrderBy(x => x.Name).ToList();
            var Locnames = dbContext.Locations.OrderBy(x => x.Name).ToList();
            int matchingItems = 0;
            int matchingNpcs = 0;
            int matchingLocations = 0;
            foreach (var item in Itnames)
            {
                if (item.Name == CBC.Text)
                {
                    matchingItems++;
                }
            }
            foreach (var npcs in NPCNames)
            {
                if (npcs.Name == CBC.Text)
                {
                    matchingNpcs++;
                }
            }
            foreach (var location in Locnames)
            {
                if (location.Name == CBC.Text)
                {
                    matchingLocations++;
                }
            }
            bool isEnabled = false;
            if (matchingItems > 0 && ChangingName.Text.Length > 0 && ChangingInput2.Text.Length > 0 && ChangeCB.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var item in Itnames)
                {
                    if (item.Name == TBAName.Text)
                    {
                        isEnabled = false;
                    }
                }
            }
            else if (matchingNpcs > 0 && ChangingName.Text.Length > 0 && ChangeCB.Text.Length > 0 && ChangingVariable.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var npc in NPCNames)
                {
                    if (npc.Name == TBAName.Text)
                    {
                        isEnabled = false;
                    }
                }
            }
            else if (matchingLocations > 0 && ChangingName.Text.Length > 0 && ChangeCB.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var loc in Locnames)
                {
                    if (loc.Name == CBA.Text)
                    {
                        isEnabled = false;
                    }
                }
            }
            if (isEnabled)
            {
                marker3.Visibility = Visibility.Hidden;
            }
            ChangeItem.IsEnabled = isEnabled;
        }

        private void CBD_DropdownClosed2(object? sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var loclist = dbContext.Locations.Where(x => x.Id > 3).ToList();
            var itlist = dbContext.Items.Where(x => x.Id > 23).ToList();
            var npclist = dbContext.NPCS.Where(x => x.Id > 8).ToList();
            if (CBD.Text == "Предмет")
            {
                foreach (var item in itlist)
                {
                    CBDVariable.Items.Add(item.Name);
                }
                CBDVariable.IsEnabled = true;
            }
            else if (CBD.Text == "NPC")
            {
                foreach (var npc in npclist)
                {
                    CBDVariable.Items.Add(npc.Name);
                }
                CBDVariable.IsEnabled = true;
            }
            else if (CBD.Text == "Локация")
            {
                foreach (var loc in loclist)
                {
                    CBDVariable.Items.Add(loc.Name);
                }
                CBDVariable.IsEnabled = true;
            }
        }


        private void OnUIElementValueChanged2(object sender, EventArgs e)
        {
            UpdateButtonState2();
        }

        private void UpdateButtonState2()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var Itnames = dbContext.Items.OrderBy(x => x.Name).ToList();
            var NPCNames = dbContext.NPCS.OrderBy(x => x.Name).ToList();
            var Locnames = dbContext.Locations.OrderBy(x => x.Name).ToList();
            bool isEnabled = false;
            if (CBD.Text == "Предмет" && CBDVariable.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var item in Itnames)
                {
                    if (item.Name == TBAName.Text)
                    {
                        isEnabled = false;
                    }
                }
            }
            else if (CBD.Text == "NPC" && CBDVariable.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var npc in NPCNames)
                {
                    if (npc.Name == TBAName.Text)
                    {
                        isEnabled = false;
                    }
                }
            }
            else if (CBD.Text == "Локация" && CBDVariable.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var loc in Locnames)
                {
                    if (loc.Name == CBA.Text)
                    {
                        isEnabled = false;
                    }
                }

            }
            if (isEnabled)
            {
                marker2.Visibility = Visibility.Hidden;
            }
            RemoveItem.IsEnabled = isEnabled;
        }

        private void OnUIElementValueChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void CBA_DropDownClosed(object? sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var loclist = dbContext.Locations.OrderBy(x => x.Id).ToList();
            var itlist = dbContext.Items.OrderBy(x => x.Id).ToList();
            CBALocations.Items.Clear();
            CBAItems.Items.Clear();
            foreach (var loc in loclist)
            {
                if (CBALocations.Items.Contains(loc.Name) == false)
                {
                    CBALocations.Items.Add(loc.Name);
                }
            }
            foreach (var item in itlist)
            {
                if (CBAItems.Items.Contains(item.Name) == false)
                {
                    CBAItems.Items.Add(item.Name);
                }
            }
            if (CBA.Text == "Предмет")
            {
                CBAItems.IsEnabled = false;
                TBAName.IsEnabled = true;
                TBADescription.IsEnabled = true;
                CBALocations.IsEnabled = true;
            }
            else if (CBA.Text == "NPC")
            {
                TBADescription.IsEnabled = false;
                TBAName.IsEnabled = true;
                CBAItems.IsEnabled = true;
                CBALocations.IsEnabled = true;
            }
            else if (CBA.Text == "Локация")
            {
                TBADescription.IsEnabled = false;
                CBALocations.IsEnabled = false;
                CBAItems.IsEnabled = false;
                TBAName.IsEnabled = true;
            }
        }

        private void UpdateButtonState()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var Itnames = dbContext.Items.OrderBy(x => x.Name).ToList();
            var NPCNames = dbContext.NPCS.OrderBy(x => x.Name).ToList();
            var Locnames = dbContext.Locations.OrderBy(x => x.Name).ToList();
            bool isEnabled = false;
            if (CBA.Text == "Предмет" && TBAName.Text.Length > 0 && TBADescription.Text.Length > 0 && CBALocations.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var item in Itnames)
                {
                    if (item.Name == TBAName.Text)
                    {
                        isEnabled = false;
                    }
                }                
            }
            else if (CBA.Text == "NPC" && CBAItems.Text.Length > 0 && CBALocations.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var npc in NPCNames)
                {
                    if (npc.Name == TBAName.Text)
                    {
                        isEnabled = false;
                    }
                }
            }
            else if (CBA.Text == "Локация" && TBAName.Text.Length > 0)
            {
                isEnabled = true;
                foreach (var loc in Locnames)
                {
                    if (loc.Name == CBA.Text)
                    {
                        isEnabled = false;
                    }
                }
                
            }
            if (isEnabled)
            {
                marker1.Visibility = Visibility.Hidden;
            }
            AddingButton.IsEnabled = isEnabled;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void AddingButton_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            string x = CBA.Text;
            switch (x)
            {
                case "Предмет":
                    Location loctemp = (Location)dbContext.Locations.Where(x => x.Name == CBALocations.Text).FirstOrDefault();
                    Item itemtemp = new Item() {Name =  TBAName.Text, Description = TBADescription.Text, Location_ = loctemp};
                    dbContext.Items.Add(itemtemp);
                    CBC.Items.Add(itemtemp.Name);
                    await dbContext.SaveChangesAsync();
                    break;
                case "NPC":
                    Location loctemp3 = (Location)dbContext.Locations.Where(x => x.Name == CBALocations.Text).FirstOrDefault();
                    Item itemtemp2 = (Item)dbContext.Items.Where(x => x.Name == CBAItems.Text).FirstOrDefault();
                    NPC npctemp = new NPC() { Name = TBAName.Text, Reward =  itemtemp2, Location_ = loctemp3};
                    dbContext.NPCS.Add(npctemp);
                    CBC.Items.Add(npctemp.Name);
                    await dbContext.SaveChangesAsync();
                    break;
                case "Локация":
                    Location loctemp2 = new Location() { Name = TBAName.Text };
                    dbContext.Locations.Add(loctemp2);
                    CBC.Items.Add(loctemp2.Name);
                    await dbContext.SaveChangesAsync();
                    break;
            };
        }

        private async void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            string x = CBD.Text;
            switch (x)
            {
                case "Предмет":
                    Item delitem = (Item)dbContext.Items.Where(x => x.Name == CBDVariable.Text).FirstOrDefault();
                    dbContext.Items.Remove(delitem);
                    CBC.Items.Remove(delitem.Name);
                    await dbContext.SaveChangesAsync();
                    break;
                case "NPC":
                    NPC delnpc = (NPC)dbContext.NPCS.Where(x => x.Name == TBAName.Text).FirstOrDefault();
                    dbContext.NPCS.Remove(delnpc);
                    CBC.Items.Remove(delnpc.Name);
                    await dbContext.SaveChangesAsync();
                    break;
                case "Локация":
                    Location locdel = (Location)dbContext.Locations.Where(x => x.Name == TBAName.Text).FirstOrDefault();
                    dbContext.Locations.Remove(locdel);
                    CBC.Items.Remove(locdel.Name);
                    await dbContext.SaveChangesAsync();
                    break;
                default:
                    break;
            }
        }

        private async void ChangeItem_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            var loclist = dbContext.Locations.Where(x => x.Id > 3).ToList();
            var itlist = dbContext.Items.Where(x => x.Id > 23).ToList();
            var npclist = dbContext.NPCS.Where(x => x.Id > 8).ToList();
            var Itnames = dbContext.Items.OrderBy(x => x.Name).ToList();
            var NPCNames = dbContext.NPCS.OrderBy(x => x.Name).ToList();
            var Locnames = dbContext.Locations.OrderBy(x => x.Name).ToList();
            int matchingItems = 0;
            int matchingNpcs = 0;
            int matchingLocations = 0;
            foreach (var item in Itnames)
            {
                if (item.Name == CBC.Text)
                {
                    matchingItems++;
                }
            }
            foreach (var npcs in NPCNames)
            {
                if (npcs.Name == CBC.Text)
                {
                    matchingNpcs++;
                }
            }
            foreach (var location in Locnames)
            {
                if (location.Name == CBC.Text)
                {
                    matchingLocations++;
                }
            }
            if (matchingItems > 0)
            {
                Item chitem = (Item)dbContext.Items.Where(x => x.Name == CBC.Text).FirstOrDefault();
                dbContext.Items.Remove(chitem);
                Location chloc = (Location)dbContext.Locations.Where(x => x.Name == ChangeCB.Text).FirstOrDefault();
                chitem = new Item() { Name = ChangingName.Text, Description =  ChangingInput2.Text,Location_ = chloc };
                dbContext.Items.Add(chitem);
                await dbContext.SaveChangesAsync();
            }
            else if (matchingNpcs > 0)
            {
                NPC chnpc = (NPC)dbContext.NPCS.Where(x => x.Name == CBC.Text).FirstOrDefault();
                Item itch = (Item)dbContext.Items.Where(x => x.Name == ChangingVariable.Text).FirstOrDefault();
                dbContext.NPCS.Remove(chnpc);
                Location chloc2 = (Location)dbContext.Locations.Where(x => x.Name == ChangeCB.Text).FirstOrDefault();
                chnpc = new NPC() { Name = ChangingName.Text, Reward = itch, Location_ = chloc2 };
                dbContext.NPCS.Add(chnpc);
                await dbContext.SaveChangesAsync();
            }
            else if (matchingLocations > 0)
            {
                Location chloc = (Location)dbContext.Locations.Where(x => x.Name == CBC.Text).FirstOrDefault();
                dbContext.Locations.Remove(chloc);
                chloc = new Location() { Name = ChangingName.Text };
                dbContext.Locations.Add(chloc);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
