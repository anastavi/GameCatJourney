using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Transform _checkPoint;

    private Transform _player;
    private SpriteRenderer _playerRenderer;

    private bool _isHigher;

    private bool IsHigher
    {
        get
        {
            if (_isHigher)
                return _checkPoint.position.y < _player.position.y;
            else
                return _checkPoint.position.y > _player.position.y;
        }
        set
        {
            _isHigher = !value;
            _renderer.sortingOrder = value ? _playerRenderer.sortingOrder + 1 : _playerRenderer.sortingOrder - 1;
        }
    }

    public void Init(Transform player, SpriteRenderer playerRenderer)
    {
        _player = player;
        _playerRenderer = playerRenderer;
        IsHigher = _isHigher = _checkPoint.position.y > _player.position.y;
    }

    private void FixedUpdate()
    {
        if (IsHigher)
            IsHigher = _isHigher;
    }
}