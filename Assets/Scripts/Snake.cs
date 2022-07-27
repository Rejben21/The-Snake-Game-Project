using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction;

    private List<Transform> snakeParts = new List<Transform>();
    public Transform partPrefab;

    public int initialSize = 2;

    private bool canGoRight, canGoLeft;
    private bool hasStarted = false;

    private void Start()
    {
        ResetState();

        if(GameManager.instance.hasStarted)
        {
            direction = Vector2.right;
        }
    }

    private void Update()
    {
        if(GameManager.instance.hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = Vector2.up;
                hasStarted = true;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = Vector2.down;
                hasStarted = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = Vector2.right;
                hasStarted = true;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = Vector2.left;
                hasStarted = true;
            }
        }
    }

    private void FixedUpdate()
    {
        for(int i = snakeParts.Count - 1; i > 0; i--)
        {
            snakeParts[i].position = snakeParts[i - 1].position;
        }

        transform.position = new Vector2(Mathf.Round(transform.position.x + direction.x), Mathf.Round(transform.position.y + direction.y));
    }

    private void Grow()
    {
        Transform part = Instantiate(partPrefab);
        part.position = snakeParts[snakeParts.Count - 1].position;

        snakeParts.Add(part);
    }

    public void ResetState()
    {
        for(int i = 1; i < snakeParts.Count; i++)
        {
            Destroy(snakeParts[i].gameObject);
        }

        snakeParts.Clear();
        snakeParts.Add(transform);

        for(int i = 1; i < initialSize; i++)
        {
            snakeParts.Add(Instantiate(partPrefab));
        }

        transform.position = Vector2.zero;

        GameManager.instance.curScore = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }
        else if(other.CompareTag("Obstacle"))
        {
            ResetState();
            if(GameManager.instance.hasStarted && hasStarted)
            {
                AudioManager.instance.PlaySFX(1);
            }
        }
    }
}
