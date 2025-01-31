import {Modal, Space, Spin} from "antd/lib";
import React, {useEffect, useState} from "react";
import ApiClient from "../../../api/ApiClient";
import {LoadingOutlined} from "@ant-design/icons";
import Title from "antd/lib/typography/Title";
import {Button, Flex, Form, Select} from "antd";
import TextArea from "antd/lib/input/TextArea";
import {ApiObjectivesUpdateStatusObjectObjectiveIdPutRequest, StatusObjectDTO, StatusValueDTO} from "../../../api";
import ErrorsHandler from "../../../utils/ErrorsHandler";
import ErrorInfo from "../../ErrorInfo";

interface IEditObjectiveStatusProps {
    isOpen: boolean;
    onClose: () => void;
    currentStatusObject: StatusObjectDTO;
    objectiveId: string;
}

interface IEditObjectiveStatus {
    statusId: number;
    statusComment: string;
}

const EditObjectiveStatusModal: React.FC<IEditObjectiveStatusProps> = (props) => {
    const [availableStatuses, setAvailableStatuses] = useState<StatusValueDTO[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [errorsState, setErrorsState] = useState<string[]>([])

    useEffect(() => {
        const fetchStatuses = async () => {
            try {
                const statuses = await ApiClient.statusesApi.apiStatusesAllGet();
                setAvailableStatuses(statuses);
            } catch (e) {
                const errors = await ErrorsHandler.getErrorMessages(e as Response);
                setErrorsState(errors)
            } finally {
                setIsLoading(false)
            }
        }

        fetchStatuses()
    }, []);

    const handleEditStatus = async (statusModel: IEditObjectiveStatus) => {
        try {
            if (props.currentStatusObject.statusValue?.id === statusModel.statusId) {
                setErrorsState(['Статус совпадает с предыдущим статусом'])
                return
            }

            const requestData: ApiObjectivesUpdateStatusObjectObjectiveIdPutRequest = {
                objectiveId: props.objectiveId,
                statusValueId: statusModel.statusId,
                statusComment: statusModel.statusComment
            }
            await ApiClient.objectivesApi.apiObjectivesUpdateStatusObjectObjectiveIdPutRaw(requestData);
            props.onClose()
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as Response);
            setErrorsState(errors)
        }
    };

    const [form] = Form.useForm<IEditObjectiveStatus>();

    if (isLoading) {
        return (
            <Space style={{marginLeft: 10, marginTop: 10}} size="small" direction="vertical">
                <p>Загрузка доступных статусов...</p>
                <Spin indicator={<LoadingOutlined spin/>} size="large"/>
            </Space>
        )
    }

    return (
        <div>
            <Modal
                title={
                    <Title
                        level={4}
                        style={{margin: 0, textAlign: 'center'}}>
                        Обновление статуса
                    </Title>}
                open={props.isOpen} onCancel={props.onClose} footer={null}>
                <Form form={form} layout="vertical"
                      onFinish={handleEditStatus}>
                    {errorsState && <ErrorInfo errors={errorsState}/>}
                    <Form.Item
                        name="statusId"
                        label="Статус"
                        initialValue={props.currentStatusObject.statusValue?.id ?? null}
                    >
                        <Select
                            options={availableStatuses.map(status => ({label: status.name, value: status.id}))}
                        />
                    </Form.Item>
                    <Form.Item
                        name="statusComment"
                        label="Комментарий к изменению статуса"
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

export default EditObjectiveStatusModal