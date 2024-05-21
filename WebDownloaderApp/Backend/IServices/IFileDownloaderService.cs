namespace DownloaderApp.Backend.IServices
{
    public interface IFileDownloadService
    {
        Task<string> DownloadImageAsync(string url, string savePath);
        Task<string> DownloadVideoAsync(string url, string savePath);
    }
}
