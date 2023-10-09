namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The i recording repository.
    /// </summary>
    public interface IRecordingRepository : ICrudRepository<Recording>
    {

        /// <summary>
        /// The get filter recording.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public Task<FilterData<Recording>> GetFilterRecording(int pageSize, int pageNumber, string filter);

        /// <summary>
        /// The generate code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public Task<string> GenerateCode();
    }
}
