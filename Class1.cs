using System;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MenuLib;
using UnityEngine;

[BepInPlugin("h3nw.adminmenubutton", "Admin Menu Button", "1.0.0")]
[BepInDependency("nickklmao.menulib", "2.5.0")]
public class AdminMenuButton : BaseUnityPlugin
{
    internal static ManualLogSource Log;

    private void Awake()
    {
        
        Log = Logger;

        MenuAPI.AddElementToEscapeMenu(delegate(Transform parent)
        {
            MenuAPI.CreateREPOButton("Admin Menu", OnAdminMenuClicked, parent, new Vector2(126f, 62f));
        });

        Log.LogInfo("Admin Menu Button registered in escape menu.");
    }

    private static void OnAdminMenuClicked()
    {
        var closeAll = AccessTools.Method(typeof(MenuManager), "PageCloseAll");
        closeAll?.Invoke(MenuManager.instance, null);
        var menuType = Type.GetType("RepoAdminMenu.Menu, RepoAdminMenu");
        menuType.GetMethod("toggleMenu", BindingFlags.Public | BindingFlags.Static)
            ?.Invoke(null, null);
    }
}