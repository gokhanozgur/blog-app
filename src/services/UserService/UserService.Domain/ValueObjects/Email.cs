namespace UserService.Domain.ValueObjects;

public class Email
{
    public record EmailAddress(string Value)
    {
        public static bool IsValid(string email) =>
            !string.IsNullOrWhiteSpace(email) && email.Contains("@");
    }
}