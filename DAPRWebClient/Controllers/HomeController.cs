﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DAPRWebClient.Models;

namespace DAPRWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DaprClient _daprClient;

        public const string statestore = nameof(statestore);

        public HomeController(ILogger<HomeController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<IActionResult> Index()
        {
            var stateValue = await _daprClient.GetStateEntryAsync<SomeState>(statestore, "some-key");
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await _daprClient.SaveStateAsync<SomeState>(statestore, "some-key", new SomeState(){ SomeProperty = "some value of SomeProperty" });
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public class SomeState
        {
            public string SomeProperty { get; set; }
        }
    }
}
