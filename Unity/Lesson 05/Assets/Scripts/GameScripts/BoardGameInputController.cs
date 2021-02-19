using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class BoardGameInputController : MonoBehaviour 
{
    public static Action<Vector3> OnBoardClick; 
    
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.down);
            
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
}
