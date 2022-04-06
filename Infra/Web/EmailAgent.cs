namespace DotnetWsRef.Infra.Web;

public interface IEmailAgent
{
    Task SendEmail(string email);
}

internal class FakeEmailAgent : IEmailAgent
{
    public Task SendEmail(string email)
    {
        return Task.CompletedTask;
    }
}