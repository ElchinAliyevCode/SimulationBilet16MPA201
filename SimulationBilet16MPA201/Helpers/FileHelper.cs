namespace SimulationBilet16MPA201.Helpers;

public static class FileHelper
{
    public static bool CheckSize(this IFormFile file, int size)
    {
        return file.Length < size * 1024 * 1024;
    }

    public static bool CheckType(this IFormFile file, string type)
    {
        return file.ContentType.Contains(type);
    }

    public static async Task<string> UploadFileAsync(this IFormFile file, string folderpath)
    {
        string uniqueFileName = Guid.NewGuid().ToString() + file.FileName;

        string path = Path.Combine(folderpath, uniqueFileName);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(stream);

        return uniqueFileName;
    }

    public static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
