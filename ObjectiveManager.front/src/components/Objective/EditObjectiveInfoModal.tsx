import * as React from "react";
import {Button, DatePicker, Flex, Form, Modal} from "antd";
import TextArea from "antd/lib/input/TextArea";
import Title from "antd/lib/typography/Title";
import dayjs from 'dayjs';
import {ApiObjectivesUpdateInfoObjectiveIdPutRequest, ObjectiveDTO, ResponseError} from "../../api";
import ApiClient from "../../api/ApiClient";
import DateTimeUtils from "../../utils/DateTimeUtils";
import ErrorsHandler from "../../utils/ErrorsHandler";
import {useState} from "react";
import ErrorInfo from "../ErrorInfo";
import {InfoCircleOutlined} from "@ant-design/icons";

interface IEditObjectiveInfoProps {
    isOpen: boolean;
    onClose: () => void;
    objective: ObjectiveDTO;
}

interface IEditObjectiveInfo {
    definition: string;
    finalDate: Date;
    comment: string;
}

const EditObjectiveInfoModal: React.FC<IEditObjectiveInfoProps> = (props) => {
    const [errorsState, setErrorsState] = useState<string[]>([])
    
    const handleCancelModal = () => {
        props.onClose();
        form.resetFields()
    }
    
    const handleEditObjective = async (objective: IEditObjectiveInfo) => {
        try {
            const requestData: ApiObjectivesUpdateInfoObjectiveIdPutRequest = {
                objectiveId: props.objective.id!,
                definition: objective.definition,
                finalDate: objective.finalDate,
                comment: objective.comment,
            }

            await ApiClient.objectivesApi.apiObjectivesUpdateInfoObjectiveIdPut(requestData);
            handleCancelModal()
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
            setErrorsState(errors)
        }
    };

    const [form] = Form.useForm<IEditObjectiveInfo>();

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
                    {errorsState && <ErrorInfo errors={errorsState}/>}
                    <Form.Item
                        name="definition"
                        label="Название"
                        rules={[{required: true, message: 'Пожалуйста, запишите название цели'}]}
                        initialValue={props.objective?.definition ?? ""}
                    >
                        <TextArea autoSize={{minRows: 1, maxRows: 2}}/>
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
                        initialValue={dayjs(props.objective?.finalDate!)}
                    >
                        <DatePicker format={DateTimeUtils.DateOnlyFormat} style={{width: '100%'}}/>
                    </Form.Item>
                    <Form.Item
                        name="comment"
                        label="Комментарий"
                        rules={[{message: 'Пожалуйста, введите описание цели'}]}
                        initialValue={props.objective?.comment ?? ""}
                    >
                        <TextArea autoSize={{minRows: 2, maxRows: 8}}/>
                    </Form.Item>
                    <Form.Item>
                        <Flex gap="small">
                            <Button variant="filled" type="primary" htmlType="submit" size="middle">
                                Сохранить
                            </Button>
                        </Flex>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    )
}

export default EditObjectiveInfoModal