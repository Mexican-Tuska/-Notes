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
using System.Globalization;

namespace NotesDis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string language;
        public MainWindow()
        {
            InitializeComponent();
            language = "rus-RU";
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            Create createNote = new Create();
            createNote.ShowDialog();
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox cb = sender as ComboBox;
            language = (cb.SelectedItem as ComboBoxItem).Tag.ToString();

            if (language != null)
            {
                CultureInfo lang = new CultureInfo(language);

                if (lang != null)
                {
                    App.Language = lang;
                }

            }
        }
    }
}
