﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;//シャッフルする時に必要
using System;

public class Order
{
    private int[] num = new int[4] { 0, 1, 2, 3 };
    private int[] num2;
    private int count;
    private int index;

    public Order()
    {
        RandomBalloon();
    }

    public int GetSprite()
    {
        index = Array.IndexOf(num, count);
        count++;
        return index;
    }

    //ランダムに表示させる
    public void RandomBalloon()
    {
        num = num.OrderBy(i => Guid.NewGuid()).ToArray();
        count = 0;
    }

    /*public void RandomBalloon()
    {
        num = (Common.Instance.Shuffle(num));
    }*/
}

