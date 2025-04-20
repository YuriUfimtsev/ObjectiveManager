import {Modal} from "antd/lib";
import {DeleteOutlined, ExclamationCircleFilled} from "@ant-design/icons";
import React from "react";
import {ObjectiveDTO, ResponseError} from "../../api";
import ApiClient from "../../api/ApiClient";
import DateTimeUtils from "../../utils/DateTimeUtils";
import ErrorsHandler from "../../utils/ErrorsHandler";

const {confirm} = Modal;

interface IDeleteObjectiveProps {
    objective: ObjectiveDTO | null;
    onClose: () => void;
}

const DeleteObjectiveModal: React.FC<IDeleteObjectiveProps> = (props) => {
    const handleDeleteObjective = async () => {
        try {
            await ApiClient.objectivesApi.apiObjectivesObjectiveIdDelete({objectiveId: props.objective?.id!});
            props.onClose()
        } catch (e) {
            const errors = await ErrorsHandler.getErrorMessages(e as ResponseError);
            await ErrorsHandler.showErrorMessage(errors[0])
        }
    };

    const showDeleteConfirm = () => {
        confirm({
            title: "Вы уверены, что хотите удалить цель?",
            icon: <ExclamationCircleFilled/>,
            content: `${props?.objective?.definition ?? ""} к
             ${props?.objective?.finalDate ? DateTimeUtils.renderDateWithoutHours(props.objective.finalDate) : ""}.`,
            okText: 'Да',
            okType: 'danger',
            cancelText: 'Нет',
            onOk() {
                handleDeleteObjective();
            }
        });
    };

    return (
        <div>
            <DeleteOutlined
                onClick={showDeleteConfirm}
                style={{
                    fontSize: "medium",
                    color: "#cf1322"
                }}
            />
        </div>
    )
}

export default DeleteObjectiveModal