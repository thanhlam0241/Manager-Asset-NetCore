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
        public Task<string> GetNewCode();

        /// <summary>
        /// The get filter data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        public Task<FilterData<Recording>> GetFilterData(int pageSize, int pageNumber, string filter);
    }
}
