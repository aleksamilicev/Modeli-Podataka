using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Scheduling
{
    public class RegularTimePoint : IdentifiedObject
    {
        public RegularTimePoint(long globalId) : base(globalId)
        {
        }

        private int sequenceNumber;
        private float value1;
        private float value2;
        private BasicIntervalSchedule intervalSchedule;

        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public float Value1
        {
            get { return value1; }
            set { value1 = value; }
        }

        public float Value2
        {
            get { return value2; }
            set { value2 = value; }
        }

        public BasicIntervalSchedule IntervalSchedule
        {
            get { return intervalSchedule; }
            set { intervalSchedule = value; }
        }

        // HasValue properties (ako trebaju u logici provere da li su vrednosti postavljene)
        public bool SequenceNumberHasValue { get; set; } = true;
        public bool Value1HasValue { get; set; } = true;
        public bool Value2HasValue { get; set; } = true;
    }
}
