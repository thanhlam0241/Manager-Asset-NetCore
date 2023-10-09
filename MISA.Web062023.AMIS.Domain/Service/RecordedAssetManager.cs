using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class RecordedAssetManager : BaseManager<RecordedAsset>, IRecordedAssetManager
    {
        public RecordedAssetManager(IRecordedAssetRepository recordedAssetRepository) : base(recordedAssetRepository)
        {

        }
    }
}
