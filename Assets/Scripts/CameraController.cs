using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform t_player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    void Start()
    {
        t_player = Player.instance.gameObject.transform;
    }
    private void LateUpdate()
    {
        float x = Mathf.Clamp(t_player.position.x, xMin, xMax);
        float y = Mathf.Clamp(t_player.position.y, yMin, yMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
