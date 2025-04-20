import {Navigate, Outlet} from 'react-router-dom';
import React, {FC} from "react";
import AuthService from "./utils/AuthService";

const AuthLayout: FC = () =>
    AuthService.isLoggedIn()
        ? <Outlet/>
        : <Navigate to={`/login`}/>

export default AuthLayout;