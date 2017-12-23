﻿using Clio.Utilities;
using ff14bot.Managers;
using ff14bot.Objects;
using ff14bot.Pathing.Avoidance;
using Sidestep.Common;
using Sidestep.Interfaces;
using Sidestep.Logging;

namespace Sidestep.Avoid
{
    /// <summary>
    /// AOE Around an object
    /// </summary>
    [Avoider(AvoiderType.CastType, 2)]
    [Avoider(AvoiderType.CastType, 5)]
    [Avoider(AvoiderType.CastType, 6)]
    public class CircleGeneric : Omen
    {
        public override AvoidInfo OmenHandle(BattleCharacter spellCaster)
        {
            if(spellCaster.SpellCastInfo.SpellData.EffectRange > 45)
                Logger.Info("Spell range is > 45. Does this require specific logic?");
            //var loc = spellCaster.SpellCastInfo.CastLocation != Vector3.Zero ? spellCaster.SpellCastInfo.CastLocation : spellCaster.Location;

            var range = Range(spellCaster, out var center);

            Logger.Info($"Avoid Cirlce: [{center}][Range: {range}]");
            var cached = spellCaster.CastingSpellId;

            return AvoidanceManager.AddAvoidLocation(
                () => spellCaster.CastingSpellId == cached,
                null,
                40f,
                bc => range + 0.5f,
                bc => center,
                () => new[] {spellCaster}
            );
            
        }
    }
}