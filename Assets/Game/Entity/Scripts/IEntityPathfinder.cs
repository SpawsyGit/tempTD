using UnityEngine;
using Pathfinding;

public interface IEntityPathfinder
{
    public Seeker seeker { get; }
    public Vector2 position { get; }
    public Vector2 targetPosition { get; }
}
