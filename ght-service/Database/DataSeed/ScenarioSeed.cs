using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;


public static partial class ContentSeedData
{
    private static void SeedScenarios(ContentContext context)
    {
        Jaws_RoadsideAmbush(context);
    }

    private static bool ScenarioExists(ContentContext context, string contentCode, Game game)
    {
        var check = context.Scenario.Local.FirstOrDefault(scenario => scenario.ContentCode == contentCode && scenario.Game.Id == game.Id);
        if(check is null) check = context.Scenario.FirstOrDefault(scenario => scenario.ContentCode == contentCode && scenario.Game.Id == game.Id);
        if(check is null) return false;
        return true;
    }
    
    private static Scenario BuildScenario(Game game, string contentCode, string name, string description, string goal, int scenarioNumber, List<int> scenarioBookPages, List<int> supplementalBookPages) =>
        new Scenario(){Game = game, ContentCode = contentCode, Name = name, Description = description, Goal = goal, ScenarioNumber = scenarioNumber, ScenarioBookPages = scenarioBookPages, SupplementalBookPages = supplementalBookPages};

    private static List<ScenarioMonster> BuildScenarioMonsters(ContentContext context, Scenario scenario, List<string> contentCodes) 
    {
        List<Monster> monsters = context.Monster.Where(monster => contentCodes.Contains(monster.ContentCode)).ToList();
        monsters.AddRange(context.Monster.Local.Where(monster => contentCodes.Contains(monster.ContentCode)));
        return monsters.Select(monster => new ScenarioMonster()
        {
            Scenario = scenario,
            ScenarioId = scenario.Id,
            Monster = monster,
            MonsterId = monster.Id
        }).ToList();
    }

    private static List<ScenarioObjective> BuildScenarioObjectives(ContentContext context, Scenario scenario, List<string> contentCodes)
    {
        List<Objective> objectives = context.Objective.Where(objective => contentCodes.Contains(objective.ContentCode)).ToList();
        objectives.AddRange(context.Objective.Local.Where(objective => contentCodes.Contains(objective.ContentCode)));
        return objectives.Select(objective => new ScenarioObjective()
        {
            Scenario = scenario,
            ScenarioId = scenario.Id,
            Objective = objective,
            ObjectiveId = objective.Id
        }).ToList();
    }

    private static void Jaws_RoadsideAmbush(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(ScenarioExists(context, "roadside_ambush", game)) return;

        var scenario = BuildScenario(
            game,
            "roadside_ambush",
            "Roadside Ambush",
            "Scenario - Roadside Ambush",
            "Kill all enemies.",
            1,
            new List<int>(){2,3},
            new List<int>()
        );
        scenario.Monsters = BuildScenarioMonsters(context, scenario, new List<string>(){"vermling_raider"});
        scenario.Objectives = BuildScenarioObjectives(context, scenario, new List<string>());
        context.Scenario.Add(scenario);
    }

}