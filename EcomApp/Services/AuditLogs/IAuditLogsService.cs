using EcomApp.Models.ActivtyLogModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomApp.Services.AuditLogs
{
    public interface IAuditLogsService
    {
        Task<List<AuditLogsModel>> getallAudit(Guid userid);
        Task<AuditLogsModel> getlogbyid(string auditid);
    }
}
