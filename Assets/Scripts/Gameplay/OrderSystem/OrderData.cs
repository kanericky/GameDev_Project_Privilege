using System;
using UnityEngine;

namespace Gameplay.OrderSystem
{
    [Serializable]
    public class OrderData
    {
        public Departure departure;
        public Destination destination;

        public float timeToExpire;
        public float timeToFinish;
    }
}