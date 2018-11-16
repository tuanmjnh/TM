using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Common
{
    public class Directories
    {
        public const string Uploads = "Uploads\\";
        public const string data = Uploads + "Data\\";
        public const string HDData = data + "HDData\\";
        public const string HDDataSource = data + "HDDataSource\\";
        public const string DatCocTraTruoc = data + "DatCocTraTruoc\\";
        public const string images = Uploads + "Images\\";
        public const string imagesProduct = images + "Product\\";
        public const string imagesCustomer = images + "Customer\\";
        public const string document = data + "Document\\";
        public const string orther = Uploads + "Orther\\";
        public const string ccbs = data + "ccbs\\";
        public const string CA = document + "CA\\";
        public const string IVAN = document + "IVAN\\";
        public const string KTR = document + "KTR\\";
        public const string TraSauReport = data + "Report\\";
        public const string Hopdong = Uploads + "Hopdong\\";
        public static string DBBak { get { return $"{TM.IO.FileDirectory.RootPartition()}\\Drive\\DBBak\\"; } }
    }
}
namespace TM.Common.Objects
{
    public class FileManager
    {
        public const string directory = "Directory";
        public const string file = "File";
    }
}