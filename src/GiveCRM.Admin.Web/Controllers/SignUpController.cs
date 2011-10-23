using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using GiveCRM.Admin.DataAccess;
using GiveCRM.Admin.Models;
using GiveCRM.Admin.Web.Extensions;
using GiveCRM.Admin.Web.Helpers;
using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.Services;
using GiveCRM.Admin.Web.ViewModels.SignUp;
using IConfiguration = GiveCRM.Admin.Web.Interfaces.IConfiguration;

namespace GiveCRM.Admin.Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ISignUpQueueingService signUpQueueingService;
        private readonly ICharityMembershipService _charityMembershipService;


        public SignUpController(IConfiguration configuration, ISignUpQueueingService signUpQueueingService, ICharityMembershipService charityMembershipService)
        {
            this.configuration = configuration;
            this.signUpQueueingService = signUpQueueingService;
            _charityMembershipService = charityMembershipService;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SignUp(RequiredInfo requiredInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(requiredInfo);
            }

            var subDomain = GetSubDomainFromCharityName(requiredInfo.CharityName);
            var activationToken = TokenHelper.CreateRandomIdentifier();

            var registrationInfo = new RegistrationInfo();
            Mapper.DynamicMap(requiredInfo, registrationInfo);

            var result = _charityMembershipService.RegisterUserAndCharity(registrationInfo);

            if (result)
            {
                var emailViewModel = new EmailViewModel
                                         {
                                             To = requiredInfo.UserIdentifier,
                                             ActivationToken = activationToken.AsQueryString()
                                         };

                signUpQueueingService.QueueEmail(emailViewModel);
                signUpQueueingService.QueueProvisioning();

                TempData["SubDomain"] = subDomain;

                return RedirectToAction("Complete");
            }

            ModelState.AddModelError("", "User and Charity registration failed. Please contact support.");
            return View();
        }

        [HttpGet]
        public ActionResult Complete()
        {
            var subDomain = TempData["SubDomain"] as string;

            var viewModel = new Complete
                                {
                                    SubDomain = subDomain
                                }.WithConfig(configuration);

            return View(viewModel);
        }

        private static string GetSubDomainFromCharityName(string charityName)
        {
            var result = Regex.Replace(charityName, @"[\s]", "-");
            return Regex.Replace(result, @"[^\w-]", "");
        }

        [HttpPost]
        public ActionResult StoreAdditionalInfo(Complete complete)
        {
            var viewModel = complete.WithConfig(configuration);

            if (!ModelState.IsValid)
            {
                return View("Complete", viewModel);
            }

            return View("Complete", viewModel);
        }
    }
}
