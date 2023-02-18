using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Ecom.Models;
using Ecom.Services;
using AppDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using System;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace Ecom.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private ISingletonRnd singletonService;
        private ITransientRnd transientService;
        private IScopedRnd scopedService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger,
        ISingletonRnd _SingletonService,
        ITransientRnd _transientService,
            IScopedRnd _scopedService,IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment _hostEnvironment, IMapper mapper) : base(unitOfWork, configuration, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
          

            _logger = logger;
            singletonService = _SingletonService;
            transientService = _transientService;
            scopedService = _scopedService;
        }
 
        public IActionResult Index()
        {
        
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel sendMailDto)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                MailMessage mail = new MailMessage();
                // you need to enter your mail address
                mail.From = new MailAddress("aveen2000hussein@gmail.com");

                //To Email Address - your need to enter your to email address
                mail.To.Add("aveen.hussein@yandex.com");

                mail.Subject = sendMailDto.Subject;

                //you can specify also CC and BCC - i will skip this
                //mail.CC.Add("");
                //mail.Bcc.Add("");

                mail.IsBodyHtml = true;

                string content = "Name : " + sendMailDto.Name;
                content += "<br/> Message : " + sendMailDto.Message;

                mail.Body = content;


                //create SMTP instant

                //you need to pass mail server address and you can also specify the port number if you required
                SmtpClient smtpClient = new SmtpClient("mail.google.com");

                //Create nerwork credential and you need to give from email address and password
                NetworkCredential networkCredential = new NetworkCredential("aveen.hussein@yandex.com", "**");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 25; // this is default port number - you can also change this
                smtpClient.EnableSsl = false; // if ssl required you need to enable it
                smtpClient.Send(mail);

                ViewBag.Message = "Mail Send";

                // now i need to create the from 
                ModelState.Clear();

            }
            catch (Exception ex)
            {
                //If any error occured it will show
                ViewBag.Message = ex.Message.ToString();
            }
            return View();
        }
    
    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Singleton()
        {
            ViewBag.ServiceType = "Singleton";
            return View("ServicesView", singletonService.GetRandom());
        }

        public IActionResult Transient()
        {
            ViewBag.ServiceType = "Transient";
            return View("ServicesView", transientService.GetRandom());
        }

        public IActionResult Scoped()
        {
            ViewBag.ServiceType = "Scoped";
            return View("ServicesView", scopedService.GetRandom());
        }
        
    }
}
