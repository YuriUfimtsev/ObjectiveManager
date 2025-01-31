import * as React from "react";
import {useEffect, useState} from "react";
import {Button, Divider, Flex, Space, Table, TablePaginationConfig, type TableProps} from "antd";
import {EditOutlined, HistoryOutlined, LoadingOutlined, RetweetOutlined} from "@ant-design/icons";
import ApiClient from "../api/ApiClient";
import DateTimeUtils from "../utils/DateTimeUtils";
import {Spin} from "antd/lib";
import {ObjectiveDTO} from "../api";
import CreateObjectiveModal from "./Objective/CreateObjectiveModal";
import DeleteObjectiveModal from "./Objective/DeleteObjectiveModal";
import EditObjectiveInfoModal from "./Objective/EditObjectiveInfoModal";
import ObjectiveStatusesHistoryModal from "./Objective/Status/ObjectiveStatusesHistoryModal";
import EditObjectiveStatusModal from "./Objective/Status/EditObjectiveStatusModal";
import ErrorsHandler from "../utils/ErrorsHandler";

const {Column} = Table;
type TablePagination<T extends object> = NonNullable<Exclude<TableProps<T>['pagination'], boolean>>;
type TablePaginationPosition = NonNullable<TablePagination<ObjectiveDTO>['position']>[number];
const paginationConfig: TablePaginationConfig = {
    position: ["bottomCenter" as TablePaginationPosition],
    hideOnSinglePage: true,
    pageSize: 5
};

interface IModalsState {
    isCreateModalOpen: boolean;
    isEditInfoModalOpen: boolean;
    isEditStatusModalOpen: boolean;
    isStatusesHistoryModalOpen: boolean;
}

const ObjectivesTable: React.FC = () => {
    const [objectives, setObjectives] = useState<ObjectiveDTO[]>([]);
    const [areObjectivesChanged, setObjectivesAreChanged] = useState(false);

    const [modalsState, setModalsState] = useState<IModalsState>({
        isCreateModalOpen: false,
        isEditInfoModalOpen: false,
        isEditStatusModalOpen: false,
        isStatusesHistoryModalOpen: false
    });

    const [hoveredRowId, setHoveredRowId] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetchObjectives = async () => {
            try {
                const objectives = await ApiClient.objectivesApi.apiObjectivesAllGet();
                setObjectives(objectives);
                setObjectivesAreChanged(false)
            } catch
                (e) {
                const errors = await ErrorsHandler.getErrorMessages(e as Response);
                await ErrorsHandler.showErrorMessage(errors[0])
            } finally {
                setIsLoading(false)
            }
        }

        fetchObjectives()
    }, [areObjectivesChanged]);

    if (isLoading) {
        return (
            <Space style={{marginLeft: 10, marginTop: 10}} size="small" direction="vertical">
                <p>Загрузка...</p>
                <Spin indicator={<LoadingOutlined spin/>} size="large"/>
            </Space>
        )
    }

    return (
        <Flex
            vertical
            gap="small"
        >
            <Table<ObjectiveDTO>
                dataSource={objectives}
                pagination={paginationConfig}
                rowKey="id"
                onRow={objective => ({
                    onMouseEnter: () => setHoveredRowId(objective.id!),
                })}
            >
                <Column
                    align="center"
                    title="Название"
                    dataIndex="definition"
                    key="definition"
                />
                <Column
                    title={
                        <div style={{textAlign: 'center', width: '100%'}}>
                            <Space size={4}>
                                Статус
                            </Space>
                        </div>
                    }
                    key="status"
                    align="center"
                    render={(_: any, objective: ObjectiveDTO) =>
                        <Space size="middle">
                            {objective.id === hoveredRowId && (
                                <RetweetOutlined
                                    onClick={() => {
                                        setModalsState((prevState) => ({
                                            ...prevState,
                                            isEditStatusModalOpen: true
                                        }))
                                    }}
                                    style={{
                                        fontSize: "small",
                                    }}/>
                            )}
                            {objective.statusObject?.statusValue?.name ?? 'Без названия'}
                            {objective.id === hoveredRowId && (
                                <HistoryOutlined
                                    onClick={() => {
                                        setModalsState((prevState) => ({
                                            ...prevState,
                                            isStatusesHistoryModalOpen: true
                                        }))
                                    }}
                                    style={{
                                        fontSize: "small",
                                    }}/>
                            )}
                        </Space>
                    }
                />
                <Column
                    title="Контрольная дата"
                    key="finalDate"
                    align="center"
                    render={(_: any, objective: ObjectiveDTO) =>
                        DateTimeUtils.renderDateWithoutHours(objective.finalDate!)}
                />
                <Column
                    title="Комментарий"
                    align="center"
                    dataIndex="comment"
                    key="comment"
                />
                <Column
                    align="center"
                    title={
                        <Button
                            type="default"
                            onClick={() => {
                                setModalsState((prevState) => ({
                                    ...prevState,
                                    isCreateModalOpen: true
                                }))
                            }}
                        >
                            Добавить
                        </Button>
                    }
                    key="action"
                    render={(_: any, selectedObjective: ObjectiveDTO) =>
                        selectedObjective.id === hoveredRowId && (
                            <Space split={<Divider type="vertical"/>} size="large">
                                <EditOutlined
                                    onClick={() => {
                                        setModalsState((prevState) => ({
                                            ...prevState,
                                            isEditInfoModalOpen: true
                                        }))
                                    }}
                                    style={{
                                        fontSize: "medium",
                                    }}
                                />
                                <DeleteObjectiveModal
                                    objective={selectedObjective}
                                    onClose={() => setObjectivesAreChanged(true)}
                                />
                            </Space>
                        )}
                />
            </Table>
            {modalsState.isCreateModalOpen && (
                <CreateObjectiveModal
                    isOpen={modalsState.isCreateModalOpen}
                    onClose={() => {
                        setModalsState((prevState) => ({
                            ...prevState,
                            isCreateModalOpen: false
                        }))
                        setObjectivesAreChanged(true)
                    }}
                />)}
            {modalsState.isEditInfoModalOpen && (
                <EditObjectiveInfoModal
                    isOpen={modalsState.isEditInfoModalOpen}
                    onClose={() => {
                        setModalsState((prevState) => ({
                            ...prevState,
                            isEditInfoModalOpen: false
                        }))
                        setObjectivesAreChanged(true)
                    }}
                    objective={objectives.find(objective => objective.id === hoveredRowId)!}
                />)}
            {modalsState.isStatusesHistoryModalOpen && (
                <ObjectiveStatusesHistoryModal
                    isOpen={modalsState.isStatusesHistoryModalOpen}
                    onClose={() => {
                        setModalsState((prevState) => ({
                            ...prevState,
                            isStatusesHistoryModalOpen: false
                        }))
                    }}
                    objectiveId={hoveredRowId}
                />)}
            {modalsState.isEditStatusModalOpen && (
                <EditObjectiveStatusModal
                    isOpen={modalsState.isEditStatusModalOpen}
                    onClose={() => {
                        setModalsState((prevState) => ({
                            ...prevState,
                            isEditStatusModalOpen: false
                        }))
                        setObjectivesAreChanged(true)
                    }}
                    objectiveId={hoveredRowId}
                    currentStatusObject={objectives.find(objective => objective.id === hoveredRowId)!.statusObject!}
                />)}
        </Flex>
    )
}

export default ObjectivesTable