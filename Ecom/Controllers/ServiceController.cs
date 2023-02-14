using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Ecom.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Ecom.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ServiceController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
        }

        public async Task<IActionResult> TrackOrder(int? OrderId)
        {
            if (OrderId != null)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44352/api/TrackOrderService/" + OrderId.ToString()))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (apiResponse == "")
                        {
                            ViewData["ShippingState"] = "An error has occurred when calling the service";
                        }
                        else
                        {
                            ShippingState shippingState = JsonConvert.DeserializeObject<ShippingState>(apiResponse);
                            ViewData["ShippingState"] = shippingState.Description;
                        }
                    }
                }
            }
            else
            {
                ViewData["ShippingState"] = "No request sent yet";
            }
            ViewData["OrderId"] = OrderId;
            return View();
        }
    }
}
