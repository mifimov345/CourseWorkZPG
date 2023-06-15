using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseWorkZPG.BDModels;
using CourseWorkZPG.DBModels;
using CourseWorkZPG.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkZPG.ViewModels
{
    /// <summary>
    /// Interaction logic for DownPanel.xaml
    /// </summary>
    public partial class DownPanel : UserControl
    {
        private readonly MyDbContext _db;
        public DownPanel()
        {
            InitializeComponent();
            
        }

        private void Stats_Open(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new StatsMain();
            }
        }

        private void Diary_Open(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new FullDiary();
            }
        }

        private void Description_Open(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new ProgramDescription();
            }
        }

        private void ChangeAcc_Open(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.SelectedView = new ChoiceOfAccount();
            }
        }
    }

}
