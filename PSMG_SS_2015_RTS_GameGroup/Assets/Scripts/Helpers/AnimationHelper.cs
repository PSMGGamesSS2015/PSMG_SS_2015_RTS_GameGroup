using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class AnimationHelper : MonoBehaviour
    {
        public Animator Animator;

        public virtual void Awake()
        {
            Animator = gameObject.GetComponent<Animator>();
        }

        public virtual void Play(string clip)
        {
            Animator.Play(clip);
        }
    }
}