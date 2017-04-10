using System;
using System.Web;
namespace TM
{
    public class ResizeImage
    {
        public static System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                System.Drawing.Bitmap cpy = new System.Drawing.Bitmap(nnx, nny, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(cpy))
                {
                    gr.Clear(System.Drawing.Color.Transparent);
                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(img,
                        new System.Drawing.Rectangle(0, 0, nnx, nny),
                        new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
                        System.Drawing.GraphicsUnit.Pixel);
                }
                return cpy;
            }
        }
        public static System.IO.MemoryStream ByteArrayToStream(byte[] arr)
        {
            return new System.IO.MemoryStream(arr, 0, arr.Length);
        }
        public static string Upload(System.Web.UI.WebControls.FileUpload fileupload)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(fileupload.InputStream);
                byte[] buffer = fileupload.FileBytes;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.UI.WebControls.FileUpload fileupload, int MW, int MH)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = fileupload.FileBytes;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.UI.WebControls.FileUpload fileupload, string path)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = fileupload.FileBytes;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.UI.WebControls.FileUpload fileupload, string path, int MW, int MH)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = fileupload.FileBytes;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.UI.WebControls.FileUpload fileupload, string path, string fileName)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = fileupload.FileBytes;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.UI.WebControls.FileUpload fileupload, string path, string fileName, int MW, int MH)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                //System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = fileupload.FileBytes;
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFile postFile)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFile postFile, int MW, int MH)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFile postFile, string path)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFile postFile, string path, int MW, int MH)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFile postFile, string path, string fileName)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFile postFile, string path, string fileName, int MW, int MH)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFileBase postFile)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFileBase postFile, int MW, int MH)
        {
            string s = Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                //System.Web.HttpFileCollection fileCol = Request.Files;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFileBase postFile, string path)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFileBase postFile, string path, int MW, int MH)
        {
            string s = path + Guid.NewGuid() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFileBase postFile, string path, string fileName)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), 1024, 1024);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
        public static string Upload(System.Web.HttpPostedFileBase postFile, string path, string fileName, int MW, int MH)
        {
            string s = path + fileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
            try
            {
                IO.FileDirectory.CreateDirectory(path);;
                System.IO.BinaryReader b = new System.IO.BinaryReader(postFile.InputStream);
                byte[] buffer = b.ReadBytes(postFile.ContentLength);
                System.Drawing.Image img = RezizeImage(System.Drawing.Image.FromStream(ByteArrayToStream(buffer)), MW, MH);
                img.Save(HttpContext.Current.Server.MapPath(TM.Url.BaseUrl + s), System.Drawing.Imaging.ImageFormat.Jpeg);
                return s;
            }
            catch (Exception) { return ""; }
        }
    }
}