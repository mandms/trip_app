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
} from '@mui/material';
import Pagination from '../../components/Pagination/Pagination';
import MomentService from '../../api/api.moment';
import CreateMomentModal from '../../features/Moment/CreateMomentModal';
import UpdateMomentModal from '../../features/Moment/UpdateMomentModal';
import { IMoment, IMoments } from '../../types/types.moment';
import { FilterParamsType, IFilter } from '../../types/types.filter';
import Filter from '../../components/Filter/Filter';
import DetailMomentModal from '../../features/Moment/DetailMomentModal';

const MomentPage: React.FC = () => {
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState(4);
  const [momentId, setMomentId] = useState<string>('');
  const [moments, setMoments] = useState<IMoments | null>(null);
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
    { label: 'Статус', value: 'status' },
    { label: 'Описание', value: 'description' },
    { label: 'Дата создания', value: 'createdAt' },
  ];
  const filterParams: FilterParamsType = 'date';

  const queryClient = useQueryClient();

  const { isLoading, isError, error } = useQuery(
    ['moments', page, rowsPerPage, filter],
    () => MomentService.getAll(page + 1, rowsPerPage, filter),
    {
      onSuccess: (moments) => setMoments(moments),
      keepPreviousData: true,
    },
  );

  const mutationFilter = useMutation(
    (query: IFilter) => MomentService.getAll(page + 1, rowsPerPage, query),
    {
      onSuccess: (filteredMoments) => {
        setMoments(filteredMoments);
      },
    },
  );

  const handleUpdateMoment = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    momentId: string,
  ) => {
    event.stopPropagation();
    setMomentId(momentId);
    handleOpen('update');
  };

  const mutationDelete = useMutation(MomentService.delete, {
    onSuccess: () => {
      queryClient.invalidateQueries('moments');
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

  const handleRowClick = (id: string) => {
    setMomentId(id);
    setOpen((prevState) => ({
      ...prevState,
      detail: true,
    }));
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
          <Filter
            filterParams={filterParams}
            sortParams={sortParams}
            onFilterChange={onFilterChange}
          />
        </Box>
        <CreateMomentModal
          open={open.create}
          close={() => handleClose('create')}
        />
        <UpdateMomentModal
          close={() => handleClose('update')}
          open={open.update}
          momentId={momentId}
        />
        <DetailMomentModal
          momentId={momentId}
          open={open.detail}
          onClose={() => handleClose('detail')}
        />
        <TableContainer>
          <Table style={{ height: '100%', tableLayout: 'fixed' }}>
            <TableHead>
              <TableRow>
                <TableCell>Описание</TableCell>
                <TableCell>Статус</TableCell>
                <TableCell>Координаты</TableCell>
                <TableCell>Дата создания</TableCell>
                <TableCell>Кол-во изображений</TableCell>
                <TableCell>Действия</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {moments &&
                moments.data.map((moment: IMoment) => (
                  <TableRow
                    hover
                    sx={{ cursor: 'pointer' }}
                    onClick={() => handleRowClick(moment.id)}
                    key={moment.id}
                  >
                    <TableCell>{moment.description}</TableCell>
                    <TableCell>
                      {moment.status ? 'Не показывать' : 'Показывать'}
                    </TableCell>
                    <TableCell>
                      {moment.coordinates.latitude.toFixed(5)},{' '}
                      {moment.coordinates.longitude.toFixed(5)}
                    </TableCell>
                    <TableCell>{moment.createdAt}</TableCell>
                    <TableCell>{moment.images.length}</TableCell>
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
                            handleUpdateMoment(event, moment.id)
                          }
                          variant="contained"
                          color="primary"
                          size="small"
                        >
                          Редактировать
                        </Button>
                        <Button
                          onClick={(event) =>
                            handleDeleteRecord(event, moment.id)
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
          totalItems={moments?.totalRecords || 0}
          currentPage={page}
          onPageChange={handlePageChange}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </Paper>
    </Box>
  );
};

export default MomentPage;
