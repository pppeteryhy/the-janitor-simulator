using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour {

	void Start () {

        //用静态的对象访问非静态的函数
        /*
         * 为什么我们需要使用单例？
         * 使用单例可以使我们快速得访问到非静态的字段（变量，属性，函数）
         * 那为什么我们不直接访问静态的字段呢？
         * 因为静态的字段里面需要用到非静态的字段，所以就不能使用static修饰
         * 使用单例的格式（知道什么是正确的！！）
         * 单例的格式： 类名.静态变量名.非静态字段
         */
        UIManager.Instance.Init();
        UIManager.Instance.Push(typeof(MenuScreen), null);

        //////用于在场景中查找某一个类的对象
        //UIManager mgr = FindObjectOfType<UIManager>();
        //if(mgr != null)
        //mgr.Init();
        //UIManager.Init();

        /*
         * 
         * 静态和非静态的区别：
         * 静态（static）修饰的对象，属性，函数，可以被类直接访问，非静态的对象，属性，函数只能被对象访问。
         * 静态方法内部实现只能使用静态的对象，属性，函数
         * 
         */
	}

	void Update () {
		
	}
}
