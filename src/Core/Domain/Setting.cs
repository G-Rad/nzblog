namespace Core.Domain
{
	public class Setting : IEntity<int>
	{
		public virtual int Id { get; set; }

		public virtual string SettingsKey { get; set; }

		public virtual string Value { get; set; }
	}
}