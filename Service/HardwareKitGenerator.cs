using System.ComponentModel;
using System.Runtime.Serialization.Formatters;
using HardwareCatalog.Domain.Components;
using HardwareCatalog.Domain.Components.AbstractComponents;
using HardwareCatalog.Persistence;

namespace HardwareCatalog.Service;

public class HardwareKitGenerator
{
    private int _amountOfPeople;
    private decimal _money;
    private int _speed;

    public int AmountOfPeople
    {
        get => _amountOfPeople;
        set
        {
            if (value < 1 || value > 100)
            {
                throw new Exception("Wrong number of people");
            }

            _amountOfPeople = value;
        }
    }

    public int Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
            {
                throw new Exception("Incorrect Speed");
            }

            _speed = value;
        }
    }
    public decimal Money
    {
        get => _money;
        set
        {
            if (value < 0)
            {
                throw new Exception("Incorrect Money");
            }

            _money = value;
        }
    }
    
    public HardwareKitGenerator(int amountOfPeople, int speed,decimal money)
    {
        AmountOfPeople = amountOfPeople;
        Speed = speed;
        Money = money;
    }

    public List<Hardware> GenerateKit()
    {
        List<Hardware> kit = new List<Hardware>();
        var components = new HardwareRepository().GetComponents();
        List<Router?> routers = new List<Router?>();
        List<Switcher> switches = new List<Switcher>();
        List<Cable> cables = new List<Cable>();
        foreach (var component in components)
        {
            switch (component)
            {
                case Router router: 
                    if (router is Switcher switcher) switches.Add(switcher);
                    else routers.Add(router); break;
              
                case Cable cable:cables.Add(cable);break;
            }
        }

        Hardware filteredRouter;
        int temp = 0;
        var minDifference = Math.Abs(routers.Max(x => x.Speed)-Speed);
        foreach (var router in routers)
        {
            int currentDifference=Math.Abs(router.Speed - Speed);
            if (currentDifference < minDifference)
            {
                temp = router.Speed;
                minDifference = currentDifference;
            }
        }
        
        foreach (var router in routers)
        {
            if (router.Speed==temp)
            {
              kit.Add(router);
              break;
            }
        }

        decimal minPrice = decimal.MaxValue;
        List<Hardware> filteredSwitchers = new List<Hardware>();
        foreach (var switcher in switches)
        {
            decimal currentPrice = 0;
           
            for (int currentPorts = 0; currentPorts < AmountOfPeople; currentPorts += switcher.Ports)
            {
                currentPrice += switcher.Price;
            }

            if (currentPrice < minPrice)
            {
                filteredSwitchers = new List<Hardware>();
                minPrice = currentPrice;
                currentPrice = 0;
                do
                {
                    filteredSwitchers.Add(switcher);
                    currentPrice += switcher.Price;
                } while (currentPrice!=minPrice);
            }
        }
     
        decimal minCablePrice = Decimal.MaxValue;
        List<Hardware> filteredCables = new List<Hardware>();
        foreach (var cable in cables)
        {
            decimal currentPrice = 0;
           
            for (int currentLength = 0; currentLength < AmountOfPeople; currentLength += cable.Length)
            {
                currentPrice += cable.Price;
            }

            if (currentPrice < minPrice)
            {
                filteredCables = new List<Hardware>();
                minPrice = currentPrice;
                currentPrice = 0;
                do
                {
                    filteredCables.Add(cable);
                    currentPrice += cable.Price;
                } while (currentPrice!=minPrice);
            }
        }
        kit.AddRange(filteredSwitchers);
        kit.AddRange(filteredCables);
        return kit;
    }
}