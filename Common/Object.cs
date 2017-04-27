using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Directories
    {
        public const string Uploads = "Uploads\\";
        public const string data = Uploads + "Data\\";
        public const string HDData = data + "HDData\\";
        public const string images = Uploads + "Images\\";
        public const string imagesProduct = images + "Product\\";
        public const string imagesCustomer = images + "Customer\\";
        public const string document = data + "Document\\";
        public const string orther = Uploads + "Orther\\";
        public const string ccbs = data + "ccbs\\";
        public const string DBBak = Uploads + "DBBak\\";
        public const string CA = document + "CA\\";
        public const string IVAN = document + "IVAN\\";
        public const string KTR = document + "KTR\\";
    }
}
namespace Common.Objects
{
    public class FileManager
    {
        public const string directory = "Directory";
        public const string file = "File";
    }
}