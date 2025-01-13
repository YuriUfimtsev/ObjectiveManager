import * as React from "react";
import {useEffect, useState} from "react";
import {Button, Col, Flex, Pagination, Row, Space, Table, TablePaginationConfig, type TableProps} from "antd";
import {Objective, ObjectivesObjectiveIdDeleteRequest} from "../api";
import {DeleteOutlined, EditOutlined, FormOutlined, PlusOutlined} from "@ant-design/icons";
import ApiClient from "../api/ApiClient";
import CreateObjectiveModal from "./CreateObjectiveModal";
import EditObjectiveModal from "./EditObjectiveModal";
import DateTimeUtils from "../utils/DateTimeUtils";

interface IObjectivesTableProps {
    isReadingMode?: boolean;
}

interface IObjectivesTableState {
}

type TablePagination<T extends object> = NonNullable<Exclude<TableProps<T>['pagination'], boolean>>;
type TablePaginationPosition = NonNullable<TablePagination<Objective>['position']>[number];

const ObjectivesTable: React.FC<IObjectivesTableProps> = (props) => {
    const [objectives, setObjectives] = useState<Objective[]>([]);
    const [areObjectivesChanged, setObjectivesAreChanged] = useState(false);

    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);

    const [hoveredRowId, setHoveredRowId] = useState<string>("");

    const paginationConfig: TablePaginationConfig = {
        position: ["bottomCenter" as TablePaginationPosition],
        hideOnSinglePage: true,
        pageSize: 5,

    };

    const {Column} = Table;

    const [tableState, setTableState] = useState<IObjectivesTableState>({
        isFound: false,
        course: {},
        courseHomework: [],
        createHomework: false,
        mentors: [],
        acceptedStudents: [],
        newStudents: [],
        isReadingMode: props.isReadingMode ?? true,
        studentSolutions: [],
        showQrCode: false
    })

    const handleCreateModalClose = () => {
        setIsCreateModalOpen(false);
        setObjectivesAreChanged(true)
    }

    const handleEditModalClose = () => {
        setIsEditModalOpen(false);
        setObjectivesAreChanged(true)
    }

    useEffect(() => {
        const updateObjectives = async () => {
            const objectives = await ApiClient.objectivesApi.objectivesAllGet();
            setObjectives(objectives);
        }

        updateObjectives()
        setObjectivesAreChanged(false)
    }, [areObjectivesChanged]);

    return (
        <Flex
            vertical
            gap="small"
        >
            <Table<Objective>
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
                    title="Статус"
                    key="status"
                    align="center"
                    render={(_: any, objective: Objective) => objective.status?.name ?? ''}
                />
                <Column
                    title="Контрольная дата"
                    key="finalDate"
                    align="center"
                    render={(_: any, objective: Objective) =>
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
                            onClick={() => setIsCreateModalOpen(true)}
                        >
                            Добавить
                        </Button>
                    }
                    key="action"
                    render={(_: any, selectedObjective: Objective) =>
                        selectedObjective.id === hoveredRowId && (
                            <EditOutlined
                                onClick={() => setIsEditModalOpen(true)}
                                style={{
                                    fontSize: "medium"
                                }}
                            />
                        )}
                />
            </Table>
            {isCreateModalOpen && (
                <CreateObjectiveModal isOpen={isCreateModalOpen} onClose={handleCreateModalClose}/>)}
            {isEditModalOpen && (
                <EditObjectiveModal
                    isOpen={isEditModalOpen}
                    onClose={handleEditModalClose}
                    objective={objectives.find(objective => objective.id === hoveredRowId)}
                />)}
        </Flex>
    )
}

export default ObjectivesTable