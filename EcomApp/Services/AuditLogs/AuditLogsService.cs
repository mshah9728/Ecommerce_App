using EcomApp.DBConfigurations.MongoConfiguration;
using EcomApp.Models.ActivtyLogModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomApp.Services.AuditLogs
{
    public class AuditLogsService : IAuditLogsService
    {
        private readonly MongoDBContext _activitlogscontext;
        public AuditLogsService(MongoDBContext activitlogscontext)
        {
            _activitlogscontext = activitlogscontext;
        }

        public async Task<List<AuditLogsModel>> getallAudit(Guid userid)
        {
            return await _activitlogscontext.auditlogs.Find(s => s.FK_AuditPerformedbyUserId == userid).ToListAsync();
        }
        public async Task<AuditLogsModel> getlogbyid(string auditid)
        {
            return await _activitlogscontext.auditlogs.Find(s => s.audit_ID == auditid).FirstOrDefaultAsync();
        }
        public AuditLogsModel storeAuditlogs(AuditLogsModel auditLogs)
        {
             _activitlogscontext.auditlogs.InsertOneAsync(auditLogs);
            return auditLogs;
        }
    }
}
