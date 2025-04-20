import React from "react";
import {Col, Row, Typography} from "antd";
import {Modal} from "antd/lib";

const {Title, Paragraph, Text} = Typography;

interface InfoSMARTProps {
    isOpen: boolean;
    onClose: () => void;
}

const InfoSMARTModal: React.FC<InfoSMARTProps> = (props) => {
    return (
        <Modal
            title={
                <Title
                    level={3}
                    style={{margin: 0, textAlign: 'center'}}>
                    Методика постановки целей SMART
                </Title>}
            open={props.isOpen}
            onCancel={props.onClose}
            style={{
                top: 20
            }}
            width={"55%"}
            footer={null}
        >
            <Row>
                <Col>
                    <Typography>
                        <Paragraph>
                            Постановка целей — важная составляющая развития.
                            Подсказка, как можно формулировать цели по SMART:
                        </Paragraph>
                        <ol>
                            <li>
                                <Text strong>Specific</Text> — цель должна быть конкретной.<br/>
                                Вопрос:
                                <ul>
                                    <li>
                                        какого результата я хочу достичь с помощью выбранной
                                        цели?
                                    </li>
                                </ul>
                            </li>
                            <li>
                                <Text strong>Measurable</Text> — цель должна быть измеримой.<br/>
                                Вопросы:
                                <ul>
                                    <li>как понять, что цель достигнута?</li>
                                    <li>над каким показателем необходимо работать, чтобы достигнуть цели?</li>
                                </ul>
                            </li>
                            <li>
                                <Text strong>Achievable</Text> — цель должна быть достижимой.<br/>
                                Вопросы:
                                <ul>
                                    <li>есть ли достаточно ресурсов для достижения?</li>
                                    <li>что может помешать достижению результата?</li>
                                </ul>
                            </li>
                            <li>
                                <Text strong>Relevant</Text> — цель должна быть значимой.<br/>
                                Вопросы:
                                <ul>
                                    <li>какие преимущества получит компания/получу я после достижения цели?</li>
                                    <li>выбранная цель соответствует стратегии компании?</li>
                                </ul>
                            </li>
                            <li>
                                <Text strong>Time bound</Text> — должна быть ограниченной по времени.<br/>
                                Вопросы:
                                <ul>
                                    <li>сколько понадобится времени, чтобы достигнуть цели?</li>
                                    <li>на какие промежуточные этапы цель будет разбита?</li>
                                </ul>
                            </li>
                        </ol>

                        Если необходимо, можно добавить любую дополнительную информацию о цели в комментарий.
                    </Typography>
                </Col>
            </Row>
        </Modal>
    )
}

export default InfoSMARTModal