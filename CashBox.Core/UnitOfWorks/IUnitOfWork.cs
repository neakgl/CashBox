
namespace CashBox.Core.UnitOfWorks;

public interface IUnitOfWork
{
    // Asenkron kaydetme işlemi için
    Task CommitAsync();

    // Senkron kaydetme işlemi için
    void Commit();
}
