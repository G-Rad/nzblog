using System;
using System.Web.Mvc;
using System.Web.Security;
using Core.Repositories;
using Web.Models.Account;

namespace Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IUnitOfWork _unitOfWork;

		public AccountController(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			_userRepository = userRepository;
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public ActionResult Login(string returnUrl)
		{
			var model = new AccountLoginModel
							{
								ReturnUrl = returnUrl
							};

			return View(model);
		}

		[HttpPost]
		public ActionResult Login(AccountLoginModel model)
		{
			var user = _userRepository.GetUser(model.Username);

			if (Membership.ValidateUser(model.Username, model.Password))
			{
				LogInSiteUser(model.Username);

				if (!string.IsNullOrEmpty(model.ReturnUrl))
				{
					return Redirect(model.ReturnUrl);
				}

				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("Login");
		}

		protected virtual void LogInSiteUser(string username)
		{
			FormsAuthentication.SetAuthCookie(username, false);
		}
	}
}