using System;
using System.Linq;
using Autofac;
using Core.Domain;
using Core.Repositories;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace Core
{
	public class NhibernateInitializer
	{
		/// <summary>
		/// Used in <see cref="DependencyInjectionEntityInterceptor"/>
		/// </summary>
		public static ILifetimeScope Container { get; set; } 

		public ISessionFactory BuildSessionFactory()
		{
			return GetConfiguration().BuildSessionFactory();
		}

		private FluentConfiguration GetConfiguration()
		{
			var configuration = new Configuration();

			configuration.SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, "false");
			configuration.SetProperty(NHibernate.Cfg.Environment.BatchSize, "0");
			NHibernate.Cfg.Environment.UseReflectionOptimizer = false;

			return Fluently.Configure(configuration)
				.ExposeConfiguration(x => x.SetInterceptor(new DependencyInjectionEntityInterceptor(() => Container)))
				.Database(MsSqlConfiguration.MsSql2008
					.ConnectionString(x => x.FromConnectionStringWithKey("Database")))
				.Mappings(x =>
				{
					x.AutoMappings.Add(
						AutoMap.Assembly(typeof(IEntity<>).Assembly, new CustomAutomapConfiguration())
						.UseOverridesFromAssembly(typeof(IEntity<>).Assembly));
					x.HbmMappings.AddFromAssemblyOf<Post>();
				})
				;
		}
	}

	public class CustomAutomapConfiguration : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntity<>));
		}
	}

}