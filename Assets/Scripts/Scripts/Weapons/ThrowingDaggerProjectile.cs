using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDaggerProjectile : MonoBehaviour
{
    Vector3 direction;

    [SerializeField] float speed;
    [SerializeField] float damageSize = 0.7f;

    bool hitDetected = false;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, damageSize);

            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(2);
                    hitDetected = true;
                    break;
                }
            }

            if (hitDetected)
            {
                Destroy(gameObject);
            }
        }
    }
    public void setDirection(float x, float y)
    {
        direction = new Vector3(x, y);

        if (y > 0)
        {
            Quaternion rotate = transform.localRotation;
            rotate.z = transform.rotation.z * -1f;
            transform.localRotation = rotate;
        }

        if (y < 0)
        {
            Quaternion rotate = transform.localRotation;
            rotate.z = transform.rotation.z * 5.5f;
            transform.localRotation = rotate;
        }

        if (x < 0)
        {
            Quaternion rotate = transform.localRotation;
            rotate.z = transform.rotation.z * -5.5f;
            transform.localRotation = rotate;
        }
    }
}
