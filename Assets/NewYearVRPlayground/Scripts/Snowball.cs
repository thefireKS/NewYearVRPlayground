using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Snowball : MonoBehaviour
{
    [SerializeField] private GameObject vfxSnowballDestroy;
    [SerializeField] private List<AudioClip> sfxSnowballDestroy;
    
    private Rigidbody _rigidbody;
    private XRGrabInteractable _grabInteractable;

    [SerializeField] private float throwForce;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(vfxSnowballDestroy, collision.contacts[0].point, Quaternion.identity);
        var sound = sfxSnowballDestroy[Random.Range(0, sfxSnowballDestroy.Count)];
        AudioSource.PlayClipAtPoint(sound, transform.position, 1f);
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
