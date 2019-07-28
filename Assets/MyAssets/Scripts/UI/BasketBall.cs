using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasketBall : MonoBehaviour //篮球
{
    //值类型    1010101111110110
    public int i; //1111111111111111111111111111111111111111111
    public float mass;
    public short s;
    public double d;


    //引用类型可以为空，值类型不能为空
    public BasketBall ball;
    public string name;

    public static string SportType = "BallSport";

    //重量
    //半径
    //logo


    public BasketBall()
    {
    }

    public BasketBall(string _name)
    {
        name = _name;
    }

    public BasketBall(float _mass)
    {
        mass = _mass + 1;
    }
}
