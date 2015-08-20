using UnityEngine;

namespace Assets.Scripts.Controllers.Objects.Water
{
    public class WaterDetector : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                transform.parent.GetComponent<Water>()
                    .Splash(transform.position.x, collider,
                        collider.GetComponent<Rigidbody2D>().velocity.y*collider.GetComponent<Rigidbody2D>().mass/40f);
            }
        }
    }
}