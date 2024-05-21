using Microsoft.AspNetCore.Mvc;
using DownloaderApp.Backend.IServices;
using System;
using System.Threading.Tasks;

namespace DownloaderApp.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IFileDownloadService _fileDownloadService;

        public DownloadController(IFileDownloadService fileDownloadService)
        {
            _fileDownloadService = fileDownloadService;
        }

        [HttpPost("image")]
        public async Task<IActionResult> DownloadImage([FromBody] DownloadRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url) || string.IsNullOrWhiteSpace(request.SavePath))
            {
                return BadRequest("URL and save path are required.");
            }

            var result = await _fileDownloadService.DownloadImageAsync(request.Url, request.SavePath);
            return Ok(result);
        }

        [HttpPost("video")]
        public async Task<IActionResult> DownloadVideo([FromBody] DownloadRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url) || string.IsNullOrWhiteSpace(request.SavePath))
            {
                return BadRequest("URL and save path are required.");
            }

            var result = await _fileDownloadService.DownloadVideoAsync(request.Url, request.SavePath);
            return Ok(result);
        }
    }

    public class DownloadRequest
    {
        public string Url { get; set; }
        public string SavePath { get; set; }
    }
}