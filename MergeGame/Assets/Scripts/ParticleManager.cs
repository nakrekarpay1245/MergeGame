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

    private void GenerateParticleSystems()
    {
        for (int i = 0; i < particleCount; i++)
        {
            ParticleSystem particle = Instantiate(particlePrefab, transform);
            particleList.Add(particle);
        }
    }

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

        particleSystem.transform.position = position;
        particleSystem.Play();
    }
}
