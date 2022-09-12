
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

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
        var fileName = $"{name}.webp";
        var path = Path.Combine(appOptions.ImgFolder, fileName);
        using var fileStream = new FileStream(path, FileMode.Create);
        using var rawImg = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
        rawImg.Save(fileStream, WebpFormat.Instance);

        var image = new IFYB.Entities.Image(fileName);
        dbContext.Images.Add(image);
        dbContext.SaveChanges();
        return Ok(new ImageDto($"/img/{fileName}"));
    }
}