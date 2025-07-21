using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportCurve();
			ImportCurveData();
			ImportEquipment();
			ImportSwitch();
			ImportOutageSchedule();
			ImportIrregularIntervalSchedule();
			ImportBasicIntervalSchedule();
			ImportIrregularTimePoint();
			ImportRegularTimePoint();
			ImportRegularIntervalSchedule();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        #region Import

        private void ImportCurve()
        {
            var cimCurves = concreteModel.GetAllObjectsOfType("FTN.Curve");
            if (cimCurves != null)
            {
                foreach (var kvp in cimCurves)
                {
                    FTN.Curve cimCurve = kvp.Value as FTN.Curve;

                    ResourceDescription rd = CreateCurveResourceDescription(cimCurve);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Curve ID = ").Append(cimCurve.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Curve ID = ").Append(cimCurve.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateCurveResourceDescription(FTN.Curve cimCurve)
        {
            if (cimCurve == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE, importHelper.CheckOutIndexForDMSType(DMSType.CURVE));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimCurve.ID, gid);

            PowerTransformerConverter.PopulateCurveProperties(cimCurve, rd);

            return rd;
        }

        private void ImportCurveData()
        {
            var cimCurveDatas = concreteModel.GetAllObjectsOfType("FTN.CurveData");
            if (cimCurveDatas != null)
            {
                foreach (var kvp in cimCurveDatas)
                {
                    FTN.CurveData cimCurveData = kvp.Value as FTN.CurveData;

                    ResourceDescription rd = CreateCurveDataResourceDescription(cimCurveData);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateCurveDataResourceDescription(FTN.CurveData cimCurveData)
        {
            if (cimCurveData == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE_DATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVE_DATA));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimCurveData.ID, gid);

            PowerTransformerConverter.PopulateCurveDataProperties(cimCurveData, rd, importHelper, report);

            return rd;
        }

        private void ImportEquipment()
        {
            var cimEquipments = concreteModel.GetAllObjectsOfType("FTN.Equipment");
            if (cimEquipments != null)
            {
                foreach (var kvp in cimEquipments)
                {
                    FTN.Equipment cimEquipment = kvp.Value as FTN.Equipment;

                    ResourceDescription rd = CreateEquipmentResourceDescription(cimEquipment);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Equipment ID = ").Append(cimEquipment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Equipment ID = ").Append(cimEquipment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateEquipmentResourceDescription(FTN.Equipment cimEquipment)
        {
            if (cimEquipment == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.EQUIPMENT, importHelper.CheckOutIndexForDMSType(DMSType.EQUIPMENT));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimEquipment.ID, gid);

            PowerTransformerConverter.PopulateEquipmentProperties(cimEquipment, rd);

            return rd;
        }

        private void ImportSwitch()
        {
            var cimSwitches = concreteModel.GetAllObjectsOfType("FTN.Switch");
            if (cimSwitches != null)
            {
                foreach (var kvp in cimSwitches)
                {
                    FTN.Switch cimSwitch = kvp.Value as FTN.Switch;

                    ResourceDescription rd = CreateSwitchResourceDescription(cimSwitch);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Switch ID = ").Append(cimSwitch.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Switch ID = ").Append(cimSwitch.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateSwitchResourceDescription(FTN.Switch cimSwitch)
        {
            if (cimSwitch == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCH, importHelper.CheckOutIndexForDMSType(DMSType.SWITCH));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimSwitch.ID, gid);

            PowerTransformerConverter.PopulateSwitchProperties(cimSwitch, rd);

            return rd;
        }

        private void ImportOutageSchedule()
        {
            var cimOutageSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
            if (cimOutageSchedules != null)
            {
                foreach (var kvp in cimOutageSchedules)
                {
                    FTN.OutageSchedule cimOutageSchedule = kvp.Value as FTN.OutageSchedule;

                    ResourceDescription rd = CreateOutageScheduleResourceDescription(cimOutageSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateOutageScheduleResourceDescription(FTN.OutageSchedule cimOutageSchedule)
        {
            if (cimOutageSchedule == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGE_SCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGE_SCHEDULE));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimOutageSchedule.ID, gid);

            PowerTransformerConverter.PopulateOutageScheduleProperties(cimOutageSchedule, rd);

            return rd;
        }

        private void ImportIrregularIntervalSchedule()
        {
            var cimIrregularIntervalSchedules = concreteModel.GetAllObjectsOfType("FTN.IrregularIntervalSchedule");
            if (cimIrregularIntervalSchedules != null)
            {
                foreach (var kvp in cimIrregularIntervalSchedules)
                {
                    FTN.IrregularIntervalSchedule cimIIS = kvp.Value as FTN.IrregularIntervalSchedule;

                    ResourceDescription rd = CreateIrregularIntervalScheduleResourceDescription(cimIIS);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularIntervalSchedule ID = ").Append(cimIIS.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularIntervalSchedule ID = ").Append(cimIIS.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateIrregularIntervalScheduleResourceDescription(FTN.IrregularIntervalSchedule cimIIS)
        {
            if (cimIIS == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULAR_INTERVAL_SCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULAR_INTERVAL_SCHEDULE));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimIIS.ID, gid);

            PowerTransformerConverter.PopulateIrregularIntervalScheduleProperties(cimIIS, rd);

            return rd;
        }

        private void ImportBasicIntervalSchedule()
        {
            var cimBasicIntervalSchedules = concreteModel.GetAllObjectsOfType("FTN.BasicIntervalSchedule");
            if (cimBasicIntervalSchedules != null)
            {
                foreach (var kvp in cimBasicIntervalSchedules)
                {
                    FTN.BasicIntervalSchedule cimBIS = kvp.Value as FTN.BasicIntervalSchedule;

                    ResourceDescription rd = CreateBasicIntervalScheduleResourceDescription(cimBIS);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("BasicIntervalSchedule ID = ").Append(cimBIS.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("BasicIntervalSchedule ID = ").Append(cimBIS.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateBasicIntervalScheduleResourceDescription(FTN.BasicIntervalSchedule cimBIS)
        {
            if (cimBIS == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BASIC_INTERVAL_SCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.BASIC_INTERVAL_SCHEDULE));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimBIS.ID, gid);

            PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimBIS, rd);

            return rd;
        }

        private void ImportIrregularTimePoint()
        {
            var cimIrregularTimePoints = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
            if (cimIrregularTimePoints != null)
            {
                foreach (var kvp in cimIrregularTimePoints)
                {
                    FTN.IrregularTimePoint cimITP = kvp.Value as FTN.IrregularTimePoint;

                    ResourceDescription rd = CreateIrregularTimePointResourceDescription(cimITP);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimITP.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimITP.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateIrregularTimePointResourceDescription(FTN.IrregularTimePoint cimITP)
        {
            if (cimITP == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULAR_TIME_POINT, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULAR_TIME_POINT));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimITP.ID, gid);

            PowerTransformerConverter.PopulateIrregularTimePointProperties(cimITP, rd, importHelper, report);

            return rd;
        }

        private void ImportRegularTimePoint()
        {
            var cimRegularTimePoints = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cimRegularTimePoints != null)
            {
                foreach (var kvp in cimRegularTimePoints)
                {
                    FTN.RegularTimePoint cimRTP = kvp.Value as FTN.RegularTimePoint;

                    ResourceDescription rd = CreateRegularTimePointResourceDescription(cimRTP);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRTP.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRTP.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateRegularTimePointResourceDescription(FTN.RegularTimePoint cimRTP)
        {
            if (cimRTP == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULAR_TIME_POINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULAR_TIME_POINT));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimRTP.ID, gid);

            PowerTransformerConverter.PopulateRegularTimePointProperties(cimRTP, rd, importHelper, report);

            return rd;
        }

        private void ImportRegularIntervalSchedule()
        {
            var cimRegularIntervalSchedules = concreteModel.GetAllObjectsOfType("FTN.RegularIntervalSchedule");
            if (cimRegularIntervalSchedules != null)
            {
                foreach (var kvp in cimRegularIntervalSchedules)
                {
                    FTN.RegularIntervalSchedule cimRIS = kvp.Value as FTN.RegularIntervalSchedule;

                    ResourceDescription rd = CreateRegularIntervalScheduleResourceDescription(cimRIS);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRIS.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRIS.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateRegularIntervalScheduleResourceDescription(FTN.RegularIntervalSchedule cimRIS)
        {
            if (cimRIS == null)
                return null;

            long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULAR_INTERVAL_SCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULAR_INTERVAL_SCHEDULE));
            ResourceDescription rd = new ResourceDescription(gid);

            importHelper.DefineIDMapping(cimRIS.ID, gid);

            PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimRIS, rd);

            return rd;
        }



        #endregion Import
    }
}

