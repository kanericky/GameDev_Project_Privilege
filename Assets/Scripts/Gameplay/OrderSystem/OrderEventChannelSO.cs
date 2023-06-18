using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.OrderSystem
{
    [CreateAssetMenu(menuName = "Gameplay/Order Event Channel")]
    public class OrderEventChannelSO : ScriptableObject
    {
        public UnityAction<Order> OnOrderSpawnRequested;

        public void RaiseEvent(Order order)
        {
            if (OnOrderSpawnRequested != null)
            {
                OnOrderSpawnRequested.Invoke(order);
            }
            else
            {
                Debug.LogWarning("An order was requested, but nothing picked it up." + 
                                 "Potential issue - NO ORDER MANAGER" + 
                                 "Potential issue - ORDER MANAGER HAS NOT SUBSCRIBED THE EVENT");
            }
        }
    }
}