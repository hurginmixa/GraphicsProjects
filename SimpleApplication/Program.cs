using CoordinateSystem;
using SimpleApplication.Models;
using SimpleApplication.Models.AstrixModels;

namespace SimpleApplication;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        
        IStrokesModel strokesModel = new AstrixStrokesModel();
        strokesModel = new RectanglesModel();
        
        Application.Run(new Form1(strokesModel));
    }
}