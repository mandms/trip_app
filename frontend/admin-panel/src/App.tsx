import React, { useState } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from 'react-query';
import Sidebar from './features/Sidebar/Sidebar';
import { Box } from '@mui/material';
import Content from './features/Content/Content';
import { CurrentUserContext } from './stores/CurrentUserContext';
import { IUser } from './types/types.user';

const queryClient = new QueryClient();

const App: React.FC = () => {
  const [user, setUser] = useState<IUser | null>(null);

  const value = { user, setUser: (user: IUser | null) => setUser(user) };

  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <Box display="flex" height="100%">
          <CurrentUserContext.Provider value={value}>
            <Sidebar />
            <Content />
          </CurrentUserContext.Provider>
        </Box>
      </Router>
    </QueryClientProvider>
  );
};

export default App;
