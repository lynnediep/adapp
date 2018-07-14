using System.Xml.Serialization;

namespace ADapp
{
    public class Address
    {
        public string OU1 { get; set; }
        public string OU2 { get; set; }
        public string OU3 { get; set; }
        public string OU4 { get; set; }
        public string OU5 { get; set; }
        public string OU6 { get; set; }
        public string OU7 { get; set; }
        public string OU8 { get; set; }
        public string DC1 { get; set; }
        public string DC2 { get; set; }
        public string DC3 { get; set; }
    }

    [XmlRootAttribute("ADAddress")]
    public class ADAddress
    {
        [XmlElement("Address")]
        public Address[] clientList { get; set; }
    }
}
