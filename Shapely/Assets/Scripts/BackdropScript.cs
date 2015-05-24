using UnityEngine;
using System.Collections;

public class BackdropScript : MonoBehaviour {

    private MeshRenderer sortSet;
    public string layerName;
    public int order;

	void Awake () 
    {
        sortSet = GetComponent<MeshRenderer>();
        sortSet.sortingLayerName = "Background";
        sortSet.sortingOrder = 2;
	}

    public void Update()
    {
        if (sortSet.sortingLayerName != layerName)
            sortSet.sortingLayerName = layerName;
        if (sortSet.sortingOrder != order)
            sortSet.sortingOrder = order;
    }

    public void OnValidate()
    {
        sortSet = GetComponent<MeshRenderer>();
        sortSet.sortingLayerName = layerName;
        sortSet.sortingOrder = order;
    }
}
