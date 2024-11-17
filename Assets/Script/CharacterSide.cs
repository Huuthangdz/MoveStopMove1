using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSide : MonoBehaviour
{

    [SerializeField] character character;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.AddTarget(other.GetComponent<character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.RemoveTaget(other.gameObject.GetComponent<character>());
        }
    }
}
