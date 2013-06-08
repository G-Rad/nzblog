using System.Web.Mvc;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Mvc;
using Blog;
using Core;
using Core.Repositories;
using Core.Services;
using NHibernate;
using NLog;
using Web.Filters;
using Web.Security;

namespace Web
{
	public class AutofacConfig
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public static void Register(MvcApplication application)
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			builder.RegisterInstance(new NhibernateInitializer().BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
			builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerLifetimeScope().OnActivating(a => Logger.Debug("ISession activating")).OnActivated(a => Logger.Debug("ISession activated")).OnPreparing(a => Logger.Debug("ISession Preparing")).OnRelease(x => { x.Dispose(); x = null; Logger.Debug("Autofac: ISession dispousing"); });
			builder.RegisterType<NHibernateUnitOfWork>().As<IUnitOfWork, INHibernateUnitOfWork>();

			//repositories
			builder.RegisterType<PostRepository>().As<IPostRepository>();
			builder.RegisterType<InstagramRepository>().As<IInstagramRepository>();
			builder.RegisterType<SettingsRepository>().As<ISettingsRepository>();
			builder.RegisterType<FlickrRepository>().As<IFlickrRepository>();
			builder.RegisterType<UserRepository>().As<IUserRepository>();

			builder.RegisterType<InstagramService>().As<IInstagramService>();
			builder.RegisterType<FlickrService>().As<IFlickrService>();
			builder.RegisterType<ApplicationSettingsProvider>().As<IApplicationSettingsProvider>();

			builder.RegisterType<BlogMembershipProvider>();
			builder.RegisterType<BlogRoleProvider>();
			builder.RegisterType<BlogMembershipProvider>().As<MembershipProvider>();

			//register filter
			builder.RegisterType<InstagramGlobalFilter>().As<IActionFilter>();

			//Inject HTTP Abstractions
			builder.RegisterModule(new AutofacWebTypesModule());

			// filters
			builder.RegisterFilterProvider();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			MvcApplication.AutofaqContainer = container;
			NhibernateInitializer.Container = container;
		}

	}
}