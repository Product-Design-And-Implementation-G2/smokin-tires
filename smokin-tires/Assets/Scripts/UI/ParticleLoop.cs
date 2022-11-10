using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLoop : MonoBehaviour
{
    private ParticleSystem partSys;

    public void LoopingOnOff()
    {
        partSys = GetComponent<ParticleSystem>();
        partSys.Stop();

        var particleMainSettings = partSys.main;
        particleMainSettings.loop ^= true;

        partSys.Play();
    }
}
