using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Outage
{
    public class OutageSchedule : IrregularIntervalSchedule
    {
        public OutageSchedule(long globalId) : base(globalId)
        {
        }

        // Trenutno nema dodatnih svojstava. Ako ih bude, dodaj ih ovde.
    }
}
