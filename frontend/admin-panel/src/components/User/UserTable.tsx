import React, { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from 'react-query';
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Box,
  Typography,
  Avatar,
} from '@mui/material';
import Pagination from '../Pagination';
import UserService from '../../api/api.user';
import CreateUser from './CreateUser';
import UpdateUser from './UpdateUser';
import { IUpdateUser, IUser, IUsers } from '../../types/types.user';
import Filter from '../Filter/Filter';
import { IFilter } from '../../types/types.filter';

const UserTable: React.FC = () => {
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState(4);
  const [userId, setUserId] = useState<string>('');
  const [users, setUsers] = useState<IUsers | null>(null);
  const [updateUser, setUpdateUser] = useState<IUpdateUser | null>(null);
  const [filter, setFilter] = useState<IFilter>();
  const [open, setOpen] = useState<{
    create: boolean;
    update: boolean;
    detail: boolean;
  }>({
    create: false,
    update: false,
    detail: false,
  });

  const sortParams = [
    { label: 'Имя пользователя', value: 'username' },
    { label: 'Почта', value: 'email' },
  ];

  const queryClient = useQueryClient();

  const { isLoading, isError, error } = useQuery(
    ['users', page, rowsPerPage, filter],
    () => UserService.getAll(page + 1, rowsPerPage, filter),
    {
      keepPreviousData: true,
      onSuccess: (users) => setUsers(users),
    },
  );

  const mutationFilter = useMutation(
    (query: IFilter) => UserService.getAll(page + 1, rowsPerPage, query),
    {
      onSuccess: (filteredUsers) => {
        setUsers(filteredUsers);
      },
    },
  );

  const handleUpdateRoute = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    user: IUser,
  ) => {
    event.stopPropagation();
    const updatedUser: IUpdateUser = {
      username: user.username,
      biography: user.biography,
      avatar: { image: user.avatar },
    };
    setUserId(user.id);
    setUpdateUser(updatedUser);
    handleOpen('update');
  };

  const mutationDelete = useMutation(UserService.delete, {
    onSuccess: async () => {
      await queryClient.invalidateQueries('users');
    },
  });

  const handleDeleteRecord = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    id: string,
  ) => {
    event.stopPropagation();
    mutationDelete.mutate(id);
  };

  const handlePageChange = (newPage: number) => {
    setPage(newPage);
  };

  const handleOpen = (modal: string) => {
    setOpen((prevState) => ({
      ...prevState,
      [modal]: true,
    }));
  };

  const handleClose = (modal: string) => {
    setOpen((prevState) => ({
      ...prevState,
      [modal]: false,
    }));
  };

  const handleRowsPerPageChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const onFilterChange = (query: IFilter) => {
    setFilter(query);
    setPage(0);
    mutationFilter.mutate(query);
  };

  if (isLoading)
    return (
      <div
        style={{
          height: '100%',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
        }}
      >
        <Typography textAlign="center" variant="h5">
          Loading...
        </Typography>
      </div>
    );
  if (isError) return <p>Error: {String(error)}</p>;

  return (
    <Box>
      <Paper>
        <Box sx={{ display: 'flex', gap: 2, mb: 2, paddingTop: 2 }}>
          <Button
            onClick={() => handleOpen('create')}
            variant="contained"
            color="success"
            size="small"
            style={{ marginLeft: '10px' }}
          >
            Создать
          </Button>
          <Filter sortParams={sortParams} onFilterChange={onFilterChange} />
        </Box>
        <CreateUser open={open.create} close={() => handleClose('create')} />
        {updateUser && (
          <UpdateUser
            close={() => handleClose('update')}
            open={open.update}
            userId={userId}
            updatedUser={updateUser}
          />
        )}
        <TableContainer>
          <Table style={{ height: '100%', tableLayout: 'fixed' }}>
            <TableHead>
              <TableRow>
                <TableCell>Имя пользователя</TableCell>
                <TableCell>Почта</TableCell>
                <TableCell>Аватар</TableCell>
                <TableCell>Роли</TableCell>
                <TableCell>Действия</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {users &&
                users.data.map((user: IUser) => (
                  <TableRow hover key={user.id}>
                    <TableCell>{user.username}</TableCell>
                    <TableCell>{user.email}</TableCell>
                    <TableCell>
                      <Avatar
                        src={`${process.env.REACT_APP_API_RESOURCES}/${user.avatar}`}
                        alt={user.username}
                      />
                    </TableCell>
                    <TableCell>{user.roles}</TableCell>
                    <TableCell>
                      <div
                        style={{
                          display: 'flex',
                          flexDirection: 'column',
                          gap: '5px',
                          minWidth: '100px',
                        }}
                      >
                        <Button
                          onClick={(event) => handleUpdateRoute(event, user)}
                          variant="contained"
                          color="primary"
                          size="small"
                        >
                          Редактировать
                        </Button>
                        <Button
                          onClick={(event) =>
                            handleDeleteRecord(event, user.id)
                          }
                          variant="contained"
                          color="secondary"
                          size="small"
                        >
                          Удалить
                        </Button>
                      </div>
                    </TableCell>
                  </TableRow>
                ))}
            </TableBody>
          </Table>
        </TableContainer>
        <Pagination
          totalItems={users?.totalRecords || 0}
          currentPage={page}
          onPageChange={handlePageChange}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </Paper>
    </Box>
  );
};

export default UserTable;
