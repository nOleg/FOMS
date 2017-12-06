using System.Collections.Generic;
using System.Xml.Serialization;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;
 
namespace DMZPage.Models
{
[XmlRoot()]
    public class Settings
    {
        [XmlElement()]
        public string RootFolderPath { get; set; }
        [XmlElement()]
        public string ErrorFolderPath { get; set; }
        [XmlElement()]
        public string ResponceFolderPath { get; set; }
        [XmlElement()]
        public string VerifiedFolderPath { get; set; }
        [XmlElement()]
        public int AttempCount { get; set; }
        [XmlElement()]
        public string ReportPath1 { get; set; }
        [XmlElement()]
        public string ReportPath2 { get; set; }
    }

}