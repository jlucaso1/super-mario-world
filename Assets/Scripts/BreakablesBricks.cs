using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BreakablesBricks : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coordinate = GetComponentInParent<Grid>().WorldToCell(mouseWorldPos);
            coordinate.z = 0;
            Debug.Log(coordinate);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var contact = col.contacts[0];
            if (contact.normal.y > 0.5f)
            {
                Tilemap map = GetComponentInParent<Tilemap>();
                Vector3Int removePos = map.WorldToCell(col.transform.position);
                removePos.y += 1;
                if (map.GetTile(removePos) != null)
                {
                    GetComponentInChildren<ParticleSystem>().transform.position = map.CellToWorld(removePos);
                    GetComponentInChildren<ParticleSystem>().Play();
                    SoundManager.instance.PlaySound(SoundManager.Sound.BreakBlock);
                    map.SetTile(removePos, null);
                    //map.RefreshTile(removePos);
                }
            }
        }
    }
}
