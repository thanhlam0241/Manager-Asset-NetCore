using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public interface IResourceAssetRepository
    {
        public Task<ResourceAsset> GetByIdAsync(int id);
        public Task<List<ResourceAsset>> GetByAssetIdAsync(Guid id);
        public Task<int> InsertOneAsync(Guid assetId, ResourceAsset resourceAsset);
        public Task<int> InsertMultipleAsync(Guid assetId, List<ResourceAsset> resourceAssets);

        public Task<int> UpdateOneAsync(ResourceAsset resourceAsset);

        public Task<int> UpdateMultipleAsync(List<ResourceAsset> resourceAssets);

        public Task<int> DeleteOneAsync(int id);

        public Task<int> DeleteMultipleAsync(List<int> ids);
    }
}
