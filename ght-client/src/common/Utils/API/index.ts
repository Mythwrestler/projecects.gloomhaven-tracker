import ENV_VARS from "../../Environment";

const baseUrl = `${ENV_VARS.API.BaseURL()}api/`;

const request = async <T>(
  url: string,
  method: "POST" | "GET" | "PUT" | "PATCH",
  accessToken: string | undefined = undefined,
  body: unknown = undefined
): Promise<T> => {
  const headers: string[][] = [
    [
      "Content-Type",
      method == "PATCH" ? "application/json+patch" : "application/json",
    ],
  ];
  if (accessToken != undefined)
    headers.push(["Authorization", `Bearer ${accessToken}`]);

  const requestInit: RequestInit = {
    method: method,
    headers,
  };

  if (body !== null && body !== undefined)
    requestInit.body = JSON.stringify(body);

  let response: Response;

  try {
    response = await fetch(url, requestInit);
  } catch (reason: unknown) {
    throw new Error(
      `Failed to make ${method} request, Reason: ${JSON.stringify(reason)}`
    );
  }

  if (response.status >= 400)
    throw new Error(
      `Failed to ${method} resources. Response Status: ${response.status}`
    );

  if (response.status == 204 || response.status < 200) return {} as T;

  try {
    const jsonString = await response.text();
    if (jsonString.trim() == "") return {} as T;
    return JSON.parse(jsonString) as T;
  } catch (error: unknown) {
    throw new Error(
      `Failed to parse response body. Response Status: ${response.status}`
    );
  }
};

export const postAPI = async <T>(
  path: string,
  accessToken: string | undefined = undefined,
  body: unknown = undefined
): Promise<T> => {
  return await request<T>(`${baseUrl}${path}`, "POST", accessToken, body);
};

export const patchAPI = async <T>(
  path: string,
  accessToken: string | undefined = undefined,
  body: unknown = undefined
): Promise<T> => {
  return await request<T>(`${baseUrl}${path}`, "PATCH", accessToken, body);
};

export const putAPI = async <T>(
  path: string,
  accessToken: string | undefined = undefined,
  body: unknown = undefined
): Promise<T> => {
  return await request<T>(`${baseUrl}${path}`, "PUT", accessToken, body);
};

export const getAPI = async <T>(
  path: string,
  accessToken: string | undefined = undefined
): Promise<T> => {
  return await request<T>(`${baseUrl}${path}`, "GET", accessToken);
};
