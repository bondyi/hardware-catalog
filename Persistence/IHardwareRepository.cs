using IBondarik.HardwareCatalog.Domain.Components;
using IBondarik.HardwareCatalog.Domain.Components.AbstractComponents;

namespace IBondarik.HardwareCatalog.Persistence;

public interface IHardwareRepository
{
    List<Hardware?> GetComponents();
}