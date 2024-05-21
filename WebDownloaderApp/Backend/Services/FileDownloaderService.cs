using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DownloaderApp.Backend.IServices;

namespace DownloaderApp.Backend.Services
{
    public class FileDownloadService : IFileDownloadService
    {
        private readonly HttpClient _httpClient;

        public FileDownloadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> DownloadImageAsync(string url, string savePath)
        {
            try
            {
                using var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return "Failed to download image. Invalid URL or server error.";
                }

                await using var fileStream = new FileStream(savePath, FileMode.Create);
                await response.Content.CopyToAsync(fileStream);

                return "Image downloaded successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to download image: {ex.Message}";
            }
        }

        public async Task<string> DownloadVideoAsync(string url, string savePath)
        {
            try
            {
                using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                {
                    return "Failed to download video. Invalid URL or server error.";
                }

                await using var fileStream = new FileStream(savePath, FileMode.Create);
                await response.Content.CopyToAsync(fileStream);

                return "Video downloaded successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to download video: {ex.Message}";
            }
        }
    }
}