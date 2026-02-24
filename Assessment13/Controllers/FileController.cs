using AspNetCoreLoggingDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLoggingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            string filePath = await _fileService.SaveFileAsync(file);
            return Ok(new { FileName = file.FileName, FilePath = filePath });
        }

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            var fileBytes = _fileService.GetFile(fileName);
            if (fileBytes == null)
                return NotFound();

            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}
