using HardwareCatalog.Domain.Components;
using HardwareCatalog.Domain.Components.AbstractComponents;

namespace HardwareCatalog.Persistence;

public interface IHardwareRepository
{
    List<Hardware?> GetComponents();
}