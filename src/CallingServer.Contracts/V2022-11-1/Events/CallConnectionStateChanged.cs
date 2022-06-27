namespace JasonShave.Azure.Communication.Service.CallingServer.Contracts.V2022_11_1.Events;

public record CallConnectionStateChanged(string CallConnectionId, string CallConnectionState);