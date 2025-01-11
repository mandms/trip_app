import React from 'react';
import {
  List,
  ListItemText,
  Box,
  ListItemButton,
  Divider,
  Typography,
} from '@mui/material';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import UserService from '../api/api.user';
import RequireAuth from './Auth/RequireAuth';

const Sidebar: React.FC = () => {
  const data = {
    tables: [
      { name: 'Маршрут', path: '/route' },
      { name: 'Пользователь', path: '/user' },
      { name: 'Тэг', path: '/tag' },
      { name: 'Локации', path: '/location' },
      { name: 'Момент', path: '/moment' },
      { name: 'Отзыв', path: '/review' },
    ],
  };

  const location = useLocation();
  const navigate = useNavigate();

  const handleLogoutClick = () => {
    UserService.logout();
    navigate('/');
  };

  return (
    <Box sx={{ width: 250, borderRight: '1px solid #ddd', paddingTop: '10px' }}>
      <Typography textAlign="center" variant="h5" gutterBottom>
        <b>
          ADMIN <br />
          TRIP APP
        </b>
      </Typography>
      <Divider />
      <RequireAuth>
        <List>
          {data?.tables.map((table: { name: string; path: string }) => (
            <ListItemButton
              component={Link}
              to={`${table.path.toLowerCase()}`}
              key={table.name}
              sx={{
                backgroundColor:
                  location.pathname === table.path ? '#f0f0f0' : 'transparent',
              }}
            >
              <ListItemText primary={table.name} />
              <ArrowForwardIosIcon fontSize="small" />
            </ListItemButton>
          ))}
          <ListItemButton onClick={() => handleLogoutClick()}>
            <ListItemText sx={{ color: 'red' }}>Выйти</ListItemText>
          </ListItemButton>
        </List>
      </RequireAuth>
    </Box>
  );
};

export default Sidebar;
