using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet.DataAccess;
using TimeSheet.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeSheet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // GET: api/<TaskController>
        [HttpGet]
        public IEnumerable<Model.Task> GetAllTasks()
        {
            TaskLogic taskLogic = new TaskLogic();
            return taskLogic.GetAllTasks();
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public Model.Task GetTaskByID(int id)
        {
            TaskLogic taskLogic = new TaskLogic();
            return taskLogic.GetTaskByID(id);
        }

        // GET api/<TaskController>/User/5
        [HttpGet("User/{userid}")]
        public IEnumerable<Model.Task> GetUserTasks(int userid)
        {
            TaskLogic taskLogic = new TaskLogic();
            return taskLogic.GetUserTasks(userid);
        }

        // GET api/<TaskController>/ActiveTasks
        [HttpGet("ActiveTasks")]
        public IEnumerable<Model.Task> GetAllActiveTasks()
        {
            TaskLogic taskLogic = new TaskLogic();
            return taskLogic.GetAllActiveTasks();
        }

        // GET api/<TaskController>/5
        [HttpGet("UserActiveTasks/{userid}")]
        public IEnumerable<Model.Task> GetAllActiveTasksFromUser(int userid)
        {
            TaskLogic taskLogic = new TaskLogic();
            return taskLogic.GetAllActiveTasksFromUser(userid);
        }

        // GET api/<TaskController>/5
        [HttpGet("UserDoneTasks/{userid}")]
        public IEnumerable<Model.Task> GetAllDoneTasksFromUser(int userid)
        {
            TaskLogic taskLogic = new TaskLogic();
            return taskLogic.GetAllDoneTasksFromUser(userid);
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<ActionResult<Model.Task>> CreateTask([FromBody] Model.Task task)
        {
            try
            {
                if (task == null)
                    return BadRequest();

                TaskLogic taskLogic = new TaskLogic();

                int newTaskID = taskLogic.CreateTask(task, task.User_idUser);
                var createdTask = taskLogic.GetTaskByID(newTaskID);

                return CreatedAtAction(nameof(GetTaskByID),
                    new { id = createdTask.IdTask }, createdTask);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new task record");
            }
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Model.Task>> UpdateTask(int id, [FromBody] Model.Task task)
        {
            try
            {
                if (id != task.IdTask)
                    return BadRequest("Task ID mismatch");

                TaskLogic taskLogic = new TaskLogic();
                var taskToUpdate = taskLogic.GetTaskByID(id);

                if (taskToUpdate == null)
                    return NotFound($"Task with Id = {id} not found");

                taskLogic.UpdateTask(task, task.IdTask, task.User_idUser);
                return StatusCode(StatusCodes.Status200OK, "Succesfully update!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Model.Task>> DeleteTask(int id)
        {
            try
            {
                TaskLogic taskLogic = new TaskLogic();
                var taskToDelete = taskLogic.GetTaskByID(id);

                if (taskToDelete == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }

                taskLogic.DeleteTask(id);
                return StatusCode(StatusCodes.Status200OK, "Succesfully delete!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
