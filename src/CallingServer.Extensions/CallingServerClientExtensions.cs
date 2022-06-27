using Azure;
using Azure.Communication;
using Azure.Communication.CallingServer;
using JasonShave.Azure.Communication.Service.CallingServer.Extensions.Version_2022_11_1.Models;

namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions;

public static class CallingServerClientExtensions
{
    public static async Task<Response<CallConnection>> CallAsync(this CallingServerClient callingServerClient,
        Action<CreateCallOptionsExtensions> createCallOptions)
    {
        var options = new CreateCallOptionsExtensions();
        createCallOptions(options);

        List<CommunicationIdentifier> targets = options.To.Select(target => target.ToCommunicationIdentifier()).ToList();

        Uri callbackUri = default;
        if (options.CallbackUri is not null)
        {
            callbackUri = new Uri(options.CallbackUri);
        }

        return await callingServerClient.CreateCallAsync(
            source: options.From.ToCommunicationIdentifier(),
            targets: targets,
            callbackUri: callbackUri,
            options: new CreateCallOptions(new PhoneNumberIdentifier(options.AlternativeCallerId), ""));
    }

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