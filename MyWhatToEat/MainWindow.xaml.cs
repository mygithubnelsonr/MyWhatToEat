using MyWhatToEat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MyWhatToEat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Meal> mealList = new System.Collections.ObjectModel.ObservableCollection<Meal>();
        private string _fileName = "meals.json";
        private int _count = 0;

        public MainWindow()
        {
            InitializeComponent();
            listboxMeals.ItemsSource = mealList;
            Load();
        }

        private void Move_Window(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            textboxInput.Text = "";
            textboxInput.Visibility = Visibility.Visible;
            textboxInput.Focus();
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (mealList.Count == 0)
                return;

            int index = listboxMeals.SelectedIndex;
            mealList.RemoveAt(index);

            index--;
            listboxMeals.SelectedIndex = index;
            listboxMeals.Focus();

            Write();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textboxList_KeyUp(object sender, KeyEventArgs e)
        {
            if (listboxMeals.SelectedItems.Count == 0)
                return;

            if (e.Key == Key.Enter)
                Write();
        }

        private void textboxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int id = 0;

                if (mealList.Count > 0)
                    id = mealList.Max(p => p.Nr) + 1;

                mealList.Add(new Meal() { Nr = id, Content = textboxInput.Text, Count = _count, Date = DateTime.Now.Date.ToShortDateString() });
                textboxInput.Visibility = Visibility.Hidden;

                Write();
            }
            if (e.Key == Key.Escape)
                textboxInput.Visibility = Visibility.Hidden;
        }

        private void Load()
        {
            if (!File.Exists(_fileName))
                return;

            var jsonData = File.ReadAllText(_fileName);
            mealList = JsonConvert.DeserializeObject<ObservableCollection<Meal>>(jsonData);

            listboxMeals.ItemsSource = mealList;
            listboxMeals.Focus();
        }

        private void Write()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(_fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, mealList);
            }
        }

        private void buttonCounter_Click(object sender, RoutedEventArgs e)
        {
            if (listboxMeals.SelectedItems.Count == 0)
            {
                MessageBox.Show("no listitem selected!");
                return;
            }

            var listindex = listboxMeals.SelectedIndex;
            var item = (Meal)listboxMeals.SelectedItem;

            int id = item.Nr;

            mealList[id].Count += 1;

            listboxMeals.ItemsSource = "";
            listboxMeals.ItemsSource = mealList;
            listboxMeals.SelectedIndex = listindex;
            listboxMeals.Focus();

            Write();
        }
    }
}
