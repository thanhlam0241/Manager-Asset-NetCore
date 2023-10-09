using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The i recording service.
    /// </summary>
    public interface IRecordingService : ICrudService<RecordingDto, RecordingCreateDto, RecordingUpdateDto>
    {

        /// <summary>
        /// The get new code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLa (15/09/2023)
        public Task<string> GetNewCode();

        /// <summary>
        /// The get filter data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLa (15/09/2023)
        public Task<FilterData<Recording>> GetFilterDataAsync(int pageSize, int pageNumber, string filter);
    }
}
