
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
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
        var fileName = $"{name}.jpeg";
        var path = Path.Combine(appOptions.ImgFolder, fileName);
        using var fileStream = new FileStream(path, FileMode.Create);
        using var rawImg = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
        if(rawImg.Width > 1920 || rawImg.Height > 1080) {
            float scaleX = rawImg.Width / 1920.0f;
            float scaleY = rawImg.Height / 1080.0f;
            float scaleFactore = scaleX > scaleY ? scaleX : scaleY;
            rawImg.Mutate(x => x.Resize((int)(rawImg.Width / scaleFactore), (int)(rawImg.Height / scaleFactore)));
        }
        rawImg.SaveAsJpeg(fileStream);
        dbContext.Images.Add(new IFYB.Entities.Image(fileName));
        dbContext.SaveChanges();
        return Ok(new ImageDto($"/img/{fileName}"));
    }
}