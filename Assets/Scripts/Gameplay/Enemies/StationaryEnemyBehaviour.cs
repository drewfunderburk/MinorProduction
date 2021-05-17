using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ArriveAtPointBehaviour))]
[RequireComponent(typeof(AIShootBehaviour))]
[RequireComponent(typeof(NavMeshAgent))]
public class StationaryEnemyBehaviour : MonoBehaviour
{
    private ArriveAtPointBehaviour _arriveBehaviour;
    private AIShootBehaviour _shootBehaviour;
    private NavMeshAgent _agent;

    private void Start()
    {
        _arriveBehaviour = GetComponent<ArriveAtPointBehaviour>();
        _shootBehaviour = GetComponent<AIShootBehaviour>();
        _agent = GetComponent<NavMeshAgent>();

        _agent.SetDestination(GetRandomPositionInView());
    }

    private void Update()
    {
        if (_agent.remainingDistance < 0.5f)
        {
            _arriveBehaviour.enabled = false;
            _agent.enabled = false;
            _shootBehaviour.enabled = true;
        }
    }

    /// <summary>
    /// Get a random position within the camera's view
    /// </summary>
    private Vector3 GetRandomPositionInView()
    {
        Camera cam = Camera.main;

        float xScreenPos = Random.Range(0, Screen.width);
        float yScreenPos = Random.Range(0, Screen.height);

        Vector3 screenPosition = cam.ScreenToWorldPoint(new Vector3(xScreenPos, 0, yScreenPos));

        return screenPosition;
    }
}
