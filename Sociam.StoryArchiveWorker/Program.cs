using Sociam.StoryArchiveWorker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<StoryArchiveWorker>();

var host = builder.Build();
host.Run();
