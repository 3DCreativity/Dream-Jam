using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    public bool ZoomActive;
    public AnimationCurve curve;
    public Vector3[] Target;
    public float Speed;

    private void Start()
    {
        if (cam == null)
        {
            cam = GetComponent<CinemachineVirtualCamera>();
        }
    }


    //public IEnumerator Shaking(float duration)
    //{
    //    Vector3 startPos = Camera.main.transform.position;
    //    float elapsedTime = 0f;
    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float strength = curve.Evaluate(elapsedTime / duration);
    //        Camera.main.transform.position = startPos + Random.insideUnitSphere * strength;
    //        yield return null;
    //    }
    //}
    public void Shake()
    {
        cam.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    }
    void LateUpdate()
    {
        if (ZoomActive)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, 6, Speed);
        }
        else
        { 
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, 8, Speed);
        }
    }
}
