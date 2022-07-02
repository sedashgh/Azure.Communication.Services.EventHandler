namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Models;

public class CreateCallOptionsExtensions
{
    public string From { get; set; }

    public ICollection<string> To { get; } = new List<string>();

    public string? CallbackUri { get; set; }

    public string? AlternativeCallerId { get; set; }

    public string? Subject { get; set; }
}