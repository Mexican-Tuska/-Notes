using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private bool isChanged;
        private string title;
        public CreationWindow()
        {
            InitializeComponent();
            isChanged = false;
          
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
            isChanged = false;
            NoteEditor.TextChanged += NoteEditor_TextChanged;
        }
        public event EventHandler CreationWindowClosed;
        private void CreationWIndow_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //string title;
            if (!Note_Title.Text.Equals("")) { title = Note_Title.Text; }
            else
            {
                TextRange tmp = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                //title = (NoteEditor.Document.ToString()).Substring(0, (NoteEditor.Document.ToString().Length));
               // title = tmp.Text.Replace(" ", "");
               title = new string (tmp.Text.Where(c => !char.IsControl(c)).ToArray());
            }
            string cur_dir = Directory.GetCurrentDirectory();

            if (this.isChanged_f() && File.Exists(cur_dir+"\\"+title+".rtf"))
            {

              
                File.Delete(cur_dir + "\\" + title + ".rtf");
                FileStream fs = new FileStream($"{title}.rtf", FileMode.CreateNew);
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
                
            }
            else if (this.isChanged_f())
            {
                FileStream fs = new FileStream($"{title}.rtf", FileMode.CreateNew);
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
              
            }
          else if (File.Exists(cur_dir + "\\" + title + ".rtf"))
            {
                File.Delete(cur_dir + "\\" + title + ".rtf");
                FileStream fs = new FileStream($"{title}.rtf", FileMode.CreateNew);
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
            }
           
           CreationWindowClosed(this, EventArgs.Empty);
            
         }


            public string get_title()
        {
            return title;
        }
      public void set_view(string title, ref FileStream  fs)
        {    Note_Title.Text = title;
             TextRange doc = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
             doc.Load(fs, DataFormats.Rtf);
          
        }
    }
}
