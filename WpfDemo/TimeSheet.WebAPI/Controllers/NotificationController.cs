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
        // GET api/<NotificationController>/5
        [HttpGet("Employee/{id}")]
        public IEnumerable<string> GetTaskNotificationsForEmployee(int id)
        {
            NotificationLogic notificationLogic = new NotificationLogic();
            return notificationLogic.GetTaskNotificationsForEmployee(id);
        }

        // GET api/<NotificationController>/5
        [HttpGet("Admin/{id}")]
        public IEnumerable<string> GetTaskNotificationsForAdmin(int id)
        {
            NotificationLogic notificationLogic = new NotificationLogic();
            return notificationLogic.GetTaskNotificationsForAdmin(id);
        }

        /*// POST api/<NotificationController>
        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotificationForTask([FromBody] Notification notification)
        {
            try
            {
                if (notification == null)
                    return BadRequest();

                NotificationLogic notificationLogic = new NotificationLogic();

                int newNotificationID = notificationLogic.CreateNotificationForTask(notification.Message, notification.);
                var createdNotification = notificationLogic.GetNotificationByID(newNotificationID);

                return CreatedAtAction(nameof(GetNotificationByID),
                    new { id = createdNotification.idNotification }, createdNotification);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new record");
            }
        }*/

        // PUT api/<NotificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
