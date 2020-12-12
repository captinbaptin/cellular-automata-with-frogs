using UnityEngine;
using System.Collections;

public class FrogScript : MonoBehaviour
{
    public float sideForce = 50f, upForce = 250f, upForceVariance=1.5f, jumpPerFrame=0.05f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.position.y<2 && Random.value<jumpPerFrame) {
            rb.AddForce(Random.Range(-sideForce, sideForce), Random.Range(upForce / upForceVariance, upForce * upForceVariance), Random.Range(-sideForce, sideForce));
        }
    }
}
