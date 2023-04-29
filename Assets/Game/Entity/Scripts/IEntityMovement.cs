using UnityEngine;

public interface IEntityMovement
{
    public bool isMoving { get; }

    public float speed { get; }
    public Vector2 velocity { get; set; }
    public Vector2 direction { get; }

    public AnimationCurve accelerationCurve { get; }
    public AnimationCurve decelerationCurve { get; }

    public float currentTime { get; }

    public void OnStartMovement();
    public void OnStopMovement();
}
