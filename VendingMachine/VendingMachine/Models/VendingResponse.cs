namespace VendingMachine.Models
{
    public class VendingResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public double Change { get; set; }
        public Dictionary<string, int> inventory { get; set; }
    }
}
