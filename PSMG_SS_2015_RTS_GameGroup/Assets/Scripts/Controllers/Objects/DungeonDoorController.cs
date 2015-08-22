using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class DungeonDoorController : MonoBehaviour
    {
        private SpriteRenderer doorOpenSprite;
        private SpriteRenderer doorClosedSprite;

        private const string DoorOpenName = "OpenDoor";
        private const string DoorClosedName = "ClosedDoor";

        public DoorState State { get; private set; }

        public enum DoorState
        {
            Open,
            Closed
        }

        public void Awake()
        {
            doorOpenSprite = GetComponentsInChildren<SpriteRenderer>().ToList().First(sr => sr.name == DoorOpenName);
            doorClosedSprite = GetComponentsInChildren<SpriteRenderer>().ToList().First(sr => sr.name == DoorClosedName);

            State = DoorState.Closed;
        }

        public void Open()
        {
            GetComponent<Collider2D>().enabled = false;

            doorClosedSprite.enabled = false;
            doorOpenSprite.enabled = true;

            State = DoorState.Open;
        }

        public void Close()
        {
            GetComponent<Collider2D>().enabled = true;

            doorOpenSprite.enabled = true;
            doorClosedSprite.enabled = false;

            State = DoorState.Closed;
        }

    }
}