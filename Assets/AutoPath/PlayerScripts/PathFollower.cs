using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;

    public float speed = 5f;

    float distance;
    Quaternion prevRot;
    public bool isGameStarted;

    private void Start()
    {
        transform.position = pathCreator.path.GetPointAtDistance(0, EndOfPathInstruction.Stop);
        transform.rotation = pathCreator.path.GetRotationAtDistance(0, EndOfPathInstruction.Stop);
    }

    private void Update()
    {
        if (!isGameStarted) return;
        distance += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        transform.rotation = Quaternion.Lerp(prevRot, pathCreator.path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop), Time.deltaTime * 5);
        prevRot = transform.rotation;
    }
}
