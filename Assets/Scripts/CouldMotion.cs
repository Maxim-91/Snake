using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouldMotion : MonoBehaviour
{
    private Vector2 direction = new Vector2(1f, 0f);
    private Vector3 pointStart = new Vector3(11.0f, 0.0f, 0.0f);   
    public float speedCould = 0.05f;   

    void FixedUpdate()
    {
        transform.Translate(direction * speedCould);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Block"))
        {
            transform.position = transform.position - pointStart;
        }
    }
}
