using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public ParticleSystem brickBreakParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        ParticleSystem ps = Instantiate(brickBreakParticleSystem);
        ps.transform.position = transform.position;
        ps.Play();
        SFXScript.Instance.playBrickBreakSound();
        Destroy(gameObject);
    }
}
