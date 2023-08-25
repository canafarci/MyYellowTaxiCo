using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaxiGame.Items
{
    public class StackPositionCalculator : MonoBehaviour
    {
        public Vector3 CalculatePosition(IEnumerable<IInventoryObject> collection, StackableItem item)
        {
            float totalHeight = 0f;

            foreach (StackableItem si in collection.Cast<StackableItem>())
            {
                totalHeight += si.ItemHeight;
            }

            return new Vector3(0f, totalHeight + item.ItemHeight / 2f, 0f);
        }

        public void RecalculatePositions(IEnumerable<IInventoryObject> collection)
        {
            float totalHeight = 0f;

            foreach (StackableItem si in collection.Cast<StackableItem>())
            {
                si.transform.localPosition = new Vector3(0f, totalHeight + si.ItemHeight / 2f, 0f);
                totalHeight += si.ItemHeight;
            }
        }
    }
}