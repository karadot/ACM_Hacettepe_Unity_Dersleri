using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleClickAgent : MonoBehaviour {
    NavMeshAgent agent;

    [SerializeField]
    AnimationCurve jumpCurve;

    bool isJumping = false;

    [SerializeField]
    float jumpHeight;

    void Start () {
        agent = GetComponent<NavMeshAgent> ();
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast (ray, out hit, 100)) {
                agent.SetDestination (hit.point);
            }
        }

        if (agent.remainingDistance > agent.stoppingDistance) {
            if (agent.isOnOffMeshLink && !isJumping) {
                StartCoroutine (Jump ());
            }
        }
    }

    IEnumerator Jump () {

        isJumping = true;
        float jumpTime = 0;

        OffMeshLinkData data = agent.currentOffMeshLinkData;

        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        Vector3 targetPos;
        float jumpOffset = 0;
        while (jumpTime <= 1) {
            targetPos = Vector3.Lerp (startPos, endPos, jumpTime);
            jumpOffset = jumpCurve.Evaluate (jumpTime) * jumpHeight;
            targetPos.y += jumpOffset;
            agent.transform.position = targetPos;
            yield return null;
            jumpTime += Time.deltaTime;
        }
        agent.CompleteOffMeshLink ();
        isJumping = false;
    }
}