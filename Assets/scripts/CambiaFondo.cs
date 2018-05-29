using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaFondo : MonoBehaviour {

	public Camera camara;

	private int CambiaColor;
	private byte R = 0;
	private byte G =0;
	private byte B= 0;
	private byte sumRes = 0;
	private System.Random ran;

	// Use this for initialization
	void Start () {
		CambiaColor = 0;
		ran = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {

		if (CambiaColor < 30)
		{
			CambiaColor++;
		}
		else
		{
			sumRes = System.Convert.ToByte(ran.Next(0, 3));
			switch (ran.Next(0, 3))
            {
                case 0:
					R = RGBSum(sumRes,R);
                    break;
				case 1:
					G = RGBSum(sumRes, G);
                    break;
                case 2:
					B = RGBSum(sumRes, B);
                    break;
            }
			CambiaColor = 0;
			camara.backgroundColor = new Color32(R,G,B,1);
		}
	}
	byte RGBSum(byte sumRes, byte RGB){
		byte resu = RGB;
		switch(ran.Next(0, 2))
		{
			case 0:
				resu += sumRes;
				break;
			case 1:
				resu -= sumRes;
				break;
		}
		return resu;
	}
}
