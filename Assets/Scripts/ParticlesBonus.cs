using UnityEngine;

public class ParticlesBonus : MonoBehaviour
{
    public UnityEventRaiseBehaviour EventRaiseBehaviour;
    [SerializeField] private ParticleSystem _leftParticleSystem;
    [SerializeField] private ParticleSystem _rightParticleSystem;

    private void Start()
    {
        if (EventRaiseBehaviour == null)
        {
            EventRaiseBehaviour = GameObject.FindObjectOfType<UnityEventRaiseBehaviour>();
        }

        if (EventRaiseBehaviour.ParticlesOnClickEvent == null)
        {
            EventRaiseBehaviour.ParticlesOnClickEvent = new EventTurnOnParticles();
        }

        EventRaiseBehaviour.ParticlesOnClickEvent.AddListener(EventChangeSceneState_Handler);
    }

    /// <summary>
    /// в случае нажатия на эскейп или пробел будут вылетать бонусные системы частиц приносящие РАДОСТЬ
    /// </summary>
    private void EventChangeSceneState_Handler(bool argument)
    {
        if (argument)
        {
            _leftParticleSystem.Play();
        }
        else
        {
            _rightParticleSystem.Play();
        }
    }
}
