using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The recording assets repository.
    /// </summary>
    public class RecordingAssetsRepository : IRecordingAssetsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecordingRepository _recordingRepository;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="recordingRepository">The recording repository.</param>
        public RecordingAssetsRepository(IUnitOfWork unitOfWork, IRecordingRepository recordingRepository)
        {
            _unitOfWork = unitOfWork;
            _recordingRepository = recordingRepository;
        }

        public Task<int> DeleteRecordingAssets(Guid recordingId, List<Guid> assetIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get recording assets.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        public async Task<IEnumerable<FixedAsset>> GetRecordingAssets(Guid recordingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("RecordingId", recordingId);
            var sqlQueryAssets = """
                WITH RecordingAssets AS (
                    SELECT
                        asset
                    FROM
                        recording_asset
                    WHERE
                        recording = @RecordingId
                )
                SELECT * FROM fixed_asset WHERE fixed_asset_id IN (SELECT asset FROM RecordingAssets)
                """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets = await _unitOfWork.Connection.QueryAsync<FixedAsset>(sqlQueryAssets, parameters);
            var l = await _unitOfWork.Connection.QueryAsync(sqlQueryAssets, parameters, _unitOfWork.Transaction);

            if (!listAssets.Any())
            {
                throw new NotFoundException(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.NoAssetWithRecording, recordingId));
            }

            return listAssets;

        }

        /// <summary>
        /// The insert recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        public async Task<int> InsertRecordingAssets(RecordingAsset recordingAssets)
        {
            var recording = recordingAssets.Recording;
            var fixedAssets = recordingAssets.FixedAssets;

            List<dynamic> lists = new List<dynamic>();

            foreach (FixedAsset fixedAsset in fixedAssets)
            {
                lists.Add(new
                {
                    Asset = fixedAsset.FixedAssetId,
                    Recording = recording.RecordingId
                });
            }

            var sqlInsertRecording = """
                INSERT INTO recording (recording_id,recording_code,recording_date,action_date,description) 
                    VALUES (@RecordingId, @RecordingCode, @RecordingDate, @ActionDate, @Description)
                """;

            var parametersInsertRecording = new DynamicParameters();
            parametersInsertRecording.Add("RecordingId", recording.RecordingId);
            parametersInsertRecording.Add("RecordingCode", recording.RecordingCode);
            parametersInsertRecording.Add("RecordingDate", recording.RecordingDate);
            parametersInsertRecording.Add("ActionDate", recording.ActionDate);
            parametersInsertRecording.Add("Description", recording.Description);

            var sqlInsertRecordingAsset = "INSERT INTO recording_asset (asset, recording) VALUES (@Asset, @Recording)";

            var resultRecord = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecording, parametersInsertRecording, _unitOfWork.Transaction);
            if (resultRecord == 1)
            {
                var resultRecordingAsset = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecordingAsset, lists, _unitOfWork.Transaction);

                if (resultRecordingAsset == fixedAssets.Count)
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }

            return 0;
        }

        /// <summary>
        /// The update recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateRecordingAssets(RecordingAsset recordingAssets)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The update recording assets.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateRecordingAssets(Guid recordingId, List<Guid> assetIds)
        {
            throw new NotImplementedException();
        }
    }
}
