using System;
using UnityEngine;

namespace Gameplay.OrderSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class Location : MonoBehaviour
    {
        public Collider2D detectZone;
        
        public string locationName;


        private void Awake()
        {
            detectZone = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}