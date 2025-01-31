import {FC, useEffect, useState} from "react";
import Title from "antd/lib/typography/Title";
import * as React from "react";
import {Button, Col, Form, Input, Row, Space} from "antd";
import {LoadingOutlined, UserOutlined} from "@ant-design/icons";
import {Spin} from "antd/lib";
import {EditDataDto} from "../../api";
import ApiClient from "../../api/ApiClient";
import ErrorsHandler from "../../utils/ErrorsHandler";
import ValidationUtils from "../../utils/ValidationUtils";
import {useNavigate} from "react-router-dom";
import ErrorInfo from "../ErrorInfo";

const EditProfile: FC<EditDataDto> = () => {
    const [errorsState, setErrorsState] = useState<string[]>([])
    const [isInitialUserDataLoading, setIsInitialUserDataLoading] = useState<boolean>(true);

    const navigate = useNavigate();

    const [form] = Form.useForm<EditDataDto>();

    useEffect(() => {
        const getUserInfo = async () => {
            try {
                const initialUserData = await ApiClient.accountApi.apiAccountUserDataGet()
                form.setFieldsValue(initialUserData)
            } catch (e) {
                const errors = await ErrorsHandler.getErrorMessages(e as Response);
                setErrorsState(errors)
            } finally {
                setIsInitialUserDataLoading(false)
            }
        }

        getUserInfo()
    }, [])

    const handleEditSubmit = async (editedData: EditDataDto) => {
        if (!editedData.email || !ValidationUtils.isCorrectEmail(editedData.email)) {
            setErrorsState(['Некорректный адрес электронной почты']);
            return;
        }

        try {
            const result = await ApiClient.accountApi.apiAccountEditPut({editDataDto: editedData})
            if (result.errors) {
                setErrorsState(result.errors)
                return
            }

            navigate("/")
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as Response);
            setErrorsState(errors)
        }
    }

    if (isInitialUserDataLoading) {
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
                        >
                            <Input placeholder="Электронная почта"/>
                        </Form.Item>
                        <Form.Item
                            name="mentorEmail"
                            rules={[{required: true, message: 'Пожалуйста, введите почту наставника'}]}
                            style={{marginBottom: "12px"}}
                        >
                            <Input placeholder="Электронная почта наставника"/>
                        </Form.Item>
                        <Form.Item>
                            <Button block variant="solid" htmlType="submit"
                                    style={{background: "#00264b", borderColor: "#001529", color: "white"}}>
                                Сохранить
                            </Button>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </div>
    )
}

export default EditProfile;