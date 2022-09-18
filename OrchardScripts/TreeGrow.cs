using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    public List<MeshRenderer> growTreeMeshes;
    public float timeToGrow = 7f;
    public float refreshRate = 0.01f;
    [Range(0, 1)]
    public float minGrow = 0f;
    [Range(0, 1)]
    public float maxGrow = 1f;
    private List<Material> growTreeMats = new List<Material>();
    void Start()
    {
        for (int i = 0; i < growTreeMeshes.Count; i++)
        {
            for (int j = 0; j < growTreeMeshes[i].materials.Length; j++)
            {
                if (growTreeMeshes[i].materials[j].HasProperty("Grow_"))
                {
                    growTreeMeshes[i].materials[j].SetFloat("Grow_", minGrow);
                    growTreeMats.Add(growTreeMeshes[i].materials[j]);
                }
            }
        }
    }
    MeshRenderer[] mr;
    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Water"))
        if (collision.gameObject.tag == "Water")
        {
            Debug.Log(collision.gameObject.name);
            mr = gameObject.GetComponentsInChildren<MeshRenderer>();
            StartCoroutine("Delay");
            mr[1].enabled = true;
        }
    }
    
    IEnumerator Delay()
    {
        for (int i = 0; i < growTreeMats.Count; i++)
        {
            yield return StartCoroutine(Growingtree(growTreeMats[i]));
        }
    }

    IEnumerator Growingtree(Material mat)
    {
        float growValue = mat.GetFloat("Grow_");
        while (growValue < maxGrow)
        {
            growValue += 1 / (timeToGrow / refreshRate) * 3;
            mat.SetFloat("Grow_", growValue);

            yield return new WaitForSeconds(refreshRate);
        }
    }

}
