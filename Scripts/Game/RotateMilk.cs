using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMilk : MonoBehaviour
{
    [SerializeField] float speed = 1;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.right * speed);
    }
}
