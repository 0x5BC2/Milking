using Rocket.Core.Plugins;
using SDG.Unturned;
using UnityEngine;

namespace Milking;

/// <summary>
/// RocketMod Version of Milking by ai-kana;
/// Inspired by: https://github.com/ai-kana/Milking/tree/main
/// </summary>

public class Main : RocketPlugin
{
    protected override void Load()
        => PlayerInput.onPluginKeyTick += HandleTick;

    protected override void Unload()
        => PlayerInput.onPluginKeyTick -= HandleTick;
    
    private static void HandleTick(Player Player, uint Sim, byte Key, bool State)
    {
        if (Key != 0) return;
        if (!State) return;
        if (Player.equipment.itemID != 337) return;

        var results = new RaycastHit[2];
        if (Physics.RaycastNonAlloc(new Ray(Player.look.aim.position, Player.look.aim.forward), results, 10f, RayMasks.PLAYER_INTERACT) == 0) return;

        Player? found = null;
        foreach (var hit in results)
        {
            if (hit.transform == null)
                continue;

            found = DamageTool.getPlayer(hit.transform);
            if (found == null)
                continue;
            
            if (found == Player)
                continue;
        }

        if (found == null || found == Player)
            return;
        
        Player.inventory.forceAddItem(new(462, true), true);
    }
}