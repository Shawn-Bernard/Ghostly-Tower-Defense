using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface ISelectable
{
    public abstract void Selected();

    public abstract void Unselected();

    public abstract void PickUp();


}
