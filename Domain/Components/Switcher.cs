﻿using System.Text.Json.Serialization;

namespace HardwareCatalog.Domain.Components;

public record Switcher : Router
{
    private readonly int _ports;
    
    [JsonPropertyName("ports")]
    public int Ports
    {
        get => _ports;
        private init
        {
            if (value < 1)
            {
                throw new ArgumentException("incorrect ports");
            }

            _ports = value;
        }
    }

    public Switcher(string? name, int speed, int ports, decimal price) :
        base(name, speed, price)
    {
        Ports = ports;
    }
}