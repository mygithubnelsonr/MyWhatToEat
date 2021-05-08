using MyWhatToEat.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MyWhatToEat
{
    public static class XmlProcessor
    {
        public static ObservableCollection<Meal> ListOfMeals;

        public static void XMLWriteMeals(ObservableCollection<Meal> mealList)
        {
            // Create an XmlWriterSettings object with the correct options.
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("\t");
            settings.OmitXmlDeclaration = true;

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Meal>));

            // Create an XmlTextWriter using a FileStream.
            using (Stream fs = new FileStream("meals.xml", FileMode.Create))
            {
                XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                writer.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(writer, mealList);
            }
        }

        public static ObservableCollection<Meal> XMLLoadMeals(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Meal>));

            // A FileStream is needed to read the XML document.
            using (Stream fs = new FileStream(filename, FileMode.Open))
            {
                ObservableCollection<Meal> meals;
                meals = (ObservableCollection<Meal>)serializer.Deserialize(fs);
                return meals;
            }
        }

    }
}
