namespace IFYB;

class Order
{
    public int Id { get; set; }
    public Framework Framework { get; set; }
    public string GitUrl { get; set; }

}

enum Framework { DotNet, Vue }