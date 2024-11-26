namespace FastNet.Infrastructure.Network.Models
{
    public class Cookie
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime Expires { get; set; }

        public Cookie(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
