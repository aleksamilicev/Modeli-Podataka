namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_DESCRIPTION, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);
			// gotov
			}
		}

		public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
		{
			if ((cimCurve != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurve, rd);

				if (cimCurve.CurveStyleHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.CURVE_STYLE, (long)cimCurve.CurveStyle));
				}
				if (cimCurve.XMultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_X_MULTIPLIER, (long)cimCurve.XMultiplier));
				}
				if (cimCurve.XUnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_X_UNIT, (long)cimCurve.XUnit));
				}
				if (cimCurve.Y1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y1_MULTIPLIER, (long)cimCurve.Y1Multiplier));
				}
				if (cimCurve.Y1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y1_UNIT, (long)cimCurve.Y1Unit));
				}
				if (cimCurve.Y2MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y2_MULTIPLIER, (long)cimCurve.Y2Multiplier));
				}
				if (cimCurve.Y2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y2_UNIT, (long)cimCurve.Y2Unit));
				}
				if (cimCurve.Y3MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y3_MULTIPLIER, (long)cimCurve.Y3Multiplier));
				}
				if (cimCurve.Y3UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_Y3_UNIT, (long)cimCurve.Y3Unit));
				}
			}
		}

		public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
		{
			if ((cimBasicIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

				if (cimBasicIntervalSchedule.StartTimeHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.BIS_START_TIME, cimBasicIntervalSchedule.StartTime));
				}
				if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE1_MULTIPLIER, (long)cimBasicIntervalSchedule.Value1Multiplier));
				}
				if (cimBasicIntervalSchedule.Value1UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE1_UNIT, (long)cimBasicIntervalSchedule.Value1Unit));
				}
				if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE2_MULTIPLIER, (long)cimBasicIntervalSchedule.Value2Multiplier));
				}
				if (cimBasicIntervalSchedule.Value2UnitHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BIS_VALUE2_UNIT, (long)cimBasicIntervalSchedule.Value2Unit));
				}
			}
		}

		public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimCurveData != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurveData, rd);

				if (cimCurveData.XvalueHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.CURVE_DATA_XVALUE, cimCurveData.Xvalue));
				}
				if (cimCurveData.Y1valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_DATA_Y1VALUE, cimCurveData.Y1value));
				}
				if (cimCurveData.Y2valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_DATA_Y2VALUE, cimCurveData.Y2value));
				}
				if (cimCurveData.Y3valueHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CURVE_DATA_Y3VALUE, cimCurveData.Y3value));
				}
				if (cimCurveData.Curve != null)
				{
					long gid = importHelper.GetMappedGID(cimCurveData.Curve.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimCurveData.GetType().ToString()).Append(" rdfID = \"").Append(cimCurveData.ID);
						report.Report.Append("\" - Failed to set reference to Curve: rdfID \"").Append(cimCurveData.Curve.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CURVE_CURVEDATAS, gid));
				}
			}
		}

		public static void PopulateIrregularTimePointProperties(FTN.IrregularTimePoint cimIrregularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimIrregularTimePoint != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimIrregularTimePoint, rd);

				if (cimIrregularTimePoint.TimeHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.ITP_TIME, cimIrregularTimePoint.Time));
				}
				if (cimIrregularTimePoint.Value1HasValue)
				{
					rd.AddProperty(new Property(ModelCode.ITP_VALUE1, cimIrregularTimePoint.Value1));
				}
				if (cimIrregularTimePoint.Value2HasValue)
				{
					rd.AddProperty(new Property(ModelCode.ITP_VALUE2, cimIrregularTimePoint.Value2));
				}
				if (cimIrregularTimePoint.IntervalSchedule != null)
				{
					long gid = importHelper.GetMappedGID(cimIrregularTimePoint.IntervalSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Failed to set reference to IrregularIntervalSchedule: rdfID \"")
									 .Append(cimIrregularTimePoint.IntervalSchedule.ID)
									 .AppendLine("\" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ITP_IRRINTSCHED, gid));
				}

			}
		}

		public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimRegularTimePoint != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

				if (cimRegularTimePoint.SequenceNumberHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.RTP_SEQNUM, cimRegularTimePoint.SequenceNumber));
				}
				if (cimRegularTimePoint.Value1HasValue)
				{
					rd.AddProperty(new Property(ModelCode.RTP_VALUE1, cimRegularTimePoint.Value1));
				}
				if (cimRegularTimePoint.Value2HasValue)
				{
					rd.AddProperty(new Property(ModelCode.RTP_VALUE2, cimRegularTimePoint.Value2));
				}
				if (cimRegularTimePoint.IntervalSchedule != null)
				{
					long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Failed to set reference to RegularIntervalSchedule: rdfID \"")
									 .Append(cimRegularTimePoint.IntervalSchedule.ID)
									 .AppendLine("\" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.RTP_REGINTSCHED, gid));
				}

			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd);

				if (cimEquipment.AggregateHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
				}
				if (cimEquipment.NormallyInServiceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd);
			}
		}

		public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd)
		{
			if ((cimSwitch != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd);

				if (cimSwitch.NormalOpenHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));
				}
				if (cimSwitch.RatedCurrentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_RATEDCURRENT, cimSwitch.RatedCurrent));
				}
				if (cimSwitch.RetainedHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
				}
				if (cimSwitch.SwitchOnCountHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_ONCOUNT, cimSwitch.SwitchOnCount));
				}
				if (cimSwitch.SwitchOnDateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SWITCH_ONDATE, cimSwitch.SwitchOnDate));
				}
			}
		}

		public static void PopulateDisconnectorProperties(FTN.Disconnector cimDisconnector, ResourceDescription rd)
		{
			if ((cimDisconnector != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSwitchProperties(cimDisconnector, rd);
			}
		}

		public static void PopulateProtectedSwitchProperties(FTN.ProtectedSwitch cimProtectedSwitch, ResourceDescription rd)
		{
			if ((cimProtectedSwitch != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateSwitchProperties(cimProtectedSwitch, rd);
			}
		}

		public static void PopulateIrregularIntervalScheduleProperties(FTN.IrregularIntervalSchedule cimIrregularIntervalSchedule, ResourceDescription rd)
		{
			if ((cimIrregularIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimIrregularIntervalSchedule, rd);
			}
		}

		public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
		{
			if ((cimRegularIntervalSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateBasicIntervalScheduleProperties(cimRegularIntervalSchedule, rd);

				if (cimRegularIntervalSchedule.EndTimeHasValue) 
				{
					rd.AddProperty(new Property(ModelCode.RIS_END_TIME, cimRegularIntervalSchedule.EndTime));
				}
				if (cimRegularIntervalSchedule.TimeStepHasValue)
				{
					rd.AddProperty(new Property(ModelCode.RIS_TIME_STEP, cimRegularIntervalSchedule.TimeStep));
				}
			}
		}

		public static void PopulateOutageScheduleProperties(FTN.OutageSchedule cimOutageSchedule, ResourceDescription rd)
		{
			if ((cimOutageSchedule != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIrregularIntervalScheduleProperties(cimOutageSchedule, rd);
			}
		}




























		/*
		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

				if (cimPowerSystemResource.CustomTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PSR_CUSTOMTYPE, cimPowerSystemResource.CustomType));
				}
				if (cimPowerSystemResource.LocationHasValue)
				{
					long gid = importHelper.GetMappedGID(cimPowerSystemResource.Location.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimPowerSystemResource.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerSystemResource.ID);
						report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimPowerSystemResource.Location.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.PSR_LOCATION, gid));
				}
			}
		}

		public static void PopulateBaseVoltageProperties(FTN.BaseVoltage cimBaseVoltage, ResourceDescription rd)
		{
			if ((cimBaseVoltage != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBaseVoltage, rd);

				if (cimBaseVoltage.NominalVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE, cimBaseVoltage.NominalVoltage));
				}
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

				if (cimEquipment.PrivateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISPRIVATE, cimEquipment.Private));
				}
				if (cimEquipment.IsUndergroundHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISUNDERGROUND, cimEquipment.IsUnderground));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

				if (cimConductingEquipment.PhasesHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDEQ_PHASES, (short)GetDMSPhaseCode(cimConductingEquipment.Phases)));
				}
				if (cimConductingEquipment.RatedVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDEQ_RATEDVOLTAGE, cimConductingEquipment.RatedVoltage));
				}
				if (cimConductingEquipment.BaseVoltageHasValue)
				{
					long gid = importHelper.GetMappedGID(cimConductingEquipment.BaseVoltage.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimConductingEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimConductingEquipment.ID);
						report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimConductingEquipment.BaseVoltage.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CONDEQ_BASVOLTAGE, gid));
				}
			}
		}

		public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerTransformer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

				if (cimPowerTransformer.FunctionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)GetDMSTransformerFunctionKind(cimPowerTransformer.Function)));
				}
				if (cimPowerTransformer.AutotransformerHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_AUTO, cimPowerTransformer.Autotransformer));
				}
			}
		}

		public static void PopulateTransformerWindingProperties(FTN.TransformerWinding cimTransformerWinding, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimTransformerWinding != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTransformerWinding, rd, importHelper, report);

				if (cimTransformerWinding.ConnectionTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimTransformerWinding.ConnectionType)));
				}
				if (cimTransformerWinding.GroundedHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimTransformerWinding.Grounded));
				}
				if (cimTransformerWinding.RatedSHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimTransformerWinding.RatedS));
				}
				if (cimTransformerWinding.RatedUHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimTransformerWinding.RatedU));
				}
				if (cimTransformerWinding.WindingTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE, (short)GetDMSWindingType(cimTransformerWinding.WindingType)));
				}
				if (cimTransformerWinding.PhaseToGroundVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimTransformerWinding.PhaseToGroundVoltage));
				}
				if (cimTransformerWinding.PhaseToPhaseVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimTransformerWinding.PhaseToPhaseVoltage));
				}
				if (cimTransformerWinding.PowerTransformerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimTransformerWinding.PowerTransformer.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimTransformerWinding.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerWinding.ID);
						report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimTransformerWinding.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
				}
			}
		}

		
		#endregion Populate ResourceDescription

		#region Enums convert
		public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}

		public static TransformerFunction GetDMSTransformerFunctionKind(FTN.TransformerFunctionKind transformerFunction)
		{
			switch (transformerFunction)
			{
				case FTN.TransformerFunctionKind.voltageRegulator:
					return TransformerFunction.Voltreg;
				default:
					return TransformerFunction.Consumer;
			}
		}

		public static WindingType GetDMSWindingType(FTN.WindingType windingType)
		{
			switch (windingType)
			{
				case FTN.WindingType.primary:
					return WindingType.Primary;
				case FTN.WindingType.secondary:
					return WindingType.Secondary;
				case FTN.WindingType.tertiary:
					return WindingType.Tertiary;
				default:
					return WindingType.None;
			}
		}

		public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
		{
			switch (windingConnection)
			{
				case FTN.WindingConnection.D:
					return WindingConnection.D;
				case FTN.WindingConnection.I:
					return WindingConnection.I;
				case FTN.WindingConnection.Z:
					return WindingConnection.Z;
				case FTN.WindingConnection.Y:
					return WindingConnection.Y;
				default:
					return WindingConnection.Y;
			}
		}
		*/
		#endregion Enums convert
	}
}
