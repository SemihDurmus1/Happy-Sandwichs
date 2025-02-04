using Sandwich;
using System.Collections.Generic;
using UnityEngine;

namespace Customer.Order
{
    public class CustomerOrder
    {
        /// <summary>
        /// Sandwiches in a CustomerOrder
        /// </summary>
        [SerializeField] private List<SandwichItem> sandwichOrderList = new();

        /// <summary>
        /// (Property) Sandwiches in a CustomerOrder
        /// </summary>
        public List<SandwichItem> SandwichOrderList => sandwichOrderList;
    }
}