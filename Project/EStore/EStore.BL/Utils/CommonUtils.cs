using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using EStore.BL.Services._Common;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;

namespace EStore.BL.Utils
{
    public static class CommonUtils
    {
        public static string ReverseMapPath(string path)
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var res = $"/{path.Replace(appPath, string.Empty).Replace("\\", "/")}";
            return res;
        }

        public static void DeleteFile(string url)
        {
            if (url.ToLower().StartsWith("http"))
                return;
            var path = HostingEnvironment.MapPath(url);
            if (File.Exists(path))
                File.Delete(path);
        }

        public static string CopyFileToDirectory(string file, string directoryName)
        {
            var fileName = Path.GetFileName(file);
            var urlPath = GenerateUrlPathForFile(directoryName, fileName);
            File.Copy(file, urlPath.Path);
            return urlPath.Url;
        }

        public static string UploadFileToDirectory(HttpPostedFileBase file, string directoryName)
        {
            if (file == null)
                throw new Exception("Empty file");

            var urlPath = GenerateUrlPathForFile(directoryName, file.FileName);
            file.SaveAs(urlPath.Path);
            return urlPath.Url;
        }

        public static string UploadFileToDirectory(MemoryStream file, string directoryName, string fileName)
        {
            if (file == null)
                throw new Exception("Empty file");

            var urlPath = GenerateUrlPathForFile(directoryName, fileName);
            file.SaveAs(urlPath.Path);
            return urlPath.Url;
        }


        public static string UploadFileToDirectory(byte[] file, string directoryName, string fileName)
        {
            using (var stream = new MemoryStream(file))
            {
                return UploadFileToDirectory(stream, directoryName, fileName);
            }
        }

        public static UrlPath GenerateUrlPathForFile(string directoryName, string fileName)
        {
            var dateFolder = DateTime.Now.ToString("dd-MM-yy");
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", directoryName, dateFolder, Guid.NewGuid().ToString());
            Directory.CreateDirectory(folder);
            var path = Path.Combine(folder, fileName);
            var url = ReverseMapPath(path);
            return new UrlPath
            {
                Path = path,
                Url = url
            };
        }

        public static void SaveAs(this MemoryStream inputStream, string path)
        {
            var ms = new MemoryStream(inputStream.ToArray());
            var fileStream = File.Create(path);
            ms.CopyTo(fileStream);
            fileStream.Close();
        }

        public static string LocationAndUnit(string unit, string location)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(unit))
                result += (unit + " / ");
            result += location;
            return result;
        }

        public static string StripHtml(string htmlString)
        {
            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public static bool HasImageExtension(this string source)
        {
            return (source.EndsWith(".png") || source.EndsWith(".jpg") || source.EndsWith(".gif") || source.EndsWith(".jpeg"));
        }

        public static byte[] DownloadData(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadData(url);
            }
        }

        public static string DownloadImageAndResize(string image)
        {
            using (var imageFactory = new ImageFactory(true))
            {
                using (var stream = new MemoryStream())
                {
                    imageFactory.Load(DownloadData(image))
                        .Resize(new ResizeLayer(new Size(800, 800),ResizeMode.Max))
                        .Format(new PngFormat())
                        .Save(stream);
                    var fileName = Path.GetFileNameWithoutExtension(image) + ".png";
                    var url = UploadFileToDirectory(stream, "Products", fileName);
                    return url;
                }
            }
        }
    }
}
