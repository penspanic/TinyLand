using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


static class GameUtil
{
    // Returns obj's top level parent, if there's no parent, returns obj.
    public static GameObject GetTopLevelParent(GameObject obj)
    {
        GameObject currParent = obj;

        while(currParent.transform.parent != null)
        {
            if (currParent.transform.parent.gameObject.name.Equals("World"))
                break;
            currParent = currParent.transform.parent.gameObject;
        }
        return currParent;
    }
    public delegate float EasingUtilMethod(float start, float end, float value);
    public static Vector3 EasingVector3(EasingUtilMethod method,Vector3 start, Vector3 end, float value)
    {
        Vector3 returnVec = new Vector3();
        returnVec.x = method(start.x, end.x, value);
        returnVec.y = method(start.y, end.y, value);
        returnVec.z = method(start.z, end.z, value);

        return returnVec;
    }

    public static Vector2 EasingVector2(EasingUtilMethod method, Vector2 start, Vector2 end, float value)
    {
        Vector2 returnVec = new Vector2();
        returnVec.x = method(start.x, end.x, value);
        returnVec.y = method(start.y, end.y, value);

        return returnVec;
    }
    
}