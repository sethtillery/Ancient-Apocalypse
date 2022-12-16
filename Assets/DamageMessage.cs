using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    float timeToLeave = 1f;

    [SerializeField] float ttl = 1f;

    private void OnEnable()
    {
        timeToLeave = ttl;
    }

    private void Update()
    {
        timeToLeave -= Time.deltaTime;

        if (timeToLeave < 0f)
            gameObject.SetActive(false);
    }
}
