[Serializable]
public class BadArgumentException : Exception
{
    public IDictionary<String, String[]> Errors { get; }

    public BadArgumentException() : base() { }
    public BadArgumentException(IDictionary<String, String[]> errors) : base() { 
        Errors = errors;
    }
    public BadArgumentException(string message) : base(message) { }
    public BadArgumentException(string message, Exception inner) : base(message, inner) { }
}