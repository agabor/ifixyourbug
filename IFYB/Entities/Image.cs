using IFYB.Models;

namespace IFYB.Entities;

public class Image
{
    public int Id { get; set; }
    public int? OrderId { get; set; }
    public string Location { get; set; }

    public Image(string location)
    {
        Location = location;
    }

    public static Image FromDto(ImageDto dto)
    {
        return new Image(dto.Location);
    }

    public ImageDto ToDto()
    {
        return new ImageDto(Location);
    }
}