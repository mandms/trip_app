import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import Sidebar from './components/Sidebar';
import { Box } from '@mui/material';
import Content from './components/Content';

const queryClient = new QueryClient();

const App: React.FC = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <Box display="flex" height="100%">
          <Sidebar />
          <Content />
        </Box>
      </Router>
    </QueryClientProvider>
  );
};

export default App;
