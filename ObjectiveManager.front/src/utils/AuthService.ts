import ApiClient from "../api/ApiClient";
import {jwtDecode} from "jwt-decode";
import {LoginViewModel, RegisterDto, ResponseError} from "../api";
import ErrorsHandler from "./ErrorsHandler";

interface TokenPayload {
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
        try {
            const tokenCredentials = await ApiClient.accountApi.apiAccountLoginPost({loginViewModel: user});
            if (tokenCredentials?.accessToken == undefined) {
                return {
                    errors: ['Ошибка аутентификации'],
                    isLogin: false
                }
            }

            this.setToken(tokenCredentials.accessToken)
            return {
                errors: [],
                isLogin: true
            }
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
            return {
                errors: errors,
                isLogin: false
            }
        }
    }

    public static logout = () => localStorage.clear();

    // todo: унифицировать с методом login
    public static async register(user: RegisterDto): Promise<AuthResult> {
        try {
            const tokenCredentials = await ApiClient.accountApi.apiAccountRegisterPost({registerDto: user})
            if (tokenCredentials?.accessToken == undefined) {
                return {
                    errors: ['Ошибка аутентификации'],
                    isLogin: false
                }
            }

            this.setToken(tokenCredentials.accessToken)
            return {
                errors: [],
                isLogin: true
            }
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
            return {
                errors: errors,
                isLogin: false
            }
        }
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
    
    private static setToken = (idToken: string) => localStorage.setItem("jwt_token", idToken);
}