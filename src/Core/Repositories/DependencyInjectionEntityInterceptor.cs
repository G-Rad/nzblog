using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Core.Domain;
using NHibernate;

namespace Core.Repositories
{
	public class DependencyInjectionEntityInterceptor : EmptyInterceptor
	{
		readonly Func<ILifetimeScope> _container;
		ISession _session;

		public DependencyInjectionEntityInterceptor(Func<ILifetimeScope> container)
		{
			_container = container;
		}

		public override void SetSession(ISession session)
		{
			_session = session;
		}

		public override object Instantiate(string clazz, EntityMode entityMode, object id)
		{
			if (entityMode == EntityMode.Poco)
			{
				var type = Assembly.GetAssembly(typeof(Post)).GetTypes().FirstOrDefault(x => x.FullName == clazz);
				if (type != null && type.GetConstructors().Any(x => x.GetParameters().Any()))
				{
					var parameters = type.GetConstructors().First(x => x.GetParameters().Any()).GetParameters();
					var args = parameters.Select(x => _container().Resolve(x.ParameterType)).ToArray();

					var instance = Activator.CreateInstance(type, args);

					var md = _session.SessionFactory.GetClassMetadata(clazz);
					md.SetIdentifier(instance, id, entityMode);
					return instance;
				}
			}
			return base.Instantiate(clazz, entityMode, id);
		}

	}
}