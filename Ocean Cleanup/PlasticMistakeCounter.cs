using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

/**
 * A plastic counter that increments the number of mistakes
 * \author Rhys Mader 33705134
 * \since 11 April 2022
 */
[RequireComponent(typeof(Collider))]
public class PlasticMistakeCounter : MonoBehaviour
{
    [SerializeField] [Tooltip("The metrics to add msitakes to\nLeave blank to search")]
    private RunMetrics _metrics;

    [SerializeField] [Tooltip("The event called when plastic collides with this")]
    public UnityEvent<float> onMistake;

    private void Awake()
    {
        this._metrics ??= Object.FindObjectOfType<RunMetrics>();
    }

    private void OnDestroy()
    {
        FindObjectOfType<LevelInterface>().RunComplete();
    }

    private void OnTriggerEnter(Collider other)
    {
        this._metrics.mistakesTotal++;
        this.onMistake.Invoke(other.attachedRigidbody.mass);
        Object.Destroy(other.attachedRigidbody.gameObject);
    }
}