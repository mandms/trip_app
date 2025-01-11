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
import Pagination from '../Pagination';
import TagService from '../../api/api.tag';
import CreateTag from './CreateTag';
import UpdateTag from './UpdateTag';
import { ITag, ITags } from '../../types/types.tag';
import { FilterParamsType, IFilter } from '../../types/types.filter';
import Filter from '../Filter/Filter';

const TagTable: React.FC = () => {
  const [page, setPage] = useState<number>(0);
  const [rowsPerPage, setRowsPerPage] = useState(4);
  const [updateTag, setUpdateTag] = useState<ITag | null>(null);
  const [tags, setTags] = useState<ITags | null>(null);
  const [filter, setFilter] = useState<IFilter>();
  const [open, setOpen] = useState<{
    create: boolean;
    update: boolean;
  }>({
    create: false,
    update: false,
  });

  const sortParams = [
    { label: 'Id', value: 'id' },
    { label: 'Название', value: 'name' },
  ];
  const filterParams: FilterParamsType = 'tag';

  const queryClient = useQueryClient();

  const { data, isLoading, isError, error } = useQuery(
    ['tags', page, rowsPerPage, filter],
    () => TagService.getAll(page + 1, rowsPerPage, filter),
    {
      onSuccess: (tags) => setTags(tags),
      keepPreviousData: true,
    },
  );

  const mutationFilter = useMutation(
    (query: IFilter) => TagService.getAll(page + 1, rowsPerPage, query),
    {
      onSuccess: (filteredTags) => {
        setTags(filteredTags);
      },
    },
  );

  const handleUpdateRoute = (
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    tag: ITag,
  ) => {
    event.stopPropagation();
    setUpdateTag(tag);
    handleOpen('update');
  };

  const mutationDelete = useMutation(TagService.delete, {
    onSuccess: async () => {
      await queryClient.invalidateQueries('tags');
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
          <Filter
            filterParams={filterParams}
            sortParams={sortParams}
            onFilterChange={onFilterChange}
          />
        </Box>
        <CreateTag open={open.create} close={() => handleClose('create')} />
        {updateTag && (
          <UpdateTag
            close={() => handleClose('update')}
            open={open.update}
            updatedTag={updateTag}
          />
        )}
        <TableContainer>
          <Table style={{ height: '100%', tableLayout: 'fixed' }}>
            <TableHead>
              <TableRow>
                <TableCell>Название</TableCell>
                <TableCell>Категория</TableCell>
                <TableCell>Действия</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {data?.data.map((tag: ITag) => (
                <TableRow hover key={tag.id}>
                  <TableCell>{tag.name}</TableCell>
                  <TableCell>{tag.category.name}</TableCell>
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
                        onClick={(event) => handleUpdateRoute(event, tag)}
                        variant="contained"
                        color="primary"
                        size="small"
                      >
                        Редактировать
                      </Button>
                      <Button
                        onClick={(event) => handleDeleteRecord(event, tag.id)}
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
          totalItems={data?.totalRecords || 0}
          currentPage={page}
          onPageChange={handlePageChange}
          rowsPerPage={rowsPerPage}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </Paper>
    </Box>
  );
};

export default TagTable;
