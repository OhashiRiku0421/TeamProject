
using System.Collections.Generic;

public interface IInventory
{
    public void AddItemToInventory(ItemData item);

    public void RemoveItemFromInventory(ItemData item);
}

