import './styles/App.css';
import React, {useState} from 'react';
import ObjectivesTable from "./components/ObjectivesTable";
import {Navigate, Route, Routes, useNavigate} from "react-router-dom";
import AuthLayout from "./AuthLayout";
import AppHeader from "./AppHeader";
import Register from "./components/Auth/Register";
import Login from "./components/Auth/Login";
import AuthService from "./utils/AuthService";
import EditProfile from "./components/Profile/EditProfile";

interface AppState {
    loggedIn: boolean;
}

const App = () => {
    const navigate = useNavigate();
    const [state, setState] = useState<AppState>({
        loggedIn: AuthService.isLoggedIn(),
    });

    const login = async (returnUrl: string | null) => {
        setState({
            loggedIn: true
        })

        return <Navigate to={returnUrl || "/"}/>;
    };

    const logout = () => {
        AuthService.logout();
        setState({
            loggedIn: false
        })
        navigate("/login");
    };

    return (
        <>
            <Routes>
                <Route element={<AppHeader loggedIn={state.loggedIn}/>}>
                    <Route element={<AuthLayout/>}>
                        <Route path="/" element={<ObjectivesTable/>}/>
                        <Route path="editProfile" element={<EditProfile/>}/>
                        <Route path={"*"} element={<ObjectivesTable/>}/>
                    </Route>

                    <Route path="login" element={<Login onLogin={login}/>}/>
                    <Route path="register" element={<Register onLogin={() => login("")}/>}/>
                </Route>
            </Routes>
        </>
    );
}

export default App;