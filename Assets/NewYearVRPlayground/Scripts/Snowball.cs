using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Snowball : MonoBehaviour
{
    [SerializeField] private GameObject vfxSnowballDestroy;
    
    private Rigidbody _rigidbody;
    private XRGrabInteractable _grabInteractable;

    [SerializeField] private float throwForce;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(vfxSnowballDestroy, collision.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void ThrowSnowball()
    {
        IXRSelectInteractor interactor = _grabInteractable.firstInteractorSelecting;
        if (interactor != null)
        {
            _grabInteractable.interactionManager.SelectExit(interactor, _grabInteractable);
        }
        _rigidbody.AddForce(throwForce*transform.forward, ForceMode.Impulse);
    }

}
