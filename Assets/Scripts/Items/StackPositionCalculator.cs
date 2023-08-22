using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Items
{
    public class StackPositionCalculator : MonoBehaviour
    {
        public Vector3 CalculatePosition(IEnumerable<IInventoryObject> collection, StackableItem item)
        {
            float totalHeight = 0f;

            foreach (StackableItem si in collection)
            {
                totalHeight += si.ItemHeight;
            }

            return new Vector3(0f, totalHeight + item.ItemHeight / 2f, 0f);
        }

        public void RecalculatePositions(IEnumerable<StackableItem> collection)
        {
            float totalHeight = 0f;

            foreach (StackableItem si in collection)
            {
                si.transform.localPosition = new Vector3(0f, totalHeight + si.ItemHeight / 2f, 0f);
                totalHeight += si.ItemHeight;
            }
        }
    }
}