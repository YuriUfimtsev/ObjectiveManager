import {FC, useEffect, useState} from "react";
import Title from "antd/lib/typography/Title";
import * as React from "react";
import {Button, Col, Form, Input, message, Row, Space} from "antd";
import {LoadingOutlined, UserOutlined} from "@ant-design/icons";
import {Spin} from "antd/lib";
import {EditAccountDto, ResponseError} from "../../api";
import ApiClient from "../../api/ApiClient";
import ErrorsHandler from "../../utils/ErrorsHandler";
import ValidationUtils from "../../utils/ValidationUtils";
import {useNavigate} from "react-router-dom";
import ErrorInfo from "../ErrorInfo";
import AuthService from "../../utils/AuthService";
import isEqual from 'lodash.isequal';

interface InitialUserDataLoadingState {
    isDataLoading: boolean;
    userData: EditAccountDto | undefined;
}

const EditProfile: FC = () => {
    const [errorsState, setErrorsState] = useState<string[]>([])
    const [initialUserDataState, setInitialUserDataState] = useState<InitialUserDataLoadingState>({
        isDataLoading: true,
        userData: undefined
    });

    const navigate = useNavigate();

    const [form] = Form.useForm<EditAccountDto>();

    useEffect(() => {
        const getUserInfo = async () => {
            try {
                const initialUserData = await ApiClient.accountApi.apiAccountUserDataGet()
                setInitialUserDataState({
                    isDataLoading: false,
                    userData: {
                        name: initialUserData.name,
                        surname: initialUserData.surname,
                        email: initialUserData.email,
                        mentorEmail: initialUserData.mentorEmail
                    }
                })
            } catch (e) {
                const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
                setErrorsState(errors)
            }
        }

        getUserInfo()
    }, [])

    const handleEditSubmit = async (editedAccountData: EditAccountDto) => {
        if (isEqual(editedAccountData, initialUserDataState.userData)) {
            setErrorsState(['Данные не изменились'])
            return
        }

        if (!editedAccountData.email || !ValidationUtils.isCorrectEmail(editedAccountData.email)) {
            setErrorsState(['Некорректный адрес электронной почты']);
            return;
        }

        try {
            await ApiClient.accountApi.apiAccountEditPut({editAccountDto: editedAccountData})
            message.success("Успешно", 8)
            navigate("/")
        } catch (e) {
            console.log(e)
            const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
            setErrorsState(errors)
        }
    }

    if (initialUserDataState.isDataLoading) {
        return (
            <Space style={{marginLeft: 10, marginTop: 10}} size="small" direction="vertical">
                <p>Загрузка данных пользователя...</p>
                <Spin indicator={<LoadingOutlined spin/>} size="large"/>
            </Space>
        )
    }

    return (
        <div>
            <Row justify="center" style={{paddingTop: 20}}>
                <Col>
                    <UserOutlined style={{fontSize: '50px', color: "darkred"}}/>
                </Col>
            </Row>
            <Row justify="center">
                <Col>
                    <Title level={3}>
                        Редактировать профиль
                    </Title>
                </Col>
            </Row>
            {errorsState && <ErrorInfo errors={errorsState}/>}
            <Row justify="center" style={{marginTop: 20}}>
                <Col span={8}>
                    <Form
                        form={form}
                        layout="vertical"
                        size="large"
                        onFinish={handleEditSubmit}
                        initialValues={initialUserDataState.userData}
                    >
                        <Row justify="space-between">
                            <Col>
                                <Form.Item
                                    name="name"
                                    rules={[{required: true, message: 'Пожалуйста, введите имя'}]}
                                    style={{marginBottom: "12px"}}
                                >
                                    <Input placeholder="Имя"/>
                                </Form.Item>

                            </Col>
                            <Col>
                                <Form.Item
                                    name="surname"
                                    rules={[{required: true, message: 'Пожалуйста, введите фамилию'}]}
                                    style={{marginBottom: "12px"}}
                                >
                                    <Input placeholder="Фамилия"/>
                                </Form.Item>
                            </Col>
                        </Row>
                        <Form.Item
                            name="email"
                            rules={[{required: true, message: 'Пожалуйста, введите почту'}]}
                            style={{marginBottom: "12px"}}
                            label="Почта"
                            layout="horizontal"
                        >
                            <Input/>
                        </Form.Item>
                        <Form.Item
                            name="mentorEmail"
                            rules={[{required: true, message: 'Пожалуйста, введите почту наставника'}]}
                            style={{marginBottom: "12px"}}
                            label="Почта наставника"
                            layout="horizontal"
                        >
                            <Input/>
                        </Form.Item>
                        <Form.Item
                            style={{marginBottom: "12px"}}
                        >
                            <Button block variant="solid" htmlType="submit"
                                    style={{background: "#00264b", borderColor: "#001529", color: "white"}}>
                                Сохранить
                            </Button>
                        </Form.Item>
                        <Form.Item>
                            <Button block variant="solid"
                                    onClick={() => {
                                        AuthService.logout()
                                        window.location.reload()
                                    }}
                                    style={{background: "darkred", borderColor: "darkred", color: "white"}}>
                                Выйти
                            </Button>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </div>
    )
}

export default EditProfile;