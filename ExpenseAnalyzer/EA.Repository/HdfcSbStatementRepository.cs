using EA.Model;
using EA.Repository.Interface;
using System.Data;

namespace EA.Repository
{
    public class HdfcSbStatementRepository : IHdfcSbStatementRepository
    {
        private readonly DataContext dataContext;
        //static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public HdfcSbStatementRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<bool> InsertBulkData(DataTable data)
        {
            if (data == null || data.Rows.Count == 0) return false;

            List<HdfcSbStatement> transactions = (from row in data.Select()
                                                  select new HdfcSbStatement
                                                  {
                                                      Date = DateTime.ParseExact(row.Field<string>(0), "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture),
                                                      Narration = row.Field<string>(1),
                                                      ChequeOrRefNo = row.Field<string>(2),
                                                      ValueDate = DateTime.ParseExact(row.Field<string>(3), "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture),
                                                      WithdrawalAmount = string.IsNullOrEmpty(row.Field<string>(4)) ? 0 : Convert.ToDecimal(row.Field<string>(4)),
                                                      DepositAmount = string.IsNullOrEmpty(row.Field<string>(5)) ? 0 : Convert.ToDecimal(row.Field<string>(5)),
                                                      ClosingBalance = Convert.ToDecimal(row.Field<string>(6))
                                                  }).ToList();

            var count = 0;
            //await semaphoreSlim.WaitAsync();
            try
            {
                dataContext.HdfcSbStatements.AddRange(transactions);
                count = await dataContext.SaveChangesAsync();
            }
            finally
            {
                //semaphoreSlim.Release();
            }

            return count == data.Rows.Count;
        }
    }
}
