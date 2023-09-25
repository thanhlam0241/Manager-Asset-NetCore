using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System.Data;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The recording repository.
    /// </summary>
    public class RecordingRepository : BaseCrudRepository<Recording>, IRecordingRepository
    {

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public RecordingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// The get filter recording.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public async Task<FilterData<Recording>> GetFilterRecording(int pageSize, int pageNumber, string filter)
        {
            var parameters = new DynamicParameters();
            var fieldAssets = new string[]
            {
               "recording_code"
            };
            string filterQuery = (filter != "") ? $"WHERE ({string.Join(" LIKE @FilterPattern OR ", fieldAssets)} LIKE @FilterPattern)  " : "";
            string query = $"SELECT * FROM recording {filterQuery}  ORDER BY modified_date DESC LIMIT @PageSize OFFSET @Offset;";
            var queryTotalRecord = $"SELECT Count(recording_code) from recording {filterQuery}";

            parameters.Add("PageSize", pageSize, DbType.Int16);
            parameters.Add("Offset", (pageNumber - 1) * pageSize, DbType.Int16);
            parameters.Add("FilterPattern", $"%{filter}%", DbType.String);

            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var recordings = await connection.QueryAsync<Recording>(query, parameters, _unitOfWork.Transaction);
            long totalRecords = await connection.ExecuteScalarAsync<long>(queryTotalRecord, parameters, _unitOfWork.Transaction);

            FilterData<Recording> filterRecording = new();
            if (recordings.ToList().Count > 0 && totalRecords > 0)
            {
                filterRecording.Data = recordings;
                filterRecording.CurrentPage = pageNumber;
                filterRecording.CurrentPageSize = pageSize;
                filterRecording.TotalRecord = totalRecords;
                filterRecording.CurrentRecord = recordings.ToList().Count;
                filterRecording.TotalPage = totalRecords / pageSize + (totalRecords % pageSize == 0 ? 0 : 1);
            }
            return filterRecording;
        }

        /// <summary>
        /// The generate code.
        /// </summary>
        /// <returns>The result.</returns>
        public Task<string> GenerateCode()
        {
            var sql = "SELECT MAX(fixed_asset_code) FROM recording WHERE recording_code RLIKE '^GT[0-9]{6}$' ";
            var connection = _unitOfWork.Connection;
            var code = connection.ExecuteScalar<string>(sql);
            if (code == null)
            {
                return Task.FromResult("GT000001");
            }
            string result;
            var number = int.Parse(code[2..]) + 1;
            if (number < 1000000)
            {
                result = $"GT{number.ToString().PadLeft(6, '0')}";
            }
            else
            {
                result = $"GT{number}";
            }
            return Task.FromResult(result);
        }
    }
}
