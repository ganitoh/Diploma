using Common.Domain;

namespace Organization.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    protected Email() { }

    public Email(string email)
    {
        if(string.IsNullOrEmpty(email))
            throw new DomainException("Email cannot be empty");
        
        if(!email.Contains("@"))
            throw new DomainException("Email not valid");
        
        Value = email;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}