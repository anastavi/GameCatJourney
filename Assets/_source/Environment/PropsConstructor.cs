using UnityEngine;

public class PropsConstructor : MonoBehaviour
{
    [SerializeField] private Transform _playerPoint;
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private BuildingView[] _propsView;

    private void Awake()
    {
        for (int i = 0, length = _propsView.Length; i < length; i++)
            _propsView[i].Init(_playerPoint, _playerRenderer);
    }

    [ContextMenu(nameof(FindAll))]
    private void FindAll()
    {
        var props = FindObjectsByType<BuildingView>(FindObjectsSortMode.None);
        _propsView = props;
    }
}