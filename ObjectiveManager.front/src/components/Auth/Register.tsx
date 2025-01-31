import {FC, useState} from "react";
import {Navigate} from "react-router-dom";
import Title from "antd/lib/typography/Title";
import * as React from "react";
import {Button, Col, Form, Input, Row} from "antd";
import {RegisterDto} from "../../api";
import ValidationUtils from "../../utils/ValidationUtils";
import ErrorsHandler from "../../utils/ErrorsHandler";
import {EyeInvisibleOutlined, EyeTwoTone, LockOutlined, LoginOutlined} from "@ant-design/icons";
import AuthService, {AuthResult} from "../../utils/AuthService";
import ErrorInfo from "../ErrorInfo";

interface RegisterProps {
    onLogin: () => void;
}

const Register: FC<RegisterProps> = (props) => {
    const [registerState, setRegisterState] = useState<AuthResult>({
        isLogin: AuthService.isLoggedIn(),
        errors: [],
    })
    
    const handleSubmitRegister = async (registerModel: RegisterDto) => {
        if (!registerModel.email || !ValidationUtils.isCorrectEmail(registerModel.email)) {
            setRegisterState({
                isLogin: false,
                errors: ['Некорректный адрес электронной почты']
            })
            return;
        }

        try {
            const result = await AuthService.register(registerModel)
            setRegisterState(result)
            if (result.isLogin) {
                props.onLogin()
            }
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as Response);
            setRegisterState({
                errors: errors,
                isLogin: false
            })
        }
    }

    const [form] = Form.useForm<RegisterDto>();

    if (registerState.isLogin) {
        return <Navigate to={"/"}/>;
    }

    return (
        <div>
            <Row justify="center" style={{paddingTop: 20}}>
                <Col>
                    <LoginOutlined style={{fontSize: '50px', color: "darkred"}}/>
                </Col>
            </Row>
            <Row justify="center">
                <Col>
                    <Title level={3}>
                        Регистрация
                    </Title>
                </Col>
            </Row>
            {registerState.errors && <ErrorInfo errors={registerState.errors} />}
            <Row justify="center" style={{marginTop: 20}}>
                <Col span={8}>
                    <Form
                        form={form}
                        layout="vertical"
                        size="large"
                        onFinish={handleSubmitRegister}
                    >
                        <Row justify="space-between">
                            <Col>
                                <Form.Item
                                    name="name"
                                    rules={[{required: true, message: 'Пожалуйста, введите имя'}]}
                                    style={{ marginBottom: "12px" }}
                                >
                                    <Input placeholder="Имя"/>
                                </Form.Item>

                            </Col>
                            <Col>
                                <Form.Item
                                    name="surname"
                                    rules={[{required: true, message: 'Пожалуйста, введите фамилию'}]}
                                    style={{ marginBottom: "12px" }}
                                >
                                    <Input placeholder="Фамилия"/>
                                </Form.Item>
                            </Col>
                        </Row>
                        <Form.Item
                            name="email"
                            rules={[{required: true, message: 'Пожалуйста, введите почту'}]}
                            style={{ marginBottom: "12px" }}
                        >
                            <Input placeholder="Электронная почта"/>
                        </Form.Item>
                        <Form.Item
                            name="password"
                            rules={[{required: true, message: 'Пожалуйста, введите пароль'}]}
                            style={{ marginBottom: "12px" }}
                        >
                            <Input.Password
                                prefix={<LockOutlined/>}
                                placeholder="Пароль"
                                iconRender={(visible) => (visible ? <EyeTwoTone/> : <EyeInvisibleOutlined/>)}/>
                        </Form.Item>
                        <Form.Item
                            name="mentorEmail"
                            rules={[{required: true, message: 'Пожалуйста, введите почту наставника'}]}
                            style={{ marginBottom: "12px" }}
                        >
                            <Input placeholder="Электронная почта наставника"/>
                        </Form.Item>
                        <Form.Item>
                            <Button block variant="solid" htmlType="submit"
                                    style={{background: "#00264b", borderColor: "#001529", color: "white"}}>
                                Зарегистрироваться
                            </Button>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </div>
    )
}

export default Register;