using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{
    public float visibilityRadius = 5.0f;
    public float visibilityConeAngle = 45.0f;
    public string npcTag = "HERD";
    public List<GameObject> visibleGameObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        visibleGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        var entititesInRadius = entitiesInRadius();
        List<GameObject> visibleEntities = new List<GameObject>();
        foreach (var entity in entititesInRadius)
        {
            if (isVisible(entity))
            {
                visibleEntities.Append(entity);
            }
        }

        visibleGameObjects = visibleEntities;
    }

    List<GameObject> entitiesInRadius()
    {
        List<GameObject> objectsInRadius = new List<GameObject>();
        var targets = Physics.OverlapSphere(gameObject.transform.position, visibilityRadius);
        foreach (var collider in targets)
        {
            if (collider.CompareTag(npcTag))
            {
                objectsInRadius.Append(collider.gameObject);
            }
        }

        return objectsInRadius;
    }

    bool isVisible(GameObject obj)
    {
        return isInVisCone(obj) && !isOccluded(obj);
    }

    bool isOccluded(GameObject obj)
    {
        // TODO: exclude npc layer
        int mask = 0;
        mask = ~mask;
        Vector3 diff = obj.transform.position - gameObject.transform.position;
        return Physics.Raycast(gameObject.transform.position, transform.TransformDirection(diff), visibilityRadius,
            mask);
    }

    bool isInVisCone(GameObject obj)
    {
        float angle = Vector3.Angle(obj.transform.forward, gameObject.transform.forward);
        return angle <= visibilityConeAngle;
    }
        
}
