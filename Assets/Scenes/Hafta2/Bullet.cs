using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Yaratıldıktan 2 saniye sonra, veya daha öncesinde başka bir objeye temas ettiğinde 
objenin yok olmasını sağlayan script. Silah mermimiz için kullandığımız için Bullet ismini vermiştim
ama daha genel bir isim verip genel amaçlı da kullanabilirsiniz.
Ayrıca, süreyi bir değişken üzerinden ayarlayabilmek de güzel olur. 
*/
[RequireComponent (typeof (Rigidbody))]
public class Bullet : MonoBehaviour {

    void Start () {
        Destroy (gameObject, 2f);
    }

    private void OnCollisionEnter (Collision other) {
        Destroy (gameObject);
    }
}