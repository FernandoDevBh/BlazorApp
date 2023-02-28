using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Client.Helper;

public static class ImageHelper
{
    public static async Task<string> GetImageDataAsync(InputFileChangeEventArgs e)
    {
        var extensions = new List<string>(3) { ".jpg", ".png", ".jpeg", ".webp" };
        var data = string.Empty;
        try
        {
            var files = e.GetMultipleFiles()
                         .Select(f => new { File = f, FileInfo = new FileInfo(f.Name) });

            if (files.Any())
            {
                foreach (var item in files)
                {
                    if (extensions.Contains(item.FileInfo.Extension.ToLower()))
                    {
                        IBrowserFile imgFile = item.File;
                        var buffers = new byte[imgFile.Size];
                        await imgFile.OpenReadStream().ReadAsync(buffers);
                        string imageType = imgFile.ContentType;
                        data = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
                    }
                    else
                    {
                       throw new BadImageFormatException("Please select .jpg/ .jpeg/ .webp/ .png file only");
                    }
                }
            }
            return data;
        }
        catch (Exception exception)
        {
            throw new ApplicationException(exception.Message, exception);
        }
    }
}
