
using UnityEngine;

public class UnityEventRaiseBehaviour : MonoBehaviour
{
    public EventTurnOnParticles ParticlesOnClickEvent;
    /// <summary>
    /// выпуск системы частиц по нажатию на эскейп и пробел 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ParticlesOnClickEvent.Invoke(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ParticlesOnClickEvent.Invoke(false);
        }
    }
}
