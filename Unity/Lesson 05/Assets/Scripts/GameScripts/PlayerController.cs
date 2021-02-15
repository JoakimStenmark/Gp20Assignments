using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int playerNr;
    public CarSettings carSettings;

    public bool mouseControl;
    Vector3 newPos;
    Vector3 movement;

    public UnityAction<float> OnJump;


    void Start()
    {
        newPos = transform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = carSettings.model[Random.Range(0, 4)];

    }

    void Update()
    {
        movement = Vector3.zero;

        if (mouseControl && Input.GetMouseButton(0))
        {
            newPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            movement = newPos - transform.position;
            movement.Normalize();

            
        }


        if(!mouseControl)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            movement = new Vector3(x, y, 0);

            if (movement.sqrMagnitude > 1)
            {
                movement = movement.normalized;
            }
        }

        MovePlayer(movement);

    }

    void MovePlayer(Vector3 movement)
    {
        if (movement.sqrMagnitude > 0.001f)
        {
            transform.up = movement;
        }

        transform.position += movement * carSettings.speed * Time.deltaTime;
    }

}
