using System.Windows;
using Teigha.Runtime;
using Vayne.Utils;

namespace Vayne;

public class App : Application
{
    [CommandMethod("GuiRun")]
    public static void Main()
    {
        var app = new App();
        var window = new Selector();
        app.Run(window);
    }

    [CommandMethod("testrun")]
    public static void run()
    {
        var a = new Draw();
        a.DrawSphere(4,3,2,1);
    }
}