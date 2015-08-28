using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class DrawBridgeController : MonoBehaviour
    {
        private const float LimitLowerAngleBeginning = 0f;
        private const float LimitUpperAngleBeginning = 0f;

        private const float LimitLowerAngleMiddle = -5f;
        private const float LimitUpperAngleMiddle = -90f;

        private const float LimitLowerAngleEnd = -5f;
        private const float LimitUpperAngleEnd = -180f;

        private new HingeJoint2D hingeJoint;

        public void Awake()
        {
            hingeJoint = GetComponent<HingeJoint2D>();

            var limits = hingeJoint.limits;
            limits.min = LimitLowerAngleBeginning;
            limits.max = LimitUpperAngleBeginning;

            hingeJoint.limits = limits;
        }

        public void Lower()
        {
            var limits = hingeJoint.limits;
            limits.min = LimitLowerAngleMiddle;
            limits.max = LimitUpperAngleMiddle;

            hingeJoint.limits = limits;
        }

        public void Drop()
        {
            var limits = hingeJoint.limits;
            limits.min = LimitLowerAngleEnd;
            limits.max = LimitUpperAngleEnd;

            hingeJoint.limits = limits;
        }
    }
}