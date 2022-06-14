using EcomApp.Services.AuditLogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;

namespace EcomApp.Controllers.AuditLogs
{
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogsService _auditLogs;
        public AuditLogsController(IAuditLogsService auditLogs)
        {
            _auditLogs = auditLogs;
        }
        [HttpGet]
        [Route("api/[controller]/{version}/getlogsbyuser/{userid}")]
        public IActionResult getAllAuditLogs(Guid userid, string version)
        {
            if(userid.Equals(null) || version == null)
            {
                return BadRequest(new {Status =StatusCodes.Status400BadRequest, Message ="Failed", Error ="Parameters are null. Please Contact Administrator"});
            }
            else
            {
                var allauditlogs = _auditLogs.getallAudit(userid);
                if (!userid.Equals(null))
                {
                    return Ok(new { Status =StatusCodes.Status200OK, Message = "Success", Result = allauditlogs.Result });
                }
                else
                    return NotFound(new { Status = StatusCodes.Status404NotFound, Message = "Success", Error ="UserId not found "});
            }
           
        }
        [HttpGet]
        [Route("api/[controller]/{version}/searchbylogid/{auditID}")]
        public IActionResult getlogsbylogid(string auditID, string version)
        {
            if(auditID.Equals(null) && version == null )
            {
                return BadRequest(new { Status = StatusCodes.Status400BadRequest, Message = "Failed", Error = "Log ID is null" });
            }
            else
            {
                var log = _auditLogs.getlogbyid(auditID);
                if (log.Result != null)
                {
                    return Ok(new { Status = StatusCodes.Status200OK, Message = "Success", Result = log.Result });
                }
                else
                    return NotFound(new { Status = StatusCodes.Status404NotFound, Message = "Failed", Error = "Log Id is not Correct" });
            }
        }
    }
}
