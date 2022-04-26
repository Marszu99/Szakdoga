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
    public class NotificationController : ControllerBase
    {
        // GET api/<RecordController>/5
        [HttpGet("{id}")]
        public Notification GetNotificationByID(int id)
        {
            NotificationLogic notificationLogic = new NotificationLogic();
            return notificationLogic.GetNotificationByID(id);
        }

        // GET api/<NotificationController>/Employee/5
        [HttpGet("Employee/{id}")]
        public IEnumerable<string> GetTaskNotificationsForEmployee(int id)
        {
            NotificationLogic notificationLogic = new NotificationLogic();
            return notificationLogic.GetTaskNotificationsForEmployee(id);
        }

        // GET api/<NotificationController>/Admin/5
        [HttpGet("Admin/{id}")]
        public IEnumerable<string> GetTaskNotificationsForAdmin(int id)
        {
            NotificationLogic notificationLogic = new NotificationLogic();
            return notificationLogic.GetTaskNotificationsForAdmin(id);
        }

        // POST api/<NotificationController>
        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotificationForTask([FromBody] Notification notification)
        {
            try
            {
                if (notification == null)
                    return BadRequest();

                NotificationLogic notificationLogic = new NotificationLogic();

                int newNotificationID = notificationLogic.CreateNotificationForTask(notification.Message, notification.NotificationFor, notification.Task_idTask);
                var createdNotification = notificationLogic.GetNotificationByID(newNotificationID);

                return CreatedAtAction(nameof(GetNotificationByID),
                    new { id = createdNotification.IdNotification }, createdNotification);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new notification");
            }
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notification>> HasRead(int id)
        {
            try
            {
                NotificationLogic notificationLogic = new NotificationLogic();
                var notificationToDelete = notificationLogic.GetNotificationByID(id);

                if (notificationToDelete == null)
                {
                    return NotFound($"Notification with Id = {id} not found");
                }

                notificationLogic.HasReadNotification(notificationToDelete.Task_idTask, notificationToDelete.NotificationFor);
                return StatusCode(StatusCodes.Status200OK, "Succesfully read!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
