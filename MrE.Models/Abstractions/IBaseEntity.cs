namespace MrE.Models.Abstractions
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}
