using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class IntroController : MonoBehaviour
    {
        private List<TextLineController> textLineControllers;

        public void Awake()
        {
            textLineControllers = GetComponentsInChildren<TextLineController>().ToList();
        }

        public void FixedUpdate()
        {
            textLineControllers.ForEach(Move);
        }

        private void Move(TextLineController textLineController)
        {
            // TODO
        }
    }
}