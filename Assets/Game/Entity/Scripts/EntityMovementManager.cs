using UnityEngine;

[System.Serializable]
public class EntityMovementManager
{
    private IEntityMovement entityMovement;

    private float movementStartTime;
    private float movementStopTime;

    public EntityMovementManager(IEntityMovement entityMovement)
    {
        this.entityMovement = entityMovement;
    }

    public void StartMovement()
    {
        movementStartTime = entityMovement.currentTime;
        entityMovement.OnStartMovement();
    }
    public void StopMovemnet()
    {
        movementStopTime = entityMovement.currentTime;
        entityMovement.OnStopMovement();
    }

    public void Update()
    {
        CheckForDirectionChanges();
        UpdateVelocity();
    }
    private void CheckForDirectionChanges()
    {
        if (!entityMovement.isMoving && entityMovement.direction != Vector2.zero)
            StartMovement();
        else if (entityMovement.isMoving && entityMovement.direction == Vector2.zero)
            StopMovemnet();
    }

    private void UpdateVelocity()
    {
        entityMovement.velocity = entityMovement.direction * entityMovement.speed * GetCurveFactor() * Time.deltaTime;
    }

    private float GetCurveFactor()
    {
        AnimationCurve curve = entityMovement.isMoving ? entityMovement.accelerationCurve :
                                                         entityMovement.decelerationCurve;
        return curve.Evaluate(TimeSinceLastAction());
    }
    private float TimeSinceLastAction()
    {
        float lastActionTime = entityMovement.isMoving ? movementStartTime : movementStopTime;
        return entityMovement.currentTime - lastActionTime;
    }
}
