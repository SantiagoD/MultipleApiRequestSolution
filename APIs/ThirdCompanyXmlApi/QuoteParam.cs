using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ThirdCompanyXmlApi
{
    //[XmlRoot(ElementName = "quoteparam")]
    public class QuoteParam
    {
        //[XmlElement(ElementName = "source")]
        public Address Source { get; set; }

        //[XmlElement(ElementName = "destination")]
        public Address Destination { get; set; }

        //[XmlArray(ElementName = "packages")]
        public List<PackageDetail> Packages { get; set; }
    }
}
