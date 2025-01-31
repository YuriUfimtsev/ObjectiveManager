import ApiClient from "../api/ApiClient";
import {jwtDecode} from "jwt-decode";
import {LoginViewModel, RegisterDto, TokenCredentialsResult} from "../api";

interface TokenPayload {
    _userName: string;
    _id: string;
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string;
    nbf: number;
    exp: number;
    iss: string;
}

export interface AuthResult {
    errors: string[],
    isLogin: boolean
}

export default class AuthService {
    public static async login(user: LoginViewModel): Promise<AuthResult> {
        const tokenCredentialsResult = await ApiClient.accountApi.apiAccountLoginPost({loginViewModel: user});
        return this.getAuthResult(tokenCredentialsResult);
    }

    public static logout = () => localStorage.clear();

    public static async register(user: RegisterDto): Promise<AuthResult> {
        const token = await ApiClient.accountApi.apiAccountRegisterPost({registerDto: user})
        return this.getAuthResult(token);
    }

    public static isLoggedIn() {
        const token = this.getToken();
        return !!token && !this.isTokenExpired(token);
    }

    public static isTokenExpired(token: any) {
        try {
            const decoded = jwtDecode<TokenPayload>(token);
            const currentTimeInSeconds = Date.now() / 1000
            
            if (decoded.exp && decoded.exp < currentTimeInSeconds) {
                return true
            }

            if (decoded.nbf && decoded.nbf > currentTimeInSeconds) {
                return true
            }
            
            return false
        } catch (err) {
            return true;
        }
    }

    public static getToken = () => localStorage.getItem("jwt_token");

    private static getAuthResult(tokenResult: TokenCredentialsResult): AuthResult {
        if (!tokenResult.succeeded || tokenResult.value?.accessToken == undefined) {
            return {
                errors: tokenResult.errors ?? [],
                isLogin: false
            }
        }

        this.setToken(tokenResult.value.accessToken)
        return {
            errors: [],
            isLogin: true
        }
    }
    
    private static setToken = (idToken: string) => localStorage.setItem("jwt_token", idToken);
}