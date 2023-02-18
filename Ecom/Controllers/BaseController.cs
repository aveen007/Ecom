using AppDbContext.Models;
using AppDbContext.UOW;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using Ecom.Models;

namespace Ecom.Controllers
{
    // somar
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IConfiguration _configuration;
        protected readonly IWebHostEnvironment hostEnvironment;
        internal IUnitOfWork unitOfWork
        {
            get { return _unitOfWork; }
        }
        public BaseController(IUnitOfWork unitOfWork, IConfiguration configuration
            , IWebHostEnvironment _hostEnvironment)
        {
            hostEnvironment = _hostEnvironment;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
           
        }
    

    public void Notify(string message,bool toastr=true, string title = "MultiShop",
                            NotificationTypeEnum notificationType = NotificationTypeEnum.success)
    {
        var msg = new
        {
            message = message,
            title = title,
            icon = notificationType.ToString(),
            type = notificationType.ToString(),
            provider = GetProvider(toastr)
        };

        TempData["Message"] = JsonConvert.SerializeObject(msg);
    }

    private string GetProvider(bool toastr)
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();

        IConfigurationRoot configuration = builder.Build();
            var value = toastr ? configuration["NotificationProvider"] : configuration["NotificationProvider1"];
      /*      if (toastr)
            {
         value = configuration["NotificationProvider"];

            }
            else
            {
                 value = configuration["NotificationProvider1"];

            }*/

            return value;
    }
}
}
