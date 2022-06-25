namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Models;

public class CreateCallOptionsExtensions
{
    public string From { get; set; }

    public IList<string> To { get; } = new List<string>();

    public string? CallbackUri { get; set; }

    public string? AlternativeCallerId { get; set; }

    public string? Subject { get; set; }
}