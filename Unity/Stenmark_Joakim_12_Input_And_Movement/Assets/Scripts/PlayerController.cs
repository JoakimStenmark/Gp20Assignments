using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int playerNr;
    [SerializeField]
    private float speed;

    void Start()
    {
        
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0) * speed * Time.deltaTime;

        if (movement.sqrMagnitude > 1)
        {
            movement = movement.normalized;
        }

        transform.Translate(movement);

    }

    void FixedUpdate()
    {
        
    }
}
