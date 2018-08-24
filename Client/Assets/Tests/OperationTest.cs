using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationTest : MonoBehaviour {


    bool ConditionTrue = true;
    int switchTest = 0;

    void ConditionOperate()
    {
        if(ConditionTrue)
        {
            //Do onething
        }
        else
        {
            //Do another
        }
    }

    void SwitchOperation()
    {
        switch(switchTest)
        {
            case 0:
                //one
                break;
            case 1:
                //one
                break;
            case 2:
                //one
                break;
            default:
                break;
        }
    }




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class ConditionOperation
{
    public void Do(bool condition,Event One,Event two)
    {

    }
}

