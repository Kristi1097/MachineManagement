namespace MachineManagement.Models
{
    public class Failure
    {
        public int Id { get; set; }
        public int MachineId { get; set; } 
        public string Name { get; set; }= null!;
        public string Priority { get; set; } = "Low";
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
    }
}
