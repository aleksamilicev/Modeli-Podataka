using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class PowerSystemResource : IdentifiedObject
    {
        public PowerSystemResource(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool HasProperty(ModelCode t)
        {
            return base.HasProperty(t);
        }

        public override void GetProperty(Property prop)
        {
            base.GetProperty(prop);
        }

        public override void SetProperty(Property property)
        {
            base.SetProperty(property);
        }

        public override bool IsReferenced
        {
            get
            {
                return base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            base.GetReferences(references, refType);
        }
    }
}
