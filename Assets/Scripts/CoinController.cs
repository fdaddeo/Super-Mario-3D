using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    // Public fields
    [SerializeField]
    private float rotationFactor = 3.0f;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 30 * rotationFactor, 0) * Time.deltaTime);
    }
}
