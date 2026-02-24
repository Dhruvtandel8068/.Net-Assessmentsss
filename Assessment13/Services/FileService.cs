using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCoreLoggingDemo.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFileAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            string folderPath = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public byte[] GetFile(string fileName)
        {
            string folderPath = Path.Combine(_env.ContentRootPath, "Uploads");
            string filePath = Path.Combine(folderPath, fileName);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllBytes(filePath);
        }
    }
}
