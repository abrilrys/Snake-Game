using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    
    Vector2 dir = Vector2.right;
    bool ate = false;
    public GameObject tailPrefab;
    public List<Transform> tail = new List<Transform>(); 
    void Start()
    {
        // Move the Snake every 300ms
        InvokeRepeating("Move", .1f, .1f);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void Move()
    {

        Vector2 v = transform.position;
        transform.Translate(dir);

        if (ate)
        {

            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);


            tail.Insert(0, g.transform);


            ate = false;
        }

        else if (tail.Count > 0)
        {

            tail.Last().position = v;


            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.name.StartsWith("Food"))
        {

            ate = true;
            Destroy(coll.gameObject);
        }

        else
        {
           // print("You lose");
            //Time.timeScale = 0;
            //enabled = false;
            SceneManager.LoadScene("GameOver");
        }
    }

   
}