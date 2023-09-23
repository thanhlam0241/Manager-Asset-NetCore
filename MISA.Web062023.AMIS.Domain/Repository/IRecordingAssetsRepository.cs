using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public interface IRecordingAssetsRepository
    {
        public Task<IEnumerable<FixedAsset>> GetRecordingAssets(Guid recordingId);

        public Task<int> InsertRecordingAssets(RecordingAsset recordingAssets);
        public Task<int> UpdateRecordingAssets(Guid recordingId, List<Guid> assetIds);

        public Task<int> DeleteRecordingAssets(Guid recordingId, List<Guid> assetIds);
    }
}
