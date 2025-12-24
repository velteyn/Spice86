namespace Spice86.Tests;

using ModelContextProtocol.Client;
using ModelContextProtocol.IO;
using NSubstitute;
using Spice86.Core.Emulator.VM;
using Spice86.Emulator.Mcp;
using System.IO;
using System.Threading.Tasks;
using Xunit;

public class McpServerTests
{
    [Fact]
    public async Task TestMcpServer()
    {
        var machine = Substitute.For<Machine>();
        var mcpServer = new McpServer(machine);

        var serverStream = new MemoryStream();
        var clientStream = new MemoryStream();

        var serverTransport = new StdioServerTransport(serverStream, clientStream);
        var clientTransport = new StdioClientTransport(clientStream, serverStream);

        mcpServer.Start(serverTransport);

        var client = await McpClient.CreateAsync(clientTransport);
        var tools = await client.GetToolsAsync();
        Assert.Contains(tools, t => t.Name == "get_cpu_registers");
    }
}
