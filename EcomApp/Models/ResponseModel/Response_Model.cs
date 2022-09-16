namespace EcomApp.Models.ResponseModel
{
    public class Response_Model<I,L,E, M,R>
    {
        public I StatusCode { get; set; }
        public M Message { get; set; }
        public L listofrecords { get; set; }
        public R records { get; set; }
        public E Error { get; set; }
    }
}
