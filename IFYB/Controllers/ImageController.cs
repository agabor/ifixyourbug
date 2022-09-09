
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Scriban;
using IFYB.Services;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/image")]
public class ImageController : BaseController
{
    private readonly AppOptions appOptions;
    public ImageController(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions) : base(dbContext)
    {
        this.appOptions = appOptions.Value;
    }

    [HttpPost]
    [Produces(typeof(ImageDto))]
    public IActionResult UploadImage(IFormFile file) {
        if(!Directory.Exists(appOptions.ImgFolder))
            Directory.CreateDirectory(appOptions.ImgFolder);
        var name = Guid.NewGuid().ToString().Replace("-", string.Empty);
        var extension = file.FileName.Split(".")[1];
        var fileName = $"{name}.{extension}";
        var path = Path.Combine(appOptions.ImgFolder, fileName);
        using var fileStream = new FileStream(path, FileMode.Create);
        file.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Close();
        Image image = new Image(fileName);
        dbContext.Images.Add(image);
        dbContext.SaveChanges();
        return Ok(new ImageDto($"/img/{fileName}"));
    }
}