using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    [SerializeField] Transform Feet;
    [SerializeField] Transform Head;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(Head.position.x, Feet.position.y, Head.position.z);
    }
}
