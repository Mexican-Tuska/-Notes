using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notes
{
    /// <summary>
    /// Interaction logic for CreationWindow.xaml
    /// </summary>
    public partial class CreationWindow : Window
    {
        private bool isChanged = false;
        public CreationWindow()
        {
            InitializeComponent();
            NoteEditor.TextChanged += NoteEditor_TextChanged;
        }

        private void NoteEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            isChanged = true;
        }
        public bool isChanged_f()
        {
            return isChanged;
        }

        public CreationWindow(string title, ref FileStream fs)
        {
            InitializeComponent();
            this.set_view(title, ref fs);
        }
        public event EventHandler CreationWindowClosed;
        private void CreationWIndow_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            string title = Note_Title.Text.ToString();
            string cur_dir = Directory.GetCurrentDirectory();

            try
            {
                FileStream fileStream = new FileStream($"{title}.rtf", FileMode.CreateNew);
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
            catch (IOException)
            {

             //  if (this.isChanged())
            }
          
                CreationWindowClosed(this, EventArgs.Empty);
           
        }

        public string get_title()
        {
            return Note_Title.Text.ToString();
        }
      public void set_view(string title, ref FileStream  fs)
        {    Note_Title.Text = title;
              TextRange doc = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                
            doc.Load(fs, DataFormats.Rtf);
          
        }
    }
}
