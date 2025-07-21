using System;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment_te
    {
        public Switch(long globalId) : base(globalId)
        {
        }

        private bool normalOpen;
        private bool normalOpenHasValue = false;

        private float ratedCurrent;
        private bool ratedCurrentHasValue = false;

        private bool retained;
        private bool retainedHasValue = false;

        private int switchOnCount;
        private bool switchOnCountHasValue = false;

        private DateTime switchOnDate;
        private bool switchOnDateHasValue = false;

        public bool NormalOpen
        {
            get { return normalOpen; }
            set
            {
                normalOpen = value;
                normalOpenHasValue = true;
            }
        }

        public bool NormalOpenHasValue
        {
            get { return normalOpenHasValue; }
        }

        public float RatedCurrent
        {
            get { return ratedCurrent; }
            set
            {
                ratedCurrent = value;
                ratedCurrentHasValue = true;
            }
        }

        public bool RatedCurrentHasValue
        {
            get { return ratedCurrentHasValue; }
        }

        public bool Retained
        {
            get { return retained; }
            set
            {
                retained = value;
                retainedHasValue = true;
            }
        }

        public bool RetainedHasValue
        {
            get { return retainedHasValue; }
        }

        public int SwitchOnCount
        {
            get { return switchOnCount; }
            set
            {
                switchOnCount = value;
                switchOnCountHasValue = true;
            }
        }

        public bool SwitchOnCountHasValue
        {
            get { return switchOnCountHasValue; }
        }

        public DateTime SwitchOnDate
        {
            get { return switchOnDate; }
            set
            {
                switchOnDate = value;
                switchOnDateHasValue = true;
            }
        }

        public bool SwitchOnDateHasValue
        {
            get { return switchOnDateHasValue; }
        }
    }
}
