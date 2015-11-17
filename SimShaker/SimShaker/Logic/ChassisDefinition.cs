using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace SimShaker.Logic
{
    internal class ChassisDefinition : VehiclePlatform
    {
        #region consts
        public const int LF = 0;
        public const int RF = 1;
        public const int LR = 2;
        public const int RR = 3;
        #endregion

        #region fields
        private Double _leftX;
        private Double _rightX;
        private Double _frontY;
        private Double _rearY;

        private Double _leftXToCGXDistance;

        private Double _leftSideWeightPercent;
        private Double _leftSideWeight;
        private Double _rightSideWeightPercent;
        private Double _rightSideWeight;

        private Double _frontYToCGYDistance;

        private Double _frontWeightPercent;
        private Double _frontWeight;
        private Double _rearWeightPercent;
        private Double _rearWeight;

        private Double _lfWeight;
        private Double _rfWeight;
        private Double _lrWeight;
        private Double _rrWeight;

        private Double _crossWeightPercent;
        private Double _crossWeight;
        #endregion

        #region properties
        /// <summary>
        /// Distance in inches between front and rear tire contact patches
        /// </summary>
        public Double Wheelbase { get; set; }
        /// <summary>
        /// Distance in inches between left and right tire contact patches
        /// </summary>
        public Double TrackWidth { get; set; }
        /// <summary>
        /// Static weight of vehicle
        /// </summary>
        public Double Weight { get; set; }
        /// <summary>
        /// Center of gravity
        /// </summary>
        public Point3D CG { get; set; }
        /// <summary>
        /// Front roll center
        /// </summary>
        public Point3D RCf { get; set; }
        /// <summary>
        /// Rear roll center
        /// </summary>
        public Point3D RCr { get; set; }
        /// <summary>
        /// Suspension corner models
        /// </summary>
        public SuspensionCorner LFSuspension { get; set; }
        public SuspensionCorner RFSuspension { get; set; }
        public SuspensionCorner LRSuspension { get; set; }
        public SuspensionCorner RRSuspension { get; set; }

        public Double ARBRate { get { return 0; } }
        #endregion

        #region ctor
        public ChassisDefinition(Double staticWeight, Point3D cg, IList<Point3D> contactPoints) : base()
        {
           

            LFContactPatch = contactPoints[LF];
            RFContactPatch = contactPoints[RF];
            LRContactPatch = contactPoints[LR];
            RRContactPatch = contactPoints[RR];

            this.Weight = staticWeight;
            this.CG = cg;

            Evaluate();

            LFSuspension = new SuspensionCorner(_lfWeight, 500);
            RFSuspension = new SuspensionCorner(_rfWeight, 500);
            LRSuspension = new SuspensionCorner(_lrWeight, 200);
            RRSuspension = new SuspensionCorner(_rrWeight, 200);

            Double halfWheelbase = RFContactPatch.X;
            Double halfTrack = RFContactPatch.Y * .5F;

            LFSuspension = new SuspensionCorner(_lfWeight, 400);
            LFSuspension.Spring.UpperMount = new Point3D(-24, halfWheelbase, 10);
            RFSuspension = new SuspensionCorner(_rfWeight, 500);
            RFSuspension.Spring.UpperMount = new Point3D(24, halfWheelbase, 10);
            LRSuspension = new SuspensionCorner(_lrWeight, 200);
            LRSuspension.Spring.UpperMount = new Point3D(-24, -halfWheelbase, 10);
            RRSuspension = new SuspensionCorner(_rrWeight, 200);
            RRSuspension.Spring.UpperMount = new Point3D(24, -halfWheelbase, 10);
        }
        public ChassisDefinition(Double staticWeight, Point3D cg, Point3D rcF, Point3D rcR, Double wheelbase, Double track) : base()
        {
            Double halfWheelbase = wheelbase * .5F;
            Double halfTrack = track * .5F;

            LFContactPatch = new Point3D(-halfTrack, halfWheelbase, 0);
            RFContactPatch = new Point3D(halfTrack, halfWheelbase, 0);
            LRContactPatch = new Point3D(-halfTrack, -halfWheelbase, 0);
            RRContactPatch = new Point3D(halfTrack, -halfWheelbase, 0);

            this.Weight = staticWeight;
            this.CG = cg;
            this.RCf = rcF;
            this.RCr = rcR;

            Evaluate();

            LFSuspension = new SuspensionCorner(_lfWeight, 400);
            LFSuspension.Spring.UpperMount = new Point3D(-24, halfWheelbase, 10);
            RFSuspension = new SuspensionCorner(_rfWeight, 500);
            RFSuspension.Spring.UpperMount = new Point3D(24, halfWheelbase, 10);
            LRSuspension = new SuspensionCorner(_lrWeight, 200);
            LRSuspension.Spring.UpperMount = new Point3D(-24, -halfWheelbase, 10);
            RRSuspension = new SuspensionCorner(_rrWeight, 200);
            RRSuspension.Spring.UpperMount = new Point3D(24, -halfWheelbase, 10);
        }
        #endregion

        #region public evaluate
        /// <summary>
        /// Evaluates property values based on VehiclePlatform properties.
        /// </summary>
        public void Evaluate()
        {
            _leftX = ((LFContactPatch.X + LRContactPatch.X) / 2);
            _rightX = ((RFContactPatch.X + RRContactPatch.X) / 2);
            TrackWidth = Math.Abs(_rightX - _leftX);
            _leftXToCGXDistance = CG.X - _leftX;
            _leftSideWeightPercent = 1.0F - (_leftXToCGXDistance / TrackWidth);
            _rightSideWeightPercent = 1.0F - _leftSideWeightPercent;
            _leftSideWeight = Weight * _leftSideWeightPercent;
            _rightSideWeight = Weight * _rightSideWeightPercent;

            _frontY = ((LFContactPatch.Y + RFContactPatch.Y) / 2);
            _rearY = ((LRContactPatch.Y + RRContactPatch.Y) / 2);
            Wheelbase = Math.Abs(_rearY - _frontY);
            _frontYToCGYDistance = Math.Abs(_frontY - CG.Y);
            _frontWeightPercent = 1.0F - (_frontYToCGYDistance / Wheelbase);
            _rearWeightPercent = 1.0F - _frontWeightPercent;
            _frontWeight = Weight * _frontWeightPercent;
            _rearWeight = Weight * _rearWeightPercent;

            _lfWeight = _frontWeight * _leftSideWeightPercent;
            _rfWeight = _frontWeight * _rightSideWeightPercent;
            _lrWeight = _rearWeight * _leftSideWeightPercent;
            _rrWeight = _rearWeight * _rightSideWeightPercent;

            _crossWeightPercent = (_rfWeight + _lrWeight) / Weight;
            _crossWeight = _crossWeightPercent * Weight;
        }
        /// <summary>
        /// Applies a vertical load to the chasses
        /// </summary>
        /// <param name="deltaWeight"></param>
        public void EvaluateChange(Double deltaWeight)
        {
            Evaluate();
            // evaluate a change in weight

            var lfDelta = deltaWeight * _frontWeightPercent * _leftSideWeightPercent;
            var rfDelta = deltaWeight * _frontWeightPercent * _rightSideWeightPercent;
            var lrDelta = deltaWeight * _rearWeightPercent * _leftSideWeightPercent;
            var rrDelta = deltaWeight * _rearWeightPercent * _rightSideWeightPercent;

            LFSuspension.Load += lfDelta;
            RFSuspension.Load += rfDelta;
            LRSuspension.Load += lrDelta;
            RRSuspension.Load += rrDelta;
        }
        /// <summary>
        /// Applies a force vector to the chassis
        /// </summary>
        /// <param name="vector"></param>
        public void EvaluateChange(Vector3D vector)
        {
            //var SMf = 1465;
            //var SMr = 855;
            //var MAf = 20;
            //var MAr = 17;

            //var OMf = OverturningMoment(SMf, .8, MAf) / 12; //  lbsft/degree
            //var OMr = OverturningMoment(SMr, .8, MAr) / 12; //  lbsft/degree

            //var Ff = OMf / (TrackWidth / 2);
            //var Fr = OMr / (TrackWidth / 2);

            //var Rollf = Ff / (LFSuspension.WheelRate + RFSuspension.WheelRate); // degrees
            //var Rollr = Fr / (LRSuspension.WheelRate + RRSuspension.WheelRate); // degrees

            var latG = vector.X;
            var wtLatPercent = LatWeightXferPercent(latG);
            var wtLat = wtLatPercent * Weight;

            var frontRS = FrontRollStiffness();
            var rearRS = RearRollStiffness();
            var totalRS = frontRS + rearRS;
            var frontRSPercent = frontRS / totalRS;
            var rearRSPercent = rearRS / totalRS;

            var wtXferF = frontRSPercent * wtLat;
            var wtXferR = rearRSPercent * wtLat;

            LFSuspension.Load -= wtXferF;
            RFSuspension.Load += wtXferF;

            LRSuspension.Load -= wtXferR;
            RRSuspension.Load += wtXferR;

            Evaluate();
            //Console.WriteLine("frontRS " + frontRS.ToString());
            //Console.WriteLine("rearRS " + rearRS.ToString());
            //Console.WriteLine("totalRS " + totalRS.ToString());
            //Console.WriteLine("frontRS % " + frontRSPercent.ToString());
            //Console.WriteLine("rearRS % " + rearRSPercent.ToString());
        }
        #endregion

        #region public overrides
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("_______________________________\r\n");
            sb.AppendFormat("LF Static Height: {0:#0.00} in\r\n", LFSuspension.StaticHeight);
            sb.AppendFormat("RF Static Height: {0:#0.00} in\r\n", RFSuspension.StaticHeight);
            sb.AppendFormat("LR Static Height: {0:#0.00} in\r\n", LRSuspension.StaticHeight);
            sb.AppendFormat("RR Static Height: {0:#0.00} in\r\n", RRSuspension.StaticHeight);

            sb.AppendFormat("LF Static Weight: {0} lbs\r\n", LFSuspension.StaticWeight);
            sb.AppendFormat("RF Static Weight: {0} lbs\r\n", RFSuspension.StaticWeight);
            sb.AppendFormat("LR Static Weight: {0} lbs\r\n", LRSuspension.StaticWeight);
            sb.AppendFormat("RR Static Weight: {0} lbs\r\n", RRSuspension.StaticWeight);

            sb.AppendFormat("Left Weight %: {0:P}\r\n", _leftSideWeightPercent);
            sb.AppendFormat("Right Weight %: {0:P}\r\n", _rightSideWeightPercent);
            sb.AppendFormat("Front Weight %: {0:P}\r\n", _frontWeightPercent);
            sb.AppendFormat("Rear Weight %: {0:P}\r\n", _rearWeightPercent);
            sb.AppendFormat("Cross Weight %: {0:P}\r\n", _crossWeightPercent);

            sb.AppendFormat("Left Weight: {0} lbs\r\n", _leftSideWeight);
            sb.AppendFormat("Right Weight: {0} lbs\r\n", _rightSideWeight);
            sb.AppendFormat("Front Weight: {0} lbs\r\n", _frontWeight);
            sb.AppendFormat("Rear Weight: {0} lbs\r\n", _rearWeight);
            sb.AppendFormat("Cross Weight: {0} lbs\r\n", _crossWeight);
            sb.AppendFormat("Total Weight: {0} lbs\r\n", Weight);

            sb.AppendFormat("LF Load: {0} lbs\r\n", LFSuspension.Load);
            sb.AppendFormat("RF Load: {0} lbs\r\n", RFSuspension.Load);
            sb.AppendFormat("LR Load: {0} lbs\r\n", LRSuspension.Load);
            sb.AppendFormat("RR Load: {0} lbs\r\n", RRSuspension.Load);

            sb.AppendFormat("Total Load: {0} lbs\r\n", RRSuspension.Load + LRSuspension.Load + RFSuspension.Load + LFSuspension.Load);

            sb.AppendFormat("LF Dynamic Height: {0:#0.00} in\r\n", LFSuspension.DynamicHeight);
            sb.AppendFormat("RF Dynamic Height: {0:#0.00} in\r\n", RFSuspension.DynamicHeight);
            sb.AppendFormat("LR Dynamic Height: {0:#0.00} in\r\n", LRSuspension.DynamicHeight);
            sb.AppendFormat("RR Dynamic Height: {0:#0.00} in\r\n", RRSuspension.DynamicHeight);
            sb.AppendLine();
            var frontRS = FrontRollStiffness();
            var rearRS = RearRollStiffness();
            var totalRS = frontRS + rearRS;
            var frontRSPercent = frontRS / totalRS;
            var rearRSPercent = rearRS / totalRS;
            sb.AppendLine("F Roll Stiffness " + frontRS.ToString() + "\r\n");
            sb.AppendLine("R Roll Stiffness " + rearRS.ToString() + "\r\n");
            sb.AppendLine("Total Roll Stiffness " + totalRS.ToString() + "\r\n");
            sb.AppendLine("F Roll Stiffness % " + frontRSPercent.ToString() + "\r\n");
            sb.AppendLine("R Roll Stiffness % " + rearRSPercent.ToString() + "\r\n");
          
            sb.Append("_______________________________\r\n");
            return sb.ToString();
        }
        #endregion

        #region protected
        protected Double FrontSpringSpan
        {
            get
            {
                return Common.Distance(LFSuspension.Spring.UpperMount, RFSuspension.Spring.UpperMount);
            }
        }
        protected Double RearSpringSpan
        {
            get
            {
                return Common.Distance(LRSuspension.Spring.UpperMount, RRSuspension.Spring.UpperMount);
            }
        }
        /// <summary>
        /// Returns roll stiffness in ft/lbs per degree
        /// </summary>
        /// <returns></returns>
        protected Double FrontRollStiffness()
        {
            return RollStiffness(LFSuspension.Spring.InstalledRate, RFSuspension.Spring.InstalledRate, FrontSpringSpan);
        }
        /// <summary>
        /// Returns roll stiffness in ft/lbs per degree
        /// </summary>
        /// <returns></returns>
        protected Double RearRollStiffness()
        {
            return RollStiffness(LRSuspension.Spring.InstalledRate, RRSuspension.Spring.InstalledRate, RearSpringSpan);
        }
        /// <summary>
        /// Returns roll stiffness in ft/lbs per degree
        /// </summary>
        /// <returns></returns>
        protected Double RollStiffness(Double srLeft, Double srRight, Double sSpan)
        {
            var springCenter = SpringCenter(srLeft, srRight, sSpan);
            var halfSpringSpan = sSpan / 2;
            return (
                    (srLeft * (halfSpringSpan + springCenter) * (halfSpringSpan + springCenter)) +
                    (srRight * (halfSpringSpan - springCenter) * (halfSpringSpan - springCenter))
                   ) / 688;
        }
        protected Double SpringCenter(Double leftSpringRate, Double rightSpringRate, Double springSpan)
        {
            return ((leftSpringRate - rightSpringRate) / (leftSpringRate + rightSpringRate)) * (springSpan / 2);
        }
        protected Double ChassisRollStiffnessR()
        {
            return ((int)((LRSuspension.WheelRate + RRSuspension.WheelRate) * TrackWidth) ^ 2) / 1375;
        }
        protected Double ChassisRollStiffness(Double wheelRateTotal, Double track)
        {
            return ((int)(wheelRateTotal * track) ^ 2) / 1375;
        }
        protected Double MomentArmF()
        {
            return Common.Distance(new Double[] { RCf.X, RCf.Z }, new Double[] { CG.X, CG.Z });
        }
        protected Double MomentArmR()
        {
            return Common.Distance(new Double[] { RCr.X, RCr.Z }, new Double[] { CG.X, CG.Z });
        }
        protected Double LonWeightXferPercent(Double accellerationG)
        {
            return (CG.Z * accellerationG) / Wheelbase;
        }
        protected Double LatWeightXferPercent(Double accellerationG)
        {
            return (CG.Z * accellerationG) / TrackWidth;
        }
        protected Double OverturningMomentF(Double mass, Double accellerationG)
        {
            return (mass) * accellerationG * MomentArmF();
        }
        protected Double OverturningMomentR(Double mass, Double accellerationG)
        {
            return (mass) * accellerationG * MomentArmR();
        }
        protected Double OverturningMoment(Double mass, Double accellerationG, Double momentArmLength)
        {
            return (mass) * accellerationG * momentArmLength;
        }
        protected Double ForceAtWheelPlane(Double overturningMoment)
        {
            return overturningMoment / (TrackWidth * .5);
        }
        protected double DeflectionAtWheelF(Double force)
        {
            return force / (LFSuspension.WheelRate + RFSuspension.WheelRate + ARBRate);
        }
        protected double DeflectionAtWheelR(Double force)
        {
            return force / (LRSuspension.WheelRate + RRSuspension.WheelRate + ARBRate);
        }
        protected Double RollAngle(Double wheelDeflectionSum)
        {
            return wheelDeflectionSum / TrackWidth;
        }
        #endregion
    }
}
