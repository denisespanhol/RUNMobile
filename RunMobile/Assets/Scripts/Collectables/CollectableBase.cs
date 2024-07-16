using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [SerializeField] private string tagToCompare = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare)) Collect();
    }

    private void Collect()
    {
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {

    }
}
