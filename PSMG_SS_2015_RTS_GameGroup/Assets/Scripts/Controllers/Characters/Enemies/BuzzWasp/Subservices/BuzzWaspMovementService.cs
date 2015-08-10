using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.BuzzWasp.Subservices
{
    public class BuzzWaspMovementService : MovingObject
    {
        private GameObject topMargin;
        private GameObject bottomMargin;

        private const float MovementSpeedDownwards = 0.9f;
        private const float MovementSpeedUpwards = 1f;

        public override void Start()
        {
            if (LevelManager.Instance == null)
            {
                topMargin = GameObject.FindGameObjectWithTag(TagReferences.TopMargin);
                bottomMargin = GameObject.FindGameObjectWithTag(TagReferences.BottomMargin);
            }
            else
            {
                topMargin = LevelManager.Instance.CurrentLevel.TopMargin;
                bottomMargin = LevelManager.Instance.CurrentLevel.BottomMargin;
            }
            
            MovementSpeed = CurrentDirection == Direction.Downwards ? MovementSpeedDownwards : MovementSpeedUpwards;
        }

        public override void FixedUpdate()
        {
            if (gameObject.transform.position.y > topMargin.transform.position.y - 2)
            {
                ChangeDirection(Direction.Downwards);
                MovementSpeed = MovementSpeedDownwards;
            }

            if (gameObject.transform.position.y < bottomMargin.transform.position.y + 2)
            {
                ChangeDirection(Direction.Upwards);
                MovementSpeed = MovementSpeedUpwards;
            }

            switch (CurrentDirection)
            {
                case Direction.Upwards:
                    MoveUpwards();
                    break;
                case Direction.Downwards:
                    MoveDownwards();
                    break;
            }
        }
    }
}