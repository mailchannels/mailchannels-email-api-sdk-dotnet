using MailChannels.EmailApi.Api;
using MailChannels.EmailApi.Extensions;
using MailChannels.EmailApi.Model;
using Microsoft.Extensions.DependencyInjection;

// Running this program makes a real API request. Build alone is offline.
string Required(string name) => Environment.GetEnvironmentVariable(name) ?? throw new InvalidOperationException($"Set {name} before running this provider dry-run example.");
var apiKey = Required("MAILCHANNELS_API_KEY");
var sender = Required("MAILCHANNELS_SENDER");
var recipient = Required("MAILCHANNELS_RECIPIENT");
var services = new ServiceCollection();
services.AddLogging();
services.AddApi();
using var provider = services.BuildServiceProvider();
var body = new MailSendBody(new() {new("text/plain", "MailChannels SDK validation")}, new(sender), new() {new(new() {new(recipient)})}, "MailChannels dry-run");
var response = await provider.GetRequiredService<ISendApi>().SendEmailAsync(apiKey, body, dryRun:true);
Console.WriteLine($"HTTP {(int)response.StatusCode}");
if (!response.IsOk || !response.TryOk(out _)) Environment.ExitCode = 1;
