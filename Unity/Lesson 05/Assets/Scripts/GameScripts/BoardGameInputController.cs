using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class BoardGameInputController : MonoBehaviour 
{
    public static Action<Vector3> OnBoardClick;
    private Touch theTouch;
    //Felt Slow
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    GameObject objectClickedOn = eventData.pointerCurrentRaycast.gameObject;
    //    Vector3 position = eventData.pointerCurrentRaycast.screenPosition;
    //    Debug.Log(objectClickedOn);
    //}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckInput(Input.mousePosition);

        }

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                CheckInput(theTouch.position);
            }
        }

    }

    private static void CheckInput(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.down);

        if (hit.collider != null)
        {
            //Debug.Log("hit something");
            if (hit.collider.CompareTag("Tile"))
            {
                if (OnBoardClick != null)
                {
                    OnBoardClick(hit.point);

                }

            }

        }
    }
}
