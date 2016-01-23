using UnityEngine;
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
}