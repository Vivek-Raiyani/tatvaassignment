using Microsoft.AspNetCore.Http;

namespace JobBoard.Services.Interfaces;

public interface IUploadServices
{
    public string GetUniqueFileName(string fileName);

    public string Upload(IFormFile Image, string folder_name);
}
