using System.Data.Common;

namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Khai báo Interface Unit of work
    /// </summary>
    /// Created by: NTLam (17/08/2023)
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Khai báo connection
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        DbConnection Connection { get; }

        /// <summary>
        /// Khai báo transaction
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        DbTransaction Transaction { get; }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public string getConnectionString();

        /// <summary>
        /// Bắt đầu transaction
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        void BeginTransaction();

        /// <summary>
        /// Bắt đầu transaction bất đồng bộ
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        Task BeginTransactionAsync();

        /// <summary>
        /// Commit transaciton
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        void Commit();

        /// <summary>
        /// Commit transaciton bất đồng bộ
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        Task CommitAsync();

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        void Rollback();

        /// <summary>
        /// Rollback transaction bất đồng bộ
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        Task RollbackAsyn();
    }
}