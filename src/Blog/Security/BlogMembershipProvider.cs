using System;
using System.Web.Mvc;
using System.Web.Security;
using Core.Domain;
using Core.Repositories;

namespace Web.Security
{
	public class BlogMembershipProvider : MembershipProvider
	{
		private BlogUser GetBlogUser(string username)
		{
			return DependencyResolver.Current.GetService<IUserRepository>().GetUser(username);
		}

		private bool UserPasswordIsValid(BlogUser blogUser, string password)
		{
			return !string.IsNullOrEmpty(password) && password == blogUser.Password;
		}

		public BlogMembershipProvider()
		{
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			if (username == null) throw new ArgumentNullException("username");

			return new MembershipUser(
				"BlogMembershipProvider",
				username, username,
				null, null, null, true, false,
				DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
		}

		public override bool ValidateUser(string username, string password)
		{
			if (username == null)
				throw new ArgumentNullException("username");

			var user = GetBlogUser(username);

			return (user != null) && UserPasswordIsValid(user, password);
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
												bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new System.NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
															string newPasswordAnswer)
		{
			throw new System.NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new System.NotImplementedException();
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new System.NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new System.NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new System.NotImplementedException();
		}

		

		public override bool UnlockUser(string userName)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new System.NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new System.NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new System.NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new System.NotImplementedException();
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool EnablePasswordReset
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string ApplicationName { get; set; }

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new System.NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new System.NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new System.NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new System.NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}