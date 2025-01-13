import * as React from "react";
import {Button, DatePicker, Flex, Form, Input, Modal, Select} from "antd";
import TextArea from "antd/lib/input/TextArea";
import {
    Objective,
    ObjectivesObjectiveIdDeleteRequest,
    ObjectivesPutRequest,
    ObjectiveStatus
} from "../api";
import ApiClient from "../api/ApiClient";
import Title from "antd/lib/typography/Title";
import {useEffect, useState} from "react";
import dayjs from 'dayjs';
import DateTimeUtils from "../utils/DateTimeUtils";

interface IEditObjectiveProps {
    isOpen: boolean;
    onClose: () => void;
    objective: Objective | undefined;
}

interface IEditObjective {
    name: string;
    finalDate: Date;
    statusId: number;
    comment: string;
}

const EditObjectiveModal: React.FC<IEditObjectiveProps> = (props) => {
    const [form] = Form.useForm<IEditObjective>();

    const [statuses, setStatuses] = useState<ObjectiveStatus[]>([]);

    useEffect(() => {
        const fetchStatuses = async () => {
            const statuses = await ApiClient.statusesApi.statusesAllGet();
            setStatuses(statuses);
        }

        fetchStatuses()
    }, []);

    const handleCancelModal = () => {
        props.onClose();
        form.resetFields()
    }

    const handleDeleteObjective = async () => {
        const requestData: ObjectivesObjectiveIdDeleteRequest = {objectiveId: props.objective?.id!}
        await ApiClient.objectivesApi.objectivesObjectiveIdDelete(requestData);
        handleCancelModal()
    };

    const dateFormat = 'DD.MM.YYYY';

    const handleEditObjective = async (objective: IEditObjective) => {
        try {
            const requestData: ObjectivesPutRequest = {
                id: props.objective!.id!,
                definition: objective.name,
                finalDate: objective.finalDate,
                comment: objective.comment,
                statusId: objective.statusId
            }

            await ApiClient.objectivesApi.objectivesPut(requestData);
            handleCancelModal()
        } catch (err) {
            console.error('Найдены ошибки заполнения:', err);
        }
    };

    return (
        <div>
            <Modal
                title={
                    <Title
                        level={4}
                        style={{margin: 0, textAlign: 'center'}}>
                        Редактирование цели
                    </Title>}
                open={props.isOpen} onCancel={handleCancelModal} footer={null}>
                <Form form={form} layout="vertical"
                      onFinish={handleEditObjective}>
                    <Form.Item
                        name="name"
                        label="Название"
                        rules={[{required: true, message: 'Пожалуйста, запишите название цели'}]}
                        initialValue={props.objective?.definition ?? ""}
                    >
                        <Input/>
                    </Form.Item>
                    <Form.Item
                        name="statusId"
                        label="Статус"
                        initialValue={props.objective?.status?.id ?? null}
                    >
                        <Select
                            options={statuses.map(status => ({label: status.name, value: status.id}))}
                        />
                    </Form.Item>
                    <Form.Item
                        name="finalDate"
                        label="Контрольная дата"
                        rules={[{required: true, message: 'Пожалуйста, выберите крайний срок'}]}
                        initialValue={dayjs(props.objective?.finalDate!)}
                    >
                        <DatePicker format={dateFormat} style={{width: '100%'}}/>
                    </Form.Item>
                    <Form.Item
                        name="comment"
                        label="Комментарий"
                        rules={[{message: 'Пожалуйста, введите описание цели'}]}
                        initialValue={props.objective?.comment ?? ""}
                    >
                        <TextArea rows={2}/>
                    </Form.Item>
                    <Form.Item>
                        <Flex gap="small">
                            <Button type="default" htmlType="submit">
                                Редактировать
                            </Button>

                            <Button danger type="default" onClick={handleDeleteObjective}>
                                Удалить
                            </Button>
                        </Flex>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    )
}

export default EditObjectiveModal