using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comaredisplay : MonoBehaviour
{
    [SerializeField] spawn2 spawn;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(spawn.playerObj.transform.position.x, spawn.playerObj.transform.position.y, -10);
    }
}
