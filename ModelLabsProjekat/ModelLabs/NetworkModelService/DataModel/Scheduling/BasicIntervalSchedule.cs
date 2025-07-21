using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Scheduling
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        public BasicIntervalSchedule(long globalId) : base(globalId) 
        {
        }

        private long startTime;
        private UnitMultiplier value1Multiplier;
        private UnitSymbol value1Unit;
        private UnitMultiplier value2Multiplier;
        private UnitSymbol value2Unit;

        private bool startTimeHasValue = false;
        private bool value1MultiplierHasValue = false;
        private bool value1UnitHasValue = false;
        private bool value2MultiplierHasValue = false;
        private bool value2UnitHasValue = false;

        public long StartTime
        {
            get { return startTime; }
            set { startTime = value; startTimeHasValue = true; }
        }

        public bool StartTimeHasValue => startTimeHasValue;

        public UnitMultiplier Value1Multiplier
        {
            get { return value1Multiplier; }
            set { value1Multiplier = value; value1MultiplierHasValue = true; }
        }

        public bool Value1MultiplierHasValue => value1MultiplierHasValue;

        public UnitSymbol Value1Unit
        {
            get { return value1Unit; }
            set { value1Unit = value; value1UnitHasValue = true; }
        }

        public bool Value1UnitHasValue => value1UnitHasValue;

        public UnitMultiplier Value2Multiplier
        {
            get { return value2Multiplier; }
            set { value2Multiplier = value; value2MultiplierHasValue = true; }
        }

        public bool Value2MultiplierHasValue => value2MultiplierHasValue;

        public UnitSymbol Value2Unit
        {
            get { return value2Unit; }
            set { value2Unit = value; value2UnitHasValue = true; }
        }

        public bool Value2UnitHasValue => value2UnitHasValue;
    }
}
