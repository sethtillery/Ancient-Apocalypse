using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectDispose : MonoBehaviour
{
    Transform playerTransform;
    float maxDistance = 20f;

    private void Start()
    {
        playerTransform = GameManager.instance.playerTransform;    
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if(distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
