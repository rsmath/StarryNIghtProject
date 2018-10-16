using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour {

    private float degreesPerSecond = 10.0f;

    void Update()
    {
        transform.Rotate(Vector3.right * degreesPerSecond * Time.deltaTime);
    }
}
