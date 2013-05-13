using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Core.Domain;
using Core.Repositories;

namespace Web.Security
{
	public class BlogRoleProvider : RoleProvider
	{
		public BlogRoleProvider()
		{
		}

		private BlogUser GetBlogUser(string username)
		{
			return DependencyResolver.Current.GetService<IUserRepository>().GetUser(username);
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			if (username == null) throw new ArgumentNullException("username");
			if (roleName == null) throw new ArgumentNullException("roleName");

			var user = GetBlogUser(username);

			if (user == null)
				return false;

			switch (roleName)
			{
				case "Admin":
					return user.IsAdmin;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override string[] GetRolesForUser(string username)
		{
			var roles = new List<string>();

			var user = GetBlogUser(username);

			if (user == null)
				throw new Exception(string.Format("There is no member with username '{0}'", username));

			if (user.IsAdmin)
				roles.Add("Admin");

			return roles.ToArray();
		}

		public override void CreateRole(string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new System.NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new System.NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new System.NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new System.NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new System.NotImplementedException();
		}

		public override string ApplicationName { get; set; }
	}
}