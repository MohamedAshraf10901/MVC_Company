using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            // 1. get location folder path

            //string folderBath = "I:\\C#Assignments\\MVC\\MVC_Session05\\MVC_Demo_5\\Demo\\wwwroot\\files\\" + folderName;

            //string folderBath = Directory.GetCurrentDirectory() + "wwwroot\\files\\" + folderName;

            string folderBath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2. get file name (uniqe)

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. get file bath --> [folderBath + fileName]

            string fileBath = Path.Combine(folderBath,fileName);

            // 4. save file as stream : Data per time

            using var fileStream = new FileStream(fileBath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileBath;



        }

        public static void DeleteFile(string fileName,string folderName)
        {
            string fileBath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);

            if (File.Exists(fileBath))
            {
                File.Delete(fileBath);
            }
        }
    }
}
