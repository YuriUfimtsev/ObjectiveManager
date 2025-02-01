import {FC, useState} from "react";
import {Navigate, useSearchParams} from "react-router-dom";
import Title from "antd/lib/typography/Title";
import * as React from "react";
import {Button, Col, Form, Input, Row} from "antd";
import {LoginViewModel, ResponseError} from "../../api";
import ValidationUtils from "../../utils/ValidationUtils";
import ErrorsHandler from "../../utils/ErrorsHandler";
import {EyeInvisibleOutlined, EyeTwoTone, LockOutlined, LoginOutlined, UserOutlined} from "@ant-design/icons";
import AuthService, {AuthResult} from "../../utils/AuthService";
import ErrorInfo from "../ErrorInfo";

interface LoginProps {
    onLogin: () => void;
}

const Login: FC<LoginProps> = (props) => {
    const [loginState, setLoginState] = useState<AuthResult>({
        isLogin: AuthService.isLoggedIn(),
        errors: []
    })

    const handleSubmitRegister = async (loginModel: LoginViewModel) => {
        if (!loginModel.email || !ValidationUtils.isCorrectEmail(loginModel.email)) {
            setLoginState({
                isLogin: false,
                errors: ['Некорректный адрес электронной почты']
            })
            return;
        }

        const result = await AuthService.login(loginModel)
        setLoginState(result)
        if (result.isLogin) {
            props.onLogin()
        }
    }

    const [form] = Form.useForm<LoginViewModel>();

    if (loginState.isLogin) {
        return <Navigate to={"/"}/>;
    }

    return (
        <div>
            <Row justify="center" style={{paddingTop: 60}}>
                <Col>
                    <LoginOutlined style={{fontSize: '50px', color: "darkred"}}/>
                </Col>
            </Row>
            <Row justify="center">
                <Col>
                    <Title level={3}>
                        Войти
                    </Title>
                </Col>
            </Row>
            {loginState.errors && <ErrorInfo errors={loginState.errors}/>}
            <Row justify="center" style={{marginTop: 20}}>
                <Col span={6}>
                    <Form
                        form={form}
                        layout="vertical"
                        size="large"
                        onFinish={handleSubmitRegister}
                    >
                        <Form.Item
                            name="email"
                            rules={[{required: true, message: 'Пожалуйста, введите почту'}]}
                            style={{marginBottom: "15px"}}
                        >
                            <Input prefix={<UserOutlined/>} placeholder="Электронная почта"/>
                        </Form.Item>
                        <Form.Item
                            name="password"
                            rules={[{required: true, message: 'Пожалуйста, введите пароль'}]}
                            style={{marginBottom: "20px"}}
                        >
                            <Input.Password
                                prefix={<LockOutlined/>}
                                placeholder="Пароль"
                                iconRender={(visible) => (visible ? <EyeTwoTone/> : <EyeInvisibleOutlined/>)}/>
                        </Form.Item>
                        <Form.Item>
                            <Button block variant="solid" htmlType="submit"
                                    style={{background: "#00264b", borderColor: "#001529", color: "white"}}>
                                Войти
                            </Button>
                            <Title level={5} style={{marginTop: 2}}>
                                Нет аккаунта? <a href="/register">Перейти к регистрации</a>
                            </Title>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </div>
    )
}

export default Login;