interface AuthEnvVars {
    Enabled: () => boolean
    Domain: () => string
    ClientId: () => string
    APIAudience: () => string
}

interface ApiEnvVars {
    BaseURL: () => string
}

interface ClientEnvVars {
    BaseURL: () => string
}

const AUTH:AuthEnvVars = {
    Enabled: () => {
        return ("ENV_AUTH_ENABLED" ?? "false").toLowerCase() == "true";
    },
    Domain: () => {
       return "ENV_AUTH_DOMAIN";
    },
    ClientId: () => {
       return "ENV_AUTH_CLIENT_ID";
    },
    APIAudience: () => {
       return "ENV_AUTH_API_AUDIENCE";
    }
}

const API:ApiEnvVars = {
    BaseURL: () => "ENV_API_BASE_URL"
}

const CLIENT:ClientEnvVars = {
    BaseURL: () => "ENV_CLIENT_BASE_URL"
}

const ENV_VARS = {
    AUTH,
    API,
    CLIENT
}

export default ENV_VARS