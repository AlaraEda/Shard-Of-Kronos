using System;
using DEV.Scripts.Inventory;
using DEV.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DEV.Scripts.UI
{
    public class InventoryItemsUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemUiPrefab;
        [SerializeField] private GameObject gridParent;

        private void Start()
        {
            SceneContext.Instance.playerInventory.OnItemAdded += OnItemAdded;
            SceneContext.Instance.playerInventory.OnItemRemoved += OnItemRemoved;
        }
        
        private void OnDestroy()
        {
            // For some reason, if we don't unsubscribe from the events, it still fires the old script instance
            SceneContext.Instance.playerInventory.OnItemAdded -= OnItemAdded;
            SceneContext.Instance.playerInventory.OnItemRemoved -= OnItemRemoved;
        }

        private void OnItemRemoved(object sender, ItemRemovedEventArgs args)
        {
            foreach (Transform childObj in gridParent.transform)
            {
                if (childObj.gameObject.name == GetObjectName(args.Item)) Destroy(childObj.gameObject);
            }
        }

        private void OnItemAdded(object sender, ItemAddedEventArgs args)
        {
            var uiImage = Instantiate(itemUiPrefab, gridParent.transform);
            uiImage.name = GetObjectName(args.Item);
            uiImage.GetComponent<Image>().sprite = args.Item.image;
        }

        private string GetObjectName(Item item) => $"ui_{item.GetType().Name}_{item.uniqueItemId}";
    }
}