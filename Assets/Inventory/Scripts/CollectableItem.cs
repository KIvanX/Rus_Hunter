using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private int _amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var inventory = other.GetComponent<Inventory>();
            if (inventory.AddItems(_item, _amount))
                Destroy(gameObject);
        }
    }
}
