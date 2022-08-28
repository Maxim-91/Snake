using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector2(Mathf.Floor(Random.Range(SnakeHead.horizontalRange.x, SnakeHead.horizontalRange.y)), Mathf.Floor(Random.Range(SnakeHead.verticalRange.x, SnakeHead.verticalRange.y)));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
       if (col.CompareTag("Block") || col.CompareTag("Player"))
            {
                transform.position = new Vector2(Mathf.Floor(Random.Range(SnakeHead.horizontalRange.x, SnakeHead.horizontalRange.y)), Mathf.Floor(Random.Range(SnakeHead.verticalRange.x, SnakeHead.verticalRange.y)));
            }         
    }
}
