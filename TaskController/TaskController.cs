using Microsoft.AspNetCore.Mvc;

namespace TaskApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private static List<TaskItem> tasks = new ();

[HttpGet]
public IActionResult GetTasks()
    {
        return Ok(tasks);
    }


[HttpGet("{id}")]
public IActionResult GetTaskById(int id)
{
    var task = tasks.FirstOrDefault(x => x.Id == id);
    if (task == null)
        return NotFound();
    return Ok(task);
}
[HttpPost]
public IActionResult PostTasks([FromBody] TaskItem task)
    {
        if(string.IsNullOrEmpty(task?.Title))
         return BadRequest("Title is required");

        task.Id =tasks.Count+1;
        tasks.Add(task);
        //return Ok(task); ;
        return CreatedAtAction(nameof(GetTaskById), new {id=task.Id}, task);
    }

[HttpPatch("{id}")]
public IActionResult PatchTasks(int id,[FromBody] TaskItem updatedtask)
{
        var task = tasks.FirstOrDefault(x => x.Id==id);

        if (task == null)
           return NotFound();

        task.IsCompleted = true;

        return Ok(task);
}
}

public class TaskItem
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public string Title { get; set; }
}