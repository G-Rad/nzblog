namespace Core.Domain
{
	public interface IEntity<out T>
	{
		T Id { get; }
	}
}