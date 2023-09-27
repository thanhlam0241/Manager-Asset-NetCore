using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public interface IRecordedAssetRepository : ICrudRepository<RecordedAsset>
    {
        public Task<int> InsertMultipleAsync(List<RecordedAsset> recordedAssets);
    }
}
