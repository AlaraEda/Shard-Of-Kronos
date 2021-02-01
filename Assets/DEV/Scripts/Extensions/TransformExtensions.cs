using UnityEngine;

namespace DEV.Scripts.Extensions
{
    public struct TargetLeftRight
    {
        public enum LeftRightEnum
        {
            Left,
            Right,
        }

        public TargetLeftRight(LeftRightEnum dir, float dist)
        {
            TargetDirection = dir;
            AngleDistance = dist;
        }

        public LeftRightEnum TargetDirection { get; set; }
        public float AngleDistance { get; set; }
    }

    public static class TransformExtensions
    {
        public static TargetLeftRight TargetLeftOrRight(this Transform self, Vector3 target)
        {
            Vector3 targetInversePoint = self.InverseTransformPoint(target);
            TargetLeftRight.LeftRightEnum dir = targetInversePoint.x < 0
                ? TargetLeftRight.LeftRightEnum.Left
                : TargetLeftRight.LeftRightEnum.Right;
            return new TargetLeftRight(dir, Mathf.Abs(targetInversePoint.x));
        }
    }
}