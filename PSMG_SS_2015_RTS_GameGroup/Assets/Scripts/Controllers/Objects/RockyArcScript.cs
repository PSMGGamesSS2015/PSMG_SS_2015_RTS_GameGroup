using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class RockyArcScript : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {
        private TriggerCollider2D enterArea;
        private BoxCollider2D boxCollider2D;
        private List<SpriteRenderer> childRenderers;
        private List<Rigidbody2D> childRigidbody2Ds;

        public void Awake()
        {
            enterArea = GetComponentInChildren<TriggerCollider2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            if (enterArea != null)
            {
                enterArea.RegisterListener(this);
            }

            childRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            childRigidbody2Ds = GetComponentsInChildren<Rigidbody2D>().ToList();

            childRenderers
                .Where(cr => cr.gameObject.tag == TagReferences.FallingRock)
                .ToList()
                .ForEach(cr => cr.enabled = false);
        }

        public void Detonate()
        {
            DestroyLadders();
            // hide current sprite renderer
           childRenderers.ForEach(cr => cr.enabled = false);
            // disable collider
            GetComponent<Collider2D>().enabled = false;
            // display rocks and stones
            childRenderers
                .Where(cr => cr.gameObject.tag == TagReferences.FallingRock)
                .ToList()
                .ForEach(cr => cr.enabled = true);
            // set rigidbody 2ds kinematic
            childRigidbody2Ds
                .Where(cr => cr.gameObject.tag == TagReferences.FallingRock)
                .ToList()
                .ForEach(cr => cr.isKinematic = false);
        }

        private void DestroyLadders()
        {
            foreach(var renderer in GetComponentsInChildren<SpriteRenderer>().ToList())
            {
                if (renderer.gameObject.tag == TagReferences.VerticalLadder)
                {
                    Destroy(renderer.gameObject);
                }
            }

        }

        

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != enterArea.GetInstanceID()) return;
            if (collider.isTrigger) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;

            Physics2D.IgnoreCollision(boxCollider2D, collider, true);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != enterArea.GetInstanceID()) return;
            if (collider.isTrigger) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;

            Physics2D.IgnoreCollision(boxCollider2D, collider, false);
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            // TODO
        }
    }
}