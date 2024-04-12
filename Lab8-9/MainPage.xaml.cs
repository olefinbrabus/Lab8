using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.EntityFrameworkCore;
using Lab8_9.Model;

namespace Lab8_9
{

    public sealed partial class MainPage : Page
    {
        Context db;
        public MainPage()
        {
            this.InitializeComponent();
            db = new Context();
            db.Database.EnsureCreated();
            db.Animals.Load();
            listView.ItemsSource = db.Animals.Local.ToList();
        }
       
        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text != "" && textBoxLimbCount.Text != "" && textBoxDescription.Text != "")
            {
                db.Add(new Animal
                {
                    Name = textBoxName.Text,
                    LimbCount = int.Parse(textBoxLimbCount.Text),
                    Description = textBoxDescription.Text,
                });
                db.SaveChanges();
                listView.ItemsSource = db.Animals.Local.ToList();
                textBoxName.Text = "";
                textBoxLimbCount.Text = "";
                textBoxDescription.Text = "";

            }
            else
            {
                ContentDialog cont = new ContentDialog
                {
                    Title = "Порожньо",
                    Content = "Ви не ввели всю потрібну інформацію.",
                    CloseButtonText = "Ok"
                };
                cont.ShowAsync();
            }
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var line = listView.SelectedItem as Animal;
            if (line != null)
            {
                var entity = db.Find<Animal>(line.Id);
                if (entity != null)
                {
                    textBoxName.Text = entity.Name;
                    textBoxLimbCount.Text = $"{entity.LimbCount}";
                    textBoxDescription.Text = entity.Description;

                }
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var entity = db.Find<Animal>((listView.SelectedItem as Animal).Id);
            if (entity != null)
            {
                db.Remove(entity);
                db.SaveChanges();
                listView.ItemsSource = db.Animals.Local.ToList();
                textBoxName.Text = "";
                textBoxLimbCount.Text = "";
                textBoxDescription.Text = "";
            }

        }

    }
}
