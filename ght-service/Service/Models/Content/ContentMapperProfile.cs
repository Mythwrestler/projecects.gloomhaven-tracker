using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Service.Models.Content;

public class ContentMapperProfile : Profile
{
    public ContentMapperProfile()
    {
        #region Element Mapping
        CreateMap<ELEMENT_DAO, ELEMENT>().ReverseMap();
        #endregion

        #region Effect Mapping

        CreateMap<EFFECT_TYPE_DAO, EFFECT_TYPE>().ReverseMap();

        CreateMap<EffectDAO, Effect>()
            .ConvertUsing((src, dst, ctx) =>
            {
                return new Effect(
                    id: src.Id,
                    type: ctx.Mapper.Map<EFFECT_TYPE>(src.Type),
                    value: src.Value,
                    duration: src.Duration,
                    range: src.Range,
                    element: ctx.Mapper.Map<ELEMENT?>(src.Element)
                );
            });

        CreateMap<Effect, EffectDAO>()
            .ConvertUsing((src, dst, ctx) => new EffectDAO(){
                Id = src.Id,
                Type = ctx.Mapper.Map<EFFECT_TYPE_DAO>(src.Type),
                Value = src.Value,
                Duration = src.Duration,
                Range = src.Range,
                Element = ctx.Mapper.Map<ELEMENT_DAO?>(src.Element)
            });

        #endregion

        #region Attack Modifier Mapping

        CreateMap<AttackModifierDAO, AttackModifier>().ConvertUsing((src, dst, ctx) => new AttackModifier
        (
            id: src.Id,
            contentCode: src.ContentCode,
            name: src.Name,
            description: src.Description,
            isCurse: src.IsCurse,
            isBlessing: src.IsBlessing,
            triggerShuffle: src.TriggerShuffle,
            value: src.Value,
            effects: ctx.Mapper.Map<List<Effect>>(src.Effects.ToList()),
            gameContentCode: src.Game.ContentCode
        ));

        #endregion

        #region Game Mapping

        CreateMap<GameBaseAttackModifierDAO, AttackModifier>().ConvertUsing((src, dst, ctx) => ctx.Mapper.Map<AttackModifier>(src.AttackModifier));

        CreateMap<GameDAO, Game>().ReverseMap();

        #endregion

        #region Monster Mapping

        CreateMap<MonsterAttackEffectDAO, Effect>().ConvertUsing((src, dst, ctx) => ctx.Mapper.Map<Effect>(src.Effect));
        CreateMap<MonsterDefenseEffectDAO, Effect>().ConvertUsing((src, dst, ctx) => ctx.Mapper.Map<Effect>(src.Effect));
        CreateMap<MonsterDeathEffectDAO, Effect>().ConvertUsing((src, dst, ctx) => ctx.Mapper.Map<Effect>(src.Effect));

        CreateMap<MonsterDAO, Monster>().ConvertUsing((src, dst, ctx) => new Monster
        (
            id: src.Id,
            contentCode: src.ContentCode,
            description: src.Description,
            name: src.Name,
            gameContentCode: src.Game.ContentCode,
            baseStats: new BaseMonsterStatSet
            (
                elite: src.BaseStats.Where(stat => stat.IsElite).Select(stat => new MonsterStatSet
                (
                    level: stat.Level,
                    attack: stat.Attack,
                    health: stat.Health,
                    movement: stat.Movement,
                    rangeAttackable: stat.RangeAttackable,
                    meleeAttackable: stat.MeleeAttackable,
                    attackEffects: ctx.Mapper.Map<List<Effect>>(stat.AttackEffects.ToList()),
                    defenseEffects: ctx.Mapper.Map<List<Effect>>(stat.DefenseEffects.ToList()),
                    deathEffects: ctx.Mapper.Map<List<Effect>>(stat.DeathEffects.ToList()),
                    immunity: ctx.Mapper.Map<List<EFFECT_TYPE>>(stat.Immunity.Select(e => e.Effect).ToList())
                )).ToList(),
                standard: src.BaseStats.Where(stat => !stat.IsElite).Select(stat => new MonsterStatSet
                (
                    level: stat.Level,
                    attack: stat.Attack,
                    health: stat.Health,
                    movement: stat.Movement,
                    rangeAttackable: stat.RangeAttackable,
                    meleeAttackable: stat.MeleeAttackable,
                    attackEffects: ctx.Mapper.Map<List<Effect>>(stat.AttackEffects.ToList()),
                    defenseEffects: ctx.Mapper.Map<List<Effect>>(stat.DefenseEffects.ToList()),
                    deathEffects: ctx.Mapper.Map<List<Effect>>(stat.DeathEffects.ToList()),
                    immunity: ctx.Mapper.Map<List<EFFECT_TYPE>>(stat.Immunity.Select(e => e.Effect).ToList())
                )).ToList()
            )
        ));

        #endregion

        #region Character Mapping

        CreateMap<CharacterDAO, Character>().ConvertUsing((src, dst, ctx) => new Character
        (
            id: src.Id,
            contentCode: src.ContentCode,
            name: src.Name,
            description: src.Description,
            gameContentCode: src.Game.ContentCode,
            baseStats: new BaseCharacterStats(
                levels: src.BaseStats.Select(stat => new CharacterLevel(
                    level: stat.Level,
                    experience: stat.Experience
                )).ToList(),
                health: src.BaseStats.Select(stat => new BaseCharacterHealth(
                    level: stat.Level,
                    health: stat.Health
                )).ToList()
            )
        ));


        #endregion

        #region Objective Mapping

        CreateMap<ObjectiveDAO, Objective>().ConvertUsing((src, dst, ctx) => new Objective(
            id: src.Id,
            contentCode: src.ContentCode,
            name: src.Name,
            description: src.Description,
            rangeAttackable: src.RangeAttackable,
            meleeAttackable: src.MeleeAttackable,
            health: src.Health,
            gameContentCode: src.Game.ContentCode
        ));

        #endregion

        #region Scenario Mapping

        CreateMap<ScenarioMonsterDAO, Monster>().ConvertUsing((src, dst, ctx) => ctx.Mapper.Map<Monster>(src.Monster));
        CreateMap<ScenarioObjectiveDAO, Objective>().ConvertUsing((src, dst, ctx) => ctx.Mapper.Map<Objective>(src.Objective));

        CreateMap<ScenarioDAO, Scenario>().ConvertUsing((src, dst, ctx) => new Scenario
        (
            id: src.Id,
            contentCode: src.ContentCode,
            name: src.Name,
            description: src.Description,
            scenarioNumber: src.ScenarioNumber,
            goal: src.Goal,
            cityMapLocation: src.CityMapLocation,
            scenarioBook: src.ScenarioBookPages,
            supplementalBook: src.SupplementalBookPages,
            monsters: ctx.Mapper.Map<List<Monster>>(src.Monsters),
            objectives: ctx.Mapper.Map<List<Objective>>(src.Objectives),
            gameContentCode: src.Game.ContentCode
        ));

        CreateMap<Scenario, ScenarioDAO>().ConvertUsing((src, dst, ctx) => {
            ScenarioDAO scenarioDAO = new ScenarioDAO(){
                Id = src.Id,
                ContentCode = src.ContentCode,
                Name = src.Name,
                Description = src.Description,
                ScenarioNumber = src.ScenarioNumber,
                Goal = src.Goal,
                CityMapLocation = src.CityMapLocation,
                ScenarioBookPages = src.ScenarioBook,
                SupplementalBookPages = src.SupplementalBook,
            };
            return scenarioDAO;
        });

        #endregion

    }
}