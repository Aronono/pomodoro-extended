using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIModeResolver : MonoBehaviour
{

    public void ResolveMode()
    {

        foreach (var text in GetComponentsInChildren<TMP_Text>())
        {
            text.color = new Color(255 - text.color[0], 255 - text.color[1], 255 - text.color[2], 1);
        }

        foreach (var image in GetComponentsInChildren<Image>())
        {
            if (image.color[3] != 0) image.color = new Color(1 - image.color[0], 1 - image.color[1], 1 - image.color[2], 1);
        }
    }

}
