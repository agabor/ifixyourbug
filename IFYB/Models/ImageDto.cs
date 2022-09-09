namespace IFYB.Models;
public class ImageDto
{
    public string Location { get; set; }

    public ImageDto(string location)
    {
        Location = location;
    }
}