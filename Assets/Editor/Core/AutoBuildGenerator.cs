using UnityEngine;
using UnityEditor;
using System.IO;

public static class AutoBuildGenerator
{
    static string CTRL_TEMPLATE_PATH = "Assets/Scripts/Core/UICtrlTemplate.cs";
    static string VIEW_TEMPLATE_PATH = "Assets/Scripts/Core/UIViewTemplate.cs";
    static string PROXY_TEMPLATE_PATH = "Assets/Scripts/Core/UITemplateProxy.cs";

    static string CTRL_PATH = "Assets/Scripts/UI/Ctrl/";
    static string VIEW_PATH = "Assets/Scripts/UI/View/";
    static string PROXY_PATH = "Assets/Scripts/UI/Proxy/";
    [MenuItem("CONTEXT/RectTransform/BuildUIScript")]
    private static void BuildUIScript()
    {
        BuildUIScript(true);
    }
    [MenuItem("CONTEXT/RectTransform/BuildUIScriptWithoutProxy")]
    private static void BuildUIScriptWithoutProxy()
    {
        BuildUIScript(false);
    }
    static void BuildUIScript(bool isGenProxy)
    {
        var objName = Selection.activeGameObject.name;
        // var relativePath = Application.dataPath;
        // Debug.Log($"name:{objName} relativePath:{relativePath}");
        var newCtrlName = objName;
        var UIName = objName.Substring(objName.IndexOf("UICtrl")+6);
        // Debug.Log($"UIName:{UIName}");
        var newProxyName = "UI"+UIName+"Proxy";
        var newViewName = "UIView"+UIName;
        var newDataName = "UI"+UIName+"Data";
        //create uictrl
        var ctrltempfil = new FileInfo(CTRL_TEMPLATE_PATH);
        var ctrlContent = "";
        using (StreamReader sr = ctrltempfil.OpenText())
        {
            ctrlContent = sr.ReadToEnd();
            ctrlContent = ctrlContent.Replace("UITemplateData", newDataName);
            ctrlContent = ctrlContent.Replace("UICtrlTemplate", newCtrlName);
            ctrlContent = ctrlContent.Replace("UIViewTemplate", newViewName);
            ctrlContent = ctrlContent.Replace("UITemplateProxy", newProxyName);
        }
        // Debug.Log(ctrlContent);
        var ctrlfileName = CTRL_PATH + newCtrlName+".cs";
        var ctrlfil = new FileInfo(ctrlfileName);
        using (StreamWriter sw = ctrlfil.CreateText())
        {
            sw.Write(ctrlContent);
        }
        //create uiview
        var viewTempfil = new FileInfo(VIEW_TEMPLATE_PATH);
        var viewContent = "";
        using (StreamReader sr = viewTempfil.OpenText())
        {
            viewContent = sr.ReadToEnd();
            viewContent = viewContent.Replace("UIViewTemplate", newViewName);
        }
        // Debug.Log(viewContent);
        var viewfileName = VIEW_PATH + newViewName+".cs";
        var viewfil = new FileInfo(viewfileName);
        using (StreamWriter sw = viewfil.CreateText())
        {
            sw.Write(viewContent);
        }
        //create uiproxy
        if(isGenProxy)
        {
            var proxyTempfil = new FileInfo(PROXY_TEMPLATE_PATH);
            var proxyContent = "";
            using (StreamReader sr = proxyTempfil.OpenText())
            {
                proxyContent = sr.ReadToEnd();
                proxyContent = proxyContent.Replace("UITemplateProxy", newProxyName);
                proxyContent = proxyContent.Replace("UITemplateData", newDataName);
            }
            // Debug.Log(proxyContent);
            var proxyfileName = PROXY_PATH + newProxyName+".cs";
            var proxyfil = new FileInfo(proxyfileName);
            using (StreamWriter sw = proxyfil.CreateText())
            {
                sw.Write(proxyContent);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Build {newCtrlName} {newViewName} {newProxyName}");
    }
}
