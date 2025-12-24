using System.Text.Json;
using ModelContextProtocol.IO;
using ModelContextProtocol.SDK;
using ModelContextProtocol.SDK.Tools;
using Spice86.Core.Emulator.Mcp;
using Spice86.Core.Emulator.VM;

namespace Spice86.Emulator.Mcp;

public class McpServer : IMcpServer
{
    private readonly Machine _machine;
    private readonly McpApplication _mcpApplication;

    public McpServer(Machine machine)
    {
        _machine = machine;
        _mcpApplication = new McpApplication(
            new McpApplicationOptions
            {
                Tools =
                {
                    new McpToolDefinition
                    {
                        Name = "get_cpu_registers",
                        Description = "Gets the current CPU registers",
                        Handler = GetCpuRegisters
                    }
                }
            });
    }

    private Task<McpToolResult> GetCpuRegisters(McpToolInvocation arg)
    {
        return Task.FromResult(new McpToolResult
        {
            Content = System.Text.Json.JsonSerializer.Serialize(_machine.Cpu.State)
        });
    }

    public void Start(IServerTransport? transport = null)
    {
        _mcpApplication.Start(transport);
    }
}
