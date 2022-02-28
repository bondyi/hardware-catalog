using System.Text.Json.Serialization;

namespace HardwareCatalog.Domain.Components.AbstractComponents;

public abstract record Hardware
{
    private readonly string? _name;
    private readonly decimal _price;

    [JsonPropertyName("name")]
    public string? Name
    {
        get => _name;
        private init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("incorrect name");
            }
            _name = value;
        }
    }
    
    [JsonPropertyName("price")]
    public decimal Price
    {
        get => _price;
        private init
        {
            if (value <= 0)
            {
                throw new ArgumentException("incorrect price");
            }
            _price = value;
        }
    }

    public Hardware(string? name, decimal price)
    {
        Name = name;
        Price = price;
    }
}