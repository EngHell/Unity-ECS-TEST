using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAtClickTest : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = UnityEngine.Input.mousePosition;
            Debug.Log("mousePos:" + mousePos);
            //mousePos.z = 10;
            Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

            var o = Instantiate(prefab);
            mouseInWorld.y = 0;
            o.transform.position = mouseInWorld;
        }
        
    }
}
