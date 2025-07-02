export abstract class BaseNetworkService {
  protected async post<T>(
    url: string,
    email: string,
    password: string
  ): Promise<T> {
    const params = new URLSearchParams();
    params.append("grant_type", "password");
    params.append("username", email);
    params.append("password", password);
    params.append("client_id", "shareTripFront-client");
    params.append("client_secret", "secret");
    params.append("scope", "openid profile email");

    const headers: Record<string, string> = {
        "Content-Type": "application/x-www-form-urlencoded",
        "Access-Control-Allow-Origin": "*"
    }

    const token = localStorage.getItem("access_token");
    const expires_at = localStorage.getItem("expires_at")

    if (token != null && expires_at != null &&  Date.now().toString() < expires_at) {
      headers["Authorization"] = "Bearer " + token;
    }

    const response = await fetch(url, {
      method: "POST",
      body: params.toString(), // ⚠️ ne pas passer l'objet, mais le stringifié
      headers: headers
    });

    if (!response.ok) {
      throw new Error(`Erreur: ${response.statusText}`);
    }

    return await response.json();
  }
}
