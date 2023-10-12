using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

[Serializable] 
public class User
{
    public string name { get; set; }
    public int age { get; set; } 
}
class pw3
{
    static void Main()
    {
        User Misha = new User
        {
            name = "Misha",
            age = 18
        };
        XmlSerializer xml = new XmlSerializer(typeof(User)); 
        using(StreamWriter xmlWriter = new StreamWriter("user.xml"))
        {
            xml.Serialize(xmlWriter, Misha); 
        }
        Console.WriteLine("xml:");
        using (StreamReader readxml = new StreamReader("user.xml"))
        {
            User desUser = (User)xml.Deserialize(readxml);
            Console.WriteLine($"name: {desUser.name}, age: {desUser.age}");
        }
        SoapFormatter soapFormatter = new SoapFormatter();
        using (Stream stream = new FileStream("person.soap", FileMode.Create, FileAccess.Write))
        {
            soapFormatter.Serialize(stream, Misha);
        }
        Console.WriteLine("soap:");
        using (Stream stream = new FileStream("person.soap", FileMode.Open, FileAccess.Read))
        {
            var deserializedPerson = (User)soapFormatter.Deserialize(stream);
            Console.WriteLine($"Name: {deserializedPerson.name}, Age: {deserializedPerson.age}");
        }
    }
}