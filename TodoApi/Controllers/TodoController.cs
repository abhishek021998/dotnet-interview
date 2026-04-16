using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TodoController : ControllerBase
    {
        private ITodoService todoService;
        // Inject the service via constructor injection, allowing for better testability and separation of concerns
        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpPost("createTodo")]
        public IActionResult CreateTodo([FromBody] Todo todo)
        {
            try
            {
                //    var todoService = new TodoService();  Injected via constructor, no need to create a new instance here
                var result = todoService.CreateTodo(todo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("getTodo")]
        public IActionResult GetTodo([FromBody] GetTodoRequest request)
        {
            try
            {
                //  var todoService = new TodoService(); Injected via constructor, no need to create a new instance here
                if (request.Id.HasValue)
                {
                    var todo = todoService.GetTodoById(request.Id.Value);
                    if (todo == null)
                    {
                        return NotFound();
                    }
                    return Ok(todo);
                }
                else
                {
                    var todos = todoService.GetAllTodos();
                    return Ok(todos);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("updateTodo")]
        public IActionResult UpdateTodo([FromBody] UpdateTodoRequest request)
        {
            try
            {
                // var todoService = new TodoService(); Injected via constructor, no need to create a new instance here
                var existingTodo = todoService.GetTodoById(request.Id);
                if (existingTodo == null)
                {
                    return NotFound();
                }

                var todo = new Todo
                {
                    Title = request.Title,
                    Description = request.Description,
                    IsCompleted = request.IsCompleted
                };

                var result = todoService.UpdateTodo(request.Id, todo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteTodo")]
        public IActionResult DeleteTodo([FromBody] DeleteTodoRequest request)
        {
            try
            {
                //   var todoService = new TodoService(); Injected via constructor, no need to create a new instance here
                var result = todoService.DeleteTodo(request.Id);
                if (result)
                {
                    return Ok(new { message = "Todo deleted successfully" });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
   
}
