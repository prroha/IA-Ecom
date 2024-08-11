using IA_Ecom.Models;

namespace IA_Ecom.Services;

public class AdminService: IAdminService
{
    public AdminService()
    {
    }
    public async Task<List<string>> UploadImagesAsync(List<IFormFile> images)
    {
        var imageUrls = new List<string>();
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data/Users");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        foreach (var image in images)
        {
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            var imageUrl = $"/uploads/{fileName}";
            imageUrls.Add(imageUrl);
        }

        return imageUrls;
    }
}