using PathCreation;
using PathCreation.Examples;
using UnityEngine;

[RequireComponent(typeof(PathCreator), typeof(RoadMeshCreator))]
public class AutoZigZagPathGenerator : MonoBehaviour
{
    [SerializeField] float pathLenght;
    [SerializeField] int amountOfPoints;

    [SerializeField] bool useY;
    [SerializeField] float yRandomness;
    public void GeneratePath()
    {
        InitializePath();
        GetComponent<RoadMeshCreator>().textureTiling = pathLenght;
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
            Vector3 newPointOffset;
            if (i % 2 != 0)
            {
                newPointOffset = new Vector3(0f, 0f, distanceBetweenPoints);
            }
            else
            {
                newPointOffset = new Vector3(distanceBetweenPoints * (Random.Range(0, 2) * 2 - 1), 0f, 0f);
            }

            if (useY)
            {
                newPointOffset.y = Random.Range(-yRandomness, yRandomness);
            }

            generatedPoints[i] = generatedPoints[i - 1] + newPointOffset;
        }
        return generatedPoints;
    }
}
