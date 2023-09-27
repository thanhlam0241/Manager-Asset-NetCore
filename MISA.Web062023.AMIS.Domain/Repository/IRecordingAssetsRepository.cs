using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public interface IRecordingAssetsRepository
    {
        public Task<IEnumerable<RecordedAsset>> GetRecordingAssetsAsync(Guid recordingId);

        public Task<int> InsertRecordingAssetsAsync(RecordingAsset recordingAssets);

        public Task<int> InsertRecordingAssetsWithoutTransactionAsync(RecordingAsset recordingAssets);
        public Task<int> UpdateRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds);

        public Task<int> DeleteRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds);
    }
}
