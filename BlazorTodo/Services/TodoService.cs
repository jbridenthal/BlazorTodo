using BlazorTodo.Models;
using Blazored.LocalStorage;
using BlazorTodo.Services.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BlazorTodo.Services;
public class TodoService : ITodoService
{
    const string LOCAL_STORAGE_TODOS = "todoList.todos";

    private ILocalStorageService _localStorage;

	public TodoService(ILocalStorageService localStorageService)
    {
        _localStorage = localStorageService;
    }

    public async Task<List<TodoModel>> GetStoredTodos() =>
        await _localStorage.GetItemAsync<List<TodoModel>>(LOCAL_STORAGE_TODOS) ?? new List<TodoModel>();


    public async Task<List<TodoModel>> AddTodo(string? todoTitle)
    {
        var todos = await GetStoredTodos();

        if (!string.IsNullOrWhiteSpace(todoTitle))
        {
            todos.Add(new TodoModel
            {
                ID = Guid.NewGuid(),
                Name = todoTitle,
                Completed = false
            });
            await SaveTodosToLocalStorage(todos);
        }

        return todos;
    }

    public async Task SaveTodosToLocalStorage(IEnumerable<TodoModel> todos) => await _localStorage.SetItemAsync(LOCAL_STORAGE_TODOS, todos);

    public async Task<List<TodoModel>> ToggleTodoCompleted(TodoModel todo)
    {
        var todos = await GetStoredTodos();
        if (todos.Any())
        {
            todos.FirstOrDefault(x => x.ID == todo.ID).Completed = !todo.Completed;
            await SaveTodosToLocalStorage(todos);
        }
        return todos;
    }


    public async Task<List<TodoModel>> ClearCompletedTodos()
    {
        var todos = await GetStoredTodos();
        todos = todos.Where(x => x.Completed == false).ToList();
        await SaveTodosToLocalStorage(todos);
        return todos;
    }

}


