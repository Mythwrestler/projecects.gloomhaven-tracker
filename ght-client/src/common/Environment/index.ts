interface AuthEnvVars {
  Enabled: () => boolean;
  Domain: () => string;
  ClientId: () => string;
}

interface ApiEnvVars {
  BaseURL: () => string;
}

interface ClientEnvVars {
  BaseURL: () => string;
}

interface ServiceContext {
  Actions: string;
  State: string;
}

interface ContextEnvVars {
  ContentService: ServiceContext;
  ActiveCombat: string;
  CombatHub: string;
}

const AUTH: AuthEnvVars = {
  Enabled: () => {
    return ("ENV_AUTH_ENABLED" ?? "false").toLowerCase() == "true";
  },
  Domain: () => {
    return "ENV_AUTH_DOMAIN";
  },
  ClientId: () => {
    return "ENV_AUTH_CLIENT_ID";
  },
};

const API: ApiEnvVars = {
  BaseURL: () => "ENV_API_BASE_URL",
};

const CLIENT: ClientEnvVars = {
  BaseURL: () => "ENV_CLIENT_BASE_URL",
};

const CONTEXT: ContextEnvVars = {
  ContentService: {
    Actions: "0fe59005-768d-4e9c-850f-54179004e63a",
    State: "6ef3b50a-6143-46c5-8f7f-33b621c1efad",
  },
  ActiveCombat: "41d944b7-4bab-492b-bb89-71a73bfe8b8d",
  CombatHub: "a67532f7-3434-4652-86d6-585a9ede30d6",
};

const ENV_VARS = {
  AUTH,
  API,
  CLIENT,
  CONTEXT,
};

export default ENV_VARS;
