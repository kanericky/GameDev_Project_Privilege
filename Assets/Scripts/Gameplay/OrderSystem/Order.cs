using System;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.OrderSystem
{
    public class Order : MonoBehaviour
    {
        [Header("Debug - Order Data")]
        [SerializeField] private Departure departure;
        [SerializeField] private Destination destination;

        [SerializeField] private float timeToExpire;
        [SerializeField] private float timeToFinish;

        [Header("Debug - Runtime Status")]
        [SerializeField] private bool isOrderPickedUp;
        [SerializeField] private bool isOrderExpired;
        [SerializeField] private bool isOrderFinished;
        [SerializeField] private bool isOrderFailed;
        
        
        public void SetupOrder(OrderData orderData)
        {
            departure = orderData.departure;
            destination = orderData.destination;

            timeToExpire = orderData.timeToExpire;
            timeToFinish = orderData.timeToFinish;

            isOrderPickedUp = false;
            isOrderFinished = false;
        }

        private void Start()
        {
            StartCoroutine(ExpireCountDown());
        }

        // Deal with the expire
        IEnumerator ExpireCountDown()
        {
            while (true)
            {
                // The order has been pickedUpByPlayer
                if (isOrderPickedUp)
                {
                    Debug.Log($"Order: {name} is picked up");
                    HandleOrderPickedUp();
                    yield break;
                }

                timeToExpire -= 0.1f;

                if (timeToExpire <= 0)
                {
                    isOrderExpired = true;
                    HandleOrderExpired();
                    yield break;
                }

                yield return new WaitForSeconds(.1f);
            }

        }

        private void HandleOrderExpired()
        {
            Debug.Log($"Order: {name} has expired");
            Destroy(gameObject);
        }

        private void HandleOrderPickedUp()
        {
            StartCoroutine(FinishCountDown());
        }

        // Deal with the order finishing
        IEnumerator FinishCountDown()
        {
            while (true)
            {
                // Order has been completed
                if (isOrderFinished)
                {
                    isOrderFinished = true;
                    HandleOrderFinished();
                    yield break;
                }

                timeToFinish -= 0.1f;

                if (timeToFinish <= 0)
                {
                    isOrderFailed = true;
                    HandleOrderFailed();
                }

                yield return new WaitForSeconds(.1f);
            }
        }

        void HandleOrderFailed()
        {
            Debug.Log($"$Order: {name} is failed");
            Destroy(gameObject);
        }

        void HandleOrderFinished()
        {
            Debug.Log($"Order: {name} is successfully finished");
            Destroy(gameObject);
        }
        
    }
}
