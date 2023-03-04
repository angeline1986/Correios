using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;



namespace TestProjectCorreios.Hooks;

[Binding]
public sealed class Hooks
{
    private readonly DriverHelper _driverHelper;

    public Hooks(DriverHelper driverHelper)
    {
        _driverHelper = driverHelper;
    }

   [BeforeScenario]
    public void BeforeScenario()
    {
        var option = new ChromeOptions();
        option.AddArguments("start-maximized");
        option.AddArguments("--disable-gpu");
        //option.AddArguments("--headless");

        new DriverManager().SetUpDriver(new ChromeConfig());
        Console.WriteLine("Setup");
        _driverHelper.Driver = new ChromeDriver(option);
        _driverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _driverHelper.Driver.Quit();
    }
}