using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimShaker.Logic
{
    internal class ChassisDefinition : VehiclePlatform
    {
        public const int LF = 0;
        public const int RF = 1;
        public const int LR = 2;
        public const int RR = 3;

        private float _leftX;
        private float _rightX;
        private float _frontY;
        private float _rearY;

        private float _leftXToCGXDistance;

        private float _leftSideWeightPercent;
        private float _leftSideWeight;
        private float _rightSideWeightPercent;
        private float _rightSideWeight;

        private float _frontYToCGYDistance;

        private float _frontWeightPercent;
        private float _frontWeight;
        private float _rearWeightPercent;
        private float _rearWeight;

        private float _lfWeight;
        private float _rfWeight;
        private float _lrWeight;
        private float _rrWeight;

        private float _crossWeightPercent;
        private float _crossWeight;

        /// <summary>
        /// Distance in inches between front and rear tire contact patches
        /// </summary>
        public float Wheelbase { get; set; }
        /// <summary>
        /// Distance in inches between left and right tire contact patches
        /// </summary>
        public float TrackWidth { get; set; }
        /// <summary>
        /// Static weight of vehicle
        /// </summary>
        public float Weight { get; set; }
        /// <summary>
        /// Center of gravity
        /// </summary>
        public Point3D CG { get; set; }
        /// <summary>
        /// Suspension corner models
        /// </summary>
        public SuspensionCorner LFSuspension { get; set; }
        public SuspensionCorner RFSuspension { get; set; }
        public SuspensionCorner LRSuspension { get; set; }
        public SuspensionCorner RRSuspension { get; set; }

        public ChassisDefinition(float staticWeight, Point3D cg, IList<Point3D> contactPoints) : base()
        {
            LFContactPatch = contactPoints[LF];
            RFContactPatch = contactPoints[RF];
            LRContactPatch = contactPoints[LR];
            RRContactPatch = contactPoints[RR];
            this.Weight = staticWeight;
            this.CG = cg;
            Evaluate();
            LFSuspension = new SuspensionCorner(_lfWeight);
            RFSuspension = new SuspensionCorner(_rfWeight);
            LRSuspension = new SuspensionCorner(_lrWeight);
            RRSuspension = new SuspensionCorner(_rrWeight);
        }

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

        public void EvaluateChange(float deltaWeight)
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

        public string Report()
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

            sb.Append("_______________________________\r\n");
            return sb.ToString();
        }
    }
}
