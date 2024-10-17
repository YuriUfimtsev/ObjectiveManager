import React, { useState, useEffect } from 'react';
import { theme, Typography, Table, Flex, Button, Pagination, Col, Row, Modal } from 'antd';
import { Layout } from 'antd/es';
import type { TableProps } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import './styles/App.css';
import ApiClient from './api/ApiClient';
import { Objective } from './api/models/Objective';

type TablePagination<T extends object> = NonNullable<Exclude<TableProps<T>['pagination'], boolean>>;
type TablePaginationPosition = NonNullable<TablePagination<any>['position']>[number];

function App() {
    const { Header, Footer, Content } = Layout;
    
    const { Text, Link } = Typography;
    const { Column } = Table;
    
    const [ paginationPosition, setPaginationPosition ] = useState<TablePaginationPosition>('none');

    const [ objectives, setObjectives ] = useState<Objective[]>([]);
    const [pagination, setPagination] = useState({
        current: 1,
        pageSize: 5,
    });

    const [isModalOpen, setIsModalOpen] = useState(false);

    const showModal = () => {
        setIsModalOpen(true);
    };

    const handleOk = () => {
        setIsModalOpen(false);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const setInitialState = async () => {
        const objectives = await ApiClient.objectivesApi.objectivesAllGet();
        setObjectives(objectives);
    }
    
    useEffect(() => {
        setInitialState()
    }, []);
        
    return (
    <div className="App">
        <Layout
            style={{ background: colorBgContainer, borderRadius: borderRadiusLG }}
        >
            <Header className="navbar">
                <Typography.Title level={4} style={{ color: '#e6e6e6' }}>
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
                        pagination={{ position: [paginationPosition] }}
                    >
                        <Column title="Номер" dataIndex="id" key="id" />
                        <Column title="Название" dataIndex="definition" key="definition" />
                        <Column title="Статус" dataIndex="status" key="status" />
                        <Column title="Контрольная дата" dataIndex="finalDate" key="finalDate" />
                        <Column title="Комментарий" dataIndex="comment" key="comment" />
                    </Table>
                    <Row>
                        <Col span={16} style={{ display: 'flex', justifyContent: 'center' }}>
                            <Pagination
                                current={pagination.current}
                                pageSize={pagination.pageSize}
                                total={objectives.length}
                                onChange={(page, pageSize) => setPagination({ current: page, pageSize })}
                                //showSizeChanger
                                //pageSizeOptions={['5', '10', '20']}
                            />
                        </Col>
                        <Col span={8} style={{ display: 'flex', justifyContent: 'flex-end' }}>
                            <Button
                                className="flex-button-margin"
                                shape="circle"
                                size="large"
                                icon={<PlusOutlined />}
                                onClick={showModal}
                            />
                        </Col>
                        {isModalOpen && (
                            <Modal title="Новая цель" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
                                <p>Создайте цель</p>
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
