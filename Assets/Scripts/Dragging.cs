using UnityEngine;

public class Dragging : MonoBehaviour
{
    private float _dist, _currentVelocity;
    private bool _dragging = false, _canMoveForward = false, _canMoveBack = false;
    private Vector3 _offset;
    [SerializeField] private Transform toDrag;
    [SerializeField] private BezierMovement movement;
    private Camera _camera;

    // Update is called once per frame
    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_canMoveForward)
        {
            float currentPos = Mathf.SmoothDamp(movement.t, 1, ref _currentVelocity, 10 * Time.deltaTime);
            movement.t = currentPos;
            if (movement.t <= 0.99f)
                return;
            _canMoveBack = true;
            _canMoveForward = false;
        }

        if (_canMoveBack)
        {
            float currentPos = Mathf.SmoothDamp(movement.t, 0, ref _currentVelocity, 10 * Time.deltaTime);
            movement.t = currentPos;
            if (movement.t >= 0.01f)
                return;
            _canMoveBack = false;
        }

        Vector3 v3;
        if (Input.touchCount != 1)
        {
            _dragging = false;
            return;
        }

        movement.DrawLine();
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            _dist = toDrag.transform.position.z - _camera.transform.position.z;
            v3 = new Vector3(pos.x, pos.y, _dist);
            v3 = _camera.ScreenToWorldPoint(v3);
            _offset = toDrag.position - v3;
            _dragging = true;
        }

        if (_dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _dist);
            v3 = _camera.ScreenToWorldPoint(v3);
            Vector3 targetPos = v3 + _offset;

            float X = Mathf.Clamp(targetPos.x, -3f, 3f);
            targetPos = new Vector3(X, targetPos.y, targetPos.z);
            float Y = Mathf.Clamp(targetPos.y, -0.15f, 2.5f);
            targetPos = new Vector3(targetPos.x, Y, targetPos.z);
            toDrag.position = targetPos;
        }

        if (!_dragging || touch.phase is not (TouchPhase.Ended or TouchPhase.Canceled)) return;
        _canMoveForward = true;
        _dragging = false;
    }
}