using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyWhatToEat.Model
{
    [XmlRoot(ElementName = "ListOfMeals")]
    public class ListOfMeals : List<Meal>
    {

    }
}
