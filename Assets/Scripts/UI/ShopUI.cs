using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public RectTransform firstSelect;
    public RectTransform secondSelect;
    public RectTransform skinSelect;
    public GameObject firstColorList;
    public GameObject secondColorList;
    public GameObject skinList;

    public Image skin;
    public Image skinFirst;
    public Image skinSecond;

    public GameObject colorUnlockPanel;
    public GameObject skinUnlockPanel;
    public Text playerPoints;

    private static List<ShopItem> shopItems;
    private int mechanicID = 0;
    private int colorCost = 20;
    private int colorSelected = -1;
    private SkinInfo skinInfo;

    private Sprite[] skinSprites;
    private Sprite[] firstSprites;
    private Sprite[] secondSprites;

    private void Start()
    {
        if (shopItems == null)
            shopItems = SQLDataBase.LoadShopItems();

        skinInfo = GameSettings.Instance.skinInfo[mechanicID];
        playerPoints.text = "Points: " + PlayerPrefsManager.GetPlayerPoints();

        firstSelect.SetParent(firstColorList.transform);
        secondSelect.SetParent(secondColorList.transform);

        SetColorsList();
        LoadSkins();
        Invoke("SetSelectionsPos", 0.1f);
    }

    public void LoadMenu()
    {
        SceneLoader.LoadLevel("Menu");
    }

    public void SetSelectionsPos()
    {
        SetSelectedColorPos();
        skinSelect.anchoredPosition = skinList.transform.GetChild(skinInfo.skinSelected).GetComponent<RectTransform>().anchoredPosition;
    }

    private void SetColorsList()
    {

        foreach (ShopItem color in shopItems.FindAll(item => item.category == 10))
        {
            if (color.unlocked)
            {
                Destroy(firstColorList.transform.GetChild(color.number).transform.GetChild(0).gameObject);
                Destroy(secondColorList.transform.GetChild(color.number).transform.GetChild(0).gameObject);
            }
        }

        SetSelectedColorPos();
    }

    private void SetSelectedColorPos()
    {
        firstSelect.anchoredPosition = firstColorList.transform.GetChild(skinInfo.firstColorIndex).GetComponent<RectTransform>().anchoredPosition;
        secondSelect.anchoredPosition = secondColorList.transform.GetChild(skinInfo.secondColorIndex).GetComponent<RectTransform>().anchoredPosition;
    }

    private void LoadSkins() //POPRAWIĆ ŚCIEŻKĘ DO SKINÓW
    {
        skinSprites = Resources.LoadAll<Sprite>("Skins/" + skinInfo.spriteName);
        firstSprites = Resources.LoadAll<Sprite>("Skins/" + skinInfo.spriteName+"_first");
        secondSprites = Resources.LoadAll<Sprite>("Skins/" + skinInfo.spriteName+"_second");

        for (int i = 0; i < skinSprites.Length; i++)
        {
            Transform skin = skinList.transform.GetChild(i);
            skin.GetComponent<Image>().sprite = skinSprites[i];
            if (shopItems.Find(item => item.category == mechanicID && item.number == i && item.unlocked) != null)
                Destroy(skin.GetChild(0).gameObject);
        }

        for (int i = skinSprites.Length; i < skinList.transform.childCount; i++)
            skinList.transform.GetChild(i).gameObject.SetActive(false);

        skinFirst.color = skinInfo.firstColor;
        skinSecond.color = skinInfo.secondColor;

        skinSelect.anchoredPosition = skinList.transform.GetChild(skinInfo.skinSelected).GetComponent<RectTransform>().anchoredPosition;

        SetSpritePreview();
    }

    private void SetSpritePreview()
    {
        int id = skinInfo.skinSelected;
        skin.sprite = skinSprites[id];
        skinFirst.sprite = firstSprites[id];
        skinSecond.sprite = secondSprites[id];
    }

    public void ColorChoosed(int panel)
    {
        GameObject bttn = EventSystem.current.currentSelectedGameObject;

        if (bttn.transform.childCount > 0)
        {
            colorSelected = bttn.transform.GetSiblingIndex();
            ShowUnlockColorMessage();
            return;
        }

        if (panel == 1)
        {
            firstSelect.anchoredPosition = bttn.GetComponent<RectTransform>().anchoredPosition;
            skinInfo.firstColor = bttn.GetComponent<Image>().color;
            skinInfo.firstColorIndex = bttn.transform.GetSiblingIndex();
            GameSettings.SkinChanged(mechanicID);

            skinFirst.color = bttn.GetComponent<Image>().color;
        }
        if (panel == 2)
        {
            secondSelect.anchoredPosition = bttn.GetComponent<RectTransform>().anchoredPosition;
            skinInfo.secondColor = bttn.GetComponent<Image>().color;
            skinInfo.secondColorIndex = bttn.transform.GetSiblingIndex();
            GameSettings.SkinChanged(mechanicID);

            skinSecond.color = bttn.GetComponent<Image>().color;
        }
    }

    private void ShowUnlockColorMessage()
    {
        colorUnlockPanel.SetActive(true);
        Text messageText = colorUnlockPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        if (PlayerPrefsManager.GetPlayerPoints() < colorCost)
        {
            messageText.text = "You need " + colorCost + " points to unlock this color!";
            colorUnlockPanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            messageText.text = "Do you want to unlock this color for " + colorCost + " points?";
            colorUnlockPanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
    }

    private void ShowUnlockSkinMessage(int skinID)
    {
        int achievID = shopItems.Find(item => item.category == mechanicID && item.number == skinID).achievToUnlockID;
        skinUnlockPanel.GetComponentsInChildren<Text>()[1].text = "Complete \"" + AchievementManager.GetAchievement(achievID).name +
                                                                  "\" to unlock this skin!";

        skinUnlockPanel.SetActive(true);
    }

    public void HideUnlockSkinMessage()
    {
        skinUnlockPanel.SetActive(false);
    }

    public void ColorUnlockResponse(bool option)
    {
        if (option)
        {
            PlayerStatsistics.NewColorUnlocked();
            Destroy(firstColorList.transform.GetChild(colorSelected).transform.GetChild(0).gameObject);
            Destroy(secondColorList.transform.GetChild(colorSelected).transform.GetChild(0).gameObject);
            PlayerPrefsManager.SetPlayerPoints(PlayerPrefsManager.GetPlayerPoints() - colorCost);
            SQLDataBase.SaveShopItem(10, colorSelected);
            playerPoints.text = "Points: " + PlayerPrefsManager.GetPlayerPoints();
        }

        colorUnlockPanel.SetActive(false);
    }

    public void SkinChanged()
    {
        GameObject bttn = EventSystem.current.currentSelectedGameObject;

        if (bttn.transform.childCount > 0)
        {
            ShowUnlockSkinMessage(bttn.transform.GetSiblingIndex());
            return;
        }

        skinInfo.skinSelected = bttn.transform.GetSiblingIndex();
        skinSelect.anchoredPosition = bttn.GetComponent<RectTransform>().anchoredPosition;
        SetSpritePreview();
        GameSettings.SkinChanged(mechanicID);
    }

    public void MechanicChanged(int id)
    {
        mechanicID = id;
        skinInfo = GameSettings.Instance.skinInfo[mechanicID];
        SetSelectionsPos();
    }
}

public class ShopItem
{
    internal int category;
    internal int number;
    internal bool unlocked;
    internal int achievToUnlockID;

    public ShopItem(int cat, int num, bool unl, int id)
    {
        this.category = cat;
        this.number = num;
        this.unlocked = unl;
        this.achievToUnlockID = id;
    }
}
