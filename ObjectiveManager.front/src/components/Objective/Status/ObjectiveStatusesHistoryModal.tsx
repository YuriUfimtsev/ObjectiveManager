import {Modal, Space, Spin, Table} from "antd/lib";
import React, {useEffect, useState} from "react";
import ApiClient from "../../../api/ApiClient";
import {LoadingOutlined} from "@ant-design/icons";
import DateTimeUtils from "../../../utils/DateTimeUtils";
import {StatusObjectDTO} from "../../../api";
import ErrorsHandler from "../../../utils/ErrorsHandler";
import ErrorInfo from "../../ErrorInfo";

const {Column} = Table;

interface IStatusesHistoryProps {
    isOpen: boolean;
    onClose: () => void;
    objectiveId: string;
}

const ObjectiveStatusesHistoryModal: React.FC<IStatusesHistoryProps> = (props) => {
    const [history, setHistory] = useState<StatusObjectDTO[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [errorsState, setErrorsState] = useState<string[]>([])

    useEffect(() => {
        const fetchHistory = async () => {
            try {
                const fetchedHistory = await ApiClient.objectivesApi.apiObjectivesStatusHistoryObjectiveIdGet(
                    {objectiveId: props.objectiveId}
                );
                setHistory(fetchedHistory)
            } catch (e) {
                const errors = await ErrorsHandler.getErrorMessages(e as Response);
                setErrorsState(errors)
            } finally {
                setIsLoading(false)
            }
        }

        fetchHistory()
    }, []);

    return (
        <div>
            <Modal
                title={
                    <div style={{textAlign: 'center', width: '100%'}}>
                        История изменений статусов
                    </div>
                }
                open={props.isOpen}
                onCancel={props.onClose}
                footer={false}
                width={"70%"}
            >
                {errorsState && <ErrorInfo errors={errorsState}/>}
                {isLoading ? (
                    <Space size="small" direction="vertical">
                        <p>Загрузка...</p>
                        <Spin indicator={<LoadingOutlined spin/>} size="large"/>
                    </Space>) : (
                    <Table<StatusObjectDTO>
                        dataSource={history}
                        rowKey="createdAt"
                        pagination={false}
                        style={{
                            marginTop: 12
                        }}
                    >
                        <Column
                            title="Дата"
                            key="createdAt"
                            align="center"
                            render={(_: any, statusObject: StatusObjectDTO) =>
                                DateTimeUtils.renderDateWithoutHours(statusObject.createdAt!)}
                        />
                        <Column
                            align="center"
                            title="Статус"
                            key="status"
                            render={(_: any, statusObject: StatusObjectDTO) => statusObject.statusValue?.name ?? ""}
                        />
                        <Column
                            title="Комментарий"
                            align="center"
                            dataIndex="comment"
                            key="comment"
                        />
                    </Table>)}
            </Modal>
        </div>
    )
}

export default ObjectiveStatusesHistoryModal