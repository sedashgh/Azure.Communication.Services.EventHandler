using Azure;
using Azure.Communication;
using Azure.Communication.CallingServer;
using JasonShave.Azure.Communication.Service.Interaction.Sdk.Contracts.V2022_11_1.Models;

namespace JasonShave.Azure.Communication.Service.Interaction.Sdk.Client.Extensions;

public static class CallingServerClientExtensions
{
    public static async Task<Response> PlayAudioAsync(this CallingServerClient callingServerClient,
        string callConnectionId,
        Action<PlayAudioOptionsExtensions> playAudioOptions, CancellationToken cancellationToken)
    {
        var options = new PlayAudioOptionsExtensions();
        playAudioOptions(options);

        CallConnection callConnection = await callingServerClient.GetCallAsync(callConnectionId, cancellationToken);

        // todo: implement play audio
        return await callConnection.HangupAsync(cancellationToken);
    }

    public static async Task<TransferCallResponse> TransferCallAsync(this CallingServerClient callingServerClient,
        string callConnectionId,
        Action<TransferCallOptionsExtensions> transferCallOptions, CancellationToken cancellationToken)
    {
        var options = new TransferCallOptionsExtensions();
        transferCallOptions(options);

        CallConnection callConnection = await callingServerClient.GetCallAsync(callConnectionId, cancellationToken);

        TransferCallResponse response = await callConnection.TransferCallToParticipantAsync(options.To.ToCommunicationIdentifier(),
            new TransferCallOptions(
                new PhoneNumberIdentifier(options.AlternateCallerId),
                options.UserToUserInformation,
                options.OperationContext,
                options.TransfereeParticipantId), cancellationToken);

        return response;
    }

    private static CommunicationIdentifier ToCommunicationIdentifier(this string input)
    {
        if (input.ToLower().StartsWith("8:acs:")) return new CommunicationUserIdentifier(input);
        if (input.ToLower().StartsWith("8:")) return new MicrosoftTeamsUserIdentifier(input);
        if (input.ToLower().StartsWith("+")) return new PhoneNumberIdentifier(input);

        return new UnknownIdentifier(input);
    }
}