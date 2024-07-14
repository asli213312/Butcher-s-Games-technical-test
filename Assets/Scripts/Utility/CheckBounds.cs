using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckBounds : MonoBehaviour, ITickable
{
    [Zenject.Inject] private TickController _tickController;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 startDir;

    private Vector3 _currentDirection;

    private Collider _col;

    private void Awake() 
    {
        _col = GetComponent<Collider>();
    }

    private void Start() 
    {
        _tickController.Subscribe(this);
        ChangeDirection(startDir);
    }

    private void OnDestroy()
    {   
        _tickController.Unsubscribe(this);
    }

    public void Tick() 
    {
        Vector3 selectedDirection = ChangeDirection(startDir);

        //transform.position = selectedDirection;

        //Vector3 clampedPosition = ClampPositionWithinBounds(startDir);

        transform.position = selectedDirection;
    }

    private Vector3 ClampPositionWithinBounds(Vector3 position)
    {
        Vector3 clampedPosition = position;

        if (_col != null)
        {
            Bounds bounds = _col.bounds;

            if (startDir.x > 0) 
            {
                if (position.x > bounds.max.x) 
                clampedPosition.x = bounds.max.x;
            else if (position.x < bounds.min.x)
                clampedPosition.x = bounds.min.x;

                clampedPosition = new Vector3(clampedPosition.x, target.position.y, target.position.z);
            }
            else if (startDir.y > 0) 
            {
                if (position.y > bounds.max.y)
                clampedPosition.y = bounds.max.y;
            else if (position.y < bounds.min.y)
                clampedPosition.y = bounds.min.y;
            }
            else 
            {
                if (position.z > bounds.max.z)
                clampedPosition.z = bounds.max.z;
            else if (position.z < bounds.min.z)
                clampedPosition.z = bounds.min.z;

                //clampedPosition.z = Mathf.Clamp(target.position.z, bounds.min.z, bounds.max.z);

                clampedPosition = new Vector3(target.position.x, target.position.y, clampedPosition.z);
            }
        }

        return clampedPosition;
    }

    public Vector3 ChangeDirection(Vector3 direction) 
    {
        Vector3 selectedDirection = Vector3.zero;

        if (direction.x > 0) 
        {
            selectedDirection = new Vector3(transform.position.x, target.position.y, target.position.z);
        }
        else if (direction.y > 0) 
        {
            selectedDirection = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
        else 
        {
            selectedDirection = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        _currentDirection = direction;
        return selectedDirection;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 position = other.transform.position;
            Vector3 closestPoint = _col.ClosestPoint(position);

            if (position.x > _col.bounds.max.x)
                position.x = _col.bounds.max.x;
            else if (position.x < _col.bounds.min.x)
                position.x = _col.bounds.min.x;

            if (position.z > _col.bounds.max.z)
                position.z = _col.bounds.max.z;
            else if (position.z < _col.bounds.min.z)
                position.z = _col.bounds.min.z;

            other.transform.position = position;
        }
    }
}