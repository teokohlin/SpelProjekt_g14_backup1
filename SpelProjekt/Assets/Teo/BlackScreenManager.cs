using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenManager : MonoBehaviour
{
   public GameObject blackScreen;
   public bool isBlack = false;
   public Door_Interactable door;
   private void Start()
   {
      StartCoroutine(FadeBlackScreen(false));
      door = FindObjectOfType<Door_Interactable>();
   }

    public void Update()
    {
       if (door.teleport)
       {
          StartCoroutine(FadeBlackScreen(false));
       }
    }

   public IEnumerator FadeBlackScreen(bool fadeToBlack = true, int fadeSpeed = 5)
   {
      Color imageColor = blackScreen.GetComponent<Image>().color;
      float fadeAmount;

      if (fadeToBlack)
      {
         while (blackScreen.GetComponent<Image>().color.a < 1)
         {
            fadeAmount = imageColor.a + (fadeSpeed * Time.deltaTime);

            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = imageColor;
            isBlack = true;
            yield return null;
         }
      }
      else
      {
         while (blackScreen.GetComponent<Image>().color.a > 0)
         {
            fadeAmount = imageColor.a - (fadeSpeed * Time.deltaTime);

            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = imageColor;
            isBlack = false;
            yield return null;
         }
      }
   }
}
