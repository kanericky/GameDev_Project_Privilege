using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.OrderSystem
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private List<Location> departureLocations;
        [SerializeField] private List<Location> destinationLocations;

        [Header("Order Event Channel Listener")] 
        [SerializeField] private OrderEventChannelSO orderSpawnEventChannel;

        private void Awake()
        {
            
        }


        // Generate new order UI
        public Order GenerateOrder(OrderData orderData)
        {
            return null;
        }
        
        // Generate new order data
        public OrderData GenerateOrderData(Departure departure, Destination destination)
        {
            
            return null;
        }

        // Get random location from list
        public Location GetRandomLocationFromList(List<Location> locations)
        {
            int numOfLocations = locations.Count;
            int randomIndex = Random.Range(0, numOfLocations);
            Location randomSelectedLocation = locations[randomIndex];

            return randomSelectedLocation;
        }
    }
}
