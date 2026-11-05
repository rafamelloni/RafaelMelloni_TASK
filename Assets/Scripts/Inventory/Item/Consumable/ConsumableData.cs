using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Inventory/Consumable")]
public class ConsumableData : ItemData
{
    [SerializeField, Min(1)] private int healAmount = 25;

    public override bool TryUse(ItemUseContext context)
    {
        if (context.Health.IsFull)
            return false;

        context.Health.Heal(healAmount);
        return true;
    }
}