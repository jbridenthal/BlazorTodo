using BlazorTodo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorTodo.Services.Interface
{
    public interface ITodoService
    {
        Task<List<TodoModel>> AddTodo(string? todoTitle);
        Task<List<TodoModel>> ClearCompletedTodos();
        Task<List<TodoModel>> GetStoredTodos();
        Task SaveTodosToLocalStorage(IEnumerable<TodoModel> todos);
        Task<List<TodoModel>> ToggleTodoCompleted(TodoModel todo);
    }
}