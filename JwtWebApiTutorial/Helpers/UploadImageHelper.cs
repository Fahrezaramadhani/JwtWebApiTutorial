// <copyright file="ImageConstant.cs" company="CV Garuda Infinity Kreasindo">
// Copyright (c) CV Garuda Infinity Kreasindo. All rights reserved.
// </copyright>

using JwtWebApiTutorial.Constants;
using System.Text.RegularExpressions;

namespace JwtWebApiTutorial.Helpers
{
    public class UploadImageHelper
    {
        public static string UploadBase64File(string identity, string base64, string fileRootFolder)
        {
            var (bytes, type) = GetFileDetail(base64);

            var filename = string.Concat(identity, type);
            var directory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(directory, ImageConstant.WWWROOT, fileRootFolder);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            filePath = Path.Combine(filePath, filename);

            if (bytes.Length > 0)
            {
                using var stream = new FileStream(filePath, FileMode.Create);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }

            return Path.Combine(fileRootFolder, filename);
        }

        public static void DeleteImage(string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), ImageConstant.WWWROOT, filename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static (byte[] bytes, string type) GetFileDetail(string base64)
        {
            var type = GetFileExtension(base64);
            var result = Regex.Replace(base64.Trim(), ImageConstant.BASE64REGEX, string.Empty).Trim();
            var bytes = Convert.FromBase64String(result);
            return (bytes, type);
        }

        public static bool IsBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                return false;
            }

            var base64String = Regex.Replace(base64.Trim(), ImageConstant.BASE64REGEX, string.Empty).Trim();
            if (base64String.Length % 4 != 0 || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 7)
                      + Path.GetExtension(fileName);
        }

        public static void RemoveDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
        }

        private static string GetFileExtension(string base64)
        {
            var match = Regex.Match(base64, ImageConstant.BASE64REGEX);
            if (match.Groups.Count < 2)
            {
                return string.Empty;
            }

            var data = match.Groups[1].Value;

            return data.ToLower() switch
            {
                "image/png" => ".png",
                "image/jpeg" => ".jpg",
                "application/pdf" => ".pdf",
                "application/msword" => ".doc",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
                "application/vnd.ms-excel" => ".xls",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ".xlsx",
                "text/csv" => ".csv",
                _ => string.Empty,
            };
        }
    }
}
