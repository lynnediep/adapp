using System.Xml.Serialization;

namespace ADapp
{
    public class Address
    {
        public string DNcomputer { get; set; }
        public string DNuser { get; set; }
    }

    [XmlRootAttribute("ADAddress")]
    public class ADAddress
    {
        [XmlElement("Address")]
        public Address[] domains { get; set; }
    }
}
