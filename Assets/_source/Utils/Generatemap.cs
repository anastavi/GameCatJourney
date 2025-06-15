using UnityEngine;

public class Generatemap : MonoBehaviour
{
    [Header("Edge colliders")]
    [SerializeField] private BoxCollider2D _top;
    [SerializeField] private BoxCollider2D _bottom;
    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _right;
    [Header("Cinemachine confiner")]
    [SerializeField] private BoxCollider2D _confiner;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _row;
    [SerializeField] private int _column;
    [SerializeField] private float _unitPerSprite;
    [SerializeField] private Sprite[] _groundSprites;

    private float _startX;
    private float _startY;

    [ContextMenu(nameof(GenerateGround))]
    private void GenerateGround()
    {
        if (IsArraySizeCorrect() == false)
            return;

        _startX = 0f;
        _startY = _row * _unitPerSprite;

        ResizeConfiner();
        SetCollidersPosition();

        float x = _startX;
        float y = _startY;
        int spriteIndex = 0;

        while (_parent.childCount > 0)
            DestroyImmediate(_parent.GetChild(0).gameObject);

        for (int i = 0; i < _row; i++)
        {
            for (int ii = 0; ii < _column; ii++)
            {
                var renderer = new GameObject
                {
                    name = $"Ground{_groundSprites[spriteIndex].name}"
                };

                renderer.transform.SetParent(_parent);
                renderer.AddComponent<SpriteRenderer>().sprite = _groundSprites[spriteIndex];
                spriteIndex++;
                renderer.transform.position = new Vector3(x, y, 0f);
                x += _unitPerSprite;
            }

            y -= _unitPerSprite;
            x = _startX;
        }
    }

    private void ResizeConfiner()
    {
        var size = new Vector2(_row * _unitPerSprite, _column * _unitPerSprite);
        _confiner.size = size;
        _confiner.offset = size / 2;
        _confiner.transform.position = Vector3.zero;
    }

    private void SetCollidersPosition()
    {
        var bottomPosition = Vector3.zero;
        var topPosition = new Vector3(0f, _startY, 0f);
        var leftPosition = Vector3.zero;
        var rightPosition = new Vector3(_column * _unitPerSprite, 0f, 0f);

        var topBottomSize = new Vector3(_column * _unitPerSprite, 1f, 0f);
        var leftRightSize = new Vector3(1f, _row * _unitPerSprite, 0f);

        var topBottomOffset = new Vector3(_column * _unitPerSprite / 2, 0f, 0f);
        var leftRightOffset = new Vector3(0f, _row * _unitPerSprite / 2, 0f);

        _top.transform.position = topPosition;
        _top.size = topBottomSize;
        _top.offset = topBottomOffset;

        _bottom.transform.position = bottomPosition;
        _bottom.size = topBottomSize;
        _bottom.offset = topBottomOffset;

        _left.transform.position = leftPosition;
        _left.size = leftRightSize;
        _left.offset = leftRightOffset;

        _right.transform.position = rightPosition;
        _right.size = leftRightSize;
        _right.offset = leftRightOffset;
    }

    private bool IsArraySizeCorrect()
    {
        var neededCount = _row * _column;
        var isSpritesCountCorrect = _groundSprites.Length == neededCount;

        if (isSpritesCountCorrect)
            return true;

        Debug.LogError($"The number of sprites doesn't match the required number of chunks");
        return false;
    }
}