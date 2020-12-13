using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgManag : MonoBehaviour
{
    public GameObject BackgroundImageMoving;
    public float speed;
    private float currentScroll = 0;
    private Material _material;

    // setup moving bacground image
    private void Start()
    {
        _material = BackgroundImageMoving.GetComponent<Image>().material;
    }

    private void Update()
    {
        currentScroll += speed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(currentScroll, 0);
      
    }
}
