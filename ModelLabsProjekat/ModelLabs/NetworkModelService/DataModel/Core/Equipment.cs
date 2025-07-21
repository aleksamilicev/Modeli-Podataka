using FTN.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Equipment : PowerSystemResource
    {
        public Equipment(long globalId) : base(globalId)
        {
        }

        private bool aggregate;
        private bool normallyInService;

        public bool Aggregate
        {
            get { return aggregate; }
            set { aggregate = value; }
        }

        public bool NormallyInService
        {
            get { return normallyInService; }
            set { normallyInService = value; }
        }

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.EQUIPMENT_AGGREGATE:
                case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_AGGREGATE:
                    property.SetValue(aggregate);
                    break;

                case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
                    property.SetValue(normallyInService);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.EQUIPMENT_AGGREGATE:
                    aggregate = property.AsBool();
                    break;

                case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
                    normallyInService = property.AsBool();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Equipment x = (Equipment)obj;
                return (x.aggregate == this.aggregate) &&
                       (x.normallyInService == this.normallyInService);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
