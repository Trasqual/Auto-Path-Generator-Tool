using UnityEngine;
using PathCreation;

public class PathFollower : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;

    float distance;
    Quaternion prevRot;

    private void Update()
    {
        distance += 5 * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distance, EndOfPathInstruction.Stop);
        transform.rotation = Quaternion.Lerp(prevRot, pathCreator.path.GetRotationAtDistance(distance, EndOfPathInstruction.Stop), Time.deltaTime*5);
        prevRot = transform.rotation;
    }
}
