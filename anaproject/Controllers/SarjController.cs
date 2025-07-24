using Microsoft.AspNetCore.Mvc;
using anaproject.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using static anaproject.Models.Cars;

namespace anaproject.Controllers
{
    public class SarjController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SarjController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> HowLongToCharge()
        {
            return View();
        }

        public IActionResult HomeSarj()
        {
            return View();
        }

        public IActionResult Ipuclari()
        {
            return View();
        }

        public IActionResult ChargeVideo()
        {
            return View();
        }
        public IActionResult SSS()
        {
            return View();
        }
    }
}