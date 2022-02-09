namespace HttpModels;

public class Category
{
    private static int Ids = 0;
    public int Id { get; set; }
    public string Name { get; set; }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }
}