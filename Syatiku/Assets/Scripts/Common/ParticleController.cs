using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        //ループしないパーティクル
        if (!particle.loop)
        {
            StartCoroutine(DisableParticle());
        }
    }

    IEnumerator DisableParticle()
    {
        yield return new WaitWhile(() => particle.IsAlive(true));

        gameObject.SetActive(false);
    }
}
