namespace SampleBankTransactions.Model.Responses
{
    public class CommonResponse
    {
        public List<object> Records { get; set; } 
        public int Errors { get; set; } = 0;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
