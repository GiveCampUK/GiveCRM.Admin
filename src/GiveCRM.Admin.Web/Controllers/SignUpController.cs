using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult SignUp(RequiredInfoViewModel requiredInfoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(requiredInfoViewModel);
            }

            var subDomain = GetSubDomainFromCharityName(requiredInfoViewModel.CharityName);
            var activationToken = TokenHelper.CreateRandomIdentifier();

            var registrationInfo = new RegistrationInfo();
            Mapper.DynamicMap(requiredInfoViewModel, registrationInfo);
            //TODO subDomain should be stored to database
            Session[activationToken.AsQueryString()] = subDomain;

            var result = _charityMembershipService.RegisterUserAndCharity(registrationInfo);

            if (result)
            {
                var emailViewModel = new EmailViewModel
                                         {
                                             To = requiredInfoViewModel.UserIdentifier,
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

            var viewModel = new CompleteViewModel
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
        public ActionResult StoreAdditionalInfo(CompleteViewModel completeViewModel)
        {
            var viewModel = completeViewModel.WithConfig(configuration);

            if (!ModelState.IsValid)
            {
                return View("Complete", viewModel);
            }

            return View("Complete", viewModel);
        }

        public ActionResult StartSite(string id)
        {
            var subDomain = GetSubDomainFromActivationToken(id);

            var viewModel = new StartSiteViewModel
                                {
                                    Url = configuration.CrmTestUrl,
                                    SubDomain = subDomain
                                };

            return View(viewModel);
        }

        private string GetSubDomainFromActivationToken(string activationToken)
        {
            //TODO get this from database
            var token = Session[activationToken];
            return token == null ? "" : token.ToString();
        }
    }
}
