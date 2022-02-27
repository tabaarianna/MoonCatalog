using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;

public class HelloWorld : MonoBehaviour
{

    // Catalog info
    public string catalogName = "Moon";
    private List<CatalogItem> catalog;
    private List<Moon> moon = new List<Moon>();


    private void Update()
    {
        // Catalog button
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetCatalog();
        }

    }
    public void GetCatalog()
    {
        GetCatalogItemsRequest getCatalogRequest = new GetCatalogItemsRequest
        {
            CatalogVersion = catalogName
        };

        PlayFabClientAPI.GetCatalogItems(getCatalogRequest,
            result =>
            {
                catalog = result.Catalog;
            },
            error => Debug.Log(error.ErrorMessage)
        );

        Invoke("SplitCatalog", 3f);
    }

    public void SplitCatalog()
    {
        foreach (CatalogItem item in catalog)
        {
            Moon m = JsonUtility.FromJson<Moon>(item.CustomData);
            m.name = item.DisplayName;
            m.description = item.Description;
            moon.Add(m);
        }

        ShowCatalog();
    }

    public void ShowCatalog()
    {
        foreach (Moon m in moon)
        {
            string logMsg = "" + m.name + "";
            logMsg += "\n" + m.description;
            logMsg += "\ndistance: " + m.distance;
            logMsg += "\ngravity: " + m.gravity;

            Debug.Log(logMsg);

        }
    }


    public class Moon
    {
        public string name;
        public string description;
        public string distance;
        public string gravity;

    }
}
