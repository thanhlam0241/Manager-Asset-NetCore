using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The unit of work.
    /// </summary>
    /// Created by: NTLam (17/8/2023)
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private DbConnection? _connection = null;
        private DbTransaction? _transaction = null;
        private readonly string _connectionString;
        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// Created by: NTLam (17/8/2023)
        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// Created by: NTLam (17/8/2023)
        public DbConnection Connection => _connection ??= new MySqlConnection(_connectionString);

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// Created by: NTLam (17/8/2023)
        public DbTransaction Transaction => _transaction;

        #endregion

        #region Methods
        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// Created by: NTLam (17/8/2023)
        public void BeginTransaction()
        {
            _connection ??= new MySqlConnection(_connectionString);
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
        }

        /// <summary>
        /// The begin transaction async.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/8/2023)
        public async Task BeginTransactionAsync()
        {
            _connection ??= new MySqlConnection(_connectionString);
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        /// <summary>
        /// The commit.
        /// </summary>
        /// Created by: NTLam (17/8/2023)
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        /// <summary>
        /// The commit async.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/8/2023)
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// Created by: NTLam (17/8/2023)
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        /// <summary>
        /// The dispose async.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/8/2023)
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
        }

        /// <summary>
        /// The rollback.
        /// </summary>
        /// Created by: NTLam (17/8/2023)
        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// The rollback asyn.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/8/2023)
        public async Task RollbackAsyn()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }

        /// <summary>
        /// The get connection string.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/8/2023)
        public string getConnectionString()
        {
            return _connectionString;
        }

        public DbConnection DbConnection()
        {
            return new MySqlConnection(_connectionString);
        }
        #endregion
    }
}
