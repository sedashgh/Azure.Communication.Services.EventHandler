namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1_preview.Models;

public class TransferCallOptionsExtensions
{
    public string To { get; set; }

    public string AlternateCallerId { get; set; } = default!;

    public string UserToUserInformation { get; set; } = default!;

    public string OperationContext { get; set; } = default!;

    public string TransfereeParticipantId { get; set; } = default!;
}