using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    public int mechanicID = -1;

    public SpriteRenderer skin;
    public SpriteRenderer skinFirst;
    public SpriteRenderer skinSecond;

	void Start ()
    {
        SkinInfo skinInfo = GameSettings.Instance.skinInfo[mechanicID];

        skin.sprite = Resources.LoadAll<Sprite>("Sprites/" + skinInfo.spriteName)[skinInfo.skinSelected];
        skinFirst.sprite = Resources.LoadAll<Sprite>("Sprites/" + skinInfo.spriteName + "_first")[skinInfo.skinSelected];
        skinSecond.sprite = Resources.LoadAll<Sprite>("Sprites/" + skinInfo.spriteName + "_second")[skinInfo.skinSelected];

        skinFirst.color = skinInfo.firstColor;
        skinSecond.color = skinInfo.secondColor;
    }
}
