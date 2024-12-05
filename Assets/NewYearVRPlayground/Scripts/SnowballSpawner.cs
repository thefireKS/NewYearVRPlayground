using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjectSpawner : XRBaseInteractable
{
    [SerializeField] private GameObject objectPrefab; 
    [SerializeField] private Transform spawnPoint;
    
    private string _tag;
    
    protected override void Awake()
    {
        _tag = objectPrefab.tag;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Object spawner Activated");
        base.OnSelectEntered(args);

        // Получаем руку, которая активировала взаимодействие
        var interactor = args.interactorObject as XRBaseInteractor;
        
        if (interactor == null) return;

        Debug.Log($"{interactor.hasSelection} | {CheckSelectedIsSpawnedObject(interactor.interactablesSelected)}");
        // Проверяем, занята ли рука
        if (interactor.hasSelection && CheckSelectedIsSpawnedObject(interactor.interactablesSelected)) return;
        Debug.Log("Hand haven't spawned object");
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        var grabInteractable = spawnedObject.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            interactor.interactionManager.SelectEnter(args.interactorObject, grabInteractable);
            Debug.Log("Spawned object grabbed");
        }
    }

    private bool CheckSelectedIsSpawnedObject(List<IXRSelectInteractable> interactables)
    {
        return interactables.Any(interactable => interactable.transform.CompareTag(_tag));
    }
}