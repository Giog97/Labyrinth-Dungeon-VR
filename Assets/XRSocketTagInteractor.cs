using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Script che fa la stessa cosa di XRSocketInteractor, solo che permette di interagire solo agli oggetti con il Tag giusto

public class XRSocketTagInteractor : XRSocketInteractor
{
    public string targetTag;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }
}
