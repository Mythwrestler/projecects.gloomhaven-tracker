import { getAPI } from "../../common/Utils/API";
import { Character, ContentItemSummary, Scenario } from "../../models/Content";
import * as GlobalError from "../Error";

class ContentServiceImplementation {
  // Available Games
  availableGames: ContentItemSummary[] = [];

  // Scenario Summary
  scenarioSummaries: Map<string, ContentItemSummary[]> = new Map<
    string,
    ContentItemSummary[]
  >();
  private getScenarioSummaries = (gameCode: string) => {
    if (!this.scenarioSummaries.has(gameCode))
      this.scenarioSummaries.set(gameCode, []);
    return this.scenarioSummaries.get(gameCode) as ContentItemSummary[];
  };
  private setScenarioSummaries = (
    gameCode: string,
    scenarios: ContentItemSummary[]
  ): ContentItemSummary[] => {
    this.scenarioSummaries.set(gameCode, scenarios);
    return this.scenarioSummaries.get(gameCode) as ContentItemSummary[];
  };

  // Scenario Defaults
  scenarioDefaults: Map<string, Scenario[]> = new Map<string, Scenario[]>();

  private getScenarioDefaults = (gameCode: string) => {
    if (!this.scenarioDefaults.has(gameCode))
      this.scenarioDefaults.set(gameCode, []);
    return this.scenarioDefaults.get(gameCode) as Scenario[];
  };
  private addScenarioDefault = (
    gameCode: string,
    scenario: Scenario
  ): Scenario[] => {
    let defaultList = [...this.getScenarioDefaults(gameCode)];
    defaultList = defaultList.filter(
      (scenarioFromList) =>
        scenarioFromList.contentCode !== scenario.contentCode
    );
    defaultList.push(scenario);
    this.scenarioDefaults.set(gameCode, defaultList);
    return defaultList;
  };

  // Character Summary
  characterSummaries: Map<string, ContentItemSummary[]> = new Map<
    string,
    ContentItemSummary[]
  >();

  private getCharacterSummaries = (gameCode: string) => {
    if (!this.characterSummaries.has(gameCode))
      this.characterSummaries.set(gameCode, []);
    return this.characterSummaries.get(gameCode) as ContentItemSummary[];
  };

  private setCharacterSummaries = (
    gameCode: string,
    characters: ContentItemSummary[]
  ): ContentItemSummary[] => {
    this.characterSummaries.set(gameCode, characters);
    return this.characterSummaries.get(gameCode) as ContentItemSummary[];
  };

  // Character Defaults
  characterDefaults: Map<string, Character[]> = new Map<string, Character[]>();

  private getCharacterDefaults = (gameCode: string) => {
    if (!this.characterDefaults.has(gameCode))
      this.characterDefaults.set(gameCode, []);
    return this.characterDefaults.get(gameCode) as Character[];
  };

  private addCharacterDefault = (
    gameCode: string,
    character: Character
  ): Character[] => {
    let defaultList = [...this.getCharacterDefaults(gameCode)];
    defaultList = defaultList.filter(
      (characterFromList) =>
        characterFromList.contentCode !== character.contentCode
    );
    defaultList.push(character);
    this.characterDefaults.set(gameCode, defaultList);
    return defaultList;
  };

  // Exposed Interfaces

  public GetAvailableGames = async (): Promise<ContentItemSummary[]> => {
    if (this.availableGames.length > 0) return this.availableGames;

    let result: ContentItemSummary[] = [];
    try {
      result = await getAPI<ContentItemSummary[]>(`content/games/`);
      if (result) {
        this.availableGames = result;
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Scenario Listing");
    }
    return this.availableGames;
  };

  public GetScenariosForGame = async (
    gameCode: string
  ): Promise<ContentItemSummary[]> => {
    let gameScenarios = this.getScenarioSummaries(gameCode);
    if (gameScenarios.length > 0) return gameScenarios;

    let result: ContentItemSummary[] = [];
    try {
      result = await getAPI<ContentItemSummary[]>(
        `content/games/${gameCode}/scenarios`
      );
      if (result) {
        this.setScenarioSummaries(gameCode, result);
        gameScenarios = this.getScenarioSummaries(gameCode);
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Scenario Listing");
    }
    return gameScenarios;
  };

  public GetScenarioDefault = async (
    gameCode: string,
    contentCode: string
  ): Promise<Scenario | undefined> => {
    let gameScenarios = this.getScenarioDefaults(gameCode);
    let scenarioDefault: Scenario | undefined = gameScenarios.find(
      (scn) => scn.contentCode === contentCode
    );
    if (scenarioDefault) return scenarioDefault;
    let result: Scenario | undefined = undefined;
    try {
      result = await getAPI<Scenario>(
        `content/games/${gameCode}/scenarios/${contentCode}`
      );
      if (result) {
        gameScenarios = this.addScenarioDefault(gameCode, result);
        scenarioDefault = gameScenarios.find(
          (scn) => scn.contentCode === contentCode
        );
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Scenario Listing");
    }
    return scenarioDefault;
  };

  public GetCharactersForGame = async (
    gameCode: string
  ): Promise<ContentItemSummary[]> => {
    let gameCharacters = this.getCharacterSummaries(gameCode);
    if (gameCharacters.length > 0) return gameCharacters;

    let result: ContentItemSummary[] = [];
    try {
      result = await getAPI<ContentItemSummary[]>(
        `content/games/${gameCode}/characters`
      );
      if (result) {
        this.setCharacterSummaries(gameCode, result);
        gameCharacters = this.getCharacterSummaries(gameCode);
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Scenario Listing");
    }
    return gameCharacters;
  };

  public GetCharacterDefault = async (
    gameCode: string,
    contentCode: string
  ): Promise<Character | undefined> => {
    let gameCharacters = this.getCharacterDefaults(gameCode);
    let characterDefault: Character | undefined = gameCharacters.find(
      (chr) => chr.contentCode === contentCode
    );
    if (characterDefault) return characterDefault;
    let result: Character | undefined = undefined;
    try {
      result = await getAPI<Character>(
        `content/games/${gameCode}/characters/${contentCode}`
      );
      if (result) {
        gameCharacters = this.addCharacterDefault(gameCode, result);
        characterDefault = gameCharacters.find(
          (chr) => chr.contentCode === contentCode
        );
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Scenario Listing");
    }
    return characterDefault;
  };
}

let contentService: ContentServiceImplementation | undefined = undefined;
const useContentService = (): ContentServiceImplementation => {
  if (!contentService) contentService = new ContentServiceImplementation();
  return contentService;
};

export { useContentService };
