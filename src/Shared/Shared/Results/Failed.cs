namespace Shared.Results
{
    public struct Failed
    {
        public string Message { get; set; }
        public Failed(string message)
        {
            Message = message;
        }
    }
}
