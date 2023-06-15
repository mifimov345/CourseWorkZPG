using CourseWorkZPG.DBModels;
using Microsoft.EntityFrameworkCore;
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
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics.Metrics;

namespace CourseWorkZPG.ViewModels
{
    /// <summary>
    /// Interaction logic for ProgramDescription.xaml
    /// </summary>
    public partial class ProgramDescription : UserControl
    {
        public ProgramDescription()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            string temporal;
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");
            var fulljournal = dbContext.Journals.OrderBy(x => x.Id).ToList();
            int counter = 1;
            foreach (var jour in fulljournal)
            {
                worksheet.Cell(counter, 1).Value = $"{jour.Happening}";
                counter++;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string filePath = System.IO.Path.Combine(desktopPath, "otchet.xlsx");

            workbook.SaveAs(filePath);
        }
    }
}
