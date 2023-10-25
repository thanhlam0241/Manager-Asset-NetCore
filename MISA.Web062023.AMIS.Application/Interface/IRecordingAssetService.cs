using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The interface recording asset service.
    /// </summary>
    /// Created by: NTLam (15/09/2023)
    public interface IRecordingAssetService
    {

        /// <summary>
        /// The get recording async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (15/09/2023)
        public Task<RecordingDto> GetRecordingAsync(Guid recordingId);

        /// <summary>
        /// The create new recording.
        /// </summary>
        /// <param name="recording">The recording.</param>
        /// <param name="assets">The assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (15/09/2023)
        public Task<int> CreateNewRecording(RecordingCreateDto recording, List<RecordedAssetCreateDto> assets);

        /// <summary>
        /// The delete recording async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="ids">The ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (15/09/2023)
        public Task<int> DeleteRecordingAsync(Guid recordingId, List<Guid> ids);

        /// <summary>
        /// The update recording.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="recordingUpdateDto">The recording update dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLa (15/09/2023)
        public Task<bool> UpdateRecordingAsync(Guid id, RecordingUpdateDto recordingUpdateDto);
    }
}
