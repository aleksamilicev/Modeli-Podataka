using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;

namespace FTN.Services.NetworkModelService.DataModel.Scheduling
{
	public class IrregularTimePoint : IdentifiedObject
	{
		public IrregularTimePoint(long globalId) : base(globalId)
		{
		}

		private DateTime time;
		private float value1;
		private float value2;
		private BasicIntervalSchedule intervalSchedule;

		public bool TimeHasValue { get; set; }
		public bool Value1HasValue { get; set; }
		public bool Value2HasValue { get; set; }

		public DateTime Time
		{
			get { return time; }
            set
			{
				time = value;
				TimeHasValue = true;
			}
		}

		public float Value1
		{
			get { return value1; }
			set
			{
				value1 = value;
				Value1HasValue = true;
			}
		}

		public float Value2
		{
            get { return value2; }
			set
			{
				value2 = value;
				Value2HasValue = true;
			}
		}

		public BasicIntervalSchedule IntervalSchedule
		{
			get { return intervalSchedule; }
			set { intervalSchedule = value; }
		}
	}
}
