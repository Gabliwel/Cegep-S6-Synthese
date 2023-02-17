using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    [SerializeField] private ParticleSystem deathParticlesPrefab;
    private int deathParticlesNb = 20;
    private ParticleSystem[] particles;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        particles = new ParticleSystem[deathParticlesNb];
        for(int i = 0; i < deathParticlesNb; i++)
        {
            particles[i] = Instantiate<ParticleSystem>(deathParticlesPrefab);
            particles[i].transform.parent = transform;
            particles[i].gameObject.SetActive(false);
        }
    }

    public void CallParticles(Vector3 position, float scale, Color colour)
    {
        ParticleSystem particle = GetAvailableParticles();
        particle.transform.position = position;
        particle.gameObject.SetActive(true);
        particle.transform.localScale = new Vector3(scale,scale,0);
        var main = particle.main;
        main.startColor = colour;
        var emission = particle.emission;

        emission.enabled = true;

        particle.Play();
    }

    private ParticleSystem GetAvailableParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            if (!particle.gameObject.activeSelf)
                return particle;
        }
        particles[particles.Length - 1].gameObject.SetActive(false);
        return particles[particles.Length - 1];
    }
}
