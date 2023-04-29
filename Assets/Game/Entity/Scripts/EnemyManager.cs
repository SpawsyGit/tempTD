using UnityEngine;
using Zenject;

public class EnemyManager : MonoBehaviour
{
    public void Constructor(Vector2 position)
    {
        transform.position = position;
    }

    public class Factory : PlaceholderFactory<Vector2, EnemyManager>
    { }
}
