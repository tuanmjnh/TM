using System;
//using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using TM.Helper;

namespace TM.IO
{
    public class FileDirectory
    {
        public static string MapPath(string path)
        {
            try
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/" + path.TrimStart('~').TrimStart('/'));
            }
            catch (Exception) { throw; } //return System.Web.HttpContext.Current.Server.MapPath("~/");
        }
        public static string MapPath()
        {
            return MapPath("~/");
        }
        public static string RootPartition()
        {
            return MapPath().Split('\\')[0];
        }
        public static System.Collections.Generic.Dictionary<string, object> Upload(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, bool Rename, string[] Extension, int MaxFileCount)
        {
            var UploadError = new System.Collections.Generic.List<string>();
            var UploadFile = new System.Collections.Generic.List<string>();
            var UploadFileSource = new System.Collections.Generic.List<string>();
            var rs = new System.Collections.Generic.Dictionary<string, object>();
            if (HttpFileCollectionBase.Count < 1 || (HttpFileCollectionBase.Count > 0 && HttpFileCollectionBase[0].ContentLength < 1))
                UploadError.Add("File does not exist!");
            else
            {
                CreateDirectory(DataSource);
                var tmp = DataSource.Trim('/').Split('/');
                for (int i = 0; i < HttpFileCollectionBase.Count; i++)
                {
                    if (MaxFileCount > 0)
                        if (i >= MaxFileCount)
                            break;

                    var file = HttpFileCollectionBase[i];

                    if (Extension != null)
                        if (!file.FileName.IsExtension(Extension))
                        {
                            UploadError.Add("Extension:" + file.FileName);
                            continue;
                        }

                    if (file.ContentLength > 0)
                    {
                        if (Rename)
                        {
                            var fileName = (tmp[tmp.Length - 1] + "_" + Guid.NewGuid().ToString("N") + file.FileName.ToExtension()).ToLower();
                            UploadFile.Add(fileName);
                            UploadFileSource.Add(MapPath(DataSource) + fileName);
                        }
                        else
                        {
                            UploadFile.Add(file.FileName);
                            UploadFileSource.Add(MapPath(DataSource) + file.FileName);
                        }
                        file.SaveAs(UploadFileSource[i]);
                    }
                }
            }
            rs.Add("UploadFileSource", UploadFileSource);
            rs.Add("UploadFileSourceString", Helper.Strings.ArrayToString(UploadFileSource));
            rs.Add("UploadFile", UploadFile);
            rs.Add("UploadFileString", Helper.Strings.ArrayToString(UploadFile));
            rs.Add("UploadError", UploadError);
            return rs;
        }
        public static System.Collections.Generic.Dictionary<string, object> Upload(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, string[] Extension, int MaxFileCount)
        {
            return Upload(HttpFileCollectionBase, DataSource, true, Extension, MaxFileCount);
        }
        public static System.Collections.Generic.Dictionary<string, object> Upload(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, string[] Extension)
        {
            return Upload(HttpFileCollectionBase, DataSource, true, Extension, 5);
        }
        public static System.Collections.Generic.Dictionary<string, object> Upload(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource)
        {
            return Upload(HttpFileCollectionBase, DataSource, true, null, 5);
        }
        public static System.Collections.Generic.Dictionary<string, object> Upload(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, bool Rename)
        {
            return Upload(HttpFileCollectionBase, DataSource, Rename, null, 5);
        }
        public static System.Collections.Generic.Dictionary<string, object> Upload(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, bool Rename, string[] Extension)
        {
            return Upload(HttpFileCollectionBase, DataSource, Rename, Extension, 5);
        }
        public static System.Collections.Generic.Dictionary<string, object> UploadImages(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, bool Rename, int MaxFileCount)
        {
            return Upload(HttpFileCollectionBase, DataSource, Rename, new[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp" }, MaxFileCount);
        }
        public static System.Collections.Generic.Dictionary<string, object> UploadImages(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, bool Rename)
        {
            return UploadImages(HttpFileCollectionBase, DataSource, Rename, 5);
        }
        public static System.Collections.Generic.Dictionary<string, object> UploadImages(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource, int MaxFileCount)
        {
            return UploadImages(HttpFileCollectionBase, DataSource, true, MaxFileCount);
        }
        public static System.Collections.Generic.Dictionary<string, object> UploadImages(System.Web.HttpFileCollectionBase HttpFileCollectionBase, string DataSource)
        {
            return UploadImages(HttpFileCollectionBase, DataSource, true, 5);
        }
        public static bool SetAccessRule(string directory, bool IsMapPath = true)
        {
            var Rights = (System.Security.AccessControl.FileSystemRights)0;
            Rights = System.Security.AccessControl.FileSystemRights.FullControl;
            // *** Add Access Rule to the actual directory itself
            var AccessRule = new System.Security.AccessControl.FileSystemAccessRule("Users", Rights,
                System.Security.AccessControl.InheritanceFlags.None,
                System.Security.AccessControl.PropagationFlags.NoPropagateInherit,
                System.Security.AccessControl.AccessControlType.Allow);

            directory = IsMapPath ? MapPath(directory) : directory;
            DirectoryInfo Info = new DirectoryInfo(directory);
            var Security = Info.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
            bool Result = false;
            Security.ModifyAccessRule(System.Security.AccessControl.AccessControlModification.Set, AccessRule, out Result);

            if (!Result)
                return false;
            // *** Always allow objects to inherit on a directory
            var iFlags = System.Security.AccessControl.InheritanceFlags.ObjectInherit;
            iFlags = System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit;
            // *** Add Access rule for the inheritance
            AccessRule = new System.Security.AccessControl.FileSystemAccessRule("Users", Rights,
                iFlags,
                System.Security.AccessControl.PropagationFlags.InheritOnly,
                System.Security.AccessControl.AccessControlType.Allow);
            Result = false;
            Security.ModifyAccessRule(System.Security.AccessControl.AccessControlModification.Add, AccessRule, out Result);
            if (!Result)
                return false;
            Info.SetAccessControl(Security);
            return true;
        }
        public static bool Rename(string sourceFile, string DestFile, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                DestFile = IsMapPath ? MapPath(DestFile) : DestFile;
                File.Move(sourceFile, DestFile);
                return true;
            }
            catch (Exception) { return false; }
        }
        public static FileInfo ReExtension(string sourceFile, string extension, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                var file = new FileInfo(sourceFile);
                var DestFile = sourceFile.Replace(file.Extension, extension);
                File.Move(sourceFile, DestFile);
                return new FileInfo(DestFile);
            }
            catch (Exception) { return null; }
        }
        public static FileInfo ReExtensionToLower(string sourceFile, bool IsMapPath = true)
        {
            sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
            var file = new FileInfo(sourceFile);
            try
            {
                var DestFile = sourceFile.Replace(file.Extension, file.Extension.ToLower());
                File.Move(sourceFile, DestFile);
                return new FileInfo(DestFile);
            }
            catch (Exception) { return file; }
        }
        public static bool Copy(string sourceFile, string DestFile, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                DestFile = IsMapPath ? MapPath(DestFile) : DestFile;
                File.Copy(sourceFile, DestFile);
                return true;
            }
            catch (Exception) { return false; }
        }
        public static bool Copy(string sourceFile)
        {
            return Copy(sourceFile, CreateFileExist(sourceFile));
        }
        public static bool Delete(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else return true;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, string[] files, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(path + item))
                        File.Delete(path + item);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, FileInfo[] files, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(path + item.Name))
                        File.Delete(path + item.Name);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool DeleteDirectory(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                if (Directory.Exists(path))
                {
                    foreach (var item in Files(path, false))
                        File.Delete(item.FullName);
                    Directory.Delete(path);
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool CreateDirectory(string path, bool IsMapPath = true)
        {
            try
            {
                var securityRules = new DirectorySecurity();
                //securityRules.AddAccessRule(new FileSystemAccessRule(@"Domain\account1", FileSystemRights.Read, AccessControlType.Allow));
                //securityRules.AddAccessRule(new FileSystemAccessRule(@"Domain\account2", FileSystemRights.FullControl, AccessControlType.Allow));

                path = IsMapPath ? MapPath(path) : path;
                path = path.Trim('/', '\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    var directory = new DirectoryInfo(path);
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }
        public static string CreateFileExist(string file, bool IsMapPath = true)
        {
            try
            {
                int countFile = 0;
                file = IsMapPath ? MapPath(file) : file;
                string extension = Path.GetExtension(MapPath(file));
                while (File.Exists(file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension))
                    countFile++;
                file = file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension;
                return file;
            }
            catch (Exception) { throw; }
        }
        public static byte[] ReturnByteFile(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                byte[] fileBytes = File.ReadAllBytes(path);
                return fileBytes;
            }
            catch (Exception) { throw; }
        }
        public static System.Web.Mvc.FileContentResult FileContentResult(string path, string DestName)
        {
            return new System.Web.Mvc.FileContentResult(ReturnByteFile(path), System.Net.Mime.MediaTypeNames.Application.Octet) { FileDownloadName = DestName };
        }
        public static System.Web.Mvc.FileContentResult FileContentResult(string path)
        {
            path = path.Replace('\\', '/');
            string[] tmp = path.Trim('/').Split('/');
            string FileName = tmp[tmp.Length - 1];
            return FileContentResult(path, FileName);
        }
        public static DirectoryInfo[] Directories(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                var Dir = new DirectoryInfo(path);
                return Dir.GetDirectories();
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.Generic.List<string> DirectoriesToList(string path, bool IsMapPath = true)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                var subDir = Directories(path, IsMapPath);
                foreach (var item in subDir)
                    list.Add(item.Name);
                return list;
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, string[] extension = null, bool IsMapPath = true)
        {
            try
            {
                //var files = System.IO.Directory.GetDirectories(path);
                //string[] ext = new[] { ".dbf" };
                path = IsMapPath ? MapPath(path) : path;
                var Dir = new DirectoryInfo(path);
                if (extension != null)
                    return Dir.GetFiles().Where(f => extension.Contains(f.Extension.ToLower())).ToArray();
                else
                    return Dir.GetFiles();
                //var subFiles = di.GetFiles("*.dbf").Concat(di.GetFiles("*.dbf2"));
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, bool IsMapPath = true)
        {
            return Files(path, null, IsMapPath);
        }
        public static System.Collections.Generic.List<string> FilesToList(string path, string[] extension, bool IsMapPath = true)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                var subFiles = Files(path, extension, IsMapPath);
                foreach (var item in subFiles)
                    list.Add(item.Name.Replace(item.Extension, item.Extension.ToLower()));
                return list;
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.Generic.List<string> FilesToList(string path, bool IsMapPath = true)
        {
            return FilesToList(path, null, IsMapPath);
        }
        public static string fileUpload(string path, System.Web.HttpPostedFileBase file)
        {
            try
            {
                CreateDirectory(path);
                string s = file.FileName.ToExtension();
                if (file.ContentLength > 0)
                {
                    //s = path + file.FileName.Replace(s, "") + "_" + DateTime.Now.ToString("yyyMMdd") + "_" + DateTime.Now.ToString("hhmmss") + s;
                    s = MapPath(path) + file.FileName;
                    file.SaveAs(s);
                }
                else
                    return null;
                return s;
            }
            catch (Exception) { return null; }
        }
        public static string[] ReadFile(string filename, bool IsMapPath = true)
        {
            filename = IsMapPath ? MapPath(filename) : filename;
            var list = System.IO.File.ReadAllLines(filename);
            return list;
        }
        public static System.Collections.Generic.List<string[]> ReadFile(string filename, char split, bool IsMapPath = true)
        {
            var rs = new System.Collections.Generic.List<string[]>();
            foreach (var item in ReadFile(filename, IsMapPath))
            {
                var tmp = item.Trim().Split(split);
                rs.Add(tmp);
            }
            return rs;
        }
        public static System.Collections.Generic.List<string[]> ReadFile(string filename, string split, bool IsMapPath = true)
        {
            var rs = new System.Collections.Generic.List<string[]>();
            foreach (var item in ReadFile(filename, IsMapPath))
            {
                var tmp = item.Trim().Replace(split, "\t").Split('\t');
                rs.Add(tmp);
            }
            return rs;
        }
        public static System.Collections.Generic.List<string> ImageCodecs()
        {
            //var rs = new System.Collections.Generic.List<string>();
            //foreach (var item in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().Select(codec => codec.FilenameExtension).ToList())
            //    rs.Add(item.TrimStart('*'));
            //return rs;
            return System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().Select(codec => codec.FilenameExtension).ToList();
        }

    }
    public class Zip
    {
        public static void CompressFolder(string path, ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream, int folderOffset)
        {

            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
                var newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

                // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
                // A password on the ZipOutputStream is required if using AES.
                //   newEntry.AESKeySize = 256;

                // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
                // you need to do one of the following: Specify UseZip64.Off, or set the Size.
                // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
                // but the zip will be in Zip64 format which not all utilities can understand.
                //   zipStream.UseZip64 = UseZip64.Off;
                newEntry.Size = fi.Length;

                zipStream.PutNextEntry(newEntry);

                // Zip the file in buffered chunks
                // the "using" will close the stream even if an exception occurs
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = File.OpenRead(filename))
                {
                    ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }
        public static void CreateSample(string outPathname, string password, string folderName)
        {

            FileStream fsOut = File.Create(outPathname);
            var zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(fsOut);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            zipStream.Password = password; // optional. Null is the same as not setting. Required if using AES.

            // This setting will strip the leading part of the folder path in the entries, to
            // make the entries relative to the starting folder.
            // To include the full path for each entry up to the drive root, assign folderOffset = 0.
            int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

            CompressFolder(folderName, zipStream, folderOffset);

            zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
            zipStream.Close();
        }
        public static MemoryStream CreateToMemoryStream(MemoryStream memStreamIn, string zipEntryName)
        {

            MemoryStream outputMemStream = new MemoryStream();
            var zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outputMemStream);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            var newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(zipEntryName);
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);

            ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(memStreamIn, zipStream, new byte[4096]);
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = false; // False stops the Close also Closing the underlying stream.
            zipStream.Close(); // Must finish the ZipOutputStream before using outputMemStream.

            outputMemStream.Position = 0;
            return outputMemStream;

            // Alternative outputs:
            // ToArray is the cleaner and easiest to use correctly with the penalty of duplicating allocated memory.

            //byte[] byteArrayOut = outputMemStream.ToArray();

            // GetBuffer returns a raw buffer raw and so you need to account for the true length yourself.
            //byte[] byteArrayOut = outputMemStream.GetBuffer();
            long len = outputMemStream.Length;
        }
        public static void ZipFile(System.Collections.Generic.List<string> filesToZip, string outFile, int compression = 3, bool IsMapPath = true)
        {
            outFile = IsMapPath ? TM.IO.FileDirectory.MapPath(outFile) : outFile;
            if (compression < 0 || compression > 9)
                throw new ArgumentException("Invalid compression rate (just 0-9).");

            if (!Directory.Exists(new FileInfo(outFile).Directory.ToString()))
                throw new ArgumentException("The Path does not exist.");

            foreach (string c in filesToZip)
                if (!File.Exists(IsMapPath ? TM.IO.FileDirectory.MapPath(c) : c))
                    throw new ArgumentException(string.Format("The File {0} does not exist!", c));

            var crc32 = new ICSharpCode.SharpZipLib.Checksum.Crc32();
            var stream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(File.Create(outFile));
            stream.SetLevel(compression);

            for (int i = 0; i < filesToZip.Count; i++)
            {
                var entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(Path.GetFileName(filesToZip[i]));
                entry.DateTime = DateTime.Now;
                var _filesToZip = IsMapPath ? TM.IO.FileDirectory.MapPath(filesToZip[i]) : filesToZip[i];
                using (FileStream fs = File.OpenRead(_filesToZip))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry.Size = fs.Length;
                    fs.Close();
                    crc32.Reset();
                    crc32.Update(buffer);
                    entry.Crc = crc32.Value;
                    stream.PutNextEntry(entry);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            stream.Finish();
            stream.Close();
        }
        public static void DownloadZipToBrowser(System.Collections.Generic.List<string> zipFileList)
        {

            System.Web.HttpContext.Current.Response.ContentType = "application/zip";
            // If the browser is receiving a mangled zipfile, IIS Compression may cause this problem. Some members have found that
            //Response.ContentType = "application/octet-stream" has solved this. May be specific to Internet Explorer.

            System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=\"Download.zip\"");
            System.Web.HttpContext.Current.Response.CacheControl = "Private";
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(5)); // or put a timestamp in the filename in the content-disposition

            byte[] buffer = new byte[4096];

            var zipOutputStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(System.Web.HttpContext.Current.Response.OutputStream);
            zipOutputStream.SetLevel(3); //0-9, 9 being the highest level of compression

            foreach (string fileName in zipFileList)
            {

                Stream fs = File.OpenRead(TM.IO.FileDirectory.MapPath(fileName)); // or any suitable inputstream
                var entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(fileName));
                entry.Size = fs.Length;
                // Setting the Size provides WinXP built-in extractor compatibility,
                //  but if not available, you can set zipOutputStream.UseZip64 = UseZip64.Off instead.

                zipOutputStream.PutNextEntry(entry);

                int count = fs.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    zipOutputStream.Write(buffer, 0, count);
                    count = fs.Read(buffer, 0, buffer.Length);
                    if (!System.Web.HttpContext.Current.Response.IsClientConnected)
                    {
                        break;
                    }
                    System.Web.HttpContext.Current.Response.Flush();
                }
                fs.Close();
            }
            zipOutputStream.Close();

            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        public static MemoryStream CopyStream(Stream input)
        {
            var output = new MemoryStream();
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
            return output;
        }
        //
        public static byte[] Compress(byte[] data, string fileName)
        {
            // Compress
            using (MemoryStream fsOut = new MemoryStream())
            {
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(fsOut))
                {
                    zipStream.SetLevel(3);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);
                    newEntry.DateTime = DateTime.UtcNow;
                    newEntry.Size = data.Length;
                    zipStream.PutNextEntry(newEntry);
                    zipStream.Write(data, 0, data.Length);
                    zipStream.Finish();
                    zipStream.Close();
                }
                return fsOut.ToArray();
            }
        }
        public static void Compress(Stream data, Stream outData, string fileName)
        {
            string str = "";
            try
            {
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outData))
                {
                    zipStream.SetLevel(3);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);
                    newEntry.DateTime = DateTime.UtcNow;
                    zipStream.PutNextEntry(newEntry);
                    data.Position = 0;
                    int size = (data.CanSeek) ? Math.Min((int)(data.Length - data.Position), 0x2000) : 0x2000;
                    byte[] buffer = new byte[size];
                    int n;
                    do
                    {
                        n = data.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, n);
                    } while (n != 0);
                    zipStream.CloseEntry();
                    zipStream.Flush();
                    zipStream.Close();
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

        }
        public static void Compress2(Stream data, Stream outData, string fileName)
        {
            string str = "";
            try
            {
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outData))
                {
                    zipStream.SetLevel(3);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);
                    newEntry.DateTime = DateTime.UtcNow;
                    zipStream.PutNextEntry(newEntry);
                    data.CopyTo(zipStream);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

        }
        public static void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
        {
            ICSharpCode.SharpZipLib.Zip.ZipFile zf = null;
            try
            {
                FileStream fs = File.OpenRead(archiveFilenameIn);
                zf = new ICSharpCode.SharpZipLib.Zip.ZipFile(fs);
                if (!String.IsNullOrEmpty(password))
                    zf.Password = password;     // AES encrypted entries are handled automatically

                foreach (ICSharpCode.SharpZipLib.Zip.ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;           // Ignore directories
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    byte[] buffer = new byte[4096];     // 4K is optimum
                    Stream zipStream = zf.GetInputStream(zipEntry);

                    // Manipulate the output filename here as desired.
                    String fullZipToPath = Path.Combine(outFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (zf != null)
                {
                    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                    zf.Close(); // Ensure we release resources
                }
            }
        }
    }
}
public static class IOS
{
    public static string ToExtension(this string file)
    {
        try
        {
            return Path.GetExtension(file);
        }
        catch (Exception) { throw; }
    }
    public static string ToExtensionNone(this string file)
    {
        return ToExtension(file).Trim('.');
    }
    public static bool IsExtension(this string file, string Extension = null)
    {
        try
        {
            if (string.IsNullOrEmpty(Extension))
                return false;
            var tmp = Extension.Trim().Trim(',').Split(',').ToLower();
            if (Array.IndexOf(tmp, Path.GetExtension(file).ToLower()) > -1)
                return true;
            return false;
        }
        catch (Exception) { throw; }
    }
    public static bool IsExtension(this string file, string[] Extension)
    {
        if (Extension.Length > 0 && Array.IndexOf(Extension.ToLower(), Path.GetExtension(file).ToLower()) > -1)
            return true;
        return false;
    }
    public static bool IsExtension(this string file, System.Collections.Generic.List<string> Extension)
    {
        if (Extension.Count > 0 && Extension.ToLower().Contains(Path.GetExtension(file).ToLower()))
            return true;
        return false;
    }
    public static System.Collections.Generic.List<string> UploadFileSource(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadFileSource"];
        }
        catch (Exception)
        {
            return null;
        }

    }
    public static string UploadFileSourceString(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (string)Upload["UploadFileSourceString"];
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static System.Collections.Generic.List<string> UploadFile(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadFile"];
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static string UploadFileString(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (string)Upload["UploadFileString"];
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static System.Collections.Generic.List<string> UploadError(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadError"];
        }
        catch (Exception)
        {
            return null;
        }
    }
}