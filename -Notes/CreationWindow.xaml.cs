﻿using Microsoft.Win32;
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
        private bool isChanged; /*Сигнализирует изменен ли был текст в окошке*/
        private string title; /*Переменная которая возвращает заголовка записки*/
        public CreationWindow() /**/
        {
            InitializeComponent();
            isChanged = false; /*Пока текст не менялся поэтому false*/

            NoteEditor.TextChanged += NoteEditor_TextChanged; /*Добавляем обработчик событий, который реагирует на изменения. Отслеживание поля текста*/
        }

        private void NoteEditor_TextChanged(object sender, TextChangedEventArgs e) /*Сам обработчик события*/
        {
            isChanged = true; /*если произошли изменения, то  true*/
        }
        public bool isChanged_f()/*геттер для переменной, который позволяет получить значения*/
        {
            return isChanged;
        }

        public CreationWindow(string title, ref FileStream fs) /*Повторное открывание записки, заголовок и данные передаются*/
        {
            InitializeComponent();
            this.set_view(title, ref fs); /*Вызывает сет */
            isChanged = false;
            NoteEditor.TextChanged += NoteEditor_TextChanged;
        }
       
        public event EventHandler CreationWindowClosed; /*Сигнал события, который реагирует на событие в окошке и в xaml свойство closing запускает*/
       
        private void CreationWIndow_Closed(object sender, System.ComponentModel.CancelEventArgs e) /*Функция, которая реагирует на закрытие окна*/
        {
            //string title;
            if (!Note_Title.Text.Equals("")) { title = Note_Title.Text; } /*Рассматривает случай, если поле заголовка не пустое, то переменные строке заголовка присваеваем данные, которые есть в этом поле*/
            else
            {
              //  Считываю файлы текстового поля и присваиваются в заголовок
                TextRange tmp = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd); /*читаем данные textRange. tmp впитывает все, что было в текстовом поле*/
                title = new string (tmp.Text.Where(c => !char.IsControl(c)).ToArray()); /*Заголовку выделяется память с помощью запроса, ему присваевается значения текста, которое есть в доке*/
            }
            string cur_dir = Directory.GetCurrentDirectory(); /*Строковая переменная получает строковую директория*/

            if (this.isChanged_f() && File.Exists(cur_dir+"\\"+title+".rtf")) /*Случай. Если текст был изменен, но файл уже существует с таким названием*/
            {
                /*Был ли существующий файл отредактирован*/

                File.Delete(cur_dir + "\\" + title + ".rtf"); /*Удаляем его*/
                FileStream fs = new FileStream($"{title}.rtf", FileMode.CreateNew); /*Создаем новый файловый поток с таким же заголовком. FileMode.CreateNew-Создает файл с заданным именем и если такой файл уже есть, то выбрасывает исключение*/
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd); /*Забирает все данные в нашем текстовом поле*/
                range.Save(fs, DataFormats.Rtf);/*Метод, который эти данные текстовые данные сохраняет в файловый поток в формате rtf*/

            }
            else if (this.isChanged_f()) /*Если текст был изменени, но такого файла не существует*/
            {
                /*Все то же самое ток без удаления*/
                FileStream fs = new FileStream($"{title}.rtf", FileMode.CreateNew);
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
              
            }
          else if (File.Exists(cur_dir + "\\" + title + ".rtf")) /*Если файл изменен не был, но мы его посмотрели*/
            {
                /*То же самое, что и впервом случае*/
                File.Delete(cur_dir + "\\" + title + ".rtf");
                FileStream fs = new FileStream($"{title}.rtf", FileMode.CreateNew);
                TextRange range = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
            }
           
           CreationWindowClosed(this, EventArgs.Empty); /*Выпускается сигнал, что окошко закрылось*/

        }

        //Когда открываем уже существующую записку то ее заголовок и данные передаются в эту самую формочку
        public string get_title() /*Просто возвращает значение заголовка*/
        {
            return title;
        }
        
      public void set_view(string title, ref FileStream  fs) /*Тот самый сет, с помощью которого устанавливается значение*/
        {    Note_Title.Text = title; /*Присваевается заголовок полю заголовка*/
            TextRange doc = new TextRange(NoteEditor.Document.ContentStart, NoteEditor.Document.ContentEnd);
             doc.Load(fs, DataFormats.Rtf); /*Присваевается содержимое файла*/

        }
    }
}
