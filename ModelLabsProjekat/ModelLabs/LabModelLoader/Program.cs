using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN;
using FTN.Common;

namespace LabModelLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            // v4a 2.
            Curve curve = new Curve
            {
                MRID = Guid.NewGuid().ToString(),
                Name = "Example Curve",
                CurveStyle = CurveStyle.straightLineYValues,
                XMultiplier = UnitMultiplier.none,
                XUnit = UnitSymbol.s,
                Y1Multiplier = UnitMultiplier.k,
                Y1Unit = UnitSymbol.V
            };

            CurveData dataPoint1 = new CurveData
            {
                MRID = Guid.NewGuid().ToString(),
                Xvalue = 0.0f,
                Y1value = 1.0f,
                Curve = curve
            };

            CurveData dataPoint2 = new CurveData
            {
                MRID = Guid.NewGuid().ToString(),
                Xvalue = 1.0f,
                Y1value = 2.0f,
                Curve = curve
            };

            // Spisak tačaka (ali pripada CurveData, ne Curve!)
            List<CurveData> curveDataList = new List<CurveData> { dataPoint1, dataPoint2 };

            // Ako hoćeš sve CurveData koje pripadaju nekom Curve, moraš ih filtrirati na osnovu Curve reference, npr
            var thisCurveData = curveDataList.Where(cd => cd.Curve == curve).ToList();

            var disconnector = new Disconnector()
            {
                MRID = Guid.NewGuid().ToString(),
                Name = "Main Disconnector"
            };

            var protectedSwitch = new ProtectedSwitch()
            {
                MRID = Guid.NewGuid().ToString(),
                Name = "Backup Switch",
                // Pretpostavimo da ima vezu sa disconnector-om ili schedule-om
            };

            var outageSchedule = new OutageSchedule()
            {
                MRID = Guid.NewGuid().ToString(),
                Name = "Planned Outage",
                // dodaj povezane tačke
            };

            IrregularIntervalSchedule irregularSchedule = new IrregularIntervalSchedule
            {
                MRID = Guid.NewGuid().ToString(),
                Name = "Example Schedule",
                StartTime = DateTime.UtcNow,
                Value1Multiplier = UnitMultiplier.k,
                Value1Unit = UnitSymbol.V
            };

            IrregularTimePoint timePoint = new IrregularTimePoint
            {
                MRID = Guid.NewGuid().ToString(),
                Time = 0.0f,
                Value1 = 123.45f,
                IntervalSchedule = irregularSchedule
            };

            // Lista tačaka (ako ti treba da ih okupiš negde)
            List<IrregularTimePoint> timePoints = new List<IrregularTimePoint> { timePoint };

            // Ako želiš da znaš koje tačke pripadaju kom rasporedu, radiš isto kao i sa Curve
            var timePointsForThisSchedule = timePoints.Where(tp => tp.IntervalSchedule == irregularSchedule).ToList();

            RegularIntervalSchedule regularSchedule = new RegularIntervalSchedule
            {
                MRID = Guid.NewGuid().ToString(),
                Name = "Daily Schedule",
                StartTime = DateTime.UtcNow,
                Value1Multiplier = UnitMultiplier.none,
                Value1Unit = UnitSymbol.A,
                TimeStep = 3600f // 1h u sekundama
            };

            RegularTimePoint regularTimePoint = new RegularTimePoint
            {
                MRID = Guid.NewGuid().ToString(),
                SequenceNumber = 1,
                Value1 = 10.0f,
                IntervalSchedule = regularSchedule
            };

            List<RegularTimePoint> regularTimePoints = new List<RegularTimePoint> { regularTimePoint };

            var pointsForThisRegularSchedule = regularTimePoints
                .Where(tp => tp.IntervalSchedule == regularSchedule)
                .ToList();

            // v4.a 4.
            var model = LabModelLoader.GenerateModel();

            Console.WriteLine($"Ukupno ResourceDescription instanci: {model.Count}");

            // ===== NOVO: Ispis broja entiteta svakog tipa =====
            Console.WriteLine("\n=== Broj entiteta po tipovima ===");

            var entityCounts = new Dictionary<string, int>();

            foreach (var rd in model)
            {
                // Izvlačimo tip iz GID-a
                short typeCode = ModelCodeHelper.ExtractTypeFromGlobalId(rd.Id);
                DMSType entityType = (DMSType)typeCode;
                string typeName = entityType.ToString();

                if (entityCounts.ContainsKey(typeName))
                {
                    entityCounts[typeName]++;
                }
                else
                {
                    entityCounts[typeName] = 1;
                }
            }

            // Sortiraj po imenu tipa za lepši ispis
            foreach (var kvp in entityCounts.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine($"Ukupno različitih tipova: {entityCounts.Count}");
            // ===== KRAJ NOVOG DELA =====

            foreach (var rd in model)
            {
                Console.WriteLine($"GID: 0x{rd.Id:X16}, Properties: {rd.Properties.Count}");
            }

            // v4.a 5.
            // Na ovoj putanji se nalazi .txt fajl ModelLabs\LabModelLoader\bin\Debug
            string outputPath = "model_report_v4a5.txt";
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Dodaj ispis broja entiteta i u fajl
                writer.WriteLine("=== Broj entiteta po tipovima ===");
                foreach (var kvp in entityCounts.OrderBy(x => x.Key))
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
                writer.WriteLine($"Ukupno različitih tipova: {entityCounts.Count}");
                writer.WriteLine("\n=== Detalji entiteta ===");

                foreach (var rd in model)
                {
                    string mRID = rd.GetProperty(ModelCode.IDOBJ_MRID)?.GetValue()?.ToString() ?? "N/A";
                    string name = rd.GetProperty(ModelCode.IDOBJ_NAME)?.GetValue()?.ToString() ?? "N/A";
                    string additional = "N/A";

                    // Pronađi dodatni atribut koji postoji
                    foreach (var prop in rd.Properties)
                    {
                        if (prop.Id != ModelCode.IDOBJ_MRID && prop.Id != ModelCode.IDOBJ_NAME)
                        {
                            additional = $"{prop.Id}: {prop.GetValue()}";
                            break;
                        }
                    }

                    writer.WriteLine($"mRID: {mRID}, name: {name}, additional: {additional}");
                }
            }

            Console.WriteLine($"✅ Izveštaj uspešno sačuvan kao '{outputPath}'");
        }
    }
}
