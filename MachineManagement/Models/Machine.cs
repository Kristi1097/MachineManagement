namespace MachineManagement.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Failure>? Failures { get; set; }
        public double? AverageTimeFailures { get; set; }
    }
}
