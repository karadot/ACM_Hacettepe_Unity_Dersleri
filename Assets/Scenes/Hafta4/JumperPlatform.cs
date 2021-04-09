using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger alanına giren PlayerMovement bileşenine sahip nesneleri belirtilen yüksekliğe fırlatan script
public class JumperPlatform : MonoBehaviour {

    //Yükseklik
    [SerializeField]
    float jumpHeight = 5f;

    private void OnTriggerEnter (Collider other) {
        PlayerMovement controller = other.GetComponent<PlayerMovement> ();

        if (controller != null) {
            //burada transform.up ile, bu scriptin olduğu objenin yukarı yönünü elde ediyoruz.
            //bu sayede, objemizi çevirdiğimizde, zıplama yapılacak yön de değişiyor.
            controller.SetExternalMotion (transform.up * jumpHeight, true);
        }
    }

    //Zıplanacak konumu editör üzerinde görebilmek için WireSphere objesi çizdiriyoruz.
    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position + transform.up * jumpHeight, .25f);
    }
}