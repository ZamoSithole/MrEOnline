using System.Data.Entity;

namespace MrE.Repository.Abstractions
{
    public interface IDataStoreBase
    {
        DbContext DataStoreContext { get; set; }
    }
}
