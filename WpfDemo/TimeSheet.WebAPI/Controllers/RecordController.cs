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
    public class RecordController : ControllerBase
    {
        // GET: api/<RecordController>
        [HttpGet]
        public IEnumerable<Record> GetAllRecords()
        {
            RecordLogic recordLogic = new RecordLogic();
            return recordLogic.GetAllRecords();
        }

        // GET api/<RecordController>/5
        [HttpGet("{id}")]
        public Record GetRecordByID(int id)
        {
            RecordLogic recordLogic = new RecordLogic();
            return recordLogic.GetRecordByID(id);
        }

        // GET api/<RecordController>/5
        [HttpGet("User/{userid}")]
        public IEnumerable<Record> GetUserRecords(int userid)
        {
            RecordLogic recordLogic = new RecordLogic();
            return recordLogic.GetUserRecords(userid);
        }

        // GET api/<RecordController>/5
        [HttpGet("Task/{taskid}")]
        public IEnumerable<Record> GetTaskRecords(int taskid)
        {
            RecordLogic recordLogic = new RecordLogic();
            return recordLogic.GetTaskRecords(taskid);
        }

        // POST api/<RecordController>
        [HttpPost]
        public async Task<ActionResult<Record>> CreateRecord([FromBody] Record record)
        {
            try
            {
                if (record == null)
                    return BadRequest();

                RecordLogic recordLogic = new RecordLogic();

                int newRecordID = recordLogic.CreateRecord(record, record.User_idUser, record.Task_idTask);
                var createdRecord = recordLogic.GetRecordByID(newRecordID);

                return CreatedAtAction(nameof(GetRecordByID),
                    new { id = createdRecord.IdRecord }, createdRecord);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new record");
            }
        }

        // PUT api/<RecordController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Record>> UpdateRecord(int id, [FromBody] Record record)
        {
            try
            {
                if (id != record.IdRecord)
                    return BadRequest("Record ID mismatch");

                RecordLogic recordLogic = new RecordLogic();
                var recordToUpdate = recordLogic.GetRecordByID(id);

                if (recordToUpdate == null)
                    return NotFound($"Record with Id = {id} not found");

                recordLogic.UpdateRecord(record, record.IdRecord, record.User_idUser, record.Task_idTask);
                return StatusCode(StatusCodes.Status200OK, "Succesfully update!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        // DELETE api/<RecordController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Record>> DeleteRecord(int id)
        {
            try
            {
                RecordLogic recordLogic = new RecordLogic();
                var recordToDelete = recordLogic.GetRecordByID(id);

                if (recordToDelete == null)
                {
                    return NotFound($"Record with Id = {id} not found");
                }

                recordLogic.DeleteRecord(id);
                return StatusCode(StatusCodes.Status200OK, "Succesfully delete!");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
