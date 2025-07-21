using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.Scheduling;

namespace FTN.Services.NetworkModelService.DataModel.Outage
{
    public class IrregularIntervalSchedule : BasicIntervalSchedule
    {
        public IrregularIntervalSchedule(long globalId) : base(globalId)
        {
        }
    }
}
