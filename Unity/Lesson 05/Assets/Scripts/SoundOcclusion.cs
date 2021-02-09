using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundOcclusion : MonoBehaviour
{
    AudioSource audioPlayer;
    GameObject listener;
    AudioLowPassFilter lowPass;

    public float maxFilter;
    int occlusionCount;
    float currentOcclusion = 0f;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        listener = FindObjectOfType<AudioListener>().gameObject;
        lowPass = GetComponent<AudioLowPassFilter>();
    }

    void Update()
    {
        occlusionCount = 0;
        CheckForOcclusion();
        DOTween.To(() => currentOcclusion, x => currentOcclusion = x, occlusionCount * 0.1f, 0.1f);
        lowPass.cutoffFrequency = Mathf.Lerp(maxFilter, 22000f, currentOcclusion);
    }

    private void CheckForOcclusion()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, listener.transform.position);

        if (hit.collider.GetComponent<AudioListener>() != null)
        {
            Debug.Log("NotOccluded");
            Debug.DrawLine(transform.position, listener.transform.position, Color.green);
            occlusionCount++;

        }
        else
        {
            Debug.Log("Occluded");
            Debug.DrawLine(transform.position, listener.transform.position, Color.red);

        }
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }


}
