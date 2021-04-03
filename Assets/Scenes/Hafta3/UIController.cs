using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    Text altinText;

    public void AltinTextGuncelle (int altinMiktari) {
        altinText.text = "Toplanacak Altın:" + altinMiktari;
    }

    public void AltinTextGuncelle (string message) {
        altinText.text = message;
    }

}