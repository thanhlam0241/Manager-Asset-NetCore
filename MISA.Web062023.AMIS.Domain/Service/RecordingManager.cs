using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class RecordingManager : BaseManager<Recording>, IRecordingManager
    {
        public RecordingManager(IRecordingRepository recordingRepository) : base(recordingRepository)
        {
        }
    }
}
