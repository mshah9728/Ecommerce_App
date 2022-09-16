using System.Collections.Generic;

namespace EcomApp.Models.ResponseModel
{
    public class JsonResponse_Model
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
        public List<dynamic> lResult { get;set; }
    }
}
