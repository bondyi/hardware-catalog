using System.Text.Json;
using IBondarik.HardwareCatalog.Domain.Components;
using IBondarik.HardwareCatalog.Domain.Components.AbstractComponents;

namespace IBondarik.HardwareCatalog.Persistence;

public class HardwareRepository : IHardwareRepository
{
    private readonly string _path = "Resources/data.json";
    
    public List<Hardware?> GetComponents()
    {
        StreamReader streamReader = new StreamReader(_path);
        JsonDocument jsonDocument = JsonDocument.Parse(streamReader.ReadToEnd());
        streamReader.Close();

        var hardwares = new List<Hardware?>();

        var catalog = jsonDocument.RootElement.EnumerateObject().First();
        foreach (var hardware in catalog.Value.EnumerateArray().First().EnumerateObject())
        {
            foreach (var typeHardware in hardware.Value.EnumerateObject())
            {
                foreach (var item in typeHardware.Value.EnumerateArray())
                {
                    switch (typeHardware.Name)
                    {
                        case "routers": hardwares.Add(item.Deserialize<Router>()); break;
                        case "switches": hardwares.Add(item.Deserialize<Switch>()); break;
                        case "cables": hardwares.Add(item.Deserialize<Cable>()); break;
                    }
                }
            }
        }
        

        return hardwares;
    }
}