using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float horizontalSpeed = 2f;

    [SerializeField] Transform playerModel;
    [SerializeField] PathCreator pathCreator;

    public Animator currentAnim;

    TouchInput input;
    CharacterController controller;

    float distanceTraveled;

    private Quaternion prevRot;

    private void Start()
    {
        input = GetComponent<TouchInput>();
        controller = GetComponent<CharacterController>();
        transform.position = pathCreator.path.GetPointAtDistance(0, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(0, EndOfPathInstruction.Stop);
    }

    private void Update()
    {
        var hor = input.horizontal * horizontalSpeed;

        var moveVector = new Vector3(hor, 0f, forwardSpeed) * Time.deltaTime;

        distanceTraveled = pathCreator.path.GetClosestDistanceAlongPath(transform.position + transform.TransformVector(new Vector3(0f, 0f, forwardSpeed) * Time.deltaTime));

        RotateCharacter(pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop));

        RotateModel(moveVector);

        controller.Move(transform.TransformVector(moveVector));


        //currentAnim.SetFloat("walk", moveVector.z);
    }

    private void LateUpdate()
    {
        var center = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, Mathf.Min(ClampPoints(center)[1].x, ClampPoints(center)[0].x), Mathf.Max(ClampPoints(center)[1].x, ClampPoints(center)[0].x));
        pos.z = Mathf.Clamp(pos.z, Mathf.Min(ClampPoints(center)[1].z, ClampPoints(center)[0].z), Mathf.Max(ClampPoints(center)[1].z, ClampPoints(center)[0].z));
        transform.position = pos;
    }

    private Vector3[] ClampPoints(Vector3 center)
    {
        var clampPoints = new Vector3[2];
        clampPoints[0] = new Vector3(center.x + 3 * Mathf.Cos((360 - transform.eulerAngles.y) * Mathf.PI / 180),
                                     0f,
                                     center.z + 3 * Mathf.Sin((360 - transform.eulerAngles.y) * Mathf.PI / 180));

        clampPoints[1] = new Vector3(center.x - 3 * Mathf.Cos((360 - transform.eulerAngles.y) * Mathf.PI / 180),
                             0f,
                             center.z - 3 * Mathf.Sin((360 - transform.eulerAngles.y) * Mathf.PI / 180));

        return clampPoints;
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

    private void OnDrawGizmos()
    {

        var center = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
        Gizmos.DrawSphere(ClampPoints(center)[0], 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ClampPoints(center)[1], 1);

    }
}
