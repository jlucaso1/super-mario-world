using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
  void Start()
  {
    scaleBackground();
  }
  void scaleBackground()
  {
    Vector2 deviceResolution = new Vector2(Screen.width, Screen.height);
    float deviceAspect = deviceResolution[0] / deviceResolution[1];
    Camera.main.aspect = deviceAspect;
    float camHeight = 100 * Camera.main.orthographicSize * 2f;
    float camWidth = camHeight * deviceAspect;
    SpriteRenderer bgImageSR = GetComponent<SpriteRenderer>();
    float bgImgH = bgImageSR.sprite.rect.height;
    float bgImgW = bgImageSR.sprite.rect.width;

    float bgImgHeight = camHeight / bgImgH;
    float bgImgWidth = camWidth / bgImgW;
    transform.localScale = new Vector3(bgImgWidth, bgImgHeight, 1);
  }
}
