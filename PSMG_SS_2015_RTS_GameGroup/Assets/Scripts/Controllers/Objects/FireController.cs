using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FireController : MonoBehaviour
    {
        public int OrderInSortingLayer;
        public string SortingLayerName;

        private List<SpriteRenderer> redFlameTongues;
        private List<SpriteRenderer> orangeFlameTongues;
        private List<SpriteRenderer> yellowFlameTongues;

        public void Awake()
        {
            if (SortingLayerName == "") return;

            redFlameTongues =
                GetComponentsInChildren<SpriteRenderer>()
                    .Where(sr => sr.tag == TagReferences.RedFlameTongue)
                    .ToList();

            orangeFlameTongues =
               GetComponentsInChildren<SpriteRenderer>()
                   .Where(sr => sr.tag == TagReferences.OrangeFlameTongue)
                   .ToList();

            yellowFlameTongues =
               GetComponentsInChildren<SpriteRenderer>()
                   .Where(sr => sr.tag == TagReferences.YellowFlameTongue)
                   .ToList();
        }

        public void Start()
        {
            if (SortingLayerName == "") return;

            foreach (var ft in redFlameTongues)
            {
                ft.sortingLayerName = SortingLayerName;
                ft.sortingOrder = OrderInSortingLayer;
            }

            foreach (var ft in orangeFlameTongues)
            {
                ft.sortingLayerName = SortingLayerName;
                ft.sortingOrder = OrderInSortingLayer + 1;
            }

            foreach (var ft in yellowFlameTongues)
            {
                ft.sortingLayerName = SortingLayerName;
                ft.sortingOrder = OrderInSortingLayer + 2;
            }
        }

    }
}