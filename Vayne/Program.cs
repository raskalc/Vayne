using System.Windows;

namespace Vayne;

public class App : Application
{
    [STAThread]
    public static void Main()
    {
        var app = new App();
        var window = new Selector();
        app.Run(window);
    }
}