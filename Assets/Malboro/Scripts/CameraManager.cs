using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    Camera _camera;
    [HideInInspector] public Animator cameraAnim;
    [SerializeField] Transform target;

    [SerializeField] [Range(0, 0.5f)] float screenOffsetWidth;
    [SerializeField] [Range(0, 0.5f)] float screenOffsetheight;
    public float speed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        _camera = GetComponent<Camera>();
        cameraAnim = GetComponent<Animator>();
    }

    //void LateUpdate()
    //{
    //    if (!EventManager.Instance.isStartGame)
    //        return;

    //    Vector3 nextPos = Vector3.zero;
    //    Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position);

    //    if (screenPoint.x < 0 + screenOffsetWidth)
    //    {
    //        nextPos = Vector3.left;
    //    }
    //    else if (screenPoint.x > 1 - screenOffsetWidth)
    //    {
    //        nextPos = Vector3.right;
    //    }

    //    if (screenPoint.y < 0.5f)
    //    {
    //        nextPos += Vector3.back;
    //    }
    //    else if (screenPoint.y > 1 - screenOffsetheight)
    //    {
    //        nextPos += Vector3.forward;
    //    }

    //    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _camera.transform.position + nextPos, Time.deltaTime * speed);
    //}

    //private void Update()
    //{
    //    if (!EventManager.Instance.isStartGame)
    //        return;

    //    Vector3 nextPos = Vector3.zero;
    //    Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position);

    //    if (screenPoint.y < 0.5f)
    //    {
    //        nextPos = Vector3.back;
    //    }
    //    else if (screenPoint.y > 1 - screenOffsetheight)
    //    {
    //        nextPos = Vector3.forward;
    //    }

    //    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _camera.transform.position + nextPos, Time.deltaTime * speed);
    //}
}
