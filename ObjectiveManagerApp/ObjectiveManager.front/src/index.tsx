import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';

import { ConfigProvider } from 'antd';

ConfigProvider.config({ theme: {  } });

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
    <ConfigProvider>
        <App />
    </ConfigProvider>
);