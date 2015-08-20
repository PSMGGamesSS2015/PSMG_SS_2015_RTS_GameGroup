using UnityEngine;

namespace Assets.Scripts.Controllers.Objects.Water
{
    public class Clickspawn : MonoBehaviour
    {
        public GameObject Brick;

        public void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var brick =
                (GameObject)
                    Instantiate(Brick, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)),
                        Brick.transform.rotation);

            brick.transform.Rotate(0, 0, Random.Range(0, 0));
            brick.transform.localScale = new Vector3(Random.Range(1f, 2f), 0.6f, 1)*0.4f;
            brick.GetComponent<Rigidbody2D>().mass = brick.transform.localScale.x*brick.transform.localScale.y*5f;

            Destroy(brick, 1.4f);
        }
    }
}