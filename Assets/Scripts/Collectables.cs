using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
    private int pineapples=0;

    [SerializeField] private Text pineapplesText;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Pineapple"))
        {
            Destroy(other.gameObject);
            pineapples++;
            pineapplesText.text="Pineapples: "+pineapples;
        }
    }
}
