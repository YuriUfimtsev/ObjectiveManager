import * as React from "react";
import {Button, DatePicker, Form, Input, Modal} from "antd";
import TextArea from "antd/lib/input/TextArea";
import Title from "antd/lib/typography/Title";
import dayjs from "dayjs";
import {InfoCircleOutlined} from "@ant-design/icons";
import {ApiObjectivesPostRequest} from "../../api";
import ApiClient from "../../api/ApiClient";
import ErrorsHandler from "../../utils/ErrorsHandler";
import {useState} from "react";
import DateTimeUtils from "../../utils/DateTimeUtils";
import ErrorInfo from "../ErrorInfo";

interface ICreateObjectiveProps {
    isOpen: boolean;
    onClose: () => void;
}

interface ICreateObjective {
    definition: string;
    finalDate: Date;
    comment: string;
}

const CreateObjectiveModal: React.FC<ICreateObjectiveProps> = (props) => {
    const [errorsState, setErrorsState] = useState<string[]>([])

    const handleCreateObjective = async (objective: ICreateObjective) => {
        try {
            const requestData: ApiObjectivesPostRequest = {
                definition: objective.definition,
                finalDate: objective.finalDate,
                comment: objective.comment
            }

            await ApiClient.objectivesApi.apiObjectivesPost(requestData);
            handleCancelModal()
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as Response);
            setErrorsState(errors)
        }
    };

    const handleCancelModal = () => {
        props.onClose();
        form.resetFields()
    }

    const [form] = Form.useForm<ICreateObjective>();

    return (
        <div>
            <Modal
                title={
                    <Title
                        level={4}
                        style={{margin: 0, textAlign: 'center'}}>
                        Новая цель
                    </Title>}
                open={props.isOpen}
                onCancel={handleCancelModal}
                footer={null}>
                <Form form={form} layout="vertical"
                      onFinish={handleCreateObjective}
                >
                    {errorsState && <ErrorInfo errors={errorsState}/>}
                    <Form.Item
                        name="definition"
                        label="Название"
                        rules={[{required: true, message: 'Пожалуйста, придумайте название цели'}]}
                    >
                        <TextArea autoSize={{minRows: 1, maxRows: 2}} />
                    </Form.Item>
                    <Form.Item
                        name="finalDate"
                        label="Контрольная дата"
                        tooltip={{
                            title: 'Чтобы подведение итогов по цели попало в ближайший мониторинг,' +
                                ' нужно, чтобы дата достижения цели не была больше даты окончания текущего полугодия.',
                            icon: <InfoCircleOutlined/>
                        }}
                        rules={[{required: true, message: 'Пожалуйста, выберите крайний срок'}]}
                        initialValue={dayjs().add(1, 'month')}
                    >
                        <DatePicker format={DateTimeUtils.DateOnlyFormat} style={{width: '100%'}}/>
                    </Form.Item>
                    <Form.Item
                        name="comment"
                        label="Комментарий"
                        rules={[{message: 'Пожалуйста, введите описание цели'}]}
                    >
                        <TextArea autoSize={{minRows: 2, maxRows: 8}}/>
                    </Form.Item>
                    <Form.Item style={{textAlign: 'start'}}>
                        <Button variant="filled" type="primary" htmlType="submit" size="middle">
                            Создать
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    )
}

export default CreateObjectiveModal