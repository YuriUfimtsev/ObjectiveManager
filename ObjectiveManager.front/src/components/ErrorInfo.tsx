import {FC} from "react";
import {Col, Row} from "antd";
import * as React from "react";

interface ErrorInfoProps {
    errors: string[];
}

const ErrorInfo: FC<ErrorInfoProps> = (props) =>
    <Row justify="center">
        <Col>
            <p style={{color: "red", marginBottom: "0"}}>
                {props.errors[0]}
            </p>
        </Col>
    </Row>

export default ErrorInfo