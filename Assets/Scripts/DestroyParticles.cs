using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private float counter = 0;

    private void Update()
    {
        counter += Time.deltaTime;

        if (GetComponent<ParticleSystem>().particleCount == 0 && counter > 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
