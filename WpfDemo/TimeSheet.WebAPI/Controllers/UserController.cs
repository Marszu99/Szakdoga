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
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetAllUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User GetUserByID(int id)
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetUserByID(id);
        }

        // GET api/<UserController>/Username/CsehMarcell
        [HttpGet("Username/{username}")]
        public User GetUserByUsername(string username)
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetUserByUsername(username);
        }

        // GET api/<UserController>/Admin
        [HttpGet("Admin")]
        public User GetAdmin()
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetAdmin();
        }

        // GET api/<UserController>/Login
        [HttpGet("Login")]
        public bool IsValidLogin(string username, string password)
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.IsValidLogin(username, password);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                UserLogic userLogic = new UserLogic();
                int newUserID = userLogic.CreateUser(user, user.Password);
                var createdUser = userLogic.GetUserByID(newUserID);

                return CreatedAtAction(nameof(GetUserByID),
                    new { id = createdUser.IdUser }, createdUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new user record");
            }
        }

        /*// POST api/<UserController>
        [HttpPost("Admin")]
        public async Task<ActionResult<User>> RegisterAdmin([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                UserLogic userLogic = new UserLogic();
                int newAdminID = userLogic.RegisterAdmin(user, user.Password, user.Email);
                var createdAdmin = userLogic.GetUserByID(newAdminID);

                return CreatedAtAction(nameof(GetUserByID),
                    new { id = createdAdmin.IdUser }, createdAdmin);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new user record");
            }
        }*/

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.IdUser)
                    return BadRequest("User ID mismatch");

                UserLogic userLogic = new UserLogic();
                var userToUpdate = userLogic.GetUserByID(id);

                if (userToUpdate == null)
                    return NotFound($"User with Id = {id} not found");

                userLogic.UpdateUser(user);
                return StatusCode(StatusCodes.Status200OK, "Succesfully update!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                UserLogic userLogic = new UserLogic();
                var userToDelete = userLogic.GetUserByID(id);

                if (userToDelete == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                userLogic.DeleteUser(id, userToDelete.Status);
                return StatusCode(StatusCodes.Status200OK,"Succesfully delete!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
