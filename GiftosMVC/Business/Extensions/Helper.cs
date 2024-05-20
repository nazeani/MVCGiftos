using Business.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class Helper
    {
        public static string SaveFile(string rootPath, string folder, IFormFile file)
        {
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png") throw new ImageContentException("Seklin uzantisi jpeg/jpg/png deyil!");
            if (file.Length > 20000000) throw new ImageLengthException("Sekil max 2mb ola biler!");
            string fileName=Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path=rootPath+ $@"\{folder}\" + fileName;
            using(FileStream filestream=new FileStream(path,FileMode.Create))
            {
               file.CopyTo(filestream);
            }
            return fileName;

        }
        public static void DeleteFile(string rootPath,string folder,string fileName) 
        {
            string path = rootPath + $@"\{folder}\" + fileName;
            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("File tapilmadi!");
            File.Delete(path);
        }
    }
}
