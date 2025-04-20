import React, {useEffect, useState} from "react";
import {Button, Col, Flex, Form, message, Row, Select, Typography} from "antd";
import {Modal, Space, Spin} from "antd/lib";
import {FrequencyValueDTO, ResponseError} from "../../api";
import ApiClient from "../../api/ApiClient";
import ErrorsHandler from "../../utils/ErrorsHandler";
import ErrorInfo from "../ErrorInfo";
import {LoadingOutlined} from "@ant-design/icons";

const {Title, Paragraph, Text} = Typography;

interface NotificationsModalProps {
    isOpen: boolean;
    onClose: () => void;
}

interface FrequencyFormState {
    frequencyId: number;
}

interface FrequencyControlState {
    isFrequencyInfoLoading: boolean;
    notificationId: string | undefined;
    initialFrequency: FrequencyValueDTO | undefined;
    availableFrequency: FrequencyValueDTO[] | undefined;
}

const NotificationsModal: React.FC<NotificationsModalProps> = (props) => {
    const [errorsState, setErrorsState] = useState<string[]>([])
    const [frequencyState, setFrequencyState] = useState<FrequencyControlState>({
        isFrequencyInfoLoading: true,
        notificationId: undefined,
        initialFrequency: undefined,
        availableFrequency: undefined
    });

    useEffect(() => {
        const fetchFrequencyInfo = async () => {
            try {
                const notification = await ApiClient.notificationsApi.apiNotificationsGet()
                const availableFrequency = await ApiClient.frequencyApi.apiFrequencyAllGet()
                
                // todo: в будущем изменить способ начального обновления значения
                form.setFieldValue("frequencyId", notification.frequencyValue?.id)
                setFrequencyState({
                    isFrequencyInfoLoading: false,
                    notificationId: notification.id,
                    availableFrequency: availableFrequency,
                    initialFrequency: notification.frequencyValue
                })
            } catch (e) {
                const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
                setErrorsState(errors)
            }
        }

        fetchFrequencyInfo()
    }, [])

    const handleEditFrequency = async (values: FrequencyFormState) => {
        console.log(values.frequencyId)
        console.log(frequencyState.initialFrequency?.id)
        if (frequencyState.initialFrequency?.id === values.frequencyId) {
            setErrorsState(['Данные не изменились'])
            return
        }

        try {
            await ApiClient.notificationsApi.apiNotificationsUpdateFrequencyNotificationIdPut({
                notificationId: frequencyState.notificationId!,
                frequencyId: values.frequencyId
            })
            message.success("Успешно", 5)
        } catch (e) {
            console.log(e)
            const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
            setErrorsState(errors)
        }
    }

    const [form] = Form.useForm<FrequencyFormState>();

    return (
        <Modal
            title={
                <Title
                    level={4}
                    style={{margin: 0, textAlign: 'center'}}>
                    Уведомления
                </Title>}
            open={props.isOpen}
            onCancel={props.onClose}
            footer={false}
        >
            {errorsState && <ErrorInfo errors={errorsState}/>}
            {frequencyState.isFrequencyInfoLoading ? (
                <Space size="small" direction="vertical">
                    <p>Загрузка информации...</p>
                    <Spin indicator={<LoadingOutlined spin/>} size="large"/>
                </Space>) : (
                <Row>
                    <Col>
                        <Typography>
                            <Paragraph>
                                По умолчанию уведомления о целях отправляются на почту создателя и почту наставника
                                по <Text strong>понедельникам в 12:00</Text>.
                                <br/>Если создано 0 целей, сообщения на почту отправлены не будут.
                                <br/>Изменить частоту получения уведомлений можно ниже.
                            </Paragraph>
                            <Form form={form} layout="vertical" onFinish={handleEditFrequency}>
                                <Form.Item name="frequencyId">
                                    <Select
                                        options={frequencyState.availableFrequency?.map(frequency => ({label: frequency.name, value: frequency.id}))}
                                    />
                                </Form.Item>
                                <Form.Item>
                                    <Flex gap="small">
                                        <Button variant="filled" type="primary" htmlType="submit" size="middle">
                                            Сохранить
                                        </Button>
                                    </Flex>
                                </Form.Item>
                            </Form>
                        </Typography>
                    </Col>
                </Row>
            )}
        </Modal>
    )
}

export default NotificationsModal