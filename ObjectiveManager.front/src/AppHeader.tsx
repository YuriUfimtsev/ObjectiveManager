import {Outlet, useNavigate} from "react-router-dom";
import {Layout, Menu} from "antd";
import {Typography} from "antd/lib";
import {UserOutlined} from "@ant-design/icons";
import React, {useState} from "react";
import InfoSMARTModal from "./components/Info/InfoSMARTModal";

const {Header, Content} = Layout;

interface HeaderProps {
    loggedIn: boolean;
}

const AppHeader: React.FC<HeaderProps> = (props) => {
    const [selectedKey, setSelectedKey] = useState<string>("");
    const [isSmartModalVisible, setIsSmartModalVisible] = useState<boolean>(false);

    const navigate = useNavigate();

    return (
        <div className="App">
            <Layout>
                <Header style={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                    paddingLeft: 30,
                    paddingRight: 30,
                    height: 50
                }}>
                    <Typography.Title
                        level={4}
                        style={{
                            color: '#e6e6e6',
                            marginBottom: 3,
                            marginRight: 10,
                            cursor: "pointer"
                        }}
                        onClick={() => {
                            setSelectedKey("/");
                            navigate("/");
                        }}
                    >
                        Управление целями
                    </Typography.Title>
                    {props.loggedIn &&
                        <Menu
                            selectedKeys={[selectedKey]}
                            onClick={({key}) => {
                                setSelectedKey(key);
                                navigate(key);
                            }}
                            theme="dark"
                            mode="horizontal"
                            style={{display: "flex", justifyContent: "flex-start", flex: 1}}
                        >
                            <Menu.Item key="/" onClick={() => setIsSmartModalVisible(true)}
                                       style={{backgroundColor: 'transparent'}}>
                                О SMART
                            </Menu.Item>
                            <Menu.Item key="/editProfile" icon={<UserOutlined/>}
                                       style={{backgroundColor: 'transparent', marginLeft: 'auto'}}>
                                Профиль
                            </Menu.Item>
                        </Menu>}
                </Header>
                <Content>
                    <div style={{backgroundColor: '#ffffff'}}>
                        <Outlet/>
                    </div>
                </Content>
                <InfoSMARTModal isOpen={isSmartModalVisible} onClose={() => setIsSmartModalVisible(false)}/>
            </Layout>
        </div>
    );
}

export default AppHeader