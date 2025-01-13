import * as React from "react";
import {Button, DatePicker, Form, Input, Modal, Select} from "antd";
import TextArea from "antd/lib/input/TextArea";
import {ObjectivesPostRequest} from "../api";
import ApiClient from "../api/ApiClient";
import Title from "antd/lib/typography/Title";
import {useEffect, useState} from "react";

interface ICreateObjectiveProps {
    isOpen: boolean;
    onClose: () => void;
}

interface ICreateObjective {
    name: string;
    finalDate: Date;
    comment: string;
}

const CreateObjectiveModal: React.FC<ICreateObjectiveProps> = (props) => {
    const [form] = Form.useForm<ICreateObjective>();

    const handleCreateObjective = async (objective: ICreateObjective) => {
        try {
            const requestData: ObjectivesPostRequest = {
                definition: objective.name,
                finalDate: objective.finalDate,
                comment: objective.comment
            }

            await ApiClient.objectivesApi.objectivesPost(requestData);
            handleCancelModal()
        } catch (err) {
            console.error('Найдены ошибки заполнения:', err);
        }
    };
    
    const handleCancelModal = () => {
        props.onClose();
        form.resetFields()
    }

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
                      onFinish={handleCreateObjective}>
                    <Form.Item
                        name="name"
                        label="Название"
                        rules={[{required: true, message: 'Пожалуйста, запишите название цели'}]}
                    >
                        <Input/>
                    </Form.Item>
                    <Form.Item
                        name="finalDate"
                        label="Контрольная дата"
                        rules={[{required: true, message: 'Пожалуйста, выберите крайний срок'}]}
                    >
                        <DatePicker style={{width: '100%'}}/>
                    </Form.Item>
                    <Form.Item
                        name="comment"
                        label="Комментарий"
                        rules={[{message: 'Пожалуйста, введите описание цели'}]}
                    >
                        <TextArea rows={2}/>
                    </Form.Item>
                    <Form.Item style={{textAlign: 'start'}}>
                        <Button type="default" htmlType="submit">
                            Создать
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    )
}

export default CreateObjectiveModal