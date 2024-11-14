import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';  // Импортируем BrowserRouter
import AdminTable from './components/AdminTable';
import { QueryClient, QueryClientProvider } from 'react-query';
import Sidebar from './components/Sidebar';
import { Box } from '@mui/material';

const queryClient = new QueryClient();

const App: React.FC = () => {
    const [selectedTable, setSelectedTable] = useState<string>('Route');

    const handleTableSelect = (tableName: string) => {
        setSelectedTable(tableName);
    };

    return (
        <QueryClientProvider client={queryClient}>
            <Router>  {/* Оборачиваем все в Router */}
                <Box display="flex" height="100vh">
                    <Sidebar onTableSelect={handleTableSelect} />
                    <Box flex={1} p={2} height="100%">
                        <Routes>
                            <Route path="/admin" element={
                                <QueryClientProvider client={queryClient}>
                                    <AdminTable table={selectedTable} />
                                </QueryClientProvider>
                            } />
                        </Routes>
                    </Box>
                </Box>
            </Router>
        </QueryClientProvider>
    );
};

export default App;
