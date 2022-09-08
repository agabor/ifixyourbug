namespace IFYB.Models;
public class ImageDto
{
    public int ClientId { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }

    public ImageDto(int clientId, string path, string name)
    {
        ClientId = clientId;
        Path = path;
        Name = name;
    }
}