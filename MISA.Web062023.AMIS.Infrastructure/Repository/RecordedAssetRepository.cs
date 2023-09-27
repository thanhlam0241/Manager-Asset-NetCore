using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{
    public class RecordedAssetRepository : BaseCrudRepository<RecordedAsset>, IRecordedAssetRepository
    {
        /// <summary>
        /// Constructor của DepartmentRepository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// Created by: NTLam (10/8/2023)
        public RecordedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<int> InsertMultipleAsync(List<RecordedAsset> recordedAssets)
        {
            var sqlInsertRecordedAsset = """
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, department_id, cost, depreciation_rate, recording_type) 
                    VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @DepartmentId, @Cost, @DepreciationRate, 1)
                """;
            List<dynamic> paramInsertRecordedAssets = new();
            foreach (var asset in recordedAssets)
            {
                if (asset.Department == null)
                {
                    throw new Exception("Tài sản phải có bộ phận sử dụng.");
                }
                paramInsertRecordedAssets.Add(new
                {
                    asset.RecordedAssetId,
                    asset.RecordedAssetCode,
                    asset.RecordedAssetName,
                    asset.Department.DepartmentId,
                    asset.Value,
                    asset.DepreciationRate
                });
            }
            var result = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecordedAsset, paramInsertRecordedAssets, transaction: _unitOfWork.Transaction);
            return result;
        }
    }
}
