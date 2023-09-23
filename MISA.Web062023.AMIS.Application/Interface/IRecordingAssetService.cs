using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public interface IRecordingAssetService
    {
        public Task<List<FixedAsset>> GetAssetsByRecordingIdAsync(Guid recordingId);
        public Task<int> CreateNewRecording(RecordingCreateDto recording, List<Guid> ids);

        public Task<int> DeleteRecordingAsset(Guid recordingId, List<Guid> ids);

        public Task<int> InsertRecordingAsset(Guid recordingId, List<Guid> ids);

        public Task<int> UpdateRecordingAsset(Guid recordingId, List<Guid> listAssetIdsAfterChange);
    }
}
