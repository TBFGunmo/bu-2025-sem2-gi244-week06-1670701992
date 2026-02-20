using UnityEngine;
using UnityEngine.InputSystem;

public class WaveSpawnManagerExam04 : MonoBehaviour
{
    public Wave[] waveConfigurations;
    public WaveController waveController;

    public bool enableWaveCycling;

    private int currentWave = 0;
    private float waveEndTime = 0f;

    private InputAction switchAction;

    private void Awake()
    {
        switchAction = InputSystem.actions.FindAction("SwitchCommand");
    }

    void Start()
    {
        waveController.StartWave(waveConfigurations[currentWave]);
    }

    void Update()
    {
        if (currentWave >= waveConfigurations.Length)
        {
            return;
        }

        if (Time.time >= waveEndTime && waveController.IsComplete())
        {
            currentWave++;
            if (currentWave >= waveConfigurations.Length)
            {
                Debug.Log("All waves completed!");
                if (enableWaveCycling) 
                {
                    Debug.Log("reset to first wave");
                    currentWave = 0;
                    waveController.StartWave(waveConfigurations[currentWave]);

                    waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;

                }
            }
            else
            {
                Debug.Log("Change wave to " + currentWave);
                waveController.StartWave(waveConfigurations[currentWave]);
                waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
            }
        }

        if (switchAction.triggered)
        {
            enableWaveCycling = !enableWaveCycling;
        }



    }
}