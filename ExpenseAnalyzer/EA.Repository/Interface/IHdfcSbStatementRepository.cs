using System.Data;

namespace EA.Repository.Interface
{
    public interface IHdfcSbStatementRepository
    {
        Task<bool> InsertBulkData(DataTable data);
    }
}
