namespace Sample.Aspire.ServiceDefaults.Exceptions;

public sealed class SampleException : Exception
{
    public string RequestName { get; }
    
    public SampleException(string requestName, Exception? innerException = null) 
        : base($"An error occurred while processing request: {requestName}", innerException)
    {
        RequestName = requestName;
    }
}