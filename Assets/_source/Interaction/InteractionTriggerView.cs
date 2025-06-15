using UnityEngine;

namespace GameInteraction
{
    public class InteractionTriggerView : MonoBehaviour
    {
        internal void Activate(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
        }

        internal void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}