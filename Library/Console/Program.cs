
using System;
using Game.Models;
using Game.Utility;
using System.Drawing;
using System.Drawing.Printing;

class Debugger {
  static void Main(string[] args) {
    Console.WriteLine("----- debugger start\n");

    var printer = new PrinterSettings();
    Console.WriteLine(printer.IsDefaultPrinter);
    Console.WriteLine(printer.IsValid);
    Console.WriteLine(printer.PrinterName);
    
    Console.WriteLine("\n----- debugger finish");
  }

  static void PrintDeviceTest() {
    var size = PrintDevice.DrawSize.one * 100f;
    Console.WriteLine(" width = " + size.width);
    Console.WriteLine("height = " + size.height);
  }
}
