using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Image image;
    public RectTransform firstSelect;
    public RectTransform secondSelect;
    public RectTransform skinSelect;
    public GameObject firstColorList;
    public GameObject secondColorList;
    public GameObject skinList;

    public Image skin;
    public Image skinFirst;
    public Image skinSecond;

    private int mechanicID = 0;
    private SkinInfo skinInfo;

    private Sprite[] skinSprites;
    private Sprite[] firstSprites;
    private Sprite[] secondSprites;

    private void Start()
    {
        skinInfo = GameSettings.Instance.skinInfo[mechanicID];
        LoadColor();
        LoadSkins();
    }

    private void Update()
    {
        LoadColor(); // TO TYLKO CHWILOWE
        skinSelect.anchoredPosition = skinList.transform.GetChild(skinInfo.skinSelected).GetComponent<RectTransform>().anchoredPosition;

    }

    public void LoadMenu()
    {
        SceneLoader.LoadLevel("Menu");
    }

    private void LoadColor()
    {
        firstSelect.anchoredPosition = firstColorList.transform.GetChild(skinInfo.firstColorIndex).GetComponent<RectTransform>().anchoredPosition;
        secondSelect.anchoredPosition = secondColorList.transform.GetChild(skinInfo.secondColorIndex).GetComponent<RectTransform>().anchoredPosition;
    }

    private void LoadSkins() //POPRAWIĆ ŚCIEŻKĘ DO SKINÓW
    {
        skinSprites = Resources.LoadAll<Sprite>("Sprites/" + skinInfo.spriteName);
        firstSprites = Resources.LoadAll<Sprite>("Sprites/" + skinInfo.spriteName+"_first");
        secondSprites = Resources.LoadAll<Sprite>("Sprites/" + skinInfo.spriteName+"_second");

        Image[] skinImages = skinList.GetComponentsInChildren<Image>();

        for (int i = 0; i < skinSprites.Length; i++)
        {
            skinImages[i].gameObject.SetActive(true);
            skinImages[i].sprite = skinSprites[i];
        }

        for (int i = skinSprites.Length; i < skinImages.Length; i++)
            skinImages[i].gameObject.SetActive(false);

        skinFirst.color = skinInfo.firstColor;
        skinSecond.color = skinInfo.secondColor;

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

    public void SkinChanged()
    {
        GameObject bttn = EventSystem.current.currentSelectedGameObject;
        skinInfo.skinSelected = bttn.transform.GetSiblingIndex();
        skinSelect.anchoredPosition = bttn.GetComponent<RectTransform>().anchoredPosition;
        SetSpritePreview();
        GameSettings.SkinChanged(mechanicID);
    }

    public void MechanicChanged(int id)
    {
        mechanicID = id;
        skinInfo = GameSettings.Instance.skinInfo[mechanicID];
        LoadSkins();
        LoadColor();
    }
}
