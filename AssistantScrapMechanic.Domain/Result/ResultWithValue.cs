namespace AssistantScrapMechanic.Domain.Result
{
    public class ResultWithValue<T> : AssistantScrapMechanic.Domain.Result.Result
    {
        public T Value { get; set; }

        public ResultWithValue(bool isSuccess, T value, string exceptionMessage) : base(isSuccess, exceptionMessage)
        {
            Value = value;
            IsSuccess = isSuccess;
            ExceptionMessage = exceptionMessage;
        }

        public ResultWithValue(AssistantScrapMechanic.Domain.Result.Result oldResult, T value) : base(oldResult.IsSuccess, oldResult.ExceptionMessage)
        {
            Value = value;
            IsSuccess = oldResult.IsSuccess;
            ExceptionMessage = oldResult.ExceptionMessage;
        }

        public override string ToString()
        {
            return $"Success: {IsSuccess}, ExceptionMessage: {ExceptionMessage}";
        }
    }
}
