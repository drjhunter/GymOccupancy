using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using charting_webapi;
using Newtonsoft.Json;

namespace charting_webapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly Uri _apiuri;


        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            var urlSetting = configuration.GetValue<string>("Apis:gymwebapiurl"); 
            _apiuri =  new Uri(urlSetting);
        }

        public async Task<List<int>> HourlyOccupancyRates()
        {
            var httpClient = new HttpClient();
            var responseAsString = await httpClient.GetStringAsync(_apiuri);
            var objects = JsonConvert.DeserializeObject<List<Occupancy>>(responseAsString);
            return objects.Select(x => x.NumberOfPeople).ToList();
                //return new List<int> { 12, 19, 3, 5, 2, 3, 5, 5, 5, 1, 2, 2, 1, 0, 0, 0, 1, 3, 5, 6, 8, 7, 9, 12 }.ToArray();
        }
    }
}
