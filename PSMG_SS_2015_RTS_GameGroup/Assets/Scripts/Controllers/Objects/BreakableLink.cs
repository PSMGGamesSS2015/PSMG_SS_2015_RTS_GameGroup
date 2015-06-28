using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class BreakableLink : MonoBehaviour
    {

        public void Break()
        {
            var hingeJoints = GetComponents<HingeJoint2D>().ToList();
            hingeJoints.ForEach(Destroy);
        }

    }
}
