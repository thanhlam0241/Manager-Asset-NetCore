using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public interface IRecordedAssetService : ICrudService<RecordedAssetDto, RecordedAssetCreateDto, RecordedAssetUpdateDto>
    {

        public Task<RecordedAsset> GetAssetByIdAsync(Guid id);
        public Task<RecordedAssetCreateDto> CreateAssetAsync(RecordedAssetCreateDto assetCreateDto);

        public Task<int> UpdateAssetAsync(Guid id, RecordedAssetUpdateDto assetUpdateDto);

        public Task<int> DeleteAssetAsync(Guid assetId);
    }
}
