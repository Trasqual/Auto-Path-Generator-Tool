using PathCreation;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class AutoPathGenerator : MonoBehaviour
{
    [SerializeField] float pathLenght;
    [SerializeField] int amountOfPoints;

    [SerializeField] float xRandomness;
    [SerializeField] float yRandomness;

    public void GeneratePath()
    {
        InitializePath();
    }

    private void InitializePath()
    {
        BezierPath bezierPath = new BezierPath(GeneratePoints());
        GetComponent<PathCreator>().bezierPath = bezierPath;
    }

    private Vector3[] GeneratePoints()
    {
        var generatedPoints = new Vector3[amountOfPoints];

        generatedPoints[0] = Vector3.zero;

        float distanceBetweenPoints = pathLenght / (amountOfPoints-1);

        for (int i = 1; i < generatedPoints.Length; i++)
        {
            var randX = Random.Range(-xRandomness, xRandomness);
            var randY = Random.Range(-yRandomness, yRandomness);

            generatedPoints[i] = new Vector3(randX, randY, distanceBetweenPoints * i);
        }

        return generatedPoints;
    }

    private void GenerateMesh()
    {

    }
}
