using JobBoard.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Services.Implementations;

public class UploadServices(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env) :IUploadServices
{
    private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment = env;

    public string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return string.Concat(Path.GetFileNameWithoutExtension(fileName), "_", Guid.NewGuid().ToString().AsSpan(0, 4), Path.GetExtension(fileName));
    }

    public string Upload(IFormFile Image, string folder_name)
    {
        try
        {
            if (Image != null)
            {
                string FileName = GetUniqueFileName(Image.FileName);

                string imgPath = Path.Combine("uploads", $"{folder_name}");
                string imgLocation = Path.Combine(imgPath, FileName);
                imgPath = Path.Combine(hostingEnvironment.WebRootPath, imgPath);

                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }
                string uploadPath = Path.Combine(imgPath, FileName);
                Image.CopyTo(new FileStream(uploadPath, FileMode.Create));

                return imgLocation;
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }

    }

}
