using PathCreation;
using PathCreation.Examples;
using UnityEngine;

[RequireComponent(typeof(PathCreator), typeof(RoadMeshCreator))]
public class AutoPathGenerator : MonoBehaviour
{
    [SerializeField] float pathLenght;
    [SerializeField] int amountOfPoints;

    [SerializeField] float xRandomness;
    [SerializeField] float yRandomness;

    public void GeneratePath()
    {
        InitializePath();
        GetComponent<RoadMeshCreator>().textureTiling = pathLenght/10f;
        GetComponent<RoadMeshCreator>().flattenSurface = true;
        GetComponent<RoadMeshCreator>().roadWidth = 10;
    }

    private void InitializePath()
    {
        BezierPath bezierPath = new BezierPath(GeneratePoints());
        GetComponent<PathCreator>().bezierPath = bezierPath;


        GetComponent<PathCreator>().bezierPath.GlobalNormalsAngle = 90f;
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
}
