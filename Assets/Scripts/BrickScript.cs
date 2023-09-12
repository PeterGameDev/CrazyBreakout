using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public ParticleSystem brickBreakParticleSystem;

    private void OnDisable()
    {

        Debug.Log("Brick break particle system");
        ParticleSystem ps = Instantiate(brickBreakParticleSystem);
        ps.transform.position = transform.position;
        ps.Play();
    }

}
