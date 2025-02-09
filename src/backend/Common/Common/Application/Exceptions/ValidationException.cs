namespace Common.Application.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary, 
        string message = "Ошибка валидации") : base(message)
    {
        ErrorsDictionary = errorsDictionary;
    }
}