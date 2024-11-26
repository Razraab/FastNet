using Autofac;
using FastNet.Infrastructure.Interfaces;
using FastNet.Infrastructure.Services;
using FastNet.UI.Core;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using FastNet.UI.Core.Console.Commands;

namespace FastNet.Domain.Core
{
    public class Application
    {
        public static Application Current { get; private set; } = null!;
        public static ApplicationConfig Config { get; private set; } = null!;

        public IContainer? Services { get; private set; }

        private Application() { }
        
        public static Application Create(string name)
        {
            if (Current == null)
            {
                Application application = new Application();
                ApplicationConfig config = new ApplicationConfig(name);
                Config = config;
                return application;
            }
            return Current;
        }

        [RequiresUnreferencedCode("")]
        public void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(c => Current).SingleInstance();
            builder.Register(c => Config).SingleInstance();

            builder.RegisterType<ConsoleUI>().SingleInstance();

            // Commands
            IEnumerable<Type> types = Assembly.Load("FastNet.UI")
                    .GetTypes()
                    .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(ConsoleCommand)));
            foreach (Type type in types)
                builder.RegisterType(type)
                    .As<ConsoleCommand>();

            // Services
            builder.RegisterGeneric(typeof(FileLogger<>))
                .As(typeof(ILogger<>));

            Services = builder.Build();
        }

        public void Run()
        {
            if (Services == null) throw new NullReferenceException("Service container is null");

            Services.Resolve<ILogger<Application>>().Reset();

            ConsoleUI ui = Services.Resolve<ConsoleUI>();
            ui.Show();
        }
    }
}
