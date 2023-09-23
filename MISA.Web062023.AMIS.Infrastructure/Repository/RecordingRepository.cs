using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{
    public class RecordingRepository : BaseCrudRepository<Recording>, IRecordingRepository
    {
        public RecordingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
