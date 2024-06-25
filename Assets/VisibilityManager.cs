using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{
    public float visibilityRadius = 5.0f;
    public float visibilityConeAngle = 45.0f;
    private List<GameObject> visibleGameObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        visibleGameObjects = new List<GameObject>();
    }

    public List<GameObject> getVisibleObjectsByTag(string tag)
    {
        List<GameObject> ret = new List<GameObject>();
        foreach (var obj in visibleGameObjects)
        {
            if (obj.CompareTag(tag))
            {
                ret.Append(obj);
            }
        }

        return ret;
    }
    
    public List<GameObject> getVisibleObjects()
    {
        return visibleGameObjects;
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

    private List<GameObject> entitiesInRadius()
    {
        List<GameObject> objectsInRadius = new List<GameObject>();
        var targets = Physics.OverlapSphere(gameObject.transform.position, visibilityRadius);
        foreach (var collider in targets)
        {
            objectsInRadius.Append(collider.gameObject);
        }

        return objectsInRadius;
    }

    private bool isVisible(GameObject obj)
    {
        return isInVisCone(obj) && !isOccluded(obj);
    }

    private bool isOccluded(GameObject obj)
    {
        // TODO: exclude npc layer
        int mask = 0;
        mask = ~mask;
        Vector3 diff = obj.transform.position - gameObject.transform.position;
        return Physics.Raycast(gameObject.transform.position, transform.TransformDirection(diff), visibilityRadius,
            mask);
    }

    private bool isInVisCone(GameObject obj)
    {
        float angle = Vector3.Angle(obj.transform.forward, gameObject.transform.forward);
        return angle <= visibilityConeAngle;
    }
        
}
