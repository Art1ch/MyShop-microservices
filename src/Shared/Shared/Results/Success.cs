namespace Shared.Results
{
    public struct Success<TValue>
    {
        public TValue? Value { get; set; }
        public Success(TValue? value)
        {
            Value = value;
        }
    }

    public struct Success
    {

    }
}
