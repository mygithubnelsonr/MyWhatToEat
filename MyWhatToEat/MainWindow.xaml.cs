using MyWhatToEat.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWhatToEat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Meal> mealList = new ObservableCollection<Meal>();
        private string _fileName = "meals.xml";
        private int _count = 0;

        public MainWindow()
        {
            InitializeComponent();
            listboxMeals.ItemsSource = mealList;
            LoadXML();
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

            WriteXML();
        }

        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(this, "MyWhatToEat Printing");

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = path + "\\MyWhatToEatPrinting.pdf";

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.Arguments = filename;
            processStartInfo.FileName = "explorer.exe";
            Process.Start(processStartInfo);
        }

        private void textboxList_KeyUp(object sender, KeyEventArgs e)
        {
            if (listboxMeals.SelectedItems.Count == 0)
                return;

            if (e.Key == Key.Enter)
                WriteXML();
        }

        private void textboxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int id = 0;

                if (mealList.Count > 0)
                    id = mealList.Max(p => p.Nr) + 1;

                mealList.Add(new Meal() { Nr = id, Content = textboxInput.Text, Count = _count, Date = "" });
                textboxInput.Visibility = Visibility.Hidden;
                WriteXML();
            }
            if (e.Key == Key.Escape)
                textboxInput.Visibility = Visibility.Hidden;
        }

        private void WriteXML()
        {
            XmlProcessor.XMLWriteMeals(mealList);
        }

        private void LoadXML()
        {
            mealList = XmlProcessor.XMLLoadMeals(_fileName);
            listboxMeals.ItemsSource = mealList;
            listboxMeals.Focus();
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
            mealList[id].Date = DateTime.Now.Date.ToShortDateString();

            listboxMeals.ItemsSource = "";
            listboxMeals.ItemsSource = mealList;
            listboxMeals.SelectedIndex = listindex;
            listboxMeals.Focus();

            WriteXML();
        }
    }
}
