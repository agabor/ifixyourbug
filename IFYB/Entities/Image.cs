using IFYB.Models;

namespace IFYB.Entities;

public class Image
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }

    public Image(string path, string name)
    {
        Path = path;
        Name = name;
    }
    public ImageDto ToDto()
    {
        return new ImageDto(Path);
    }
}