using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using UnityEngine.UI;

public class HeatlhManager : MonoBehaviour
{

    public int health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    
    // Update is called once per frame
    void Update()
    {
        var player = model.player;
        health = player.health.GetHP();
        
        foreach (var img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (var i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}



