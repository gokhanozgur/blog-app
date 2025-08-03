namespace SharedEvents.Events.Categories;

public class CategoryCreatedEvent
{
    public string Id { get; set; } =  null!;
    public string Name { get; set; }  = null!;
}