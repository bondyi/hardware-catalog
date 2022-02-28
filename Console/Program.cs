using HardwareCatalog.Persistence;
using HardwareCatalog.Service;
using System;
using System.Security.Cryptography;

namespace HardwareCatalog.Console;

public class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Enter a number of users on the network");
        int amountOfPeople = Convert.ToInt32(System.Console.ReadLine());
        System.Console.WriteLine("Enter required network bandwidth");
        int speed = Convert.ToInt32(System.Console.ReadLine());
        System.Console.WriteLine("Enter allocated funds");
        decimal money = Convert.ToDecimal(System.Console.ReadLine());
      
       var kit= new HardwareKitGenerator(amountOfPeople,speed,money).GenerateKit();
       
       var totalPrice = kit.Sum(x => x.Price);
       if (totalPrice < money)
       {
           System.Console.WriteLine("Your kit is:");
           foreach (var component in kit)
           {
               System.Console.WriteLine(component.Name+' '+component.Price);
           
           }
           System.Console.WriteLine("Total price: "+kit.Sum(x=>x.Price));
       }
       else   System.Console.WriteLine("You are missing "+ (totalPrice-money));
    }
}