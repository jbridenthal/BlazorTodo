
namespace BlazorTodo.Models;
public class TodoModel
{
    public Guid ID {  get; set; }
    public string? Name {  get; set; }
    public bool? Completed {  get; set; }
}
