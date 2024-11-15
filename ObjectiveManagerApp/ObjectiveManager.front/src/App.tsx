import React, {useState, useEffect} from 'react';
import {theme, Typography, Table, Flex, Button, Pagination, Col, Row, Modal, Space, Form, DatePicker, Input} from 'antd';
import {Layout} from 'antd/es';
import type {TableProps} from 'antd';
import {DeleteOutlined, PlusOutlined} from '@ant-design/icons';
import './styles/App.css';
import ApiClient from './api/ApiClient';
import {Objective} from './api/models/Objective';
import {ObjectivesObjectiveIdDeleteRequest, ObjectivesPostRequest} from "./api";
import TextArea from "antd/lib/input/TextArea";

type TablePagination<T extends object> = NonNullable<Exclude<TableProps<T>['pagination'], boolean>>;
type TablePaginationPosition = NonNullable<TablePagination<any>['position']>[number];

function App() {
    const {Header, Footer, Content} = Layout;

    const {Text, Link} = Typography;
    const {Column} = Table;

    const [paginationPosition, setPaginationPosition] = useState<TablePaginationPosition>('none');

    const [objectives, setObjectives] = useState<Objective[]>([]);
    const [pagination, setPagination] = useState({
        current: 1,
        pageSize: 5,
    });

    const [form] = Form.useForm();

    const [isModalOpen, setIsModalOpen] = useState(false);

    const showModal = () => {
        setIsModalOpen(true);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const handleDeleteObjective = async (id: string) => {
        const requestData: ObjectivesObjectiveIdDeleteRequest = {objectiveId: id}
        await ApiClient.objectivesApi.objectivesObjectiveIdDelete(requestData);
        await setObjectivesState();
    };

    const handleCreateObjective = async (validate: React.FormEvent<HTMLFormElement>) => {
        try {
            const values = await form.validateFields(); // Валидация полей

            const requestData: ObjectivesPostRequest = {
                definition: values.name,
                finalDate: values.finalDate,
                comment: values.comment
            }
            await ApiClient.objectivesApi.objectivesPost(requestData);

            await setObjectivesState();
            setIsModalOpen(false);
        } catch (err) {
            console.error('Найдены ошибки заполнения:', err);
        }
    };

    const {
        token: {colorBgContainer, borderRadiusLG},
    } = theme.useToken();

    const setObjectivesState = async () => {
        const objectives = await ApiClient.objectivesApi.objectivesAllGet();
        setObjectives(objectives);
    }

    useEffect(() => {
        setObjectivesState()
    }, []);

    return (
        <div className="App">
            <Layout
                style={{background: colorBgContainer, borderRadius: borderRadiusLG}}
            >
                <Header className="navbar">
                    <Typography.Title level={4} style={{color: '#e6e6e6'}}>
                        Управление целями SMART/OKR
                    </Typography.Title>
                </Header>
                <Content>
                    <Flex
                        vertical
                        gap="small"
                    >
                        <Table<Objective>
                            dataSource={objectives}
                            pagination={{position: [paginationPosition]}}
                        >
                            <Column title="Название" dataIndex="definition" key="definition"/>
                            <Column title="Статус" dataIndex="status" key="status"/>
                            <Column title="Контрольная дата" dataIndex="finalDate" key="finalDate"/>
                            <Column title="Комментарий" dataIndex="comment" key="comment"/>
                            <Column
                                title=""
                                key="action"
                                render={(_: any, selectedObjective: Objective) => (
                                    <Space size="middle">
                                        <DeleteOutlined onClick={() => handleDeleteObjective(selectedObjective.id!)}/>
                                    </Space>
                                )}
                            />
                        </Table>
                        <Row>
                            <Col span={16} style={{display: 'flex', justifyContent: 'center'}}>
                                <Pagination
                                    current={pagination.current}
                                    pageSize={pagination.pageSize}
                                    total={objectives.length}
                                    onChange={(page, pageSize) => setPagination({current: page, pageSize})}
                                    //showSizeChanger
                                    //pageSizeOptions={['5', '10', '20']}
                                />
                            </Col>
                            <Col span={8} style={{display: 'flex', justifyContent: 'flex-end'}}>
                                <Button
                                    className="flex-button-margin"
                                    shape="circle"
                                    size="large"
                                    icon={<PlusOutlined/>}
                                    onClick={showModal}
                                />
                            </Col>
                            {isModalOpen && (
                                <Modal title="Новая цель" open={isModalOpen} onCancel={handleCancel} footer={null}>
                                    <Form form={form} layout="vertical"
                                          onFinish={formValues => handleCreateObjective(formValues)}>
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
                                        <Form.Item>
                                            <Button type="primary" htmlType="submit">
                                                Создать цель
                                            </Button>
                                        </Form.Item>
                                    </Form>
                                </Modal>
                            )}
                        </Row>
                    </Flex>
                </Content>
            </Layout>
        </div>
    );
}

export default App;
