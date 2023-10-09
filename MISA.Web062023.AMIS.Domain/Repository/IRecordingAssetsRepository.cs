using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The i recording assets repository.
    /// </summary>
    /// Created by: NTLam (19/8/2023)
    public interface IRecordingAssetsRepository
    {

        /// <summary>
        /// The get recording assets async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<List<RecordedAsset>> GetRecordingAssetsAsync(Guid recordingId);

        /// <summary>
        /// The insert recording assets async.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<int> InsertRecordingAssetsAsync(RecordingAsset recordingAssets);

        /// <summary>
        /// The update recording assets async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assets">The assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<bool> UpdateRecordingAssetsAsync(Guid recordingId, List<RecordedAsset> assets);

        /// <summary>
        /// The delete recording assets async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<int> DeleteRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds);
    }
}
