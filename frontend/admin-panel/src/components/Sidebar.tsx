import React from 'react';
import {List, ListItemText, Box, ListItemButton, Divider, Typography} from '@mui/material';
import {useQuery} from 'react-query';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';

// Запрос на получение списка таблиц
const fetchTables = async () => {
    const response = await fetch('/api/tables'); // Запрос на получение таблиц
    if (!response.ok) throw new Error('Ошибка при загрузке таблиц');
    return response.json(); // Возвращаем данные в виде списка таблиц
};

const Sidebar: React.FC<{ onTableSelect: (tableName: string) => void }> = ({onTableSelect}) => {
    let {data, isLoading, isError, error} = useQuery('tables', fetchTables);

    // if (isLoading) return <p>Loading...</p>;
    // if (isError) return <p>Error: {String(error)}</p>;
    data = {tables: ["Route", "User", "Tag"]}

    return (
        <Box sx={{width: 250, borderRight: '1px solid #ddd'}} paddingTop="10px">
            <Typography textAlign="center" variant="h5" gutterBottom>
                <b>ADMIN <br/>TRIP APP</b>
            </Typography>
            <Divider/>
            <List>
                {data?.tables.map((tableName: string) => (
                    <ListItemButton component="a" key={tableName} onClick={() => onTableSelect(tableName)}>
                        <ListItemText primary={tableName}/>
                        <ArrowForwardIosIcon fontSize="small" />
                    </ListItemButton>
                ))}
            </List>
        </Box>
    );
};

export default Sidebar;