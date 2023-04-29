using UnityEngine;
using Pathfinding;

public class EntityPathfinderManager
{
    private IEntityPathfinder entityPathfinder;

    private Vector2 lastTargetPosition;

    private Path currentPath;
    private int currentPointID;
    private Vector2 currentPoint => currentPath.vectorPath[currentPointID];

    public bool reachedTarget { get; private set; }

    public EntityPathfinderManager(IEntityPathfinder entityPathfinder)
    {
        this.entityPathfinder = entityPathfinder;
        lastTargetPosition = entityPathfinder.targetPosition;
        CalculatePath();
    }

    public void Update()
    {
        if (lastTargetPosition != entityPathfinder.targetPosition)
            CalculatePath();
        if (!reachedTarget && currentPath != null)
            CheckForPointChanges();
    }

    public Vector2 GetDirection()
    {
        if (reachedTarget || currentPath == null) return Vector2.zero;

        float distanceX = currentPoint.x - entityPathfinder.position.x;
        float distanceY = currentPoint.y - entityPathfinder.position.y;
        return new Vector2(distanceX, distanceY).normalized;
    }

    public void CalculatePath()
    {
        entityPathfinder.seeker.StartPath(entityPathfinder.position, entityPathfinder.targetPosition, OnCalculatePath);
    }
    private void OnCalculatePath(Path path)
    {
        if (path.error)
        {
            Debug.LogError($"Couldn't find a path. Details: {path.error}");
            return;
        }
        reachedTarget = false;
        lastTargetPosition = entityPathfinder.targetPosition;
        currentPath = path;
        currentPointID = 0;
    }

    private void CheckForPointChanges()
    {
        float distanceFromCurrentPoint = Vector2.Distance(entityPathfinder.position, currentPoint);
        if (distanceFromCurrentPoint <= 0.1f)
            ChangeToNextPoint();
    }
    private void ChangeToNextPoint()
    {
        currentPointID++;
        if (currentPointID >= currentPath.vectorPath.Count)
            reachedTarget = true;
    }
}