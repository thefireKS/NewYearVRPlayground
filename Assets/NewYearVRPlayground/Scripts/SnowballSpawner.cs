using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SnowballInteractable : XRBaseInteractable
{
    [SerializeField] private GameObject snowballPrefab; // Префаб снежка
    [SerializeField] private Transform spawnPoint; // Точка спавна снежков

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("SnowballSpawner Activated");
        base.OnSelectEntered(args);

        // Получаем руку, которая активировала взаимодействие
        var interactor = args.interactorObject as XRBaseInteractor;
        
        if (interactor == null) return;

        Debug.Log($"{interactor.hasSelection} | {CheckSelectedIsSnowball(interactor.interactablesSelected)}");
        // Проверяем, занята ли рука
        if (interactor.hasSelection && CheckSelectedIsSnowball(interactor.interactablesSelected)) return;
        Debug.Log("Hand is dont have a Snowball");
        // Спавним снежок
        GameObject snowball = Instantiate(snowballPrefab, spawnPoint.position, spawnPoint.rotation);

        // Захватываем снежок автоматически
        var grabInteractable = snowball.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            interactor.interactionManager.SelectEnter(args.interactorObject, grabInteractable);
            Debug.Log("Snowball grabbed");
        }
    }

    private bool CheckSelectedIsSnowball(List<IXRSelectInteractable> interactables)
    {
        return interactables.Any(interactable => interactable.transform.CompareTag("Snowball"));
    }
}