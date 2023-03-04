using TestProjectCorreios.Pages;

namespace TestProjectCorreios.Steps
{
    [Binding]
    public class SearchZipCodeStep
    {
        private readonly DriverHelper _driverHelper;

        private readonly SearchZipCodePages searchZipCodePages;
        //string zipCodeVar;

        public SearchZipCodeStep(DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            searchZipCodePages = new SearchZipCodePages(_driverHelper.Driver);
        }


        [Given(@"I access the CEP Search on the Correios website")]
        public void GivenIAccessThePostOfficeWebsite()
        {
            // _driverHelper.Driver.Navigate().GoToUrl("https://www.correios.com.br/");
            _driverHelper.Driver.Navigate().GoToUrl("https://buscacepinter.correios.com.br/app/endereco/index.php");
            searchZipCodePages.AccessCepSearch();
        }

        [When(@"I enter zip code I get address")]
        public void WhenIEnterZipCodeIGetAddress(Table table)
        {
            var data = table.Rows;
            foreach (var tableRow in data)
            {
                searchZipCodePages.InputZipCode(tableRow[0]);
                Console.WriteLine("CEP Consultado: " + tableRow[0]);
                var dataAddress = tableRow[1];
                searchZipCodePages.DataReturn(dataAddress);
                Console.WriteLine("Retorno esperado: " + tableRow[1]);
            }
        }

        [Then(@"I send an order ""(.*)""")]
        public void ThenISendAnOrder(string order)
        {
            _driverHelper.Driver.Navigate().GoToUrl("https://www.correios.com.br/");
            searchZipCodePages.SearcOrder(order);
        }
    }
    
}