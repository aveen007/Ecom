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
    

    public void Notify(string message, string title = "MultiShop",
                            NotificationTypeEnum notificationType = NotificationTypeEnum.success)
    {
        var msg = new
        {
            message = message,
            title = title,
            icon = notificationType.ToString(),
            type = notificationType.ToString(),
            provider = GetProvider()
        };

        TempData["Message"] = JsonConvert.SerializeObject(msg);
    }

    private string GetProvider()
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();

        IConfigurationRoot configuration = builder.Build();

        var value = configuration["NotificationProvider"];

        return value;
    }
}
}
