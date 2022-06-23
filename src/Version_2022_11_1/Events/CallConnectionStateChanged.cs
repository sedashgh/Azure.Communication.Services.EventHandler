namespace JasonShave.Azure.Communication.Service.CallingServer.Extensions.Events;

public record CallConnectionStateChanged(string CallConnectionId, string ConnectionState);