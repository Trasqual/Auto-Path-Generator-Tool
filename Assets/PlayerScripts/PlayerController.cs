using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float horizontalSpeed = 2f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Transform playerModel;
    [SerializeField] PathCreator pathCreator;

    public Animator currentAnim;

    TouchInput input;
    CharacterController controller;

    float distanceTraveled;

    private Quaternion prevRot;

    public bool onRoad;

    private void Start()
    {
        input = GetComponent<TouchInput>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var hor = input.horizontal * horizontalSpeed;
        if (OnLeftEdge())
        {
            if (hor < 0) hor = 0;
        }
        else if (OnRightEdge())
        {
            if (hor > 0) hor = 0;
        }

        var moveVector = new Vector3(hor, 0f, forwardSpeed) * Time.deltaTime;

        distanceTraveled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);

        RotateCharacter(pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop));

        RotateModel(moveVector);


        controller.Move(transform.TransformVector(moveVector));

        currentAnim.SetFloat("walk", moveVector.z);
    }

    private bool OnLeftEdge()
    {
        return !Physics.Raycast(transform.position - transform.right + transform.up, Vector3.down, 5, groundMask);
    }

    private bool OnRightEdge()
    {
        return !Physics.Raycast(transform.position + transform.right + transform.up, Vector3.down, 5, groundMask);
    }

    private void RotateCharacter(Quaternion dir)
    {
        transform.rotation = Quaternion.Lerp(prevRot, dir, Time.deltaTime * 5f);
        prevRot = transform.rotation;
    }

    private void RotateModel(Vector3 dir)
    {
        playerModel.LookAt(transform.position + transform.TransformVector(dir));
        float ry = playerModel.localEulerAngles.y;
        if (ry >= 180) ry -= 360;
        playerModel.localEulerAngles = new Vector3(0, Mathf.Clamp(ry, -45, 45), 0);
    }
}
