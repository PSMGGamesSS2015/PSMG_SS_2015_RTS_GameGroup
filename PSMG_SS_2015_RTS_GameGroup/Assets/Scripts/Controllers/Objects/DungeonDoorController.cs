using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class DungeonDoorController : MonoBehaviour
    {
        private SpriteRenderer doorOpenSprite;
        private SpriteRenderer doorClosedSprite;
        private AudioHelper audioHelper;

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

            doorOpenSprite.enabled = false;

            State = DoorState.Closed;

            audioHelper = gameObject.AddComponent<AudioHelper>();
        }

        public void Open()
        {
            GetComponentInChildren<Collider2D>().enabled = false;

            doorClosedSprite.enabled = false;
            doorOpenSprite.enabled = true;

            audioHelper.Play(SoundReferences.DoorOpen);

            State = DoorState.Open;
        }

        public void Close()
        {
            GetComponentInChildren<Collider2D>().enabled = true;

            doorOpenSprite.enabled = true;
            doorClosedSprite.enabled = false;

            audioHelper.Play(SoundReferences.DoorOpen);

            State = DoorState.Closed;
        }

    }
}