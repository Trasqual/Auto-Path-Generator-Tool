using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowPlayer : MonoBehaviour
{
    public float horizontalSpeed = 2f;
    [SerializeField] Transform playerModel;

    TouchInput input;
    PathFollower pathFollower;
    Animator anim;
    Vector3 prevPos;
    Vector3 rotVel;
    Vector3 xDir;

    public bool isStarted;
    public bool isFinished;


    private void Start()
    {
        pathFollower = GetComponentInParent<PathFollower>();
        input = GetComponent<TouchInput>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        MoveHorizontal();

        anim.SetFloat("walk", CalculateSpeed());
        prevPos = transform.position;
    }

    private void MoveHorizontal()
    {
        if (!isStarted) return;
        if (isFinished) return;

        var hor = input.horizontal * horizontalSpeed;
        transform.localPosition += new Vector3(input.horizontal * horizontalSpeed, 0, 0);
        var pos = transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -2, 2);
        transform.localPosition = pos;

        RotateModel(new Vector3(hor, playerModel.position.y, pathFollower.speed * Time.deltaTime));
    }

    private float CalculateSpeed()
    {
        return (Mathf.Abs(transform.position.magnitude - prevPos.magnitude)) / Time.deltaTime;
    }

    private void RotateModel(Vector3 dir)
    {

        dir = Vector3.SmoothDamp(xDir, dir, ref rotVel, 0.2f);
        xDir = dir;
        playerModel.LookAt(transform.position + transform.TransformVector(dir));
        float ry = playerModel.localEulerAngles.y;
        if (ry >= 180) ry -= 360;
        playerModel.localEulerAngles = new Vector3(0, Mathf.Clamp(ry, -15, 15), 0);
    }
}
