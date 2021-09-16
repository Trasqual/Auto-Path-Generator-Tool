using PathCreation;
using PathCreation.Examples;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PathCreator), typeof(RoadMeshCreator))]
public class AutoPathGenerator : MonoBehaviour
{
    [Header("Path Creation Fields")]
    [SerializeField] float pathLenght = 100f;
    [SerializeField] int amountOfPoints = 5;
    public float roadWidth = 3f;
    [SerializeField] float xRandomness;
    [SerializeField] float yRandomness;

    [Header("Start and Finish Prefabs")]
    [SerializeField] GameObject startPrefab;
    [SerializeField] GameObject finishPrefab;

    private GameObject previousStartPrefab;
    private GameObject previousFinishPrefab;

    public void GeneratePath()
    {
        var roadMeshCreator = GetComponent<RoadMeshCreator>();

        InitializePath();
        roadMeshCreator.textureTiling = pathLenght;
        roadMeshCreator.flattenSurface = true;
        roadMeshCreator.roadWidth = roadWidth;
    }

    private void InitializePath()
    {
        var pathCreator = GetComponent<PathCreator>();

        BezierPath bezierPath = new BezierPath(GeneratePoints());
        pathCreator.bezierPath = bezierPath;

        pathCreator.bezierPath.GlobalNormalsAngle = 90f;
        pathCreator.bezierPath.AutoControlLength = 0.01f;

        RemovePreviousStartAndFinish();

        previousStartPrefab = PrefabUtility.InstantiatePrefab(startPrefab, transform) as GameObject;
        previousFinishPrefab = PrefabUtility.InstantiatePrefab(finishPrefab, transform) as GameObject;

        previousStartPrefab.transform.position = pathCreator.path.GetPointAtDistance(0, EndOfPathInstruction.Stop);
        previousStartPrefab.transform.rotation = pathCreator.path.GetRotationAtDistance(0, EndOfPathInstruction.Stop);

        previousFinishPrefab.transform.position = pathCreator.path.GetPointAtDistance(pathLenght, EndOfPathInstruction.Stop);
        previousFinishPrefab.transform.rotation = pathCreator.path.GetRotationAtDistance(pathLenght, EndOfPathInstruction.Stop);
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

    public void UpdateStartAndFinish()
    {
        var pathCreator = GetComponent<PathCreator>();

        if (previousStartPrefab != null && previousFinishPrefab != null)
        {
            previousStartPrefab.transform.position = pathCreator.path.GetPointAtDistance(0, EndOfPathInstruction.Stop);
            previousStartPrefab.transform.rotation = pathCreator.path.GetRotationAtDistance(0, EndOfPathInstruction.Stop);
            previousFinishPrefab.transform.position = pathCreator.path.GetPointAtDistance(pathLenght, EndOfPathInstruction.Stop);
            previousFinishPrefab.transform.rotation = pathCreator.path.GetRotationAtDistance(pathLenght, EndOfPathInstruction.Stop);
        }
    }

    public void RemovePreviousStartAndFinish()
    {
        if (previousStartPrefab != null && previousFinishPrefab != null)
        {
            DestroyImmediate(previousStartPrefab);
            DestroyImmediate(previousFinishPrefab);
        }
    }
}
