using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateImage : MonoBehaviour {

    Image image;

    private float startTime;
    private int count = 0;

    public float aniSpeed;
    public Sprite[] aniSprite;

    void Start () {
        image = gameObject.GetComponent<Image>();
        startTime = Time.time;

    }

	void Update () {
		if(startTime + aniSpeed < Time.time)
        {
            int index = (count % aniSprite.Length);
            image.sprite = aniSprite[index];
            startTime = Time.time;
            count++;
        }
	}
}
