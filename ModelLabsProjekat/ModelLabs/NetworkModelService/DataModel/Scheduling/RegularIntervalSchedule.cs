using System;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Scheduling
{
    public class RegularIntervalSchedule : BasicIntervalSchedule
    {
        public RegularIntervalSchedule(long globalId) : base(globalId)
        {
        }

        private DateTime endTime;
        private bool endTimeHasValue = false;

        private float timeStep;
        private bool timeStepHasValue = false;

        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                endTimeHasValue = true;
            }
        }

        public bool EndTimeHasValue
        {
            get { return endTimeHasValue; }
        }

        public float TimeStep
        {
            get { return timeStep; }
            set
            {
                timeStep = value;
                timeStepHasValue = true;
            }
        }

        public bool TimeStepHasValue
        {
            get { return timeStepHasValue; }
        }
    }
}
