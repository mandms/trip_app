import React, { useState } from 'react';
import { useQuery, useMutation, useQueryClient } from 'react-query';
import RouteService from '../../../api/api.route';
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
  Rating,
} from '@mui/material';
import Pagination from '../../Pagination';
import CreateRoute from '../create/CreateRoute';
import UpdateRoute from '../update/UpdateRoute';
import RouteDetail from '../detail/RouteDetail';
import { IRoute, IRoutes } from '../../../types/types.route';
import Filter from '../../Filter/Filter';
import { FilterParamsType, IFilter } from '../../../types/types.filter';

const RouteTable: React.FC = () => {
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState(4);
  const [routeId, setRouteId] = useState<string>('');
  const [routes, setRoutes] = useState<IRoutes | null>(null);
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
    { label: 'Id', value: 'id' },
    { label: 'Название', value: 'name' },
    { label: 'Описание', value: 'description' },
    { label: 'Продолжительность', value: 'duration' },
  ];
  const filterParams: FilterParamsType = 'tag';

  const queryClient = useQueryClient();

  const { isLoading, isError, error } = useQuery(
    ['routes', page, rowsPerPage, filter],
    () => RouteService.getAll(page + 1, rowsPerPage, filter),
    {
      onSuccess: (routes) => setRoutes(routes),
      keepPreviousData: true,
    },
  );

  const mutationFilter = useMutation(
    (query: IFilter) => RouteService.getAll(page + 1, rowsPerPage, query),
    {
      onSuccess: (filteredRoutes) => {
        setRoutes(filteredRoutes);
      },
    },
  );

  const handleUpdateRoute = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    routeId: string,
  ) => {
    event.stopPropagation();
    setRouteId(routeId);
    handleOpen('update');
  };

  const mutationDelete = useMutation(RouteService.delete, {
    onSuccess: async () => {
      await queryClient.invalidateQueries('routes');
    },
  });

  const handleDeleteRoute = (
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

  const handleRowClick = (id: string) => {
    setRouteId(id);
    setOpen((prevState) => ({
      ...prevState,
      detail: true,
    }));
  };

  const transformDuration = (duration: number) => {
    if (duration > 24) {
      return `${Math.floor(duration / 24)} д. ${duration % 24} ч.`;
    }
    return `${duration} ч.`;
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

  const onFilterChange = (query: IFilter) => {
    setFilter(query);
    setPage(0);
    mutationFilter.mutate(query);
  };

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
          <Filter
            filterParams={filterParams}
            sortParams={sortParams}
            onFilterChange={onFilterChange}
          />
        </Box>
        <CreateRoute open={open.create} close={() => handleClose('create')} />
        <UpdateRoute
          close={() => handleClose('update')}
          open={open.update}
          routeId={routeId}
        />
        <RouteDetail
          open={open.detail}
          routeId={routeId}
          onClose={() => handleClose('detail')}
        />
        <TableContainer>
          <Table style={{ height: '100%', tableLayout: 'fixed' }}>
            <TableHead>
              <TableRow>
                <TableCell>Средний рейтинг</TableCell>
                <TableCell>Название</TableCell>
                <TableCell>Описание</TableCell>
                <TableCell>Продолжительность</TableCell>
                <TableCell>Пользователь</TableCell>
                <TableCell>Тэги</TableCell>
                <TableCell>Действия</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {routes &&
                routes.data.map((route: IRoute) => (
                  <TableRow
                    hover
                    sx={{ cursor: 'pointer' }}
                    onClick={() => handleRowClick(route.id)}
                    key={route.id}
                  >
                    <TableCell>
                      <Rating
                        name="rate"
                        value={route.rating}
                        max={5}
                        readOnly={true}
                      />
                    </TableCell>
                    <TableCell>
                      <Typography noWrap>{route.name}</Typography>
                    </TableCell>
                    <TableCell>
                      <Typography noWrap>{route.description}</Typography>
                    </TableCell>
                    <TableCell>{transformDuration(route.duration)}</TableCell>
                    <TableCell>{route.user.username}</TableCell>
                    <TableCell>
                      <Typography noWrap>
                        {route.tags.map((tag) => tag.name).join(', ')}
                      </Typography>
                    </TableCell>
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
                          onClick={(event) =>
                            handleUpdateRoute(event, route.id)
                          }
                          variant="contained"
                          color="primary"
                          size="small"
                        >
                          Редактировать
                        </Button>
                        <Button
                          onClick={(event) =>
                            handleDeleteRoute(event, route.id)
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
          totalItems={routes?.totalRecords || 0}
          currentPage={page}
          onPageChange={handlePageChange}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </Paper>
    </Box>
  );
};

export default RouteTable;
