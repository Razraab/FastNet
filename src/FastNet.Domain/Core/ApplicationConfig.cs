using System.Numerics;

namespace FastNet.Domain.Core
{
    public class ApplicationConfig
    {
        public string Name { get; set; }

        public ApplicationConfig(string name)
        {
            Name = name;
        }
    }
}
