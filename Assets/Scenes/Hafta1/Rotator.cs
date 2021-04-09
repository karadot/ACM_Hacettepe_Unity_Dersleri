using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objenin verilen hızda Y ekseni etrafında dönmesini sağlayan script
public class Rotator : MonoBehaviour {

    //SerializeField ve Range kullanabileceğimiz bazı Attribute, özelliklerdir
    //SerializeField public olmayan değişkenleri editörde görmemizi
    //Range ise, editör üzerinde beilrli aralıklarda değer verilebilmesini sağlar.
    [SerializeField][Range (45, 90)]
    float donusHizi = 5f;

    void Update () {
        //şu haliyle yalnızca y ekseni etrafında dönüş gerçekleşiyor, diğer eksenlerde de döndürmeyi deneyin.
        transform.Rotate (0, donusHizi * Time.deltaTime, 0);
    }
}