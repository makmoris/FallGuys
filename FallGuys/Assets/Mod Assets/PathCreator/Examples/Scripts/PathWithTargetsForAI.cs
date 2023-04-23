using PathCreation;
using PathCreation.Examples;
using UnityEngine;

[ExecuteInEditMode]
public class PathWithTargetsForAI : PathSceneTool
{
    public GameObject prefab;
    public GameObject holder;

    void Generate()
    {
        if (pathCreator != null && prefab != null && holder != null)
        {
            DestroyObjects();

            BezierPath bezierPath = pathCreator.bezierPath;
            VertexPath path = pathCreator.path;

            foreach (var anchorPosition in bezierPath.GetAnchorPositions())
            {
                var distance = path.GetClosestDistanceAlongPath(anchorPosition);
                Quaternion rotation = path.GetRotationAtDistance(distance);
                Instantiate(prefab, anchorPosition, rotation, holder.transform);
            }
        }
    }

    void DestroyObjects()
    {
        int numChildren = holder.transform.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            DestroyImmediate(holder.transform.GetChild(i).gameObject, false);
        }
    }

    protected override void PathUpdated()
    {
        if (pathCreator != null)
        {
            Generate();
        }
    }
}
