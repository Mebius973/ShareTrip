import { API_BASE_URL, API_USER_PREFIX, API_USER_REGISTER_ENDPOINT, API_USER_LOGIN_ENDPOINT } from "../contants"
import { BaseNetworkService } from "./BaseNetworkService";

interface LoginResponse {
    access_token: string;
    token_type: string;
    expires_in: number;
}

export class UserService extends BaseNetworkService {
    private baseUrl = API_BASE_URL + API_USER_PREFIX

    async register(email: string, password: string): Promise<boolean> {
        return await this.post(this.baseUrl + API_USER_REGISTER_ENDPOINT, email, password) != null
    }

    async login(email: string, password: string): Promise<boolean> {
        const loginResponse: LoginResponse = await this.post(this.baseUrl + API_USER_LOGIN_ENDPOINT, email, password)

        const expires_at = Date.now() + loginResponse.expires_in

        localStorage.setItem("access_token", loginResponse.access_token);
        localStorage.setItem("token_type", loginResponse.token_type);
        localStorage.setItem("expires_at", expires_at.toString());

        return loginResponse != null
    }    
}