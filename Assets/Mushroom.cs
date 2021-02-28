using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
  // Start is called before the first frame update
  private void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.tag == "Player")
    {
      Player.instance.GrowUp();
      Destroy(gameObject);
    }
  }
}
