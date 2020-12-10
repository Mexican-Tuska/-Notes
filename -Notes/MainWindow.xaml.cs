﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            

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
            if (!NotesList.Items.Contains(((CreationWindow)sender).get_title()))
            { NotesList.Items.Add(((CreationWindow)sender).get_title()); }
            else
            {
                
            }
           
        }

        private void NotesList_ItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = NotesList.SelectedIndex;
            string tit = (string) NotesList.Items[index];
            FileStream note_file = File.Open(tit + ".rtf", FileMode.Open);

            CreationWindow s_window = new CreationWindow(tit, ref note_file);
            s_window.Owner = this;
            s_window.CreationWindowClosed += CreationWindowClosed_React;
            s_window.Show();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void NotesList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                MessageBox.Show("WORKS");
            }
        }
    }
}
