using GameContracts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowView : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private Image _view;

    private void Awake()
    {
        _view.gameObject.SetActive(false);
        _group.alpha = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPlayer _))
            StartCoroutine(Showing());        
    }

    private IEnumerator Showing()
    {
        _view.gameObject.SetActive(true);

        while(_group.alpha < 1)
        {
            _group.alpha += _speed;
            yield return null;
        }
    }
}