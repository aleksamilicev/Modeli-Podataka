/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace LabModelLoader
{
    public class LabModelLoader
    {
        private static long globalIdCounter = 0x1000000000000001;

        private static long GetNextGlobalId()
        {
            return globalIdCounter++;
        }

        public static List<ResourceDescription> GenerateModel()
        {
            List<ResourceDescription> model = new List<ResourceDescription>();

            for (int i = 0; i < 20; i++)
            {
                long transformerGid = GetNextGlobalId();
                var transformerRD = new ResourceDescription(transformerGid);

                transformerRD.AddProperty(new Property(ModelCode.IDOBJ_MRID, Guid.NewGuid().ToString()));
                transformerRD.AddProperty(new Property(ModelCode.IDOBJ_NAME, $"Transformer_{i}"));
                transformerRD.AddProperty(new Property(ModelCode.POWERTR_AUTO, false));
                transformerRD.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)TransformerFunction.Transmission));

                // Dodaj windinge
                List<long> windingGIDs = new List<long>();
                for (int w = 0; w < 2; w++)
                {
                    long windingGid = GetNextGlobalId();
                    var windingRD = new ResourceDescription(windingGid);

                    windingRD.AddProperty(new Property(ModelCode.IDOBJ_MRID, Guid.NewGuid().ToString()));
                    windingRD.AddProperty(new Property(ModelCode.IDOBJ_NAME, $"Winding_{i}_{w}"));
                    windingRD.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, transformerGid));

                    model.Add(windingRD);
                    windingGIDs.Add(windingGid);
                }

                // NMS-style reference dodavanje
                transformerRD.AddProperty(new Property(ModelCode.POWERTR_WINDINGS, windingGIDs));

                model.Add(transformerRD);
            }

            return model;
        }
    }

}
*/