using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform pivotSphere;  
    [SerializeField] private Transform gunBarrel;    
    [SerializeField] private float rotateSpeed = 8f;

    private Transform _currentTarget;
    private Coroutine _trackingCo;

    public void SetTarget(Transform target)
    {
        _currentTarget = target;

        if (_trackingCo == null)
        {
            _trackingCo = StartCoroutine(TrackingRoutine());
        }
    }

    public void ClearTarget()
    {
        _currentTarget = null;
    }

    private IEnumerator TrackingRoutine()
    {
        while (true)
        {
            if (_currentTarget == null || !_currentTarget.gameObject.activeInHierarchy)
            {
                yield return null;
                continue;
            }

            // Yaw
            Vector3 flatDir = _currentTarget.position - pivotSphere.position;
            flatDir.y = 0f;

            if (flatDir.sqrMagnitude > 0.001f)
            {
                Quaternion yawRot = Quaternion.LookRotation(flatDir);
                pivotSphere.rotation = Quaternion.Slerp(
                    pivotSphere.rotation,
                    yawRot,
                    Time.deltaTime * rotateSpeed
                );
            }

            // Pitch
            Vector3 localDir = pivotSphere.InverseTransformPoint(_currentTarget.position);
            float pitch = Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;
            pitch = Mathf.Clamp(pitch, -5f, 45f);

            gunBarrel.localRotation = Quaternion.Slerp(
                gunBarrel.localRotation,
                Quaternion.Euler(pitch, 0f, 0f),
                Time.deltaTime * rotateSpeed
            );

            yield return null;
        }
    }
}
