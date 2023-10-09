using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public interface IRecordedAssetRepository : ICrudRepository<RecordedAsset>
    {
        public Task<RecordedAsset> GetAssetAsync(Guid id);
        public Task<int> InsertMultipleAsync(Guid id, List<RecordedAsset> recordedAssets);
        public Task<int> UpdateAsync(RecordedAsset recordedAsset);
        public Task<int> UpdateMultipleAsync(Guid id, List<RecordedAsset> recordedAssets);
        public Task<int> DeleteMultipleAsync(List<Guid> ids);
        public Task<int> DeleteRecordedAsset(Guid assetId);

        public Task<bool> CheckIsExistCode(List<string> codes, Guid recordingId);
    }
}
