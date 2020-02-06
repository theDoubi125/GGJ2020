using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintZone : MonoBehaviour
{
    public ParticleSystem system1;
    public ParticleSystem system2;
    
    private float timeWaitingToPaint = 0.5f;
    private IEnumerator waitingCoroutine;
    private Collider colliderToPaint;

    private void OnTriggerEnter(Collider other)
    {
        
        Repairable repairable= other.GetComponent<Repairable>();
        if(repairable != null && !other.isTrigger)
        {
            colliderToPaint = other;
            waitingCoroutine = PaintWithDelay(other, timeWaitingToPaint);
            StartCoroutine(waitingCoroutine);
            if(system1 != null)
            {
                system1.Play();
            }
            if(system2 != null){
                system2.Play();
            }
        }
    }

    IEnumerator PaintWithDelay(Collider colliderToPaint, float time)
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            yield return new WaitForSeconds(time);
            colliderToPaint.GetComponent<Repairable>().Repaint();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(colliderToPaint != null && waitingCoroutine !=null && colliderToPaint == other)
        {
            StopCoroutine(waitingCoroutine);
        }
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
            interactable.interactDelegate -= OnInteract;
    }

    public void OnInteract(Interactable interactable, Hand hand, Tool.ToolType toolType)
    {
        Repairable repairable = interactable.GetComponent<Repairable>();
        if(repairable != null)
        {
            repairable.RepairWithTool(toolType);
        }
    }
}