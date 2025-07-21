using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;

namespace FTN.Services.NetworkModelService.DataModel.Curves
{
	public class Curve : IdentifiedObject
	{
		private CurveStyle curveStyle;
		private UnitMultiplier xMultiplier;
		private UnitSymbol xUnit;
		private UnitMultiplier y1Multiplier;
		private UnitSymbol y1Unit;
		private UnitMultiplier y2Multiplier;
		private UnitSymbol y2Unit;
		private UnitMultiplier y3Multiplier;
		private UnitSymbol y3Unit;

		public Curve(long globalId) : base(globalId) 
		{
		}

		public CurveStyle CurveStyle
		{
			get { return curveStyle; }
			set { curveStyle = value; }
		}

		public UnitMultiplier XMultiplier
		{
			get { return xMultiplier; }
			set { xMultiplier = value; }
		}

		public UnitSymbol XUnit
		{
			get { return xUnit; }
			set { xUnit = value; }
		}

		public UnitMultiplier Y1Multiplier
		{
			get { return y1Multiplier; }
			set { y1Multiplier = value; }
		}

		public UnitSymbol Y1Unit
		{
			get { return y1Unit; }
			set { y1Unit = value; }
		}

		public UnitMultiplier Y2Multiplier
		{
			get { return y2Multiplier; }
			set { y2Multiplier = value; }
		}

		public UnitSymbol Y2Unit
		{
			get { return y2Unit; }
			set { y2Unit = value; }
		}

		public UnitMultiplier Y3Multiplier
		{
			get { return y3Multiplier; }
			set { y3Multiplier = value; }
		}

		public UnitSymbol Y3Unit
		{
			get { return y3Unit; }
			set { y3Unit = value; }
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Curve c = (Curve)obj;
				return c.curveStyle == this.curveStyle &&
					   c.xMultiplier == this.xMultiplier &&
					   c.xUnit == this.xUnit &&
					   c.y1Multiplier == this.y1Multiplier &&
					   c.y1Unit == this.y1Unit &&
					   c.y2Multiplier == this.y2Multiplier &&
					   c.y2Unit == this.y2Unit &&
					   c.y3Multiplier == this.y3Multiplier &&
					   c.y3Unit == this.y3Unit;
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

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.CURVE_STYLE:
				case ModelCode.CURVE_X_MULTIPLIER:
				case ModelCode.CURVE_X_UNIT:
				case ModelCode.CURVE_Y1_MULTIPLIER:
				case ModelCode.CURVE_Y1_UNIT:
				case ModelCode.CURVE_Y2_MULTIPLIER:
				case ModelCode.CURVE_Y2_UNIT:
				case ModelCode.CURVE_Y3_MULTIPLIER:
				case ModelCode.CURVE_Y3_UNIT:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.CURVE_STYLE:
					prop.SetValue((short)curveStyle);
					break;

				case ModelCode.CURVE_X_MULTIPLIER:
					prop.SetValue((short)xMultiplier);
					break;

				case ModelCode.CURVE_X_UNIT:
					prop.SetValue((short)xUnit);
					break;

				case ModelCode.CURVE_Y1_MULTIPLIER:
					prop.SetValue((short)y1Multiplier);
					break;

				case ModelCode.CURVE_Y1_UNIT:
					prop.SetValue((short)y1Unit);
					break;

				case ModelCode.CURVE_Y2_MULTIPLIER:
					prop.SetValue((short)y2Multiplier);
					break;

				case ModelCode.CURVE_Y2_UNIT:
					prop.SetValue((short)y2Unit);
					break;

				case ModelCode.CURVE_Y3_MULTIPLIER:
					prop.SetValue((short)y3Multiplier);
					break;

				case ModelCode.CURVE_Y3_UNIT:
					prop.SetValue((short)y3Unit);
					break;

				default:
					base.GetProperty(prop);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.CURVE_STYLE:
					curveStyle = (CurveStyle)property.AsEnum();
					break;

				case ModelCode.CURVE_X_MULTIPLIER:
					xMultiplier = (UnitMultiplier)property.AsEnum();
					break;

				case ModelCode.CURVE_X_UNIT:
					xUnit = (UnitSymbol)property.AsEnum();
					break;

				case ModelCode.CURVE_Y1_MULTIPLIER:
					y1Multiplier = (UnitMultiplier)property.AsEnum();
					break;

				case ModelCode.CURVE_Y1_UNIT:
					y1Unit = (UnitSymbol)property.AsEnum();
					break;

				case ModelCode.CURVE_Y2_MULTIPLIER:
					y2Multiplier = (UnitMultiplier)property.AsEnum();
					break;

				case ModelCode.CURVE_Y2_UNIT:
					y2Unit = (UnitSymbol)property.AsEnum();
					break;

				case ModelCode.CURVE_Y3_MULTIPLIER:
					y3Multiplier = (UnitMultiplier)property.AsEnum();
					break;

				case ModelCode.CURVE_Y3_UNIT:
					y3Unit = (UnitSymbol)property.AsEnum();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}
	}
}
