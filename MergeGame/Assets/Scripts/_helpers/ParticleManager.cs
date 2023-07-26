using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoSingleton<ParticleManager>
{
    [Header("Particle Pool Parameters")]
    [Tooltip("Particle Pool Prefab Reference")]
    [SerializeField]
    private ParticleSystem particlePrefab;
    [Tooltip("Particle Pool Size")]
    [SerializeField]
    private int particleCount = 10;
    [SerializeField]
    private List<ParticleSystem> particleList = new List<ParticleSystem>();

    private void Awake()
    {
        GenerateParticleSystems();
    }

    /// <summary>
    /// This method generates a number of particle systems based on the particle count variable.
    /// It instantiates a prefab particle system and adds it to a list of particle systems.
    /// </summary>
    private void GenerateParticleSystems()
    {
        for (int i = 0; i < particleCount; i++)
        {
            ParticleSystem particle = Instantiate(particlePrefab, transform);
            particleList.Add(particle);
        }
    }

    /// <summary>
    /// This script defines a method called PlayParticleAtPoint which takes a position vector as its parameter.
    /// The method looks for a particle system in the list of available particles that is not currently 
    /// playing and plays it at the specified position. If there are no available particles, the method simply
    /// returns
    /// </summary>
    /// <param name="position"></param>
    public void PlayParticleAtPoint(Vector3 position)
    {
        // Find audio source which is not playing
        ParticleSystem particleSystem = null;
        for (int i = 0; i < particleList.Count; i++)
        {
            if (!particleList[i].isPlaying)
            {
                //Debug.LogWarning(particleList[i].name + " is not playing");
                particleSystem = particleList[i];
                break;
            }
        }

        if (particleSystem == null)
        {
            //Debug.LogWarning("There is no any other particle");
            return;
        }

        particleSystem.transform.position = new Vector3(position.x, position.y, 0);
        particleSystem.Play();
    }
}
