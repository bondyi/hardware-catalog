using IBondarik.HardwareCatalog.Domain.Components.AbstractComponents;
using System.Text.Json.Serialization;

namespace IBondarik.HardwareCatalog.Domain.Components;

public record Router : Hardware
{
    private readonly int _speed;
    [JsonPropertyName("speed")]
    public int Speed
    {
        get => _speed; 
        private init
        {
            if (value <= 0)
            {
                throw new ArgumentException("incorrect bandwidth");
            }

            _speed = value;
        }
    }

    public Router(string? name, int speed, decimal price) :
        base(name, price)
   {
       Speed = speed;
   }
}