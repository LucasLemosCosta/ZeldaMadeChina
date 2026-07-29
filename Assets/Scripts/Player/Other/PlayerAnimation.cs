using UnityEngine;

public class PlayerAnimation : EntityAnimation
{
    private PlayerController player;

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerController>();
    }

    public void EquipedWeapon()
    {
        player.SwordEquiped = true;
    }
}
