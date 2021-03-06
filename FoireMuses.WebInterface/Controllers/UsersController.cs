﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoireMuses.WebInterface.Controllers;
using FoireMuses.Client;
using MindTouch.Tasking;
using FoireMuses.Webinterface.Models;
using System.Web.Security;
using FoireMuses.WebInterface.Models;
using System.Web.Routing;
using FoireMuses.WebInterface;

namespace FoireMuses.Webinterface.Controllers
{
	public class UsersController : FoireMusesController
	{

		public IFormsAuthenticationService FormsService { get; set; }
		public IMembershipService MembershipService { get; set; }

		protected override void Initialize(RequestContext requestContext)
		{
			if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
			if (MembershipService == null) { MembershipService = new AccountMembershipService(new MyMembershipProvider()); }

			base.Initialize(requestContext);
		}
		//
		// GET: /Login/

		public ActionResult Index()
		{
			return View("Login");
		}

		public ViewResult Login()
		{
			return View();
		}

		public ViewResult Denied()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string username, string password, bool rememberMe)
		{
			try
			{
				if (MembershipService.ValidateUser(username, password))
				{
					FormsService.SignIn(username, rememberMe);
				}
				else
				{
					ViewBag.Error = "Username ou password incorrect";
					return View("Login");
				}
			}
			catch (ArgumentException e)
			{
				ViewBag.Error = "Veuillez remplir les champs correctement";
				return View("Login");
			}
			catch (Exception e)
			{
				return RedirectToAction("Problem", "Error", null);
			}
			return RedirectToAction("Index","Home",null);
		}

		public ActionResult Logout()
		{
			FormsService.SignOut();
			return RedirectToAction("Index", "Home");
		}

	}
}
