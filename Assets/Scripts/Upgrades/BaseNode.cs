using System.Collections;
using System.Collections.Generic;
using XNode;


public class BaseNode : Node
{
    public virtual PlayerUpgrade GetUpgrade()
    {
        return null;
    }
}
