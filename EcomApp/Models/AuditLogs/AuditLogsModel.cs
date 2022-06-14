using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcomApp.Models.ActivtyLogModel
{
    [CollectionName("AuditLogs")]
    public class AuditLogsModel
    {
        [BsonId]
        [BsonElement("audit_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string audit_ID { get; set; }
        [BsonElement("AuditLogID")]
        public string AuditLogID { get; set; }
        [BsonElement("AuditLogsName")]
        public string AuditLogName { get; set; }
        [BsonElement("FK_UserId")]
        public Guid FK_AuditPerformedbyUserId { get; set; }
        [BsonElement("UserAuditLogsName")]
        public string UserAuditLogsName { get; set; }
        [BsonElement("MethodType")]
        public string MethodType { get; set; }
        [BsonElement("ControllerName")]
        public string ControllerName { get; set; }
        [BsonElement("RequestPayload")]
        public string RequestPayload { get; set; }
        [BsonElement("RequestURL")]
        public string RequestURL { get; set; }
        [BsonElement("ActiviyLogsDatetime")]
        public DateTime? ActiviyLogsDatetime { get; set; }
    }
}
