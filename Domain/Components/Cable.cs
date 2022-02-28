using System.Text.Json.Serialization;
using HardwareCatalog.Domain.Components.AbstractComponents;

namespace HardwareCatalog.Domain.Components;

public record Cable : Hardware
{
    private readonly int _length;
    
    [JsonPropertyName("length")]
    public int Length
    {
        get => _length;
        private init
        {
            if (value is < 1 or > 100)
            {
                throw new ArgumentException("incorrect length");
            }
            _length = value;
        }
    }

    public Cable(string? name, decimal price, int length) :
        base(name, price)
    {
        Length = length;
    }
}