namespace CarWash.BL.Services
{
    public class WashRequestAssignedEvent
    {
        public Guid RequestId { get; set; }
        public Guid WasherId { get; set; }
    }
}