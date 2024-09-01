using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    Vector3 StartingVector;
    [SerializeField] Vector3 MovementVector;
    [SerializeField] [Range(0,1)] float MovementFactor;
    [SerializeField] float period;
    
    void Start()
    {
        StartingVector = transform.position;
    }
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        float tau = Mathf.PI * 2;
        MovementFactor = Mathf.Sin(cycles * tau);
        MovementFactor = (MovementFactor + 1f) / 2f;

        Vector3 Offset = MovementVector * MovementFactor;
        transform.position = StartingVector+Offset;
    }
}
