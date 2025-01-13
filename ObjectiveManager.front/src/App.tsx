import React from 'react';
import {theme, Typography, Flex} from 'antd';
import {Layout} from 'antd/es';
import './styles/App.css';
import ObjectivesTable from "./components/ObjectivesTable";

function App() {
    const {Header, Content} = Layout;

    const {
        token: {colorBgContainer, borderRadiusLG},
    } = theme.useToken();

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
                    <ObjectivesTable/>
                </Content>
            </Layout>
        </div>
    );
}

export default App;
