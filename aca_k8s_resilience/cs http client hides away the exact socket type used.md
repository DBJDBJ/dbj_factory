The .NET's HttpClient indeed hides the underlying socket type, which can make the transition between different socket types more seamless. 

1. HttpClient abstraction:
   - HttpClient in .NET provides a high-level API for making HTTP requests.
   - It abstracts away the details of the underlying connection, whether it's TCP/IP or Unix sockets.

2. Using HttpClient with Unix sockets:
   - In .NET, you can use SocketsHttpHandler to configure HttpClient to use Unix sockets.

Here's an example C#:

```csharp
// Let's assume we are deploying some imaginary 'Sidecar' container
using System.Net.Http;
using System.Net.Sockets;

// can be anywhere
var socketPath = "/tmp/Sidecar-socket/Sidecar-http.sock";
var uds = new UnixDomainSocketEndPoint(socketPath);

var connection = new ConnectionFactory(uds);
var socketsHandler = new SocketsHttpHandler
{
    ConnectCallback = connection.ConnectAsync
};

var httpClient = new HttpClient(socketsHandler);

// Now use httpClient as normal to communicate with Sidecar
```

3. Benefits of this approach:
   - User application code remains largely unchanged.
   - One can easily switch between TCP/IP and Unix sockets by changing the configuration.
   - Performance benefits of Unix sockets can be leveraged without major code changes.

4. Considerations for Azure Container Apps:
   - Ensure the Unix socket file is created in a location accessible to both the app and the sidecar.
   - May need to handle scenarios where the socket file doesn't exist (e.g., on container startup).

6. Environment-specific configuration:
   - Code could use environment variables or configuration files to determine whether to use Unix sockets or TCP/IP, making the app more portable across different environments.

```csharp
// Let's assume we are deploying some imaginary 'Sidecar' container
// changeable through env vars usage
var isSidecarUnixSocket = Environment.GetEnvironmentVariable("USE_Sidecar_UNIX_SOCKET") == "true";
var SidecarHttpPort = Environment.GetEnvironmentVariable("Sidecar_HTTP_PORT");

HttpClient httpClient;
if (isSidecarUnixSocket)
{
    // Configure for Unix socket as shown above
}
else
{
    // Standard HttpClient setup for localhost TCP
    httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri($"http://localhost:{SidecarHttpPort}");
}
```

This approach gives the flexibility to use Unix sockets when available (like in local development environment or in containers that support it) while falling back to standard TCP/IP when necessary.

# Conclusion

This abstraction in .NET should make it easier to experiment with Unix sockets in various environments, including Azure Container Apps, without significant code changes.