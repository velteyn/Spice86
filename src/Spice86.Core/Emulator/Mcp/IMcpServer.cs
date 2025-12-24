using ModelContextProtocol.IO;

namespace Spice86.Core.Emulator.Mcp;

public interface IMcpServer
{
    void Start(IServerTransport? transport = null);
}
