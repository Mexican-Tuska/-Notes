using System;
using System.Collections.Generic;
using System.IO;
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
namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> notes_titles;
        public MainWindow()
        {
            InitializeComponent();
            notes_titles = new List<string> ();

        }
        public string note_title;
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            CreationWindow creationWindow = new CreationWindow();
            creationWindow.Owner = this;
            creationWindow.CreationWindowClosed += CreationWindowClosed_React;
            creationWindow.Show();
                   
        }

        private void CreationWindowClosed_React(object sender, EventArgs e)
        {
            NotesList.Items.Add(((CreationWindow)sender).get_title());
           
        }

        private void NotesList_ItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = NotesList.SelectedIndex;
            string tit = (string) NotesList.Items[index];
            CreationWindow s_window = new CreationWindow(tit);
            s_window.Owner = this;
            
            // s_window.set_view(tit);
            s_window.Show();
        }
    }
}
