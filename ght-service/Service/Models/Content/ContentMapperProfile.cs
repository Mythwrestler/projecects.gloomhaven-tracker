using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Service.Models.Content;

public class ContentMapperProfile : Profile
{
    public ContentMapperProfile()
    {
        CreateMap<GameDAO, Game>();

        CreateMap<EFFECT_TYPE_DAO, EFFECT_TYPE>();


        #region Monster Mapping

        CreateMap<MonsterAttackEffectDAO, Effect>()
            .ConvertUsing((src, dst, ctx) =>
            {
                return new Effect()
                {
                    Duration = src.Effect.Duration,
                    Type = ctx.Mapper.Map<EFFECT_TYPE>(src.Effect.Type),
                    Value = src.Effect.Value
                };
            });
        CreateMap<MonsterDefenseEffectDAO, Effect>()
            .ConvertUsing((src, dst, ctx) =>
            {
                return new Effect()
                {
                    Duration = src.Effect.Duration,
                    Type = ctx.Mapper.Map<EFFECT_TYPE>(src.Effect.Type),
                    Value = src.Effect.Value
                };
            });
        CreateMap<MonsterDeathEffectDAO, Effect>()
            .ConvertUsing((src, dst, ctx) =>
            {
                return new Effect()
                {
                    Duration = src.Effect.Duration,
                    Type = ctx.Mapper.Map<EFFECT_TYPE>(src.Effect.Type),
                    Value = src.Effect.Value
                };
            });

        CreateMap<MonsterDAO, Monster>()
            .ConvertUsing((src, dst, ctx) =>
            {
                return new Monster()
                {
                    ContentCode = src.ContentCode,
                    Description = src.Description,
                    Name = src.Name,
                    BaseStats = new BaseMonsterStatSet()
                    {
                        Elite = src.BaseStats.Where(stat => stat.IsElite).Select(stat => new MonsterStatSet()
                        {
                            Level = stat.Level,
                            Attack = stat.Attack,
                            Health = stat.Health,
                            Movement = stat.Movement,
                            RangeAttackable = stat.RangeAttackable,
                            MeleeAttackable = stat.MeleeAttackable,
                            AttackEffects = ctx.Mapper.Map<List<Effect>>(stat.AttackEffects.ToList()),
                            DefenseEffects = ctx.Mapper.Map<List<Effect>>(stat.DefenseEffects.ToList()),
                            DeathEffects = ctx.Mapper.Map<List<Effect>>(stat.DeathEffects.ToList()),
                            Immunity = ctx.Mapper.Map<List<EFFECT_TYPE>>(stat.Immunity.Select(e => e.Effect).ToList())
                        }).ToList(),
                        Standard = src.BaseStats.Where(stat => !stat.IsElite).Select(stat => new MonsterStatSet()
                        {
                            Level = stat.Level,
                            Attack = stat.Attack,
                            Health = stat.Health,
                            Movement = stat.Movement,
                            RangeAttackable = stat.RangeAttackable,
                            MeleeAttackable = stat.MeleeAttackable,
                            AttackEffects = ctx.Mapper.Map<List<Effect>>(stat.AttackEffects.ToList()),
                            DefenseEffects = ctx.Mapper.Map<List<Effect>>(stat.DefenseEffects.ToList()),
                            DeathEffects = ctx.Mapper.Map<List<Effect>>(stat.DeathEffects.ToList()),
                            Immunity = ctx.Mapper.Map<List<EFFECT_TYPE>>(stat.Immunity.Select(e => e.Effect).ToList())
                        }).ToList()
                    }
                };
            });

        #endregion

        #region Character Mapping

        CreateMap<CharacterDAO, Character>().ConvertUsing((src, dst, ctx) => {
            return new Character()
            {
                ContentCode = src.ContentCode,
                Name = src.Name,
                Description = src.Description,
                BaseStats = new BaseCharacterStats()
                {
                    Levels = src.BaseStats.Select(stat => new CharacterLevel(){Level = stat.Level, Experience = stat.Experience}).ToList(),
                    Health = src.BaseStats.Select(stat => new BaseCharacterHealth(){Level = stat.Level, Health = stat.Health}).ToList(),
                }
            };
        });


        #endregion


        #region Objective Mapping

        CreateMap<ObjectiveDAO, Objective>();

        #endregion



        #region Scenario Mapping

        CreateMap<ScenarioMonsterDAO, Monster>().ConvertUsing((src, dst, ctx) => {
            return ctx.Mapper.Map<Monster>(src.Monster);
        });

        CreateMap<ScenarioDAO, Scenario>();

        #endregion


    }
}